namespace FsBulletML.Core.Tests

open System.Text.RegularExpressions
open NUnit.Framework
open FsBulletML.Processable

/// 参照にパラメータを足りなく渡す形。上の式を直に書く経路と違って、
/// **本番で踏める経路**かどうかをこちらで見る。
module Expressions =
  let private head = """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
"""

  let private tail = """</fireRef>
  <wait>10</wait>
</action>
<fire label="f">
  <direction type="absolute">0</direction>
  <speed>$1+$2</speed>
  <bullet/>
</fire>
</bulletml>"""

  let private refBody paramTags =
    head + "<action label=\"top\">\n  <fireRef label=\"f\">" + paramTags + tail

  /// $1 だけ渡す。参照先は $1 と $2 を使うので $2 が余る
  let shortParams = refBody "<param>2</param>"
  /// 過不足なく 2 つ渡す
  let fullParams = refBody "<param>2</param><param>5</param>"

  let firstBullet (xml: string) =
    let t = Trace.run xml 3
    let m = Regex.Match(t, @"\+b1 d=([-\d.]+) s=([-\d.]+)")
    if m.Success then sprintf "d=%s s=%s" m.Groups.[1].Value m.Groups.[2].Value
    else "撃っていない"

/// 式の評価。Util の TryParse は internal なので直接は呼べないが、
/// getValue 経由で <speed> に式を書けば届く。
/// 本体に InternalsVisibleTo を足さずに測るため、この形にしてある。
[<TestFixture>]
[<NonParallelizable>]
type Expressions() =

  let bml speed =
    sprintf """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
<action label="top">
  <fire>
    <direction type="absolute">0</direction>
    <speed>%s</speed>
    <bullet/>
  </fire>
  <wait>10</wait>
</action>
</bulletml>""" speed

  /// 撃った 1 発めの速さだけ取り出す
  let speedOf (expr: string) =
    let t = Trace.run (bml expr) 3
    let m = Regex.Match(t, @"\+b1 d=[-\d.]+ s=([-\d.]+)")
    if m.Success then m.Groups.[1].Value else "撃っていない"

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.25f, 30.0f, 100.0f))

  [<Test>]
  member _.``四則と優先順位``() =
    [ "1+2*3"      // 掛け算が先なら 7
      "(1+2)*3"    // 括弧が効けば 9
      "10/4"       // div に置換される。2.5 か 2 か
      "7%3"        // mod に置換される
      "0-3"        // 負の数
      "2*3+4*5" ]  // 26
    |> List.map (fun e -> sprintf "%-10s -> %s" e (speedOf e))
    |> String.concat "\n"
    |> Golden.check "expr-arithmetic"

  [<Test>]
  member _.``rand と rank が式に混ざる``() =
    // rand=0.5 / rank=0.25 で Init してある
    [ "$rand"
      "$rank"
      "$rand+$rank"
      "1+$rand*2"
      "$rand*$rank*8" ]
    |> List.map (fun e -> sprintf "%-14s -> %s" e (speedOf e))
    |> String.concat "\n"
    |> Golden.check "expr-rand-rank"

  /// getValue は残った $ を 0 に潰す。その正規表現が `\$d*` になっていて、
  /// `\$\d*` のつもりだったように見える。`$1` が丸ごと消えるのか、
  /// `$` だけ消えて `01` になるのかで結果が変わるので測る。
  [<Test>]
  member _.``参照の外に書いた 未解決の パラメータ``() =
    [ "$1"      // 丸ごと 0 になれば 0、$ だけなら 01 = 1
      "$2"      // 同じく 0 か 2 か
      "$12"     // 0 か 012 = 12 か
      "1+$1"    // 1 か 2 か
      "$rand+$1" ]
    |> List.map (fun e -> sprintf "%-10s -> %s" e (speedOf e))
    |> String.concat "\n"
    |> Golden.check "expr-unresolved-param"

  /// 上は式を直に書いた場合。**本番の経路で踏めるのか**を別に測る。
  /// 参照に渡すパラメータが足りないと、余った $N がそのまま getValue へ行くはず。
  [<Test>]
  member _.``参照にパラメータを足りなく渡したとき``() =
    sprintf "param 1 つ（$2 が余る）  %s\nparam 2 つ（過不足なし）  %s"
      (Expressions.firstBullet Expressions.shortParams)
      (Expressions.firstBullet Expressions.fullParams)
    |> Golden.check "expr-short-params"

  /// 4 の当てる先を実物で見る。samples 227 本のうち、
  /// **ref に渡す param が参照先の使う $N に足りないのは 2 本**（静的に数えた）。
  ///
  /// どちらも `<actionRef label="impl:30"></actionRef>` のように param を 1 つも渡さず、
  /// 参照先が `<direction>$2</direction>` `<speed>$1</speed>` を使う。
  ///
  /// 7 で「当てる先が在ること」と「値が動くこと」を別に測らずに踏んだので、
  /// ここは先に実物の値を控えにする
  [<Test>]
  member _.``実物 2 本の 未解決の パラメータ``() =
    let names = [ "[Bulletsmorph]_kunekune_plus_homing.xml"; "[Bulletsmorph]_satoru4.xml" ]
    let dir = System.IO.Path.GetFullPath(System.IO.Path.Combine(__SOURCE_DIRECTORY__, "..", "..", "samples"))
    let rows =
      names
      |> List.map (fun n ->
          let found =
            System.IO.Directory.EnumerateFiles(dir, n, System.IO.SearchOption.AllDirectories)
            |> Seq.filter (fun p ->
                let s = p.Replace('\\', '/')
                [ "/bin/"; "/obj/"; "/Library/"; "/Temp/" ] |> List.forall (s.Contains >> not))
            |> Seq.sort |> Seq.tryHead
          match found with
          | None -> sprintf "%-42s ★samples に無い" n
          | Some p ->
            let trace =
              try Trace.run (System.IO.File.ReadAllText p) 200
              with e ->
                let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
                sprintf "%s" ((inner e).GetType().Name)
            let speeds =
              trace.Split('\n')
              |> Array.filter (fun l -> l.Contains "  +b")
              |> Array.map (fun l ->
                  let m = Regex.Match(l, @"d=([-\d.]+) s=([-\d.]+)")
                  if m.Success then m.Groups.[1].Value + "/" + m.Groups.[2].Value else "?")
              |> Array.distinct |> Array.sort
            sprintf "%-42s 撃った %3d 発  向き/速さ %s"
              n (trace.Split('\n') |> Array.filter (fun l -> l.Contains "  +b") |> Array.length)
              (speeds |> String.concat " "))
    (rows @ [ sprintf "―― 2 本中 %d 本が撃った（0 なら走査が壊れている）"
                (rows |> List.filter (fun r -> not (r.Contains "撃った   0 発")) |> List.length) ])
    |> String.concat "\n"
    |> Golden.check "real-unresolved-param"
