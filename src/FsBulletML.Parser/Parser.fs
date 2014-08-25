namespace FsBulletML

open System
open System.IO 
open System.Text 
open System.Xml
open System.Text.RegularExpressions
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open FParsec
open FsBulletML
open FsBulletML.Processable 

[<AutoOpen>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Bulletml = 

  let readXmlString (xml : string) : Bulletml = 
    use reader = new System.IO.StringReader(xml)
    use reader = XmlReader.Create(reader, readerSettingsIndented) 
    XmlNode.Read(xml, reader) |> IntermediateParser.convertBulletmlFromXmlNode

  let tryReadXmlString (xml : string) : Bulletml option = 
    use reader = new System.IO.StringReader(xml)
    use reader = XmlReader.Create(reader, readerSettingsIndented) 
    XmlNode.Read(xml, reader) |> IntermediateParser.tryBulletmlFromXmlNode

  let readXml (xmlFile : string) : Bulletml =
    use reader = XmlReader.Create((xmlFile:string), readerSettingsIndented) 
    XmlNode.Read(xmlFile, reader) |> IntermediateParser.convertBulletmlFromXmlNode

  let tryReadXml (xmlFile : string) : Bulletml option =
    use reader = XmlReader.Create((xmlFile:string), readerSettingsIndented) 
    XmlNode.Read(xmlFile, reader) |> IntermediateParser.tryBulletmlFromXmlNode

  let readSxmlString (sxml : string) : Bulletml =
    match Sxml.parse sxml with 
    | Success (r,_,_) -> r |> IntermediateParser.convertBulletmlFromXmlNode 
    | Failure (_,_,_) -> failwith "sxml parse error"

  let tryReadSxmlString (sxml : string) : Bulletml option =
    match Sxml.parse sxml with 
    | Success (r,_,_) -> r |> IntermediateParser.tryBulletmlFromXmlNode 
    | Failure (_,_,_) -> None

  let readSxml (sxmlFile : string) : Bulletml =
    match Sxml.parseFromFile sxmlFile with 
    | Success (r,_,_) -> r |> IntermediateParser.convertBulletmlFromXmlNode 
    | Failure (_,_,_) -> failwith "sxml parse error"

  let tryReadSxml (sxmlFile : string) : Bulletml option =
    match Sxml.parseFromFile sxmlFile with 
    | Success (r,_,_) -> r |> IntermediateParser.tryBulletmlFromXmlNode 
    | Failure (_,_,_) -> None

  let readFsbString (fsb: string) : Bulletml =
    match Offside.parse fsb with 
    | Success (r,_,_) -> r |> IntermediateParser.convertBulletmlFromXmlNode
    | Failure (_,_,_) -> failwith "fsb parse error"

  let tryReadFsbString (fsb: string) : Bulletml option =
    match Offside.parse fsb with 
    | Success (r,_,_) -> r |> IntermediateParser.tryBulletmlFromXmlNode
    | Failure (_,_,_) -> None

  let readFsb (fsbFile : string) : Bulletml =
    match Offside.parseFromFile fsbFile with 
    | Success (r,_,_) -> r |> IntermediateParser.convertBulletmlFromXmlNode
    | Failure (_,_,_) -> failwith "fsb parse error"

  let tryReadFsb (fsbFile : string) : Bulletml option =
    match Offside.parseFromFile fsbFile with 
    | Success (r,_,_) -> r |> IntermediateParser.tryBulletmlFromXmlNode
    | Failure (_,_,_) -> None

  type Bulletml with

    [<CompiledName "ReadXmlString">]
    static member ReadXmlString (xml : string) : Bulletml = readXmlString xml

    [<CompiledName "TryReadXmlString">]
    static member TryReadXmlString (xml : string) : Bulletml option = tryReadXmlString xml

    [<CompiledName "ReadSxmlString">]
    static member ReadSxmlString (sxml : string) : Bulletml = readSxmlString sxml

    [<CompiledName "TryReadSxmlString">]
    static member TryReadSxmlString (sxml : string) : Bulletml option = tryReadSxmlString sxml

    [<CompiledName "ReadSxml">]
    static member ReadSxml (sxmlFile : string) : Bulletml = readSxml sxmlFile

    [<CompiledName "TryReadSxml">]
    static member TryReadSxml (sxmlFile : string) : Bulletml option = tryReadSxml sxmlFile

    [<CompiledName "ReadFsbString">]
    static member ReadFsbString (fsb: string) : Bulletml = readFsbString fsb

    [<CompiledName "TryReadFsbString">]
    static member TryReadFsbString (fsb: string) : Bulletml option = tryReadFsbString fsb

    [<CompiledName "ReadFsb">]
    static member ReadFsb (fsbFile : string) : Bulletml = readFsb fsbFile

    [<CompiledName "TryReadFsb">]
    static member TryReadFsb (fsbFile : string) : Bulletml option = tryReadFsb fsbFile

    member this.ToXmlString() =
      let recBulletml = this |> IntermediateParser.convertRecBulletml 
      recBulletml.ToXmlString()

    member this.ToXmlStringForTest() =
      let recBulletml = this |> IntermediateParser.convertRecBulletmlForTest
      recBulletml.ToXmlString()

    member this.ToXmlString(?encodingAndDoctype) = 
      let recBulletml = this |> IntermediateParser.convertRecBulletml 
      let encodingAndDoctype = defaultArg encodingAndDoctype EncodingAndDoctype.Nothing
      recBulletml.ToXmlString(encodingAndDoctype)

    member this.ToXmlStringForTest(?encodingAndDoctype) = 
      let recBulletml = this |> IntermediateParser.convertRecBulletmlForTest
      let encodingAndDoctype = defaultArg encodingAndDoctype EncodingAndDoctype.Nothing
      recBulletml.ToXmlString(encodingAndDoctype)

    member this.ToIndentedXmlString([<Optional; DefaultParameterValue(4)>]?indentation : int, ?encodingAndDoctype) =
      let recBulletml = this |> IntermediateParser.convertRecBulletml 
      let indentation = defaultArg indentation 4
      let encodingAndDoctype = defaultArg encodingAndDoctype EncodingAndDoctype.Nothing
      recBulletml.ToIndentedXmlString(indentation, encodingAndDoctype)

    member this.ToIndentedXmlStringForTest([<Optional; DefaultParameterValue(4)>]?indentation : int, ?encodingAndDoctype) =
      let recBulletml = this |> IntermediateParser.convertRecBulletmlForTest
      let indentation = defaultArg indentation 4
      let encodingAndDoctype = defaultArg encodingAndDoctype EncodingAndDoctype.Nothing
      recBulletml.ToIndentedXmlString(indentation, encodingAndDoctype)
