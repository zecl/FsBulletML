namespace FsBulletML.Core.Tests

open System.Collections.Generic
open NUnit.Framework
open FsBulletML
open FsBulletML.Processable

/// ここまでの控えが 1 度も通っていなかった 2 つ。
///
///   BulletType.Player の分岐（BulletRunner.fs:58 :189 :195 :298）
///     こちらの弾はずっと Enemy だった。Player だと aim の相手が変わる
///
///   label が "top" で始まる action が複数あるとき（BulletRunner.fs:25-36）
///     StartsWith("top") で拾うので top / top1 / top2 が全部 task になる
[<TestFixture>]
[<NonParallelizable>]
type PlayerAndTops() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  let runOr frames xml =
    try Trace.run xml frames
    with e ->
      let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
      let i = inner e
      sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r", "").Replace("\n", " "))

  let firedBullets (trace: string) =
    trace.Split('\n')
    |> Array.filter (fun l -> l.Contains "  +b")
    |> Array.map (fun l -> l.Trim())
    |> String.concat "\n"

  /// 根の弾を Player にして回す。Trace.run は Enemy 固定なのでここだけ自前で組む。
  let runAsPlayer (xml: string) (frames: int) =
    let born = List<FakeBullet>()
    let root = FakeBullet(0, born)
    let o = root :> IBulletmlObject
    o.Init()
    o.BulletType <- BulletType.Player
    o.Task <- BulletRunner.convertBulletmlTaskOption (readXmlString xml)
    let acc = System.Text.StringBuilder()
    let mutable seen = 0
    for _ in 1 .. frames do
      let live = Array.append [| root |] (born.ToArray())
      for b in live do
        let bo = b :> IBulletmlObject
        if bo.Used then
          match bo.Task with
          | None -> ()
          | Some task ->
            let r = BulletRunner.run bo
            bo.X <- bo.X + r.X
            bo.Y <- bo.Y + r.Y
            if r.Processed then task.Init()
      while seen < born.Count do
        let b = born.[seen]
        let bo = b :> IBulletmlObject
        acc.AppendLine(sprintf "+b%d d=%.3f s=%.3f type=%A" b.Id bo.Dir bo.Speed bo.BulletType) |> ignore
        seen <- seen + 1
    acc.ToString().Replace("\r\n", "\n")

  [<SetUp>]
  member _.SetUp() =
    // 自機 (30,100)。敵は FakeEnemy が (-40,-60) に置いている
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  /// direction を省くと aim になる。Player なら「敵を狙う」、Enemy なら「自機を狙う」。
  /// BulletRunner.fs:189-198 の分岐がここで初めて通る。
  [<Test>]
  member _.``Player の弾は aim の相手が変わる``() =
    let x = bml """<action label="top">
  <fire><bullet/></fire>
  <wait>10</wait>
</action>"""
    let asEnemy = Trace.run x 3 |> firedBullets
    let asPlayer = runAsPlayer x 3
    sprintf "Enemy として撃つ:\n%s\n\nPlayer として撃つ:\n%s\n\n\
             自機 (30,100) を狙う aim = 2.850 / 敵 (-40,-60) を狙う aim = atan2(-40,60) = -0.588 -> calcDir が 2pi 足して 5.695"
      asEnemy (asPlayer.TrimEnd('\n'))
    |> Golden.check "player-aim"

  /// 撃った弾に BulletType が伝わるか。GetNewBullet が親の型を写している。
  [<Test>]
  member _.``撃った弾は親の BulletType を継ぐ``() =
    bml """<action label="top">
  <fire><direction type="absolute">0</direction><speed>2</speed><bullet/></fire>
  <wait>10</wait>
</action>"""
    |> fun x -> runAsPlayer x 3
    |> Golden.check "player-bullettype-inherit"

  /// label が top で始まる action が複数あるとき。
  /// StartsWith("top") なので top / top1 / top2 が全部 task になるはず。
  [<Test>]
  member _.``top で始まる label が 3 つあると、全部が走る``() =
    bml """<action label="top">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
  <wait>10</wait>
</action>
<action label="top1">
  <fire><direction type="absolute">90</direction><speed>2</speed><bullet/></fire>
  <wait>10</wait>
</action>
<action label="top2">
  <fire><direction type="absolute">180</direction><speed>3</speed><bullet/></fire>
  <wait>10</wait>
</action>"""
    |> runOr 3 |> firedBullets |> Golden.check "multiple-top"

  /// 上で top / top1 / top2 のうち top しか撃たなかった。
  /// run は tasks を順に回すが、wait が Stop を返した時点で残りを見ない
  /// （BulletRunner.fs:355-372 の while が `not stop` で抜ける）ので、
  /// 先頭の wait が後ろの top を塞いでいる、というのが読み。確かめる。
  [<Test>]
  member _.``先頭の top の wait が、後ろの top を塞いでいるか``() =
    let withWait = bml """<action label="top">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
  <wait>10</wait>
</action>
<action label="top1">
  <fire><direction type="absolute">90</direction><speed>2</speed><bullet/></fire>
  <wait>10</wait>
</action>"""
    // 先頭に wait を置かない形。Stop を返さなければ後ろも回るはず
    let noWait = bml """<action label="top">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
</action>
<action label="top1">
  <fire><direction type="absolute">90</direction><speed>2</speed><bullet/></fire>
</action>"""
    sprintf "先頭に wait あり:\n%s\n\n先頭に wait なし:\n%s\n\n0 度 speed1 = top / 90 度(1.571) speed2 = top1"
      (runOr 3 withWait |> firedBullets) (runOr 3 noWait |> firedBullets)
    |> Golden.check "multiple-top-blocked"

  /// top で始まらない label は拾われないはず。topmost のような紛らわしい名前も見る。
  [<Test>]
  member _.``top で始まる名前とそうでない名前``() =
    bml """<action label="topmost">
  <fire><direction type="absolute">0</direction><speed>1</speed><bullet/></fire>
  <wait>10</wait>
</action>
<action label="nottop">
  <fire><direction type="absolute">90</direction><speed>9</speed><bullet/></fire>
  <wait>10</wait>
</action>"""
    |> runOr 3 |> firedBullets
    |> fun s -> sprintf "topmost は拾われるか / nottop は拾われないか\n%s\n（0 度 speed1 だけなら topmost のみ）" s
    |> Golden.check "top-prefix-match"
