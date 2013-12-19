namespace FsBulletML

open System
open System.IO 
open System.Text 
open System.Text.RegularExpressions
open FParsec
open FParsec.Internals
open FParsec.Error
open FParsec.Primitives
open FParsec.CharParsers

module Offside =

  type UserState = 
    { Current: int 
      Next: int 
      Depth: int list }
    with
      static member Create() = { Depth = []; Current = 0; Next = 0 }
      static member UpdateCurrent current self = { self with Current = current }
      static member UpdateNext next self = { self with Next = next }
      static member UpdateDepth depth self = { self with Depth = depth }

  let str s = pstring s
  let ws = manyChars (pchar ' ') 
  let pid = (fun _ -> Reply(()))
  let nextline = attempt (eof) <|> skipNewline >>. skipMany (regex "\s*$\n")
  let ptagName =  many1Chars (asciiLetter <|> digit)
  let pattrLabel = many1Chars (asciiLetter)
  let chr c = skipChar c
  let skipSpaces1 = skipMany (spaces1) <?> "no skip"
  let pattrValue = skipSpaces1 >>. chr '\"' >>. manyChars (noneOf "\"") .>> chr '\"'  
  let dprintPosition fmt = parse {
    let! p,_ = getPosition .>>. pid
    let dprintfn (fmt:Printf.StringFormat<_ -> _, unit>) = Printf.ksprintf System.Diagnostics.Debug.WriteLine fmt
    dprintfn fmt p
    return ()
  }

  /// attribute
  let pAttr = parse {
    let! label = pattrLabel
    do! (ws >>. str "=" >>. ws |>> ignore)
    let! value = pattrValue 
    return label,value} <?> "attribute error"

  /// attributes and body
  let pAttrsAndBody = parse{
      let pBodyValue = 
        chr '"' >>.
        manyChars (asciiLetter <|> digit <|> anyOf "()$+-*/.%" )
        .>> chr '"'
      let pSep = (str ":" >>. ws) 
      let pBody = attempt ((pSep >>. pBodyValue .>> ws) <|> str "")
      let pAttrsAndBody = (sepEndBy pAttr (many1Chars (pchar ' '))) .>>. pBody
      return! attempt (ws >>. pAttrsAndBody)
    }

  let dprintPointIndented = parse {
    let! state = getUserState
    let indent = ("".PadLeft(state.Current, ' '))
    let fmt  = Printf.StringFormat<_ -> _, unit>(indent + "%A")
    do! dprintPosition fmt
  }

  let pUpdateDepth = 
    attempt (parse {
      do! eof
      do! updateUserState (UserState.UpdateNext 0)
    }) <|>  parse {
      let! depth = ws |>> String.length
      do! (UserState.UpdateNext depth) |> updateUserState
    }

  let pSameDepth = parse {
    let! state = getUserState
    do! userStateSatisfies (fun state -> state.Next = state.Current)
    } 
  
  let pOpenIndent = parse {
    let! state = getUserState
    do! dprintPointIndented
    do! userStateSatisfies (fun state -> state.Current < state.Next)
    do! (UserState.UpdateDepth (state.Current :: state.Depth) >> UserState.UpdateCurrent (state.Next)) |> updateUserState
  }

  let pCloseIndent = parse {
    let! state = getUserState
    do! userStateSatisfies (fun state -> state.Next <= state.Current) 
    do! (UserState.UpdateDepth (List.tail state.Depth) >> UserState.UpdateCurrent (List.head state.Depth)) |> updateUserState
  }

  let pAst, pAstRef  = createParserForwardedToRef()

  let pChildren = opt <| parse {
    let! state = getUserState
    let! children = between pOpenIndent pCloseIndent (many pAst)
    return children } 

  let pElement = parse {
    do! skipMany <| pchar ' '
    let! name = ptagName
    let! attrs,body = pAttrsAndBody
    return name, attrs, body
  }

  pAstRef := parse {
    let! state = getUserState
    
    do! pSameDepth
    let! name, attrs, body = pElement
    do! nextline
    do! pUpdateDepth

    let! children = pChildren
    return children |> function
      | Some chiled -> Element(name, attrs, chiled)
      | None -> Element(name, attrs ,if body="" then [] else [PCData(body)])
  }

  [<CompiledName "Parse">]
  let parse input = runParserOnString pAst (UserState.Create()) "" input

  [<CompiledName "ParseFromFile">]
  let parseFromFile sxmlFile = 
    let sr = new StreamReader((sxmlFile:string), Encoding.GetEncoding("UTF-8"))
    let input = sr.ReadToEnd()
    parse input
