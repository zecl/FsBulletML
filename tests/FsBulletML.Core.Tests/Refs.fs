namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.Processable

/// bulletRef / actionRef / fireRef と、そこへ渡すパラメータ（$1 $2 …）の展開。
/// IntermediateParser がいちばん大きく、参照の解決はそこに居る。
[<TestFixture>]
[<NonParallelizable>]
type Refs() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``bulletRef にパラメータを渡すと、弾の速さと向きに入る``() =
    bml """<action label="top">
  <fire>
    <bulletRef label="b"><param>30</param><param>2</param></bulletRef>
  </fire>
  <wait>2</wait>
  <fire>
    <bulletRef label="b"><param>60</param><param>3</param></bulletRef>
  </fire>
  <wait>30</wait>
</action>

<bullet label="b">
  <direction type="absolute">$1</direction>
  <speed>$2</speed>
</bullet>"""
    |> fun x -> Trace.run x 8 |> Golden.check "bullet-ref-params"

  [<Test>]
  member _.``actionRef のパラメータが、入れ子の中まで届く``() =
    bml """<action label="top">
  <actionRef label="shot"><param>3</param><param>20</param></actionRef>
  <wait>30</wait>
</action>

<action label="shot">
  <repeat><times>$1</times>
    <action>
      <fire>
        <direction type="sequence">$2</direction>
        <speed>2</speed>
        <bullet/>
      </fire>
      <wait>2</wait>
    </action>
  </repeat>
</action>"""
    |> fun x -> Trace.run x 10 |> Golden.check "action-ref-params"

  [<Test>]
  member _.``fireRef のパラメータが、fire の中の向きに入る``() =
    bml """<action label="top">
  <fireRef label="f"><param>45</param></fireRef>
  <wait>2</wait>
  <fireRef label="f"><param>90</param></fireRef>
  <wait>30</wait>
</action>

<fire label="f">
  <direction type="absolute">$1</direction>
  <speed>2</speed>
  <bullet/>
</fire>"""
    |> fun x -> Trace.run x 8 |> Golden.check "fire-ref-params"

  [<Test>]
  member _.``入れ子の repeat が、内側と外側の回数の積になる``() =
    bml """<action label="top">
  <repeat><times>2</times>
    <action>
      <repeat><times>3</times>
        <action>
          <fire>
            <direction type="sequence">15</direction>
            <speed>2</speed>
            <bullet/>
          </fire>
          <wait>1</wait>
        </action>
      </repeat>
      <wait>2</wait>
    </action>
  </repeat>
  <wait>30</wait>
</action>"""
    |> fun x -> Trace.run x 16 |> Golden.check "nested-repeat"

  [<Test>]
  member _.``弾の中の action から、さらに撃つ``() =
    bml """<action label="top">
  <fire>
    <direction type="absolute">0</direction>
    <speed>1</speed>
    <bullet>
      <action>
        <wait>2</wait>
        <fire>
          <direction type="relative">90</direction>
          <speed>2</speed>
          <bullet/>
        </fire>
        <wait>30</wait>
      </action>
    </bullet>
  </fire>
  <wait>60</wait>
</action>"""
    |> fun x -> Trace.run x 8 |> Golden.check "bullet-fires-bullet"

  /// 同じ label が 2 つあるとどちらが走るか。これは「いまはこうなる」の控えで、
  /// 「こうあるべき」ではない。DTD は label の一意性を要求していない。
  ///
  /// tryFindAction / tryFindFire / tryFindBullet は 3 つとも List.tryFind
  /// （3 つとも List.tryFind）なので、最初に見つかったものを返す。
  ///
  /// 凍結した予測: 文書順で先にあるほうが走る。
  /// 後ろが走ったなら、並び順についての読みのほうが外れている。
  ///
  /// samples の 227 本には実例が 0 本。当てる先が無いので回帰の網としては働かない。
  /// リファクタリングで意味が変わっても誰も気づかない側なので、現状の固定として置く。
  [<Test>]
  member _.``同じ label が 2 つあるとき、いまはどちらが走るか``() =
    bml """<action label="top">
  <actionRef label="dup"/>
  <wait>4</wait>
</action>

<action label="dup">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
</action>

<action label="dup">
  <fire><direction type="absolute">90</direction><speed>9</speed><bullet/></fire>
</action>"""
    |> fun x ->
      Trace.run x 6
      + "\n前の dup なら d=0.000 s=1.000、後ろの dup なら d=1.571 s=9.000"
    |> Golden.check "now-duplicate-label-first-wins"

  /// 上は兄弟に 2 つ置いた形。リポジトリに実在するのは入れ子のほう。
  ///
  ///   tests/TestData/xml/bulletRef/elements/success/bulletRef-param-nothing.xml
  ///   tests/TestData/xml/fireRef/elements/success/fireRef-param-nothing.xml
  ///     どちらも <action label="top"> の中に <action label="top">
  ///
  /// この 2 本を使っているのは XmlParse.fs と OtherParse.fs のパース経路だけで、
  /// どちらの `top` が走るかは既存の 346 件が 1 件も見ていない。
  ///
  /// 凍結予測（経路つき）: 外側が勝つ。
  /// `IntermediateParser` の `getAction` が `list@[recBulletml]@getChildren2` と
  /// 自分を子より先に置く行きがけ順なので、平らにした並びで外側が先に来る。
  /// `tryFindAction`（`:737`）はそこへ `List.tryFind` を当てるだけ。
  /// 外側が勝つなら、外側にしかない speed 1 も撃たれる。内側だけなら 9 だけ。
  [<Test>]
  member _.``同じ label が入れ子のとき、いまはどちらが走るか``() =
    bml """<action label="top">
  <actionRef label="dup"/>
  <wait>6</wait>
</action>

<action label="dup">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
  <action label="dup">
    <fire><direction type="absolute">90</direction><speed>9</speed><bullet/></fire>
  </action>
</action>"""
    |> fun x ->
      Trace.run x 6
      + "\n外側が勝つなら s=1.000 と s=9.000 の両方、内側だけなら s=9.000 のみ"
    |> Golden.check "now-duplicate-label-nested"
