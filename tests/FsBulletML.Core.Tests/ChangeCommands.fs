namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.Processable

/// changeDirection / changeSpeed / accel の type ごとの効き方と、vanish。
/// absolute は Behavior.fs で見ているので、ここは残りを埋める。
[<TestFixture>]
[<NonParallelizable>]
type ChangeCommands() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  /// 弾 1 発を撃って、その弾に body の action を持たせる形。
  /// 撃つ側の都合を軌跡から締め出して、弾側の振る舞いだけを見る。
  let oneBullet dir speed body =
    bml (sprintf """<action label="top">
  <fire>
    <direction type="absolute">%s</direction>
    <speed>%s</speed>
    <bullet>
      <action>
%s
        <wait>30</wait>
      </action>
    </bullet>
  </fire>
  <wait>60</wait>
</action>""" dir speed body)

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``changeDirection relative は、いまの向きからの差として効く``() =
    oneBullet "45" "1" """        <changeDirection>
          <direction type="relative">90</direction>
          <term>3</term>
        </changeDirection>"""
    |> fun x -> Trace.run x 8 |> Golden.check "cd-relative"

  [<Test>]
  member _.``changeDirection aim は、自機の向きを狙う``() =
    oneBullet "0" "1" """        <changeDirection>
          <direction type="aim">0</direction>
          <term>3</term>
        </changeDirection>"""
    |> fun x -> Trace.run x 8 |> Golden.check "cd-aim"

  [<Test>]
  member _.``changeDirection sequence は、毎フレーム足し込む``() =
    oneBullet "0" "1" """        <changeDirection>
          <direction type="sequence">10</direction>
          <term>4</term>
        </changeDirection>"""
    |> fun x -> Trace.run x 8 |> Golden.check "cd-sequence"

  [<Test>]
  member _.``changeSpeed relative は、いまの速さからの差として効く``() =
    oneBullet "0" "2" """        <changeSpeed>
          <speed type="relative">3</speed>
          <term>3</term>
        </changeSpeed>"""
    |> fun x -> Trace.run x 8 |> Golden.check "cs-relative"

  [<Test>]
  member _.``changeSpeed sequence は、毎フレーム足し込む``() =
    oneBullet "0" "1" """        <changeSpeed>
          <speed type="sequence">0.5</speed>
          <term>4</term>
        </changeSpeed>"""
    |> fun x -> Trace.run x 8 |> Golden.check "cs-sequence"

  [<Test>]
  member _.``accel relative と sequence``() =
    oneBullet "0" "1" """        <accel>
          <horizontal type="relative">1</horizontal>
          <vertical type="sequence">0.5</vertical>
          <term>3</term>
        </accel>"""
    |> fun x -> Trace.run x 8 |> Golden.check "accel-relative-sequence"

  [<Test>]
  member _.``vanish で弾が消え、以降そのフレームに出てこない``() =
    oneBullet "0" "2" """        <wait>2</wait>
        <vanish/>"""
    |> fun x -> Trace.run x 8 |> Golden.check "vanish"
