namespace FsBulletML.Core.Tests

open System.Text.RegularExpressions
open NUnit.Framework
open FsBulletML.Processable

/// bullet 要素の中に書いた direction が、弾の向きに入るか。
///
/// **ここは「正しい姿」ではなく「いまの姿」を記録している。**
/// 測った結果はこう。
///
///   fire 側に direction を書く   効く
///   bullet の中に direction      効かない（aim に落ちる）。参照でもリテラルでも同じ
///   bullet の中の speed          効く
///
/// 機構も見えている。BulletRunner.fs の fireCommand が
///
///   | ProcessableBulletml.Bullet(attr,_,speed,_) -> ...
///
/// と分解していて、2 番めの `Direction option` を捨てている。向きは fire 側の
/// SrcDir からしか入らない。同じ位置の speed は読んでいるので非対称。
/// ProcessableBulletml.Bullet の型は
///
///   Bullet of BulletAttrs * Direction option * Speed option * ProcessableBulletml list
///
/// で、その 3 行上の DTD には `<!ELEMENT bullet (direction?, speed?, ...)>` とある。
/// 仕様は許していて、パーサも AST に入れていて、走らせる側だけが捨てている。
///
/// **不具合の可能性が高いが、ここでは直していない。** リファクタリングでこの控えが
/// 動いたら、直したのかどうかを人が決めること。
[<TestFixture>]
[<NonParallelizable>]
type BulletDirection() =

  let bml body =
    """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
""" + body + "\n</bulletml>"

  let firstBullet (trace: string) =
    let m = Regex.Match(trace, @"\+b1 d=([-\d.]+) s=([-\d.]+)")
    if m.Success then sprintf "d=%s s=%s" m.Groups.[1].Value m.Groups.[2].Value else "撃っていない"

  let inlineLiteral = bml """<action label="top">
  <fire><bullet><direction type="absolute">30</direction><speed>2</speed></bullet></fire>
  <wait>10</wait>
</action>"""

  let refLiteral = bml """<action label="top">
  <fire><bulletRef label="b"/></fire>
  <wait>10</wait>
</action>
<bullet label="b"><direction type="absolute">30</direction><speed>2</speed></bullet>"""

  let refParam = bml """<action label="top">
  <fire><bulletRef label="b"><param>30</param><param>2</param></bulletRef></fire>
  <wait>10</wait>
</action>
<bullet label="b"><direction type="absolute">$1</direction><speed>$2</speed></bullet>"""

  let fireDir = bml """<action label="top">
  <fire><direction type="absolute">30</direction><bullet><speed>2</speed></bullet></fire>
  <wait>10</wait>
</action>"""

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``direction をどこに書いたかで、弾の向きがどうなるか``() =
    // 30 度 = 0.524。自機 (30,100) を狙う aim = 2.850
    [ sprintf "inline literal (bullet 内)  %s" (Trace.run inlineLiteral 4 |> firstBullet)
      sprintf "ref literal    (bullet 内)  %s" (Trace.run refLiteral 4 |> firstBullet)
      sprintf "ref param      (bullet 内)  %s" (Trace.run refParam 4 |> firstBullet)
      sprintf "fire 側        (fire 内)    %s" (Trace.run fireDir 4 |> firstBullet)
      "30 度 = 0.524 / aim = 2.850" ]
    |> String.concat "\n"
    |> Golden.check "bullet-direction-where"
