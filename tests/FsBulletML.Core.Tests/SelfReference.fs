namespace FsBulletML.Core.Tests

open System.Threading
open NUnit.Framework
open FsBulletML.Processable

/// 輪になった参照。展開を打ち止めているのは IntermediateParser.convertRefBulletmlIn。
/// bullet と action の輪はそこで残し、走らせる側が 1 段ずつ解く。
///
/// withTimeout と 1MB スタックは、打ち止めに漏れがあったときと、
/// 解いた段を積んでしまったときの保険。戻ってこなければ控えに出る。
[<TestFixture>]
[<NonParallelizable>]
type SelfReference() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  /// 別スレッドで走らせて、決めた秒数で戻らなければ「止まらない」とする
  let withTimeout seconds (f: unit -> string) =
    let mutable result = "時間内に戻らなかった"
    let t = Thread((fun () ->
              result <- try f () with e ->
                          let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
                          let i = inner e
                          sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r","").Replace("\n"," "))),
                   1 <<< 20)
    t.IsBackground <- true
    t.Start()
    t.Join(seconds * 1000) |> ignore
    result

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``action が自分を actionRef する``() =
    withTimeout 5 (fun () ->
      Trace.run (bml """<action label="top">
  <actionRef label="top"/>
</action>""") 2)
    |> Golden.check "cycle-action-self"

  /// wait を挟んだ action の自己参照。実物は [ESP_RADE]_..._izuna_fan.xml の fan。
  /// 上の形は fire が無いので、回っていても軌跡に出ない。
  /// こちらは 3 フレームごとに 1 発 出るので、輪が回っていることが読める
  [<Test>]
  member _.``action が wait を挟んで自分を actionRef する``() =
    withTimeout 5 (fun () ->
      Trace.run (bml """<action label="top">
  <actionRef label="fan"/>
</action>
<action label="fan">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
  <wait>2</wait>
  <actionRef label="fan"/>
</action>""") 6)
    |> Golden.check "cycle-action-self-with-wait"

  /// 2 段の輪は解いていない。展開時に落とす。
  ///
  /// 自分自身へ戻る輪は、解いた結果の並びの中に同じ actionRef がそのまま現れるので、
  /// 同じ action の並びを差し替え続けられる。2 段の輪は解いた結果に別の action が
  /// 挟まるので、差し替え先がフレームごとに 1 つ内側へ移り、呼び出しが深くなる。
  /// コーパスに実例は無い（輪 8 個のうち action の輪は fan の自己参照 1 個だけ）
  [<Test>]
  member _.``2 つの action が互いを参照する``() =
    withTimeout 5 (fun () ->
      Trace.run (bml """<action label="top">
  <actionRef label="b"/>
</action>
<action label="b">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
  <wait>1</wait>
  <actionRef label="top"/>
</action>""") 6)
    |> Golden.check "cycle-action-mutual"

  /// 解いた段を積む実装だと、1MB スタックのここで落ちる。
  /// 撃たない形にしてあるのは、測りたいのが段の積み方だけだから
  [<Test>]
  member _.``輪の action を 1000 フレーム回しても落ちない``() =
    withTimeout 30 (fun () ->
      Trace.run (bml """<action label="top">
  <actionRef label="top"/>
</action>""") 1000 |> ignore
      "1000 フレーム走って落ちなかった")
    |> Golden.check "cycle-action-long-run"

  /// 上は最小の輪だけ。実物の fan は fire と、輪でない actionRef を持つ。
  /// 輪でない actionRef は解くと action として並びに入るので、そこも長く回して見る。
  /// 撃った弾は vanish させて、測っているものが段の積み方だけになるようにしてある
  [<Test>]
  member _.``実物の fan と同じ骨格を 1000 フレーム回しても落ちない``() =
    withTimeout 30 (fun () ->
      let trace =
        Trace.run (bml """<action label="top">
  <actionRef label="fan"/>
</action>
<action label="fan">
  <actionRef label="stop"/>
  <fire>
    <direction type="absolute">0</direction><speed>1</speed>
    <bullet><action><vanish/></action></bullet>
  </fire>
  <wait>2</wait>
  <actionRef label="fan"/>
</action>
<action label="stop">
  <changeSpeed><speed>0</speed><term>1</term></changeSpeed>
</action>""") 1000
      let fired = trace.Split('\n') |> Array.filter (fun l -> l.Contains("+b")) |> Array.length
      sprintf "1000 フレーム走って落ちなかった。撃った弾 %d 発" fired)
    |> Golden.check "cycle-action-long-run-fan"


  [<Test>]
  member _.``bullet が自分を bulletRef する``() =
    withTimeout 5 (fun () ->
      Trace.run (bml """<action label="top">
  <fire><bulletRef label="b"/></fire>
  <wait>10</wait>
</action>
<bullet label="b">
  <action><fire><bulletRef label="b"/></fire><wait>1</wait></action>
</bullet>""") 4)
    |> Golden.check "cycle-bullet-self"

  /// 2 段の相互参照。実物は [Original]_water_lv10.xml の longbit と shortbit
  [<Test>]
  member _.``2 つの bullet が互いを参照する（実物にある形）``() =
    withTimeout 5 (fun () ->
      Trace.run (bml """<action label="top">
  <fire><bulletRef label="longbit"/></fire>
  <wait>10</wait>
</action>
<bullet label="longbit">
  <action><wait>2</wait><fire><bulletRef label="shortbit"/></fire></action>
</bullet>
<bullet label="shortbit">
  <action><wait>2</wait><fire><bulletRef label="longbit"/></fire></action>
</bullet>""") 4)
    |> Golden.check "cycle-bullet-mutual"

  /// コーパスに実例が無い形。守りとして置いてある
  [<Test>]
  member _.``fire が自分を fireRef する``() =
    withTimeout 5 (fun () ->
      Trace.run (bml """<action label="top">
  <fireRef label="f"/>
  <wait>10</wait>
</action>
<fire label="f">
  <direction type="absolute">0</direction><speed>1</speed>
  <bullet><action><fireRef label="f"/></action></bullet>
</fire>""") 4)
    |> Golden.check "cycle-fire-self"

  /// 種別をまたいで同じ action へ戻る輪。コーパスに実例 0 本。
  /// 間に bullet が入るので fire が段を切る。解けるかもしれないが測っていない。
  /// いまは展開時に落としている。この控えはその線引きを固定するもの
  [<Test>]
  member _.``action から bullet を経由して同じ action へ戻る``() =
    withTimeout 5 (fun () ->
      Trace.run (bml """<action label="top">
  <actionRef label="a"/>
</action>
<action label="a">
  <fire><bulletRef label="b"/></fire>
  <wait>2</wait>
</action>
<bullet label="b">
  <action><actionRef label="a"/></action>
</bullet>""") 4)
    |> Golden.check "cycle-action-via-bullet"

  /// 打ち止めが輪でないものを巻き込んでいないかを見る。赤くなったら広すぎる
  [<Test>]
  member _.``兄弟に同じ label を 2 回書いても輪ではない``() =
    bml """<action label="top">
  <actionRef label="a"/>
  <actionRef label="a"/>
  <wait>6</wait>
</action>

<action label="a">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
</action>"""
    |> fun x -> Trace.run x 4
    |> fun s -> s + "\n2 発とも撃てていれば、打ち止めは兄弟を巻き込んでいない"
    |> Golden.check "cycle-siblings-not-a-cycle"

  /// 輪でない参照の鎖。深さで打ち止めていたら、ここが先に落ちる
  [<Test>]
  member _.``輪でない参照の鎖は、深くても通る``() =
    bml """<action label="top">
  <actionRef label="a"/>
  <wait>6</wait>
</action>

<action label="a"><actionRef label="b"/></action>
<action label="b"><actionRef label="c"/></action>
<action label="c"><actionRef label="d"/></action>
<action label="d">
  <fire><direction type="absolute">0</direction><speed>2</speed><bullet/></fire>
</action>"""
    |> fun x -> Trace.run x 4
    |> fun s -> s + "\n鎖の深さは 4。深さで打ち止めていたら、ここが先に落ちる"
    |> Golden.check "cycle-deep-chain-ok"
