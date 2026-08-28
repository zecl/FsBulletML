namespace FsBulletML.Parser.Tests
open System
open System.IO
open NUnit.Framework

[<AutoOpen>]
module Test =
  /// Original tests used Windows paths relative to bin/Debug (../../../TestData/...).
  /// Resolve to TestData copied next to the test assembly, with a source-tree fallback.
  let resolveTestPath (p: string) =
    let normalized = p.Replace('\\', '/')
    let prefix = "../../../TestData/"
    let relative =
      if normalized.StartsWith(prefix, StringComparison.Ordinal) then
        normalized.Substring(prefix.Length)
      else
        normalized
    let fromOutput = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "TestData", relative.Replace('/', Path.DirectorySeparatorChar)))
    if File.Exists(fromOutput) then fromOutput
    else
      Path.GetFullPath(Path.Combine(__SOURCE_DIRECTORY__, "..", "TestData", relative.Replace('/', Path.DirectorySeparatorChar)))

  type TestCaseParam =
    private 
    | SingleParam of obj
    | DoubleParam of obj*obj
    | TripleParam of obj*obj*obj
    | MultiParam  of obj[]

  let (|TestCaseParam|) = 
    let p = function
    | SingleParam a       -> [new TestCaseData(a)]
    | DoubleParam (a,b)   -> [new TestCaseData(a, b)]
    | TripleParam (a,b,c) -> [new TestCaseData(a, b, c)]
    | MultiParam  a       -> [new TestCaseData(a)]
    p
 
  let singleParam a = SingleParam(box a)
  let doubleParam a b = DoubleParam(box a,box b)
  let tripleParam a b c = TripleParam(box a,box b,box c)
  let testCase p = (|TestCaseParam|) p
  let inline (==>) a b = new TestCaseData(box a, box b)
  let and' p list = testCase p @ list
  let (&>) list p = and' p list

  /// NUnit 3 dropped TestCaseData.Throws. Stash the expected exception for withExpectedException.
  let throws (exceptionType:System.Type) (list:TestCaseData list) =
    match list with
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.SetProperty ("ExpectedException", exceptionType.AssemblyQualifiedName))::rest

  let withExpectedException (action: unit -> unit) =
    let props = TestContext.CurrentContext.Test.Properties
    if props.ContainsKey("ExpectedException") then
      let name = props.Get("ExpectedException") :?> string
      let t = Type.GetType(name, throwOnError=true)
      Assert.Throws(t, fun () -> action()) |> ignore
    else
      action()

  let (&!) list t = throws t list
    
  let setName (name:string) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.SetName name)::rest

  let setDescription (description:string) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.SetDescription description)::rest

  let setCategory (category:string) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.SetCategory category)::rest

  let setProperty (propName:string) (propValue:string) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.SetProperty (propName,propValue))::rest

  let returns (result:string) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.Returns result)::rest

  let ignoreCase (reason:string) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.Ignore reason :> TestCaseData)::rest

  let makeExpli (reason:string) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.Explicit reason)::rest