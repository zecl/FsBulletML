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

module Sxml =

  type SxmlParser<'a> = Parser<'a, unit>
  type SxmlParser = Parser<XmlNode, unit>

  let chr c = skipChar c
  let skipSpaces1 : SxmlParser<unit> = skipMany (spaces1) <?> "no skip"
  let endBy p sep = many (p .>> sep)
  let pAst, pAstRef : SxmlParser * SxmlParser ref = createParserForwardedToRef()

  let parenOpen = skipSpaces1 >>. chr '('
  let parenClose = skipSpaces1 >>. chr ')'
  let parenOpenAt = skipSpaces1 >>. skipString "(@"
  let pChildOfElement = (sepEndBy pAst skipSpaces1)
  let betweenParen p = between parenOpen parenClose p
  let betweenParenAt p = between parenOpenAt parenClose p

  let pAttr = 
    let pFollowed = followedBy <| manyChars (noneOf "\"() \n\t") 
    let pLabel = manyChars asciiLetter 
    let pVal = 
      skipSpaces1 >>. chr '"' >>. 
      (manyChars (asciiLetter <|> digit <|> noneOf "\"'|*`^><}{][" <|> anyOf "()$+-*/.%:.~_" ))  
      .>> (skipSpaces1 >>. chr '"')
    skipSpaces1 .>>
    pFollowed >>. pLabel .>>. pVal

  let pAttrs = skipSpaces1 >>. sepEndBy (betweenParen pAttr) skipSpaces1 
  let pBody = skipSpaces1 >>. chr '\"' >>. manyChars (noneOf "\"") .>> chr '\"'  

  let pElement = 
      skipSpaces1 >>. (followedBy <| manyChars (noneOf "\" \t()\n")) >>.
      pipe4 (manyChars asciiLetter)
            (attempt (betweenParenAt pAttrs) <|>% [])
            (attempt pBody <|>% "")
            (pChildOfElement) 
            (fun name attrs body cdr -> cdr |> function
            | [] when body <> ""  -> Element(name, attrs, [PCData(body)])
            | [] -> Element(name, attrs, [])
            | cdr -> Element(name, attrs ,cdr)) 

  let ptop = parse {
      let! car = betweenParen pElement
      return car
  }

  do pAstRef := ptop

  [<CompiledName "Parse">]
  let parse input = runParserOnString pAst () "" input
  
  [<CompiledName "ParseFromFile">]
  let parseFromFile sxmlFile = 
    let sr = new StreamReader((sxmlFile:string), Encoding.GetEncoding("UTF-8"))
    let input = sr.ReadToEnd()
    parse input
