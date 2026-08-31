namespace FsBulletML.Core.Tests

open System.IO
open System.Text.RegularExpressions
open NUnit.Framework

/// `BulletRunner.run` が返すものを、呼ぶ側がどう使っているかを固める。
///
/// run が返すのは **差分**（そのフレームの移動量）で、呼ぶ側が座標に足す。
/// 3 で見つけたとおり、絶対値を返す枝に届くと座標が膨らむ。膨らむ量は
/// 呼ぶ側の係数しだいで、同梱では MonoGame が 1 倍、Unity2D が 1/100。
///
/// **フロントは Core.Tests から呼べない**（MonoGame と UnityEngine が要る）。
/// なのでソースを読む門にする。壊れ方は 3 通りあって、どれもここに出る。
///
///   1  足すのをやめて代入にした（差分を絶対値として扱う）
///   2  係数を 1 か所だけ変えた（front-end の間でずれる）
///   3  Y の符号を 1 か所だけ変えた（Unity は下が正なので引いている）
///
/// **古びやすい門**（行を動かすと赤くなる）ことは承知のうえ。
/// Core を直したときにフロントで壊れることを、他に測る手が無い。
///
/// **網が前提にしている書き方**（いまの 6 ファイルは全部この形）。
///
///   let result = BulletRunner.run x      返り値の名前が `result`
///   self.X <- self.X + (x / 100)         代入の左が `.X` / `.Y` で終わる
///
/// **この形から外れると、係数の行だけが控えから消える。**
///
///   var d = BulletRunner.run(this)                        run の行は載る
///   self.Position = self.Position + new Vector2(d.X, d.Y) **この行は載らない**
///
/// 変数名が `result` でなく、代入の左が `.Position` なのでどちらの網にも当たらない。
/// **ファイル自体は run を呼ぶので控えに出る**（だから「新しいフロントが増えても
/// 何も起きない」ではない）。**出ないのは「どう座標へ入れたか」の行のほう。**
///
/// 網を広げると自機や背景の移動を拾い直すほうへ戻るので、
/// **前提を書いて残すほうを採った。** 控えにファイル名だけの行が出ていたら、
/// その形になっていないかを見ること。
///
/// **`NonParallelizable` を付けていないのは意図。** 他の fixture は
/// `BulletMLManager` という 1 つのグローバル可変を触るので逐次でないと混ざるが、
/// ここはソースを読むだけで走らせない。**規約から外れて見えるが、漏れではない。**
[<TestFixture>]
type CallingConvention() =

  let repoRoot =
    Path.GetFullPath(Path.Combine(__SOURCE_DIRECTORY__, "..", ".."))

  let isBuildOutput (p: string) =
    let s = p.Replace('\\', '/')
    [ "/bin/"; "/obj/"; "/Library/"; "/Temp/"; "/.git/" ] |> List.exists s.Contains

  let sources () =
    [ "src"; "samples" ]
    |> List.collect (fun d ->
        let dir = Path.Combine(repoRoot, d)
        if not (Directory.Exists dir) then []
        else
          Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories)
          |> Seq.filter (fun p -> p.EndsWith ".fs" || p.EndsWith ".cs")
          |> Seq.filter (isBuildOutput >> not)
          |> List.ofSeq)
    |> List.sort

  let relative (p: string) =
    p.Substring(repoRoot.Length).Replace('\\', '/').TrimStart('/')

  /// run を呼ぶ行と、返り値を座標へ入れる行
  let callsRun = Regex(@"BulletRunner\.run\b")
  let usesResult = Regex(@"result\.[XY]\b")
  /// `self.X <- self.X + ...` / `self.X = self.X + ...` の形（足しているか代入か）
  let movesPos = Regex(@"\.[XY]\s*(<-|=)\s*[^;]*\.[XY]\s*[+\-]")

  [<Test>]
  member _.``run の返り値の使われ方を固める``() =
    let files = sources ()
    // 当てる先が本当に在るかを先に見る。0 本なら緑にしない
    if List.length files < 10 then
      Assert.Fail(sprintf "src と samples のソースが %d 本しか見つかりません。走査が壊れています" (List.length files))

    // run の経路に居るファイルだけを本体にする。座標を足す行は自機や背景にもあり、
    // 混ぜると自機の速度を変えただけで赤くなる
    let inPipeline (text: string) =
      callsRun.IsMatch text || usesResult.IsMatch text || text.Contains "RunTask"

    let read = files |> List.map (fun f -> relative f, File.ReadAllLines f)

    let pick (lines: string[]) =
      lines
      |> Array.filter (fun l -> callsRun.IsMatch l || usesResult.IsMatch l || movesPos.IsMatch l)
      // 行番号は入れない。行を動かしただけで赤くしても意味が無い
      |> Array.map (fun l -> Regex.Replace(l.Trim(), @"\s+", " "))
      |> List.ofArray

    let core, others =
      read
      |> List.map (fun (name, lines) -> name, lines, pick lines)
      |> List.filter (fun (_, _, hit) -> not (List.isEmpty hit))
      |> List.partition (fun (_, lines, _) -> inPipeline (String.concat "\n" lines))

    let lines =
      [ yield "run が返すのは差分。呼ぶ側が座標に足す。"
        yield "係数と Y の符号はフロントごとに違う（MonoGame は 1 倍、Unity2D は 1/100 で Y を反転）。"
        yield ""
        for (file, _, hit) in core |> List.sortBy (fun (n, _, _) -> n) do
          yield file
          for l in hit do yield sprintf "    %s" l
          yield ""
        // 絞った外も出す。件数だけにしておくと、新しいフロントが増えたときは
        // ファイル名が増えて赤くなり、自機の速度をいじっただけでは動かない
        yield "run の経路に居ないが、座標を足している所（件数だけ）:"
        if List.isEmpty others then yield "  なし"
        else
          for (file, _, hit) in others |> List.sortBy (fun (n, _, _) -> n) do
            yield sprintf "  %-64s %d 行" file (List.length hit)
        yield ""
        // 0 件を緑にしないための締め
        yield sprintf "―― 本体 %d ファイル / %d 行（0 なら走査か網が壊れている）"
                (List.length core) (core |> List.sumBy (fun (_, _, h) -> List.length h)) ]

    Assert.That(core |> List.sumBy (fun (_, _, h) -> List.length h), Is.GreaterThan 5,
                "当てる先が少なすぎます。網が壊れています")
    String.concat "\n" lines |> Golden.check "calling-convention"
