namespace FsBulletML.Core.Tests

open System
open System.Globalization
open System.Text.RegularExpressions
open System.Threading
open NUnit.Framework
open FsBulletML.Processable

/// 式の評価がカルチャに影響されるか。
///
/// TryParse.eval（Util.fs）は
///   XPath の number() で計算 -> 文字列化 -> Single.Parse(ev)
/// という形で、**Single.Parse にカルチャを渡していない**。
/// XPath が返すのは "." 区切りなので、"," が小数点のカルチャだと
/// 解釈が変わるはず、というのが読み（未測定の読みとして立てたもの）。
///
/// ここで実際に走らせて確かめる。
[<TestFixture>]
[<NonParallelizable>]
type Culture() =

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

  let speedOf (expr: string) =
    try
      let t = Trace.run (bml expr) 3
      let m = Regex.Match(t, @"\+b1 d=[-\d.]+ s=([-\d.]+)")
      if m.Success then m.Groups.[1].Value else "撃っていない"
    with e -> sprintf "%s" (e.GetType().Name)

  /// 型名だけだと理由が分からないので、メッセージまで取る版
  let speedOfVerbose (expr: string) =
    try
      let t = Trace.run (bml expr) 3
      let m = Regex.Match(t, @"\+b1 d=[-\d.]+ s=([-\d.]+)")
      if m.Success then m.Groups.[1].Value else "撃っていない"
    with e ->
      let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
      let i = inner e
      sprintf "%s: %s" (i.GetType().Name) (i.Message.Replace("\r", "").Replace("\n", " "))

  /// カルチャを差し替えて測り、必ず戻す
  let under (name: string) f =
    let before = Thread.CurrentThread.CurrentCulture
    try
      Thread.CurrentThread.CurrentCulture <- CultureInfo(name)
      f ()
    finally
      Thread.CurrentThread.CurrentCulture <- before

  [<SetUp>]
  member _.SetUp() =
    BulletMLManager.Init(FixedManager(0.5f, 0.5f, 30.0f, 100.0f))

  [<Test>]
  member _.``小数の出る式を、小数点がカンマのカルチャで評価する``() =
    // "10/4" は XPath が 2.5 を返す。ここが "." 区切りのまま Single.Parse に渡る
    let exprs = [ "10/4"; "1/3"; "0-2.5"; "2*1.5" ]
    let row name =
      under name (fun () ->
        exprs
        |> List.map (fun e -> sprintf "%s=%s" e (speedOf e))
        |> String.concat "  ")
      |> sprintf "%-8s %s" name
    [ row "en-US"      // "." が小数点
      row "ja-JP"      // "." が小数点
      row "de-DE"      // "," が小数点
      row "fr-FR" ]    // "," が小数点
    |> String.concat "\n"
    |> Golden.check "culture-decimal"

  /// 整数だけの式も落ちるのか、小数が出るときだけなのかを分ける。
  /// ここを測らずに「小数のときだけ」と書くと、根拠のない断定になる。
  [<Test>]
  member _.``整数だけの式は、カルチャに影響されるか``() =
    let exprs = [ "1+2*3"; "(1+2)*3"; "7%3"; "0-3"; "8/4" ]   // 8/4 は割り切れる
    let row name =
      under name (fun () ->
        exprs
        |> List.map (fun e -> sprintf "%s=%s" e (speedOf e))
        |> String.concat "  ")
      |> sprintf "%-8s %s" name
    [ row "en-US"; row "ja-JP"; row "de-DE"; row "fr-FR" ]
    |> String.concat "\n"
    |> Golden.check "culture-integer"

  /// 型名だけでは理由が分からなかったので、落ちる中身まで残す。
  /// ここを読めば「何が原因で落ちているか」が控えに残る。
  [<Test>]
  member _.``落ちるときのメッセージ``() =
    [ sprintf "de-DE 1+2*3   %s" (under "de-DE" (fun () -> speedOfVerbose "1+2*3"))
      sprintf "de-DE 2.5     %s" (under "de-DE" (fun () -> speedOfVerbose "2.5"))
      sprintf "fr-FR 1+2*3   %s" (under "fr-FR" (fun () -> speedOfVerbose "1+2*3"))
      sprintf "fr-FR 2.5     %s" (under "fr-FR" (fun () -> speedOfVerbose "2.5"))
      sprintf "en-US 1+2*3   %s" (under "en-US" (fun () -> speedOfVerbose "1+2*3")) ]
    |> String.concat "\n"
    |> Golden.check "culture-messages"

  /// 落ちている式が「うちが書いた speed」ではなく別の値だったので、
  /// どの値が化けているのかを、wait を振って突き止める。
  [<Test>]
  member _.``化けているのはどの値か``() =
    let bmlWait w =
      sprintf """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
<action label="top">
  <fire>
    <direction type="absolute">0</direction>
    <speed>1</speed>
    <bullet/>
  </fire>
  <wait>%s</wait>
</action>
</bulletml>""" w
    let probe w =
      try
        Trace.run (bmlWait w) 3 |> ignore
        "落ちない"
      with e ->
        let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
        (inner e).Message.Replace("\r", "").Replace("\n", " ")
    [ sprintf "wait=10 %s" (under "de-DE" (fun () -> probe "10"))
      sprintf "wait=3  %s" (under "de-DE" (fun () -> probe "3"))
      sprintf "wait=7  %s" (under "de-DE" (fun () -> probe "7")) ]
    |> String.concat "\n"
    |> Golden.check "culture-which-value"

  /// $rand / $rank の経路（Processable.getValue の Replace）もカルチャ依存か。
  /// rand.ToString() にカルチャを渡していないので de-DE では 0,5 が式に入るはず、
  /// というのが読み（未実測の読みとして立てたもの）。
  ///
  /// **どこで落ちるかが要点。** 上流の ToString("F10") で先に落ちるなら
  /// wait と同じメッセージになるし、$rand のほうが先なら 0,5 が出るはず。
  [<Test>]
  member _.``rand と rank の置換はカルチャ依存か``() =
    let probe expr =
      try
        let t = Trace.run (bml expr) 3
        let m = Regex.Match(t, @"\+b1 d=[-\d.]+ s=([-\d.]+)")
        if m.Success then m.Groups.[1].Value else "撃っていない"
      with e ->
        let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
        (inner e).Message.Replace("\r", "").Replace("\n", " ")
    [ sprintf "en-US $rand      %s" (under "en-US" (fun () -> probe "$rand"))
      sprintf "de-DE $rand      %s" (under "de-DE" (fun () -> probe "$rand"))
      sprintf "de-DE $rank      %s" (under "de-DE" (fun () -> probe "$rank"))
      sprintf "de-DE 1+$rand*2  %s" (under "de-DE" (fun () -> probe "1+$rand*2"))
      sprintf "fr-FR $rand      %s" (under "fr-FR" (fun () -> probe "$rand")) ]
    |> String.concat "\n"
    |> Golden.check "culture-rand-rank"

  /// 上は <wait>10</wait> が先に落ちて $rand まで届かなかった。
  /// wait を外して $rand だけを通し、この経路が単独で落ちるのかを見る。
  [<Test>]
  member _.``wait を外して rand だけを通す``() =
    let noWait expr =
      sprintf """<?xml version="1.0" ?>
<!DOCTYPE bulletml SYSTEM "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
<action label="top">
  <fire>
    <direction type="absolute">0</direction>
    <speed>%s</speed>
    <bullet/>
  </fire>
</action>
</bulletml>""" expr
    let probe expr =
      try
        let t = Trace.run (noWait expr) 2
        let m = Regex.Match(t, @"\+b1 d=[-\d.]+ s=([-\d.]+)")
        if m.Success then m.Groups.[1].Value else "撃っていない"
      with e ->
        let rec inner (x: exn) = if isNull x.InnerException then x else inner x.InnerException
        (inner e).Message.Replace("\r", "").Replace("\n", " ")
    [ sprintf "en-US $rand   %s" (under "en-US" (fun () -> probe "$rand"))
      sprintf "de-DE $rand   %s" (under "de-DE" (fun () -> probe "$rand"))
      sprintf "de-DE $rank   %s" (under "de-DE" (fun () -> probe "$rank"))
      sprintf "de-DE 2       %s" (under "de-DE" (fun () -> probe "2"))
      sprintf "fr-FR $rand   %s" (under "fr-FR" (fun () -> probe "$rand")) ]
    |> String.concat "\n"
    |> Golden.check "culture-rand-isolated"

  /// 直す前は fr-FR だけ FormatException で、de-DE は XPathException だった。
  /// 理由は推論のまま残り、直したあとは再現しないので測れない。
  /// せめて材料として、4 カルチャの数の書き方を控えに残す。
  ///
  /// 不変カルチャへ寄せた実装が正しい理由も、この表で読める ——
  /// 小数点が 2 通り、桁区切りが 3 通りあり、BulletML の文書は . 固定
  [<Test>]
  member _.``カルチャごとの数の書き方``() =
    let esc (s: string) =
      s |> Seq.map (fun c -> if int c < 0x20 || int c > 0x7E then sprintf "U+%04X" (int c) else string c)
        |> String.concat ""
    let row (name: string) (f: NumberFormatInfo) =
      sprintf "%-10s 小数点 %-8s 桁区切り %-8s 負号 %s"
        name (esc f.NumberDecimalSeparator) (esc f.NumberGroupSeparator) (esc f.NegativeSign)
    [ for n in [ "en-US"; "ja-JP"; "de-DE"; "fr-FR" ] -> row n (CultureInfo(n).NumberFormat)
      yield row "Invariant" CultureInfo.InvariantCulture.NumberFormat ]
    |> String.concat "\n"
    |> Golden.check "culture-numberformat"

  /// XML に書いた小数リテラルのほうもカルチャで揺れるか。
  /// こちらは eval に入る前の文字列なので、揺れるなら別経路。
  [<Test>]
  member _.``小数リテラルだけの式``() =
    let row name =
      under name (fun () -> speedOf "2.5") |> sprintf "%-8s 2.5 -> %s" name
    [ row "en-US"; row "ja-JP"; row "de-DE"; row "fr-FR" ]
    |> String.concat "\n"
    |> Golden.check "culture-literal"
