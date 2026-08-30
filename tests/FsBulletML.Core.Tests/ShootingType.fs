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

  /// DTD は type を省略可（既定 "none"）と定めている。実装がそれに従うかを見る。
  ///
  /// 測った結果は「落ちない」。IntermediateParser.fs:200-204 が
  /// 属性が無ければ bulletmlType = None を返すので、
  /// BulletRunner.fs:404 の `None -> BulletVertical` に**届く**。
  ///
  /// つまり同じ問いに 3 つ別の答えがある。
  ///   DTD       none          （DTD.fs:141 のコメント）
  ///   Core      vertical      （BulletRunner.fs:404。省略時に届く）
  ///   フロント   horizontal    （BaseBullet.fs:28 / DefaultBullet.fs:30）
  ///
  /// いまは type 自体が挙動に効かないので見えない。効くようにした瞬間に効いてくる。
  ///
  /// なお IntermediateParser.fs:221 の例外は
  /// 「this element should have ShootingDirection attribute.」と言うが、
  /// 条件は type 属性ではなく attrs レコードが None のとき。**メッセージが実際の条件と違う。**
  [<Test>]
  member _.``type を省くとどうなるか``() =
    let noType = """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
<action label="top">
  <fire><direction type="absolute">30</direction><speed>2</speed><bullet/></fire>
  <wait>3</wait>
</action>
</bulletml>"""
    let result =
      try
        let t = Trace.run noType 4
        sprintf "落ちない。軌跡が出た（先頭 2 行）\n%s" (t.Split('\n') |> Array.truncate 2 |> String.concat "\n")
      with e ->
        let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
        let i = inner e
        sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r", "").Replace("\n", " "))
    sprintf "DTD の定め: <!ATTLIST bulletml type (none|vertical|horizontal) \"none\"> （省略可・既定 none）\n実装: %s" result
    |> Golden.check "shooting-type-omitted"

  [<Test>]
  member _.``type に知らない値を書くと DTD 違反で落ちる``() =
    let ex =
      Assert.Throws<FsBulletML.Exception.BulletmlDTDViolationException>(fun () ->
        Trace.run (bmlOfType "diagonal") 2 |> ignore)
    sprintf "例外: %s\nメッセージ: %s" (ex.GetType().Name) ex.Message
    |> Golden.check "shooting-type-unknown"
