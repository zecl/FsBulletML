namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.Processable

/// 同じ定義から作られた弾どうしが、可変状態を共有していないか。
///
/// Processable.fs には cloneProcessable が在って、コメントにこうある。
///
///   Deep-copy a processable tree so child bullets get independent mutable state
///   (finish/term/first/...). Replaces BinaryFormatter deepCopyClone.
///
/// **共有していると、片方の弾が進めた term や finish がもう片方に効く。**
/// リファクタリングで clone を外したり浅くしたりすると静かに壊れる場所なので、
/// 振る舞いの側から固めておく。
[<TestFixture>]
[<NonParallelizable>]
type StateIsolation() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  /// 同じ bulletRef から 3 発を別々のフレームで撃つ。
  /// 弾の中の action は changeSpeed（term 3）を持つので、term が共有されていれば
  /// 2 発目以降は「もう終わっている」扱いになって加速しないはず。
  [<Test>]
  member _.``同じ定義から撃った弾が、それぞれ自分の term を持つ``() =
    bml """<action label="top">
  <fire><bulletRef label="b"/></fire>
  <wait>2</wait>
  <fire><bulletRef label="b"/></fire>
  <wait>2</wait>
  <fire><bulletRef label="b"/></fire>
  <wait>30</wait>
</action>

<bullet label="b">
  <speed>1</speed>
  <action>
    <changeSpeed><speed>4</speed><term>3</term></changeSpeed>
    <wait>30</wait>
  </action>
</bullet>"""
    |> fun x -> Trace.run x 10 |> Golden.check "isolation-term"

  /// 同じ action を repeat で何度も通す。repeat の中の changeDirection が
  /// 毎回 最初から始まるか、前の回の続きになるかを見る。
  [<Test>]
  member _.``repeat で同じ action を通すと、中の term はどうなるか``() =
    bml """<action label="top">
  <fire>
    <direction type="absolute">0</direction><speed>1</speed>
    <bullet>
      <action>
        <repeat><times>3</times>
          <action>
            <changeDirection><direction type="relative">30</direction><term>2</term></changeDirection>
            <wait>3</wait>
          </action>
        </repeat>
        <wait>20</wait>
      </action>
    </bullet>
  </fire>
  <wait>30</wait>
</action>"""
    |> fun x -> Trace.run x 14 |> Golden.check "isolation-repeat-term"

  /// 弾が 2 発同時に飛んでいて、片方だけ vanish する。
  /// finish が共有されていれば、もう片方も消えるはず。
  [<Test>]
  member _.``片方だけ消しても、もう片方は飛び続ける``() =
    bml """<action label="top">
  <fire>
    <direction type="absolute">0</direction><speed>2</speed>
    <bullet>
      <action><wait>2</wait><vanish/></action>
    </bullet>
  </fire>
  <fire>
    <direction type="absolute">90</direction><speed>2</speed>
    <bullet>
      <action><wait>30</wait></action>
    </bullet>
  </fire>
  <wait>30</wait>
</action>"""
    |> fun x -> Trace.run x 8 |> Golden.check "isolation-vanish"

  /// top の action がひと回りして task.Init() で作り直されるとき、
  /// sequence の累積が持ち越されるか、最初に戻るか。
  [<Test>]
  member _.``ひと回りしたあと、sequence の累積は戻るか``() =
    bml """<action label="top">
  <repeat><times>3</times>
    <action>
      <fire><direction type="sequence">30</direction><speed>2</speed><bullet/></fire>
      <wait>1</wait>
    </action>
  </repeat>
  <wait>2</wait>
</action>"""
    |> fun x ->
      let t = Trace.run x 20
      t.Split('\n')
      |> Array.filter (fun l -> l.Contains "  +b")
      |> Array.map (fun l -> l.Trim())
      |> String.concat "\n"
      |> fun s -> sprintf "%s\n\n30 度 = 0.524。3 発ごとにひと回りする。\n累積が戻るなら 0.524/1.047/1.571 の繰り返し、続くなら増え続ける" s
    |> Golden.check "isolation-sequence-across-loop"
