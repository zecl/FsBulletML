namespace FsBulletML

open System
open System.IO 
open System.Runtime.Serialization.Formatters.Binary
open System.Runtime.CompilerServices

[<AutoOpen>]
module internal Util = 
  let deepCopyClone<'a> (target:'a) =
    let clone : Object = null;
    use stream = new MemoryStream()
    let formatter = new BinaryFormatter()
    formatter.Serialize(stream, target)
    stream.Position <- 0L
    formatter.Deserialize(stream) :?> 'a

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

  let parseDate = tryParseWith System.DateTime.TryParse
  let parseInt32 = tryParseWith System.Int32.TryParse
  let parseInt64 = tryParseWith System.Int64.TryParse
  let parseSingle = tryParseWith System.Single.TryParse
  let parseDouble = tryParseWith System.Double.TryParse
  let parseDecimal = tryParseWith System.Decimal.TryParse 
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
  