namespace FsBulletML

open System
open System.Diagnostics 
open System.IO 
open System.Text 
open System.Xml
open System.Text.RegularExpressions
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
#if NET40
open System.Xml.Resolvers
#endif

[<AutoOpen>]
module Xml =
#if NET40
  let resolver = new XmlPreloadedResolver(new XmlUrlResolver(), XmlKnownDtds.Xhtml10)
  let readerSettingsIndented dtdProcessing = new XmlReaderSettings(DtdProcessing = dtdProcessing, ValidationType = ValidationType.DTD, XmlResolver = resolver, IgnoreComments = true, IgnoreProcessingInstructions = true)
  let readerSettingsIgnoreWhitespace dtdProcessing = new XmlReaderSettings(DtdProcessing = dtdProcessing, ValidationType = ValidationType.DTD, XmlResolver = resolver, IgnoreComments = true, IgnoreProcessingInstructions = true, IgnoreWhitespace = true)
#endif
#if NET35
  let readerSettingsIndented = 
    let settings = new XmlReaderSettings(ValidationType = ValidationType.DTD, IgnoreComments = true, IgnoreProcessingInstructions = true)
    settings.ProhibitDtd <- false
    settings.XmlResolver <- null
    settings 

  let readerSettingsIgnoreWhitespace = 
    let settings = new XmlReaderSettings(ValidationType = ValidationType.DTD, IgnoreComments = true, IgnoreProcessingInstructions = true, IgnoreWhitespace = true)
    settings.ProhibitDtd <- false
    settings.XmlResolver <- null
    settings 
#endif

  let readXml xmlUri settings = 
    let rec read (reader:XmlReader) = 
      seq { 
        if reader.Read() then
          match reader.NodeType with
          | XmlNodeType.XmlDeclaration 
          | XmlNodeType.DocumentType ->
            yield! read reader
          | XmlNodeType.Element ->
            yield reader.ReadOuterXml() + reader.ReadInnerXml()
          | _ -> 
            yield! read reader
        else () }
    use reader = XmlReader.Create((xmlUri:string), settings) 
    Seq.reduce (+) (read reader) |> (fun str -> str.Replace("\n","\r\n"))

  [<Literal>]
  let docType = "bulletml"
  [<Literal>]
  let sysid = "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd"

  [<Extension>]
  type AST.XmlNode with 
    [<Extension>]
    member private this.WriteContentTo (writer:XmlWriter) =
      let rec write element =
        match element with
        | Element (name, attrs, children) -> 
          writer.WriteStartElement(name)
          for attr in attrs do
            let localName,value = attr
            writer.WriteAttributeString(localName, value)
          children |> Seq.iter (fun child -> write child)
          writer.WriteEndElement()
        | PCData s -> 
          writer.WriteString(s)
      write this

    [<Extension>]
    member internal this.GetXmlString (formatting, encdoc:EncodingAndDoctype, indentation) = 
      let output = new StringBuilder()             
      let sw (output:StringBuilder) = 
        { new StringWriter(output) with
          override w.Encoding with get () = Encoding.UTF8 }
        
      use writer = new XmlTextWriter(sw output, Formatting= formatting, Indentation = indentation )

      encdoc |> function
      | Nothing -> ()
      | Exist -> writer.WriteStartDocument()
                 writer.WriteDocType(docType, null, sysid, null)

      this.WriteContentTo(writer)
      output.ToString()

    [<Extension>]
    member this.ToString() =
      this.GetXmlString (Formatting.None, EncodingAndDoctype.Nothing, 4)

    [<Extension>]
    member this.ToXmlString(?encodingAndDoctype) = 
      let encodingAndDoctype = defaultArg encodingAndDoctype EncodingAndDoctype.Nothing
      this.GetXmlString (Formatting.None, encodingAndDoctype, 0)

    [<Extension>]
    member this.ToIndentedXmlString([<Optional; DefaultParameterValue(4)>]?indentation : int, ?encodingAndDoctype) =
      let indentation = defaultArg indentation 4
      let encodingAndDoctype = defaultArg encodingAndDoctype EncodingAndDoctype.Nothing
      this.GetXmlString (Formatting.Indented, encodingAndDoctype, indentation)


    [<Extension>]
    static member Read (xmlUri, reader:XmlReader) = 
      let readAttributes (reader : XmlReader) = 
        if reader.HasAttributes then
          [ while reader.MoveToNextAttribute() do
              yield (reader.Name, reader.Value) ]
        else []

      let rec read (reader : XmlReader)  = 
        seq {
          if reader.Read() then
            match reader.NodeType with
            | XmlNodeType.EndElement -> ()
            | XmlNodeType.Element ->
              let reader' = (reader.ReadSubtree() |> (fun reader' -> reader'.Read() |> ignore; reader'))
              yield Element (reader.Name, readAttributes reader, reader' |> read |> List.ofSeq)
              yield! read reader
            | XmlNodeType.Whitespace ->
              yield! read reader
            | XmlNodeType.Text ->
              yield PCData reader.Value
              yield! read reader
            | XmlNodeType.XmlDeclaration 
            | XmlNodeType.DocumentType ->
              yield! read reader
            | _ -> 
              yield! read reader 
          else () }
      reader |> read |> List.ofSeq |> List.head



#if NET40
    [<Extension>]
    static member ReadXmlString (xml : string, ?dtdProcessing) : AST.XmlNode = 
      let dtdProcessing = defaultArg dtdProcessing DtdProcessing.Ignore
      use reader = new System.IO.StringReader(xml)
      use reader = XmlReader.Create(reader, readerSettingsIndented dtdProcessing) 
      XmlNode.Read(xml, reader) 
