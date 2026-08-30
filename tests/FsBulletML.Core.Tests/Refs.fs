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
