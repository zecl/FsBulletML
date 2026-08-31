namespace FsBulletML.Core.Tests

open NUnit.Framework
open FsBulletML.DTD
open FsBulletML.Processable

/// `<bulletml type="none|vertical|horizontal">` が走らせる側に効くか。
///
/// **効かない、を固めるテスト。** ふつうに軌跡の控えを 1 本取っても、
/// 読む側が居ないので「どの type でも緑」になり何も担保しない。
/// なので 3 つの type で**同じ軌跡になること**のほうを見る。
/// リファクタリングで効くようになったら、ここが赤くなる。
///
/// 数えた結果、`ShootingDirection` を右辺で読んで
/// 分岐している箇所は Core にもフロントにも無い。運ばれる経路はこう。
///
///   XML の type="vertical"
///     -> createBulletml の toShootingDirection    文字列から DU へ
///     -> BulletmlAttrs.bulletmlType へ
///     -> convertBulletmlTask が Task に set
///     -> ここで終わり
///
/// 1 か所だけ読む所がある。DTD の WriteContentTo が XML へ書き戻すときに使う。
/// つまり**挙動には効かないが、XML の往復には効く**。往復のほうは
/// Parser.Tests の領分なので、ここでは触っていない。
///
/// フロント側は別物が同じ名前で置かれていた。BaseBullet.fs と DefaultBullet.fs が
/// `member val ... = BulletHorizontal` の独立プロパティで、Core が set した値を
/// 受け取る経路が無く、既定値も Core（BulletVertical）と食い違っていた。
///
/// いまは**値を届けるところまで**を繋いである。
///
///   run           bulletmlTask.ShootingDirection -> bullet.ShootingDirection
///   createTask    親の task -> 撃たれた弾の task（ResolveBulletRef と同じ理由）
///   フロント 2 本  既定値を BulletVertical に揃えた
///
/// **上の「軌跡が変わらない」テストは、繋いだあとも緑のままが正しい。**
/// 読んで分岐する所は 1 つも無いので、値が届いても軌跡は動かない。
///
/// TODO: 縦横で何を変えるかは未決定。決めて分岐を足すと、上のテストが赤くなる。
///       そのときは「変わらない」を固めている控え（shooting-type-no-effect）を
///       捨てて、type ごとに違う軌跡を固める控えに置き換えること。
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

  /// type だけ省いたもの。上の bmlOfType と中身を揃えてある（type の有無だけが違う）
  let noTypeBml = """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
<action label="top">
  <fire>
    <direction type="absolute">30</direction>
    <speed>2</speed>
    <bullet/>
  </fire>
  <wait>3</wait>
</action>
</bulletml>"""

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
  /// 測った結果は「落ちない」。createBulletml が
  /// 属性が無ければ bulletmlType = None を返すので、
  /// convertBulletmlTask の `None -> BulletVertical` に**届く**。
  ///
  /// 直す前は、同じ問いに 3 つ別の答えがあった。
  ///   DTD       none          （`DTD.fs` の ATTLIST のコメント）
  ///   Core      vertical      （`convertBulletmlTask` の `None ->`。省略時に届く）
  ///   フロント   horizontal    （`BaseBullet` / `DefaultBullet` の member val）
  ///
  /// そのあと 5 でフロントを Core に揃えたので、いまは vertical で 2 つ。
  /// DTD の none だけが残るが、「縦でも横でもない」は受け取る側が書けることが
  /// 無いので採っていない（上のヘッダに書いた線引き）。
  ///
  /// type は弾まで届くようになったが、読んで分岐する所はまだ 1 つも無いので、
  /// 軌跡は動かない。
  ///
  /// なお `IntermediateParser` の `| None ->` の例外は
  /// 「this element should have ShootingDirection attribute.」と言っていたが、
  /// 条件は type 属性ではなく attrs レコードが None のとき。
  /// メッセージが実際の条件と違ったので、条件のほうに合わせた。
  /// その枝に届く入力は見つかっていない。下の `bulletml-no-attrs` が確かめている。
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

  /// IntermediateParser.fs の `| None ->` に届く入力があるかを探す。
  ///
  /// 条件は `tryFindBulletmlAttrs` が None のとき。中身は `maybe { ... }` だが
  /// `let!` が 1 つも無いので Bind を通らず、必ず `return` に着く。
  /// 属性をぜんぶ省いた `<bulletml>` が、いちばん届きそうな入力にあたる。
  [<Test>]
  member _.``bulletml の属性をぜんぶ省くとどうなるか``() =
    let noAttrs = """<?xml version="1.0" ?>
<bulletml>
<action label="top">
  <fire><direction type="absolute">30</direction><speed>2</speed><bullet/></fire>
  <wait>3</wait>
</action>
</bulletml>"""
    let result =
      try
        let t = Trace.run noAttrs 4
        // 「落ちない」だけだと、黙って 1 つも走らなかった場合と見分けがつかない
        let fired = t.Split('\n') |> Array.filter (fun l -> l.Contains "  +b") |> Array.length
        sprintf "落ちない。撃った弾 %d 発" fired
      with e ->
        let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
        let i = inner e
        sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r", "").Replace("\n", " "))
    sprintf "xmlns も type も name も無い <bulletml>\n実装: %s\n\n落ちないなら、attrs が None になる入力は見つかっていない"
      result
    |> Golden.check "bulletml-no-attrs"

  [<Test>]
  member _.``type に知らない値を書くと DTD 違反で落ちる``() =
    let ex =
      Assert.Throws<FsBulletML.Exception.BulletmlDTDViolationException>(fun () ->
        Trace.run (bmlOfType "diagonal") 2 |> ignore)
    sprintf "例外: %s\nメッセージ: %s" (ex.GetType().Name) ex.Message
    |> Golden.check "shooting-type-unknown"

  /// `<bulletml type>` が弾まで届くか。**根の弾と、撃たれた子の弾の両方**を見る。
  ///
  /// 子まで見ないと片手落ちになる。届けるのは 2 ホップあって、
  ///
  ///   1  bulletmlTask -> 弾               （run）
  ///   2  親の task -> 撃たれた弾の task    （createTask）
  ///
  /// 2 を入れずに 1 だけ入れた実装でも、**根の弾だけを見る門なら全部 緑になる**。
  /// 撃たれた弾の task は convertBulletmlTask を通らないので、
  /// 引き継がないと ShootingDirection が未設定のまま残る。
  /// これは DU なので、未設定は 0 ではなく null。
  ///
  /// type を省いたときは Core の既定（BulletVertical）が出る。
  /// DTD の既定は "none" だがそちらは採っていない。「縦でも横でもない」は
  /// 受け取った側が書けることが無いので、意味を持つ 2 つのどちらかに寄せた。
  [<Test>]
  member _.``type が根の弾と撃たれた子の弾の両方へ届く``() =
    let probe (label: string) (xml: string) =
      // 4 フレームあれば fire が 1 回 走って子が 1 つ産まれ、次のフレームで子も回る
      let _, bullets = Trace.runWithBullets ignore xml 4
      let seen =
        bullets
        |> List.map (fun b ->
            let o = b :> IBulletmlObject
            let v = if isNull (box o.ShootingDirection) then "★null" else string o.ShootingDirection
            sprintf "b%d=%s" b.Id v)
      // 0 件を緑にしないための締め。子が産まれていなければ 1 本しか出ない
      sprintf "%-22s 弾 %d 本  %s" label (List.length bullets) (String.concat " " seen)

    let lines =
      [ probe "type=vertical"   (bmlOfType "vertical")
        probe "type=horizontal" (bmlOfType "horizontal")
        probe "type 省略"        noTypeBml ]

    // 締めの 1 行に「見た弾の数」を出す。0 や 1 なら走査か網が壊れている
    let total =
      [ bmlOfType "vertical"; bmlOfType "horizontal"; noTypeBml ]
      |> List.sumBy (fun x -> Trace.runWithBullets ignore x 4 |> snd |> List.length)

    (String.concat "\n" lines) + sprintf "\n\n見た弾 ぜんぶで %d 本（3 条件 × 根と子）" total
    |> Golden.check "shooting-type-delivered"
