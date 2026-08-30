namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.Processable

/// `<bulletml type="none|vertical|horizontal">` が走らせる側に効くか。
///
/// **効かない、を固めるテスト。** ふつうに軌跡の控えを 1 本取っても、
/// 読む側が居ないので「どの type でも緑」になり何も担保しない。
/// なので 3 つの type で**同じ軌跡になること**のほうを見る。
/// リファクタリングで効くようになったら、ここが赤くなる。
///
/// 2026-08-31 に dba9d27 で数えた結果、`ShootingDirection` を右辺で読んで
/// 分岐している箇所は Core にもフロントにも無い。運ばれる経路はこう。
///
///   XML の type="vertical"
///     -> IntermediateParser.fs:191-196  文字列から DU へ
///     -> :201-202                       BulletmlAttrs.bulletmlType へ
///     -> BulletRunner.fs:399-413        取り出して Task に set
///     -> ここで終わり
///
/// 1 か所だけ読む所がある。DTD.fs:204-206 が XML へ書き戻すときに使う。
/// つまり**挙動には効かないが、XML の往復には効く**。往復のほうは
/// Parser.Tests の領分なので、ここでは触っていない。
///
/// フロント側は別物が同じ名前で置かれている。BaseBullet.fs:28 と
/// DefaultBullet.fs:30 が `member val ... = BulletHorizontal` の独立プロパティで、
/// Core が set した値を受け取る経路が無い。Core 側の既定は BulletVertical
/// （BulletRunner.fs:404）なので、既定値も食い違っている。
[<TestFixture>]
[<NonParallelizable>]
type ShootingType() =

  let bmlOfType t =
    sprintf """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="%s" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
<action label="top">
  <fire>
    <direction type="absolute">30</direction>
    <speed>2</speed>
    <bullet/>
  </fire>
  <wait>3</wait>
</action>
</bulletml>""" t

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``type を none vertical horizontal に振っても軌跡が変わらない``() =
    let none = Trace.run (bmlOfType "none") 6
    let vert = Trace.run (bmlOfType "vertical") 6
    let horz = Trace.run (bmlOfType "horizontal") 6
    Assert.Multiple(fun () ->
      Assert.That(vert, Is.EqualTo none, "none と vertical で軌跡が違う")
      Assert.That(horz, Is.EqualTo none, "none と horizontal で軌跡が違う"))
    // 3 つとも同じなので、代表 1 本だけ控えに残す
    none |> Golden.check "shooting-type-no-effect"

  [<Test>]
  member _.``type に知らない値を書くと DTD 違反で落ちる``() =
    let ex =
      Assert.Throws<FsBulletML.Exception.BulletmlDTDViolationException>(fun () ->
        Trace.run (bmlOfType "diagonal") 2 |> ignore)
    sprintf "例外: %s\nメッセージ: %s" (ex.GetType().Name) ex.Message
    |> Golden.check "shooting-type-unknown"