#endif
#if NET35
    [<Extension>]
    static member ReadXmlString (xml : string) : AST.XmlNode = 
      use reader = new System.IO.StringReader(xml)
      use reader = XmlReader.Create(reader, readerSettingsIndented) 
      XmlNode.Read(xml, reader) 
#endif

#if NET40
    [<Extension>]
    static member ReadXml (xmlUri : string, ?dtdProcessing) : AST.XmlNode =
      let dtdProcessing = defaultArg dtdProcessing DtdProcessing.Ignore
      use reader = XmlReader.Create((xmlUri:string), readerSettingsIndented dtdProcessing) 
      XmlNode.Read(xmlUri, reader) 
#endif
#if NET35
    [<Extension>]
    static member ReadXml (xmlUri : string) : AST.XmlNode =
      use reader = XmlReader.Create((xmlUri:string), readerSettingsIndented) 
      XmlNode.Read(xmlUri, reader) 
#endif


#if NET40
    [<Extension>]
    static member ReadIndentedString (xmlUri : string, ?dtdProcessing) : string = 
      let dtdProcessing = defaultArg dtdProcessing DtdProcessing.Ignore
      (xmlUri,readerSettingsIndented dtdProcessing) ||> readXml
#endif
#if NET35
    static member ReadIndentedString (xmlUri : string) : string = 
      (xmlUri,readerSettingsIndented) ||> readXml
#endif

#if NET40
    [<Extension>]
    static member ReadIgnoreWhitespaceString (xmlUri : string, ?dtdProcessing) : string =
      let dtdProcessing = defaultArg dtdProcessing DtdProcessing.Ignore
      (xmlUri,readerSettingsIgnoreWhitespace dtdProcessing) ||> readXml
#endif
#if NET35
    static member ReadIgnoreWhitespaceString (xmlUri : string) : string =
      (xmlUri,readerSettingsIgnoreWhitespace) ||> readXml
#endif
  [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
  module Bulletml =

#if NET40
    [<CompiledName "ReadXmlString">]
    let readXmlString (xml : string, [<Optional; DefaultParameterValue(DtdProcessing.Ignore)>]dtdProcessing) : Bulletml = 
      let dtdProcessing = defaultArg dtdProcessing DtdProcessing.Ignore
      use reader = new System.IO.StringReader(xml)
      use reader = XmlReader.Create(reader, readerSettingsIndented dtdProcessing) 
      XmlNode.Read(xml, reader) |> IntermediateParser.convertBulletmlFromXmlNode
    [<CompiledName "TryReadXmlString">]
    let tryReadXmlString (xml : string, [<Optional; DefaultParameterValue(DtdProcessing.Ignore)>]dtdProcessing) : Bulletml option = 
      let dtdProcessing = defaultArg dtdProcessing DtdProcessing.Ignore
      use reader = new System.IO.StringReader(xml)
      use reader = XmlReader.Create(reader, readerSettingsIndented dtdProcessing) 
      XmlNode.Read(xml, reader) |> IntermediateParser.tryBulletmlFromXmlNode
#endif
#if NET35
    [<CompiledName "ReadXmlString">]
    let readXmlString (xml : string) : Bulletml = 
      use reader = new System.IO.StringReader(xml)
      use reader = XmlReader.Create(reader, readerSettingsIndented) 
      XmlNode.Read(xml, reader) |> IntermediateParser.convertBulletmlFromXmlNode
    [<CompiledName "TryReadXmlString">]
    let tryReadXmlString (xml : string) : Bulletml option = 
      use reader = new System.IO.StringReader(xml)
      use reader = XmlReader.Create(reader, readerSettingsIndented) 
      XmlNode.Read(xml, reader) |> IntermediateParser.tryBulletmlFromXmlNode
#endif

#if NET40
    [<CompiledName "ReadXml">]
    let readXml (xmlFile : string, [<Optional; DefaultParameterValue(DtdProcessing.Ignore)>]dtdProcessing) : Bulletml =
      let dtdProcessing = defaultArg dtdProcessing DtdProcessing.Ignore
      use reader = XmlReader.Create((xmlFile:string), readerSettingsIndented dtdProcessing) 
      XmlNode.Read(xmlFile, reader) |> IntermediateParser.convertBulletmlFromXmlNode
    [<CompiledName "TryReadXml">]
    let tryReadXml (xmlFile : string, [<Optional; DefaultParameterValue(DtdProcessing.Ignore)>]dtdProcessing) : Bulletml option =
      let dtdProcessing = defaultArg dtdProcessing DtdProcessing.Ignore
      use reader = XmlReader.Create((xmlFile:string), readerSettingsIndented dtdProcessing) 
      XmlNode.Read(xmlFile, reader) |> IntermediateParser.tryBulletmlFromXmlNode
#endif
#if NET35
    [<CompiledName "ReadXml">]
    let readXml (xmlFile : string) : Bulletml =
      use reader = XmlReader.Create((xmlFile:string), readerSettingsIndented) 
      XmlNode.Read(xmlFile, reader) |> IntermediateParser.convertBulletmlFromXmlNode
    [<CompiledName "TryReadXml">]
    let tryReadXml (xmlFile : string) : Bulletml option =
      use reader = XmlReader.Create((xmlFile:string), readerSettingsIndented) 
      XmlNode.Read(xmlFile, reader) |> IntermediateParser.tryBulletmlFromXmlNode
#endif