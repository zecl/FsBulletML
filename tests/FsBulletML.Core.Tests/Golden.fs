namespace FsBulletML.Core.Tests

open System
open System.IO
open NUnit.Framework

/// 軌跡を控え（tests/TestData/trace/<name>.txt）と突き合わせる。
///
/// **控えが無いときは書いて落とす。** 黙って作って緑にすると、
/// 「控えを作った」だけで何も担保していない状態が緑になる。
/// 作った控えは目で読んでから、もう一度回して緑にすること。
module Golden =

  let private sourceDir =
    Path.GetFullPath(Path.Combine(__SOURCE_DIRECTORY__, "..", "TestData", "trace"))

  let private outputDir =
    Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "TestData", "trace"))

  let private normalize (s: string) = s.Replace("\r\n", "\n").TrimEnd('\n')

  /// 控えを読む。出力先に無ければソース側を見る（Content のコピーが古いことがある）
  let private read (name: string) =
    let fromOutput = Path.Combine(outputDir, name + ".txt")
    let fromSource = Path.Combine(sourceDir, name + ".txt")
    if File.Exists fromSource then Some(File.ReadAllText fromSource |> normalize)
    elif File.Exists fromOutput then Some(File.ReadAllText fromOutput |> normalize)
    else None

  let check (name: string) (actual: string) =
    let actual = normalize actual
    match read name with
    | None ->
      Directory.CreateDirectory sourceDir |> ignore
      let path = Path.Combine(sourceDir, name + ".txt")
      File.WriteAllText(path, actual + "\n")
      Assert.Fail(
        sprintf "控えがありませんでした。いまの実装の出力を %s に書きました。\n\
                 **目で読んでから**もう一度回してください。読まずに緑にすると何も担保しません。\n\n%s"
          path actual)
    | Some expected ->
      if expected <> actual then
        // 落ちたときに何行目で割れたかを出す。diff を目で追う手間を減らす
        let e = expected.Split('\n')
        let a = actual.Split('\n')
        let i = Seq.init (min e.Length a.Length) id |> Seq.tryFind (fun i -> e.[i] <> a.[i])
        let where =
          match i with
          | Some i -> sprintf "%d 行目で割れました。\n  控え: %s\n  いま: %s" (i + 1) e.[i] a.[i]
          | None -> sprintf "行数が違います。控え %d 行 / いま %d 行" e.Length a.Length
        Assert.Fail(sprintf "軌跡が控えと違います（%s）。\n%s\n\n--- 控え ---\n%s\n\n--- いま ---\n%s"
                      name where expected actual)
