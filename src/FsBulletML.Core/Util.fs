namespace FsBulletML

open System
open System.Globalization
open System.IO
open System.Runtime.CompilerServices

[<AutoOpen>]
module internal Util = 
  // BinaryFormatter was removed on modern .NET (throws / does not compile on netstandard2.1).
  // ProcessableBulletml trees are cloned by Processable.cloneProcessable (BulletRunner.createTask).
  // TODO: do not bring BinaryFormatter back; add a typed clone if another type needs a deep copy.

  open System.Diagnostics
  let dprintf fmt = Printf.ksprintf Debug.Write fmt
  let dprintfn fmt = Printf.ksprintf Debug.WriteLine fmt

[<RequireQualifiedAccess>]
module internal TryParse =
  /// 式の値は BulletML の文書と同じ書き方（小数点は . ）で持ち回る。
  /// 読む側を既定カルチャのままにすると、de-DE は "2.5" の . を桁区切りと読んで
  /// 例外なく 25 を返す。作る側だけ直すと、落ちる不具合が静かな不具合に変わる。
  ///
  /// 作る側はここでは元から不変（F# の string 演算子）。明示にしてあるのは、
  /// その不変性が実装に依るところなので、版が変わっても動くようにするため
  let private xpathNumber (expression:string) =
    let regx = new System.Text.RegularExpressions.Regex(@"([\+\-\*])")
    let xexpr = regx.Replace(expression, " ${1} ").Replace("/", " div ").Replace("%", " mod ")
    let doc = new System.Xml.XPath.XPathDocument(new StringReader("<r/>"))
    let nav = doc.CreateNavigator()
    Convert.ToString(nav.Evaluate(String.Format("number({0})", xexpr)), CultureInfo.InvariantCulture)

  let tryEval (expression:string) =
    Single.TryParse(xpathNumber expression, NumberStyles.Float, CultureInfo.InvariantCulture)

  let eval (expression:string) =
    Single.Parse(xpathNumber expression, NumberStyles.Float, CultureInfo.InvariantCulture)

  let tryParseWith tryParseFunc = 
    tryParseFunc >> function
    | true, v    -> Some v
    | false, _   -> None

  let parseDate = tryParseWith (fun (s: string) -> System.DateTime.TryParse(s))
  let parseInt32 = tryParseWith (fun (s: string) -> System.Int32.TryParse(s))
  let parseInt64 = tryParseWith (fun (s: string) -> System.Int64.TryParse(s))
  let parseSingle = tryParseWith (fun (s: string) -> System.Single.TryParse(s))
  let parseDouble = tryParseWith (fun (s: string) -> System.Double.TryParse(s))
  let parseDecimal = tryParseWith (fun (s: string) -> System.Decimal.TryParse(s))
  let parseEval = tryParseWith tryEval

[<AutoOpen>]
module TryParseActivePattern =
  let (|Date|_|) = TryParse.parseDate
  let (|Int32|_|) = TryParse.parseInt32
  let (|Int64|_|) = TryParse.parseInt64
  let (|Single|_|) = TryParse.parseSingle
  let (|Double|_|) = TryParse.parseDouble
  let (|Decimal|_|) = TryParse.parseDecimal
  let (|Eval|_|) = TryParse.parseEval

[<AutoOpen>]
module Monad = 
  type MaybeBuilder() =
    member b.Bind(m, f) = Option.bind f m
    member b.Return(a) = Some a
    member b.ReturnFrom(m) = m
    member b.Zero() = None

  let maybe = new MaybeBuilder()

  [<Extension>]
  type OptionExtentions =
    [<Extension>]
    static member Match<'T>(this:'T option,ifSome: Func<_,_>, ifNone: Func<_,_>) =
      match this with
      | Some x -> ifSome.Invoke(x)
      | None -> ifNone.Invoke()

    [<Extension>]
    static member Action<'T>(this:'T option,ifSome: Action<_>, ifNone: Action<_>) =
      match this with
      | Some x -> ifSome.Invoke(x)
      | None -> ifNone.Invoke()
  