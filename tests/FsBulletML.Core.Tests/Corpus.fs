namespace FsBulletML.Core.Tests

open System
open System.IO
open NUnit.Framework
open FsBulletML.Processable

/// samples に入っている実物の BulletML を全部走らせる。
///
/// 控えは 1 本しか作らない。200 本ぶんの軌跡を控えにすると、
/// リファクタリングのたびに巨大な差分が出て誰も読まなくなる。
/// ここで残すのは「落ちたもの」「1 発も撃たなかったもの」の名前だけにして、
/// 軌跡そのものは見ない。広く浅い網として置く。
///
/// 同じ XML が 4 つのサンプルに重複して置かれているので、
/// 中身のハッシュで潰してから数える（906 ファイル = 227 本）。
[<TestFixture>]
[<NonParallelizable>]
type Corpus() =

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

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``samples の弾幕を全部 60 フレーム走らせる``() =
    let files = uniqueSamples ()
    // 当てる先が本当に在るかを先に見る。0 本なら緑にしない
    if List.isEmpty files then
      Assert.Fail(sprintf "samples に xml が 1 本もありません（%s）。測れていません" samplesDir)

    let mutable ok = 0
    let broke = ResizeArray<string * string>()
    let silent = ResizeArray<string>()
    let skipped = ResizeArray<string>()
    File.WriteAllText(progressPath, "")

    for f in files do
      let name = relative f
      if Set.contains name known再帰 then skipped.Add name
      else
        // 処理する前に書く。StackOverflow で死んでも最後の行が犯人を指す
        File.AppendAllText(progressPath, name + "\n")
        try
          let xml = File.ReadAllText f
          let t = Trace.run xml 60
          if t.Contains "  +b" then ok <- ok + 1 else silent.Add name
        with e ->
          let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
          let i = inner e
          broke.Add(name, sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r", "").Replace("\n", " ")))

    let lines =
      [ yield sprintf "一意な弾幕 %d 本（中身のハッシュで潰したあと）" (List.length files)
        yield sprintf "  撃った       %d 本" ok
        yield sprintf "  撃たなかった %d 本" silent.Count
        yield sprintf "  落ちた       %d 本" broke.Count
        yield sprintf "  避けた       %d 本（StackOverflow で捕まえられないもの）" skipped.Count
        yield ""
        yield "避けたもの（参照が輪になっていて展開できないもの）:"
        if skipped.Count = 0 then yield "  なし"
        else for n in Seq.sort skipped do yield sprintf "  %s" n
        yield ""
        yield "落ちたもの:"
        if broke.Count = 0 then yield "  なし"
        else for (n, m) in Seq.sortBy fst broke do yield sprintf "  %s\n    %s" n m
        yield ""
        yield "60 フレームで 1 発も撃たなかったもの:"
        if silent.Count = 0 then yield "  なし"
        else for n in Seq.sort silent do yield sprintf "  %s" n ]
    String.concat "\n" lines |> Golden.check "corpus-smoke"
