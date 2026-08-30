namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.Processable

/// 走らせる側の振る舞いを軌跡で固める。
/// BulletMLManager が static mutable なので並列にしない。
[<TestFixture>]
[<NonParallelizable>]
type Behavior() =

  let head = """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
"""

  let bml body = head + body + "\n</bulletml>"

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 0.0f, 100.0f))

  /// changeSpeed / changeDirection は term フレームかけて補間する。
  /// 「何フレーム目にどこまで来ているか」が全部で、境界を 1 つずらすと静かに壊れる。
  [<Test>]
  member _.``changeDirection と changeSpeed が term をかけて補間する``() =
    bml """<action label="top">
  <fire>
    <direction type="absolute">0</direction>
    <speed>1</speed>
    <bullet>
      <action>
        <changeDirection>
          <direction type="absolute">90</direction>
          <term>4</term>
        </changeDirection>
        <changeSpeed>
          <speed>3</speed>
          <term>4</term>
        </changeSpeed>
        <wait>10</wait>
      </action>
    </bullet>
  </fire>
  <wait>20</wait>
</action>"""
    |> fun x -> Trace.run x 10 |> Golden.check "change-dir-speed-term"

  /// repeat の中で direction type="sequence" を使うと、撃つたびに前の向きへ足し込む。
  /// 「何発めがどの向きか」は累積なので、1 発ぶんずれると全部ずれる。
  [<Test>]
  member _.``repeat の中の sequence が、撃つたびに向きを足し込む``() =
    bml """<action label="top">
  <repeat><times>5</times>
    <action>
      <fire>
        <direction type="sequence">30</direction>
        <speed>2</speed>
        <bullet/>
      </fire>
      <wait>1</wait>
    </action>
  </repeat>
  <wait>10</wait>
</action>"""
    |> fun x -> Trace.run x 10 |> Golden.check "repeat-sequence-fire"

  /// accel は horizontal / vertical を term フレームかけて積む。
  /// 速さや向きと違って弾の加速度に入るので、位置の出方が別経路になる。
  [<Test>]
  member _.``accel が term をかけて加速度を積む``() =
    bml """<action label="top">
  <fire>
    <direction type="absolute">0</direction>
    <speed>1</speed>
    <bullet>
      <action>
        <accel>
          <horizontal type="absolute">2</horizontal>
          <vertical type="absolute">1</vertical>
          <term>3</term>
        </accel>
        <wait>10</wait>
      </action>
    </bullet>
  </fire>
  <wait>20</wait>
</action>"""
    |> fun x -> Trace.run x 9 |> Golden.check "accel-term"
