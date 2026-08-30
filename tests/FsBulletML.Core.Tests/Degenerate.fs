namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML
open FsBulletML.Processable

/// 中身の無い BulletML を食わせたときにどうなるか。
/// run には X / Y を「差分ではなく絶対値」で返す枝が 2 つあり、
/// 呼ぶ側はどちらの枝でも足すので、通ると座標が 2 倍になる。
/// **その枝に本当に入れるのか**をここで測る。
[<TestFixture>]
[<NonParallelizable>]
type Degenerate() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``top ラベルの action が無い BulletML``() =
    bml """<action label="notTop">
  <fire><direction type="absolute">0</direction><speed>2</speed><bullet/></fire>
  <wait>5</wait>
</action>"""
    |> fun x -> Trace.run x 4 |> Golden.check "no-top-action"

  [<Test>]
  member _.``action が 1 つも無い BulletML``() =
    bml """<bullet label="b"><speed>2</speed></bullet>"""
    |> fun x -> Trace.run x 4 |> Golden.check "no-action-at-all"

  /// run が絶対値を返す枝に入れるかを直接見る。
  /// 入れるなら「そのフレームで座標が 2 倍になる」ので、呼ぶ側の足し算と噛み合わない。
  [<Test>]
  member _.``Tasks が空のとき、run は差分を返すか絶対値を返すか``() =
    let born = System.Collections.Generic.List<FakeBullet>()
    let b = FakeBullet(0, born)
    let o = b :> IBulletmlObject
    o.Init()
    o.Task <- BulletRunner.convertBulletmlTaskOption (readXmlString (bml """<action label="notTop"><wait>1</wait></action>"""))
    // 座標を 0 でない値にしておく。絶対値で返す枝に入ればそれがそのまま返る
    o.X <- 7.0f
    o.Y <- 11.0f
    o.Speed <- 0.0f
    let r = BulletRunner.run o
    let verdict =
      if r.X = 7.0f && r.Y = 11.0f then "絶対値を返した（呼ぶ側が足すと 2 倍になる枝）"
      elif r.X = 0.0f && r.Y = 0.0f then "差分を返した（speed 0 なので 0）"
      else sprintf "どちらでもない X=%f Y=%f" r.X r.Y
    sprintf "Processed=%b X=%f Y=%f\n%s" r.Processed r.X r.Y verdict
    |> Golden.check "run-returns-delta-or-absolute"
