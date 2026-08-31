namespace FsBulletML.Core.Tests

open System
open System.IO
open System.Security.Cryptography
open System.Text
open NUnit.Framework
open FsBulletML.Processable

/// samples に入っている実物の BulletML を全部走らせる下ごしらえ。
///
/// 227 本 × 60 フレームの走行は 8 秒 かかる。控えを 2 本 取るが、
/// 走らせるのは 1 回だけにして、結果を両方で使う。
///
/// 同じ XML が 4 つのサンプルに重複して置かれているので、
/// 中身のハッシュで潰してから数える（906 ファイル = 227 本）。
module private CorpusData =

  let samplesDir =
    Path.GetFullPath(Path.Combine(__SOURCE_DIRECTORY__, "..", "..", "samples"))

  /// ビルドが吐いたコピーを外す。
  ///
  /// 入れないと分母がビルド状態に依存する。
  /// samples の下の xml は 1399 個あるが、git が追跡しているのは 906 個で、
  /// 差の 493 個はほとんど bin/Debug へ複写されたもの。
  /// 焼いた直後と clean clone で数が変わるので、測るたびに分母が動く。
  let isBuildOutput (p: string) =
    let s = p.Replace('\\', '/')
    [ "/bin/"; "/obj/"; "/Library/"; "/Temp/" ] |> List.exists s.Contains

  /// 中身が同じものを 1 本に潰して返す（相対パスの辞書順で最初のものを代表にする）
  let uniqueSamples () =
    if not (Directory.Exists samplesDir) then []
    else
      Directory.EnumerateFiles(samplesDir, "*.xml", SearchOption.AllDirectories)
      |> Seq.filter (isBuildOutput >> not)
      |> Seq.map (fun p -> p, File.ReadAllText p)
      |> Seq.groupBy snd
      |> Seq.map (fun (_, g) -> g |> Seq.map fst |> Seq.sort |> Seq.head)
      |> Seq.sort
      |> List.ofSeq

  let relative (p: string) =
    let root = Path.GetFullPath(Path.Combine(__SOURCE_DIRECTORY__, "..", ".."))
    p.Substring(root.Length).Replace('\\', '/').TrimStart('/')

  /// StackOverflow で落ちるので避けていた弾幕。**2026-08-31 に空にした。**
  ///
  /// 打ち止め（`convertRefBulletml` が展開中の参照を種別つきの集合で持ち、
  /// 再訪したら `BulletmlDTDViolationException`）を入れたので、
  /// **7 本ともプロセスを落とさずに例外で戻るようになった。**
  /// 避ける必要が無くなったので空にしてある。控え側は「落ちた」に分類される。
  ///
  /// 空にする前に避けていたのはこの 7 本（`bosses.d/` の下）。
  ///
  ///   [ESP_RADE]_round_123_boss_izuna_fan / [Original]_cont_circle
  ///   [Original]_light_lv10 / [Original]_light_lv25 / [Original]_light_max
  ///   [Original]_water_lv10 / [OtakuTwo]_accel_jump
  ///
  /// **また落ちるようになったら、ここに戻して隔離すること。**
  /// 見つけ方は下の `progressPath` —— 処理する前にファイル名を書くので、
  /// プロセスが死んでも最後の行が犯人を指す。
  let known再帰 : Set<string> = Set.empty

  /// 落ちた場所を突き止めるための足跡。プロセスが死んでも残る
  let progressPath =
    Path.Combine(Path.GetTempPath(), "fsb-corpus-progress.txt")

  /// 弾幕 1 本ぶんの結果
  type Row =
    { Name : string
      /// 60 フレームのあいだに産まれた弾の数
      Fired : int
      /// 最終フレームに残っていた弾の数
      Alive : int
      /// 軌跡そのものの指紋。値が 1 つでも動けば変わる
      Digest : string
      /// 落ちた／避けた理由。走ったものは None
      Error : string option }

  let private digest (s: string) =
    use h = SHA256.Create()
    h.ComputeHash(Encoding.UTF8.GetBytes s)
    |> Array.take 6
    |> Array.map (fun b -> b.ToString("x2"))
    |> String.concat ""

  /// 最終フレームに残っていた弾を数える。
  /// 軌跡は `f00` の行のあとに `  b0 ...` が並ぶので、最後の `f` 行より下を数える
  let private aliveAtEnd (lines: string[]) =
    match lines |> Array.tryFindIndexBack (fun l -> l.StartsWith "f") with
    | Some i -> lines.[i + 1 ..] |> Array.filter (fun l -> l.StartsWith "  b") |> Array.length
    | None -> 0

  /// 227 本を走らせるのに掛かった時間。`all` を最初に触った側が測る
  let mutable elapsedMs = 0.0

  /// 走行は 1 回だけ。corpus-smoke と corpus-trace が同じ結果を読む
  let all : Lazy<Row list> =
    lazy (
      let sw = Diagnostics.Stopwatch.StartNew()
      BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))
      let files = uniqueSamples ()
      File.WriteAllText(progressPath, "")
      [ for f in files do
          let name = relative f
          if Set.contains name known再帰 then
            { Name = name; Fired = 0; Alive = 0; Digest = ""; Error = Some "避けた" }
          else
            // 処理する前に書く。StackOverflow で死んでも最後の行が犯人を指す
            File.AppendAllText(progressPath, name + "\n")
            try
              let t = Trace.run (File.ReadAllText f) 60
              let lines = t.Split('\n')
              { Name = name
                Fired = lines |> Array.filter (fun l -> l.Contains "  +b") |> Array.length
                Alive = aliveAtEnd lines
                Digest = digest t
                Error = None }
            with e ->
              let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
              let i = inner e
              { Name = name; Fired = 0; Alive = 0; Digest = ""
                Error = Some(sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r", "").Replace("\n", " "))) } ]
      // 並べ直すのは Name（/ 区切り）で。走る順はフルパス（\ 区切り）なので順序が違う
      // —— Enemy\move.xml と EnemyBullet\... は '\'(92) > 'B'(66) で逆になる
      |> List.sortBy (fun r -> r.Name)
      |> fun rows ->
          sw.Stop()
          elapsedMs <- sw.Elapsed.TotalMilliseconds
          rows)

/// 実物の弾幕を走らせて固める網。控えは 2 本で、見ているものが違う。
///
///   corpus-smoke  撃った／撃たなかった／落ちた／避けた の数と名前。広く浅い
///   corpus-trace  1 本ずつの弾数・生存数・軌跡の指紋。広く深い
///
/// smoke だけだと、227 本ぜんぶの軌跡が変わっても緑のまま通る。
/// 2026-08-31 に wait を直したとき、控えが 17 本 動いたのに smoke の 3 つの数は
/// 1 つも動かなかった。走らせた軌跡を捨てていたので、同じ走行から指紋を残す
/// ようにした（走行そのものは増えていない）。
[<TestFixture>]
[<NonParallelizable>]
type Corpus() =

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``samples の弾幕を全部 60 フレーム走らせる``() =
    let rows = CorpusData.all.Value
    // 当てる先が本当に在るかを先に見る。0 本なら緑にしない
    if List.isEmpty rows then
      Assert.Fail(sprintf "samples に xml が 1 本もありません（%s）。測れていません" CorpusData.samplesDir)

    let skipped = rows |> List.filter (fun r -> r.Error = Some "避けた")
    let broke = rows |> List.filter (fun r -> match r.Error with Some m -> m <> "避けた" | None -> false)
    let silent = rows |> List.filter (fun r -> r.Error.IsNone && r.Fired = 0)
    let ok = rows |> List.filter (fun r -> r.Error.IsNone && r.Fired > 0)

    let lines =
      [ yield sprintf "一意な弾幕 %d 本（中身のハッシュで潰したあと）" (List.length rows)
        yield sprintf "  撃った       %d 本" (List.length ok)
        yield sprintf "  撃たなかった %d 本" (List.length silent)
        yield sprintf "  落ちた       %d 本" (List.length broke)
        yield sprintf "  避けた       %d 本（StackOverflow で捕まえられないもの）" (List.length skipped)
        yield ""
        yield "避けたもの（参照が輪になっていて展開できないもの）:"
        if List.isEmpty skipped then yield "  なし"
        else for r in skipped do yield sprintf "  %s" r.Name
        yield ""
        yield "落ちたもの:"
        if List.isEmpty broke then yield "  なし"
        else for r in broke do yield sprintf "  %s\n    %s" r.Name (Option.defaultValue "" r.Error)
        yield ""
        yield "60 フレームで 1 発も撃たなかったもの:"
        if List.isEmpty silent then yield "  なし"
        else for r in silent do yield sprintf "  %s" r.Name ]
    String.concat "\n" lines |> Golden.check "corpus-smoke"

  /// 上の smoke と同じ走行から、1 本ずつの中身を残す。
  ///
  /// 指紋だけだと何が変わったか読めないので、弾の数と最終フレームの生存数を
  /// 先に出す。その 2 つが同じで指紋だけ動いたら、弾の数は変わらず
  /// 値（向き・速さ・座標）が動いたと分かる。
  [<Test>]
  member _.``samples の弾幕の軌跡を 1 本ずつ控えにする``() =
    let rows = CorpusData.all.Value
    if List.isEmpty rows then
      Assert.Fail(sprintf "samples に xml が 1 本もありません（%s）。測れていません" CorpusData.samplesDir)

    let ran = rows |> List.filter (fun r -> r.Error.IsNone)
    let body =
      [ for r in rows do
          match r.Error with
          | Some m -> yield sprintf "  %-104s %s" r.Name m
          | None -> yield sprintf "  %-104s 撃った %4d 発  残り %3d 本  %s" r.Name r.Fired r.Alive r.Digest ]

    [ yield sprintf "一意な弾幕 %d 本。うち走ったのは %d 本" (List.length rows) (List.length ran)
      yield sprintf "撃った弾の合計 %d 発 ／ 最終フレームに残っていた合計 %d 本"
              (ran |> List.sumBy (fun r -> r.Fired)) (ran |> List.sumBy (fun r -> r.Alive))
      yield ""
      yield! body
      yield ""
      // 0 件を緑にしないための締め。指紋が 1 つも出ていなければ走査が壊れている
      yield sprintf "―― 指紋が付いたのは %d 本（0 なら走査が壊れている）" (List.length ran) ]
    |> String.concat "\n"
    |> Golden.check "corpus-trace"

  /// 227 本を走らせる時間の天井。**これは性能の測定ではない。**
  ///
  /// 壁時計は台と時刻で 15% くらい平気で動くので、締めた値を置くと
  /// 中身が何も変わっていない日に赤くなる。ここで捕まえたいのは
  /// 「桁で遅くなった」——- たとえばうっかり O(n^2) を入れた、という壊れ方だけ。
  ///
  /// 手元の素の値は 8〜16 秒。天井は 120 秒に置いてある（10 倍 弱の余裕）。
  /// **数字そのものは控えに残さない。**残すと走るたびに動いて門が死ぬ。
  /// 実測は下の WriteLine に出るので、遅くなっていく傾向は人が読める。
  [<Test>]
  member _.``227 本の走行が桁で遅くなっていない``() =
    CorpusData.all.Value |> ignore
    let ms = CorpusData.elapsedMs
    TestContext.WriteLine(sprintf "227 本 × 60 フレームの走行: %.0f ms" ms)
    // 0 は「測れていない」。lazy を誰かが先に触っていても elapsedMs は残る
    Assert.That(ms, Is.GreaterThan 0.0, "時間が測れていません")
    Assert.That(ms, Is.LessThan 120000.0,
                sprintf "227 本の走行に %.0f ms 掛かっています。桁で遅くなっていないか見てください" ms)
