namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.Processable

/// 骨が通るかを見る 1 本。
/// BulletMLManager が static mutable なので、この fixture は並列にしない。
[<TestFixture>]
[<NonParallelizable>]
type Smoke() =

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 0.0f, 100.0f))

  [<Test>]
  member _.``fire と wait だけの top action が、控えどおりの軌跡になる``() =
    let xml = """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
<action label="top">
  <fire>
    <direction type="absolute">0</direction>
    <speed>2</speed>
    <bullet/>
  </fire>
  <wait>3</wait>
</action>
</bulletml>"""
    Trace.run xml 6 |> Golden.check "smoke-fire-wait"
