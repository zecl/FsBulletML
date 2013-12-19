namespace FsBulletML.Parser.Tests
open NUnit.Framework

[<AutoOpen>]
module Test = 
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

  let throws (exceptionType:System.Type) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.Throws exceptionType)::rest

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
    | x::rest -> (x.Ignore reason)::rest

  let makeExpli (reason:string) (list:TestCaseData list) =
    match list with 
    | [] -> invalidArg "list" "対象のテーストデータがありません。"
    | x::rest -> (x.MakeExplicit reason)::rest