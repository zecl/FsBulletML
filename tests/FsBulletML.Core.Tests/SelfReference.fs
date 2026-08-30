namespace FsBulletML.Core.Tests

open System.Threading
open NUnit.Framework
open FsBulletML.Processable

/// 自分を参照する action / bullet / fire。
///
/// **この fixture は Explicit にしてある。** 無限再帰なら StackOverflowException で、
/// .NET ではこれを捕まえられずプロセスごと落ちる。通常の走行に混ぜると
/// 他の 48 件を巻き添えにするので、当てるときだけ --filter で名指しすること。
///
///   dotnet test tests/FsBulletML.Core.Tests --filter "FullyQualifiedName~SelfReference"
///
/// 測った結果は下のコメントに残す。控えは取らない（落ちると控えが書けないため）。
///
/// 2026-08-31 に dba9d27 で測った結果はここに追記すること。
[<TestFixture>]
[<NonParallelizable>]
[<Explicit "無限再帰ならプロセスごと落ちるので、当てるときだけ名指しで回す">]
type SelfReference() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  /// 別スレッドで走らせて、決めた秒数で戻らなければ「止まらない」とする。
  /// StackOverflow はこれでも防げないが、単なる無限ループなら拾える。
  let withTimeout seconds (f: unit -> string) =
    let mutable result = "時間内に戻らなかった"
    let t = Thread((fun () ->
              result <- try f () with e ->
                          let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
                          let i = inner e
                          sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r","").Replace("\n"," "))),
                   1 <<< 20)   // 1MB。深い再帰なら早めに落ちる
    t.IsBackground <- true
    t.Start()
    t.Join(seconds * 1000) |> ignore
    result

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``action が自分を actionRef する``() =
    let r =
      withTimeout 5 (fun () ->
        Trace.run (bml """<action label="top">
  <actionRef label="top"/>
</action>""") 2)
    TestContext.WriteLine("action 自己参照: " + r)
    Assert.Pass(r)

  [<Test>]
  member _.``2 つの action が互いを参照する``() =
    let r =
      withTimeout 5 (fun () ->
        Trace.run (bml """<action label="top">
  <actionRef label="b"/>
</action>
<action label="b">
  <actionRef label="top"/>
</action>""") 2)
    TestContext.WriteLine("相互参照: " + r)
    Assert.Pass(r)

  [<Test>]
  member _.``bullet が自分を bulletRef する``() =
    let r =
      withTimeout 5 (fun () ->
        Trace.run (bml """<action label="top">
  <fire><bulletRef label="b"/></fire>
  <wait>10</wait>
</action>
<bullet label="b">
  <action><fire><bulletRef label="b"/></fire><wait>1</wait></action>
</bullet>""") 4)
    TestContext.WriteLine("bullet 自己参照: " + r)
    Assert.Pass(r)
