namespace FsBulletML

open System
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
  let tryEval (expression:string) = 
    let regx = new System.Text.RegularExpressions.Regex(@"([\+\-\*])")
    let xexpr = regx.Replace(expression, " ${1} ").Replace("/", " div ").Replace("%", " mod ")
    let doc = new System.Xml.XPath.XPathDocument(new StringReader("<r/>"))
    let nav = doc.CreateNavigator()
    let ev = nav.Evaluate(String.Format("number({0})", xexpr)) |> string
    Single.TryParse(ev)

  let eval (expression:string) = 
    let regx = new System.Text.RegularExpressions.Regex(@"([\+\-\*])")
    let xexpr = regx.Replace(expression, " ${1} ").Replace("/", " div ").Replace("%", " mod ")
    let doc = new System.Xml.XPath.XPathDocument(new StringReader("<r/>"))
    let nav = doc.CreateNavigator()
    let ev = nav.Evaluate(String.Format("number({0})", xexpr)) |> string
    Single.Parse(ev)

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
  