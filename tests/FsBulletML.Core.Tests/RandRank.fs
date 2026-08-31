namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.Processable

/// $rand と $rank。値そのものは IBulletMLManager から来るので、
/// ここで見るのは「その値が式のどこに、どう入るか」。
///
/// $rand を含む BulletML は convertBulletmlTask で Original を持たされ、
/// task.Init() のたびに作り直される（IntermediateParser.existRandomParam）。
/// **毎周ちがう値になる経路がここにある**ので、固定値でも通り道は測れる。
[<TestFixture>]
[<NonParallelizable>]
type RandRank() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  let randXml = """<action label="top">
  <fire>
    <direction type="absolute">$rand*90</direction>
    <speed>1+$rand*2</speed>
    <bullet/>
  </fire>
  <wait>2</wait>
</action>"""

  [<Test>]
  member _.``rand が 0 のとき``() =
    BulletMLManager.Init(FixedManager(0.0f, 0.5f, 30.0f, 100.0f))
    Trace.run (bml randXml) 6 |> Golden.check "rand-0"

  [<Test>]
  member _.``rand が 1 のとき``() =
    BulletMLManager.Init(FixedManager(1.0f, 0.5f, 30.0f, 100.0f))
    Trace.run (bml randXml) 6 |> Golden.check "rand-1"

  [<Test>]
  member _.``rank が式に入る``() =
    BulletMLManager.Init(FixedManager(0.5f, 0.8f, 30.0f, 100.0f))
    bml """<action label="top">
  <fire>
    <direction type="absolute">0</direction>
    <speed>1+$rank*4</speed>
    <bullet/>
  </fire>
  <wait>2</wait>
</action>"""
    |> fun x -> Trace.run x 6 |> Golden.check "rank-in-expr"
