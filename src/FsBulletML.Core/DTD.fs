namespace FsBulletML

open System
open System.Diagnostics 
open System.IO 
open System.Text 
open System.Xml
open System.Text.RegularExpressions
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Microsoft.FSharp.Reflection 

[<AutoOpen>]
module DTD =
  let stringifyFullName (discriminatedUnion:'T) = 
    if box discriminatedUnion = null  then
      nullArg  "discriminatedUnion"  
    if FSharpType.IsUnion(typeof<'T>)|> not then
      invalidArg "discriminatedUnion" (sprintf "not DU:%s" typeof<'T>.FullName)
 
    let info, objects = FSharpValue.GetUnionFields(discriminatedUnion, typeof<'T>)
    let typeName = 
      if info.DeclaringType.IsGenericType then
        info.DeclaringType.Name.Substring(0, info.DeclaringType.Name.LastIndexOf("`"))  + "." + info.Name
      else
        info.DeclaringType.Name + "." + info.Name
    match objects  with
    | [||] -> typeName
    | elements ->
      let fields = info.GetFields()
      if fields.Length = 1 then
        sprintf "%s %A" typeName elements.[0]
      else
        let tupleType = 
          fields
          |> Array.map( fun pi -> pi.PropertyType )
          |> FSharpType.MakeTupleType
        let tuple = FSharpValue.MakeTuple(elements, tupleType)
        sprintf "%s %A" typeName tuple

  /// BulletML DTD
  /// <!ELEMENT vertical (#PCDATA)>
  /// <!ATTLIST vertical type (absolute|relative|sequence) "absolute">
  type Vertical =
  | Vertical of VerticalAttrs option * string 
  and VerticalAttrs = { verticalType : VerticalType }
  and [<StructuredFormatDisplay("{ToStructuredDisplay}")>]VerticalType = 
  | Absolute 
  | Relative
  | Sequence
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t 

  /// BulletML DTD
  /// <!ELEMENT param (#PCDATA)>  
  type Params = string list

  module Param =
    /// List to Params
    let ofList source =
      source |> List.mapi (fun i x -> ("$" + (i + 1).ToString(), x)) |> Map.ofList 

    let replace s param =
      let result = ref s
      let f x = 
        let x = "$" + string x
        match Map.tryFind x (param:Map<string, string>) with
        | Some v ->
          result := Regex.Replace(!result, "\\" + x , v)
        | None -> new BulletmlDTDViolationException(sprintf "not found parameter:[%s]" x) |> raise
      List.iter f [1..param.Count]
      !result

  /// BulletML DTD
  /// <!ELEMENT speed (#PCDATA)>
  /// <!ATTLIST speed type (absolute|relative|sequence) "absolute">
  type Speed =
  | Speed of SpeedAttrs option * string
  and SpeedAttrs = { speedType : SpeedType }
  and [<StructuredFormatDisplay("{ToStructuredDisplay}")>]SpeedType = 
  | Absolute 
  | Relative 
  | Sequence
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t 

  /// BulletML DTD
  /// <!ELEMENT direction (#PCDATA)>
  /// <!ATTLIST direction type (aim|absolute|relative|sequence) "aim">
  type Direction = 
  | Direction of DirectionAttrs option * string
  and DirectionAttrs = { directionType : DirectionType }
  and [<StructuredFormatDisplay("{ToStructuredDisplay}")>]DirectionType =
  | Aim | Absolute | Relative | Sequence
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t 

  /// BulletML DTD
  /// <!ELEMENT term (#PCDATA)>
  type Term = Term of string

  /// BulletML DTD
  /// <!ELEMENT times (#PCDATA)>
  type Times = Times of string

  /// BulletML DTD
  /// <!ELEMENT horizontal (#PCDATA)>
  /// <!ATTLIST horizontal type (absolute|relative|sequence) "absolute">
  type Horizontal = 
  | Horizontal of HorizontalAttrs option * string
  and HorizontalAttrs = { horizontalType : HorizontalType }
  and [<StructuredFormatDisplay("{ToStructuredDisplay}")>]HorizontalType = 
  | Absolute // Default
  | Relative
  | Sequence
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t 

  type BulletmlAttrs = { bulletmlXmlns : string option; bulletmlType : ShootingDirection option; bulletmlName : string option; bulletmlDescription : string option }
  and [<StructuredFormatDisplay("{ToStructuredDisplay}")>]ShootingDirection = 
  | BulletNone // Default 
  | BulletVertical 
  | BulletHorizontal
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t   
  
  type ActionAttrs = { actionLabel : string option }
  type ActionRefAttrs = { actionRefLabel : string }
  type FireAttrs = { fireLabel : string option }
  type FireRefAttrs = { fireRefLabel : string }
  type BulletAttrs = { bulletLabel : string option }
  type BulletRefAttrs = { bulletRefLabel : string }

  //[<DebuggerDisplay("BulletML = { this.ToXmlString() }")>]
  [<RequireQualifiedAccess>]
  type internal RecBulletml =
    internal
  /// BulletML DTD
  /// <!ELEMENT bulletml (bullet | fire | action)*>
  /// <!ATTLIST bulletml xmlns CDATA #IMPLIED>
  /// <!ATTLIST bulletml type (none|vertical|horizontal) "none">
    | Bulletml of BulletmlAttrs * RecBulletml list 
  /// BulletML DTD
  /// <!ELEMENT action (changeDirection | accel | vanish | changeSpeed | repeat | wait | (fire | fireRef) | (action | actionRef))*>
  /// <!ATTLIST action label CDATA #IMPLIED>
    | Action of ActionAttrs * RecBulletml list 
  /// BulletML DTD
  /// <!ELEMENT actionRef (param* )>
  /// <!ATTLIST actionRef label CDATA #REQUIRED>
    | ActionRef of ActionRefAttrs * Params
  /// BulletML DTD
  /// <!ELEMENT fire (direction?, speed?, (bullet | bulletRef))>
  /// <!ATTLIST fire label CDATA #IMPLIED>
    | Fire of FireAttrs * Direction option * Speed option * RecBulletml  
  /// BulletML DTD
  /// <!ELEMENT fireRef (param* )>
  /// <!ATTLIST fireRef label CDATA #REQUIRED>
    | FireRef of FireRefAttrs * Params
  /// BulletML DTD
  /// <!ELEMENT wait (#PCDATA)>
    | Wait of string
  /// BulletML DTD
  /// <!ELEMENT vanish (#PCDATA)>
    | Vanish 
  /// BulletML DTD
  /// <!ELEMENT changeSpeed (speed, term)>
    | ChangeSpeed of Speed * Term
  /// BulletML DTD
  /// <!ELEMENT changeDirection (direction, term)>
    | ChangeDirection of Direction * Term
  /// BulletML DTD
  /// <!ELEMENT accel (horizontal?, vertical?, term)>  
    | Accel of Horizontal option * Vertical option * Term
  /// BulletML DTD
  /// <!ELEMENT bullet (direction?, speed?, (action | actionRef)* )>
  /// <!ATTLIST bullet label CDATA #IMPLIED>
    | Bullet of BulletAttrs * Direction option * Speed option * RecBulletml list 
  /// BulletML DTD
  /// <!ELEMENT bulletRef (param* )>
  /// <!ATTLIST bulletRef label CDATA #REQUIRED>
    | BulletRef of BulletRefAttrs * Params
  /// BulletML DTD
  /// <!ELEMENT repeat (times, (action | actionRef))>
    | Repeat of Times * RecBulletml 
    | NotCommand

    /// BulletML 書き込み
    member private this.WriteContentTo(writer:XmlWriter) =
      let rec write element =
        match element with
        | RecBulletml.Bulletml (attrs, children) ->
          writer.WriteStartElement("bulletml")
          let localName,xmlnsName = "xmlns", attrs.bulletmlXmlns 
          xmlnsName |> function 
          | Some v -> 
            writer.WriteAttributeString(localName, v)
          | None -> ()

          let localName,typeName = "type", attrs.bulletmlType 
          match typeName with
          | Some typeName ->
            let t = 
              typeName |> function 
              | ShootingDirection.BulletNone -> "none"
              | ShootingDirection.BulletHorizontal -> "horizontal"
              | ShootingDirection.BulletVertical   -> "vertical"
            writer.WriteAttributeString(localName, t )
          | None -> ()

          children |> Seq.iter (fun child -> write child)
          writer.WriteEndElement()
        | RecBulletml.Action (attrs, children) -> 
          writer.WriteStartElement("action")
          let localName,labelName = "label", attrs.actionLabel 
          labelName |> function 
          | Some v -> 
            writer.WriteAttributeString(localName, v)
          | None -> ()
          children |> Seq.iter (fun child -> write child)
          writer.WriteEndElement()
        | RecBulletml.ActionRef (attrs, prams) ->
          writer.WriteStartElement("actionRef")
          
          let localName,labelName = "label", attrs.actionRefLabel
          writer.WriteAttributeString(localName, labelName)

          prams |> Seq.iter(fun s -> 
            writer.WriteStartElement("param")
            writer.WriteString(s)
            writer.WriteEndElement())

          writer.WriteEndElement()
        | RecBulletml.Repeat (times , child) ->
          writer.WriteStartElement("repeat")
          
          match times with
          | Times s ->
            writer.WriteStartElement("times")
            writer.WriteString(s)
            writer.WriteEndElement()

          write child
          writer.WriteEndElement()
        | RecBulletml.Fire (attrs, direction, speed, child) ->
          writer.WriteStartElement("fire")
          
          let localName,labelName = "label", attrs.fireLabel
          labelName |> function 
          | Some v -> 
            writer.WriteAttributeString(localName, v)
          | None -> ()
          direction |> function
          | Some d ->
            writer.WriteStartElement("direction")
            match d with
            | Direction (attrs, s) ->
              match attrs  with
              | Some attrs ->
                let localName, typeName = "type", attrs.directionType
                let t = 
                  typeName |> function 
                  | DirectionType.Aim      -> "aim"
                  | DirectionType.Absolute -> "absolute"
                  | DirectionType.Relative -> "relative"
                  | DirectionType.Sequence -> "sequence"
                writer.WriteAttributeString(localName, t)
              | None -> ()
              writer.WriteString(s)
            writer.WriteEndElement()
          | None -> ()

          speed |> function
          | Some d ->
            writer.WriteStartElement("speed")
            match d with
            | Speed (attrs, s) ->
              match attrs  with
              | Some attrs ->
                let localName, typeName = "type", attrs.speedType
                let t = 
                  typeName |> function 
                  | SpeedType.Absolute -> "absolute"
                  | SpeedType.Relative -> "relative"
                  | SpeedType.Sequence -> "sequence"
                writer.WriteAttributeString(localName, t)
              | None -> ()
              writer.WriteString(s)
            writer.WriteEndElement()
          | None -> ()
          write child
          writer.WriteEndElement()
        | RecBulletml.FireRef (attrs, prams) ->
          writer.WriteStartElement("fireRef")
          let localName,labelName = "label", attrs.fireRefLabel
          writer.WriteAttributeString(localName, labelName)
          prams |> Seq.iter(fun s -> 
            writer.WriteStartElement("param")
            writer.WriteString(s)
            writer.WriteEndElement())
          writer.WriteEndElement()
        | RecBulletml.Bullet (attrs, direction, speed, children) ->
          writer.WriteStartElement("bullet")
          let localName,labelName = "label", attrs.bulletLabel
          labelName |> function 
          | Some v -> 
            writer.WriteAttributeString(localName, v)
          | None -> ()
          direction |> function
          | Some d ->
            writer.WriteStartElement("direction")
            match d with
            | Direction (attrs, s) ->
              match attrs  with
              | Some attrs ->
                let localName, typeName = "type", attrs.directionType
                let t = 
                  typeName |> function 
                  | DirectionType.Aim      -> "aim"
                  | DirectionType.Absolute -> "absolute"
                  | DirectionType.Relative -> "relative"
                  | DirectionType.Sequence -> "sequence"
                writer.WriteAttributeString(localName, t)
              | None -> ()
              writer.WriteString(s)
            writer.WriteEndElement()
          | None -> ()
          speed |> function
          | Some speed ->
            writer.WriteStartElement("speed")
            match speed with
            | Speed (attrs, s) ->
              match attrs  with
              | Some attrs ->
                let localName, typeName = "type", attrs.speedType
                let t = 
                  typeName |> function 
                  | SpeedType.Absolute -> "absolute"
                  | SpeedType.Relative -> "relative"
                  | SpeedType.Sequence -> "sequence"
                writer.WriteAttributeString(localName, t)
              | None -> ()
              writer.WriteString(s)
            writer.WriteEndElement()
          | None -> ()
          children |> Seq.iter (fun child -> write child)
          writer.WriteEndElement()
        | RecBulletml.BulletRef (attrs, prams) ->
          writer.WriteStartElement("bulletRef")
          let localName,labelName = "label", attrs.bulletRefLabel
          writer.WriteAttributeString(localName, labelName)
          prams |> Seq.iter(fun s -> 
            writer.WriteStartElement("param")
            writer.WriteString(s)
            writer.WriteEndElement())
          writer.WriteEndElement()
        | RecBulletml.ChangeDirection (direction, term) ->
          writer.WriteStartElement("changeDirection")
          writer.WriteStartElement("direction")
          match direction with
          | Direction (attrs, s) ->
            match attrs  with
            | Some attrs ->
              let localName, typeName = "type", attrs.directionType
              let t = 
                typeName |> function 
                | DirectionType.Aim      -> "aim"
                | DirectionType.Absolute -> "absolute"
                | DirectionType.Relative -> "relative"
                | DirectionType.Sequence -> "sequence"
              writer.WriteAttributeString(localName, t)
            | None -> ()
            writer.WriteString(s)
          writer.WriteEndElement()
          match term with
          | Term s ->
            writer.WriteStartElement("term")
            writer.WriteString(s)
            writer.WriteEndElement()
          writer.WriteEndElement()
        | RecBulletml.ChangeSpeed (speed, term) ->
          writer.WriteStartElement("changeSpeed")
          writer.WriteStartElement("speed")
          match speed with
          | Speed (attrs, s) ->
            match attrs  with
            | Some attrs ->
              let localName, typeName = "type", attrs.speedType
              let t = 
                typeName |> function 
                | SpeedType.Absolute -> "absolute"
                | SpeedType.Relative -> "relative"
                | SpeedType.Sequence -> "sequence"
              writer.WriteAttributeString(localName, t)
            | None -> ()
            writer.WriteString(s)
          writer.WriteEndElement()
          match term with
          | Term s ->
            writer.WriteStartElement("term")
            writer.WriteString(s)
            writer.WriteEndElement()
          writer.WriteEndElement()
        | RecBulletml.Accel (horizontal, vertical, term) ->
          writer.WriteStartElement("accel")
          match horizontal with
          | Some (Horizontal.Horizontal(attrs,s)) ->
            writer.WriteStartElement("horizontal")
            match attrs with
            | Some attrs ->
              let localName,typeName = "type", attrs.horizontalType 
              let t = 
                typeName |> function 
                | HorizontalType.Absolute -> "absolute"
                | HorizontalType.Relative -> "relative"
                | HorizontalType.Sequence -> "sequence"
              writer.WriteAttributeString(localName, t)
            | None -> ()
            writer.WriteString(s)
            writer.WriteEndElement()
          | _ -> ()
          match vertical with
          | Some (Vertical.Vertical(attrs,s)) ->
            writer.WriteStartElement("vertical")
            match attrs with
            | Some attrs ->
              let localName,typeName = "type", attrs.verticalType  
              let t = 
                typeName |> function 
                | VerticalType.Absolute -> "absolute"
                | VerticalType.Relative -> "relative"
                | VerticalType.Sequence -> "sequence"
              writer.WriteAttributeString(localName, t)
            | None -> ()
            writer.WriteString(s)
            writer.WriteEndElement()
          | _ -> ()
          match term with
          | Term s ->
            writer.WriteStartElement("term")
            writer.WriteString(s)
            writer.WriteEndElement()
          writer.WriteEndElement()
        | RecBulletml.Wait (s) ->
          writer.WriteStartElement("wait")
          writer.WriteString(s)
          writer.WriteEndElement()
        | RecBulletml.Vanish ->
          writer.WriteStartElement("vanish")
          writer.WriteEndElement()
        | RecBulletml.NotCommand -> ()
      write this

    member private this.GetXmlString formatting (encdoc:EncodingAndDoctype) indentation = 
      let output = new StringBuilder()             
      let sw (output:StringBuilder) = 
        { new StringWriter(output) with
          override this.Encoding with get () = Encoding.UTF8 }

      use writer = new XmlTextWriter(sw output, Formatting=formatting, Indentation = indentation)
      encdoc |> function
      | Nothing -> ()
      | Exist -> writer.WriteStartDocument()
                 let docType = "bulletml"
                 let sysid = "http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/bulletml.dtd"
                 writer.WriteDocType(docType, null, sysid, null)

      this.WriteContentTo(writer)
      output.ToString()

    override this.ToString() =
      this.GetXmlString Formatting.None EncodingAndDoctype.Nothing 0

    member this.ToXmlString(?encodingAndDoctype) = 
      let encodingAndDoctype = defaultArg encodingAndDoctype EncodingAndDoctype.Nothing 
      this.GetXmlString Formatting.None encodingAndDoctype 0

    member this.ToIndentedXmlString([<Optional; DefaultParameterValue(4)>]?indentation : int, ?encodingAndDoctype) =
      let indentation = defaultArg indentation 4
      let encodingAndDoctype = defaultArg encodingAndDoctype EncodingAndDoctype.Nothing
      this.GetXmlString Formatting.Indented encodingAndDoctype indentation

  /// Innternal DSL
  [<StructuredFormatDisplay("{ToStructuredDisplay}")>]
  type Bulletml =
/// BulletML DTD
/// <!ELEMENT bulletml (bullet | fire | action)*>
/// <!ATTLIST bulletml xmlns CDATA #IMPLIED>
/// <!ATTLIST bulletml type (none|vertical|horizontal) "none">
  | Bulletml of BulletmlAttrs * BulletmlElm list 
/// BulletML DTD
/// <!ELEMENT action (changeDirection | accel | vanish | changeSpeed | repeat | wait | (fire | fireRef) | (action | actionRef))*>
/// <!ATTLIST action label CDATA #IMPLIED>
  | Action of ActionAttrs * Action list 
/// BulletML DTD
/// <!ELEMENT actionRef (param* )>
/// <!ATTLIST actionRef label CDATA #REQUIRED>
  | ActionRef of ActionRefAttrs * Params
/// BulletML DTD
/// <!ELEMENT fire (direction?, speed?, (bullet | bulletRef))>
/// <!ATTLIST fire label CDATA #IMPLIED>
  | Fire of FireAttrs * Direction option * Speed option * BulletElm  
/// BulletML DTD
/// <!ELEMENT fireRef (param* )>
/// <!ATTLIST fireRef label CDATA #REQUIRED>
  | FireRef of FireRefAttrs * Params
/// BulletML DTD
/// <!ELEMENT wait (#PCDATA)>
  | Wait of string
/// BulletML DTD
/// <!ELEMENT vanish (#PCDATA)>
  | Vanish 
/// BulletML DTD
/// <!ELEMENT changeSpeed (speed, term)>
  | ChangeSpeed of Speed * Term
/// BulletML DTD
/// <!ELEMENT changeDirection (direction, term)>
  | ChangeDirection of Direction * Term
/// BulletML DTD
/// <!ELEMENT accel (horizontal?, vertical?, term)>  
  | Accel of Horizontal option * Vertical option * Term
/// BulletML DTD
/// <!ELEMENT bullet (direction?, speed?, (action | actionRef)* )>
/// <!ATTLIST bullet label CDATA #IMPLIED>
  | Bullet of BulletAttrs * Direction option * Speed option * ActionElm list 
/// BulletML DTD
/// <!ELEMENT bulletRef (param* )>
/// <!ATTLIST bulletRef label CDATA #REQUIRED>
  | BulletRef of BulletRefAttrs * Params
/// BulletML DTD
/// <!ELEMENT repeat (times, (action | actionRef))>
  | Repeat of Times * ActionElm 
  | NotCommand
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t 
    member this.ToNodeString() = 
      this.ToString().Replace("null","None")
    member this.Type
        with get() = 
            match this with
            | Bulletml (x,_) -> x.bulletmlType
            | _ -> None
    member this.Name
        with get() = 
            match this with
            | Bulletml (x,_) -> x.bulletmlName   
            | _ -> None
    member this.Description
        with get() = 
            match this with
            | Bulletml (x,_) -> x.bulletmlDescription 
            | _ -> None

  and [<StructuredFormatDisplay("{ToStructuredDisplay}")>]BulletmlElm =
  | Bullet of BulletAttrs * Direction option * Speed option * ActionElm list 
  | Fire of FireAttrs * Direction option * Speed option * BulletElm 
  | Action of ActionAttrs * Action list 
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t 

  and [<StructuredFormatDisplay("{ToStructuredDisplay}")>]Action = 
  | ChangeDirection of Direction * Term
  | Accel of Horizontal option * Vertical option * Term
  | Vanish 
  | ChangeSpeed of Speed * Term
  | Repeat of Times * ActionElm 
  | Wait of string
  | Fire of FireAttrs * Direction option * Speed option * BulletElm 
  | FireRef of FireRefAttrs * Params
  | Action of ActionAttrs * Action list 
  | ActionRef of ActionRefAttrs * Params
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t 

  and [<StructuredFormatDisplay("{ToStructuredDisplay}")>]BulletElm =
  | Bullet of BulletAttrs * Direction option * Speed option * ActionElm list 
  | BulletRef of BulletRefAttrs * Params
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t 

  and [<StructuredFormatDisplay("{ToStructuredDisplay}")>]ActionElm =
  | Action of ActionAttrs * Action list 
  | ActionRef of ActionRefAttrs * Params
    member private t.ToStructuredDisplay = t.ToString()
    override t.ToString () = stringifyFullName t 
