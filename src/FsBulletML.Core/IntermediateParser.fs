namespace FsBulletML
open System
open FsBulletML.Processable 

module IntermediateParser =
  let internal existsAttribute attrs f = attrs |> List.exists (fun (label, v) -> if f label v then true else false)
  let internal tryFindPCData children =  children |> List.tryPick (function | PCData x -> Some x | _ -> None)

  let internal getElement children parentElementName elementName factory =
    let termXml = children |> List.tryFind (fun xml -> 
      match xml with
      | Element(name, attrs, _) -> 
        if name.ToLower() = elementName then 
          if existsAttribute attrs (fun _ _ -> true) then
            new BulletmlDTDViolationException (sprintf "this element has no attributes.:[%s]" elementName) |> raise
          true 
        else false 
      | _ -> false) 

    let pcdata = function
      | Element(_, _, children) -> tryFindPCData children 
      | _ -> None
          
    match termXml with
    | Some term ->
      match pcdata term with
      | Some text -> factory(text)
      | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise
    | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have [%s] element." parentElementName elementName) |> raise

  let internal createTerm children parentElementName = getElement children parentElementName "term" (Term)
  let internal createTimes children parentElementName = getElement children parentElementName "times" (Times)

  let internal getParam xml =
    match xml with
    | PCData x -> [] 
    | Element(_, _, children) -> 
      let rec f x = 
        match x with
        | PCData x -> x
        | Element(elementName, attrs, children) -> 
          if elementName = "param" then 
            if existsAttribute attrs (fun _ _ -> true) then
               new BulletmlDTDViolationException(sprintf "this element has no attributes.:[%s]" elementName) |> raise
            else     
              if children |> List.length = 0 then
                new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise
              f (children.[0]) 
          else 
            new BulletmlDTDViolationException(sprintf "not support element.:[%s]" elementName) |> raise
      List.map f children
   
  let internal getTextDefault children defaultText = 
    children |> List.tryPick (function | PCData x -> Some x | _ -> None)
    |> function | Some x -> x | _ -> defaultText

  let tryFindAttrValue attrs attrName = attrs |> List.tryPick (fun (label, v) -> if label = attrName && v <> "" then Some v else None)
  let internal getAttrValue attrs attrName = attrs |> List.pick (fun (label, v) -> if label = attrName && v <> "" then Some v else None)
  let internal tryFindLabelValue attrs = tryFindAttrValue attrs "label" 
  let internal getLabelValue attrs = getAttrValue attrs "label" 

  let internal toSpeedType (s:string) = 
    match s.ToLower () with
    | "absolute"  -> SpeedType.Absolute   
    | "relative"   -> SpeedType.Relative 
    | "sequence" -> SpeedType.Sequence 
    | x -> new BulletmlDTDViolationException(sprintf "not support SpeedType.:[%s]" x) |> raise 

  let internal tryFindDirection (children:XmlNode list) = 
    let f = function 
      | Element(elementName, attrs, children) -> 
        match elementName.ToLower() with
        | "direction" ->
          let attr = attrs |> List.tryPick (fun (_, x) -> if x <> "" then Some x else None)
          match attr with
          | Some attr -> 
            let toDirectionType (s:string) = 
              match s.ToLower () with
              | "aim"      -> DirectionType.Aim 
              | "absolute" -> DirectionType.Absolute  
              | "relative" -> DirectionType.Relative 
              | "sequence" -> DirectionType.Sequence 
              | x -> new BulletmlDTDViolationException(sprintf "not support DirectionType.:[%s]" x) |> raise 
            let attr = { DirectionAttrs.directionType = attr |> toDirectionType }
            match tryFindPCData children with
            | Some text -> Direction(Some attr, text) |> Some
            | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise
          | None -> 
            match tryFindPCData children with
            | Some text -> Direction(None, text) |> Some
            | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise
        | _ -> None
      | _ -> new BulletmlDTDViolationException("not support element.") |> raise
    children |> List.tryPick f

  let internal tryFindSpeed (children:XmlNode list) = 
    let f = function 
      | Element(elementName, attrs, children) -> 
        match elementName.ToLower() with
        | "speed" ->
          let attr = attrs |> List.tryPick (fun (_,x) -> if x <> "" then Some x else None)
          match attr with
          | Some attr -> 
            let attr = { SpeedAttrs.speedType = attr |> toSpeedType }
            match tryFindPCData children with
            | Some text -> Speed(Some attr, text) |> Some
            | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise
          | None -> 
            match tryFindPCData children with
            | Some text -> Speed(None, text) |> Some
            | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise
        | _ -> None
      | _ -> new BulletmlDTDViolationException("not support element.") |> raise
    children |> List.tryPick f

  let internal tryFindHorizontal (children:XmlNode list) = 
    let f = function 
      | Element(elementName, attrs, children) -> 
        match elementName.ToLower() with
        | "horizontal" ->
          let attr = attrs |> List.tryPick (fun (_,x) -> if x <> "" then Some x else None)
          match attr with
          | Some attr -> 
            let toHorizontalType (s:string) = 
              match s.ToLower () with
              | "absolute" -> HorizontalType.Absolute   
              | "relative" -> HorizontalType.Relative  
              | "sequence" -> HorizontalType.Sequence 
              | x -> new BulletmlDTDViolationException(sprintf "not support HorizontalType.:[%s]" x) |> raise 
            let attr = { HorizontalAttrs.horizontalType = attr |> toHorizontalType }
            match tryFindPCData children with
            | Some text -> 
              Horizontal(Some attr, text) |> Some
            | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise   
          | None -> 
            match tryFindPCData children with
            | Some text -> 
              Horizontal(None, text) |> Some
            | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise   
        | _ -> None
      | _ -> new BulletmlDTDViolationException("not support element.") |> raise
    children |> List.tryPick f

  let internal tryFindVertical (children:XmlNode list) = 
    let f = function 
      | Element(elementName, attrs, children) -> 
        match elementName.ToLower() with 
        | "vertical" ->
          let attr = attrs |> List.tryPick (fun (_, x) -> if x <> "" then Some x else None)
          match attr with
          | Some attr -> 
            let toVerticalType (s:string) = 
              match s.ToLower () with
              | "absolute" -> VerticalType.Absolute   
              | "relative" -> VerticalType.Relative  
              | "sequence" -> VerticalType.Sequence 
              | x -> new BulletmlDTDViolationException(sprintf "not support VerticalType.:[%s]" x) |> raise 
            let attr = { VerticalAttrs.verticalType = attr |> toVerticalType }
            match tryFindPCData children with
            | Some text -> 
              Vertical(Some attr, text) |> Some
            | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise   
          | None ->
            match tryFindPCData children with
            | Some text -> 
              Vertical(None, text) |> Some
            | None -> new BulletmlDTDViolationException(sprintf "[%s] element should have #PCDATA." elementName) |> raise   
        | _ -> None
      | _ -> new BulletmlDTDViolationException("not support element.") |> raise
    children |> List.tryPick f

  let internal getActions commands = 
    commands 
    |> List.map(fun command -> command |> function 
      | Bulletml.Action (attr, commands) -> ActionElm.Action(attr, commands) |> Some 
      | Bulletml.ActionRef (attr, commands) -> ActionElm.ActionRef(attr, commands) |> Some 
      | _ -> None)
    |> List.filter (fun x -> match x with | Some x -> true | _ -> false )
    |> List.map (fun x -> match x with | Some x -> x | _ -> new BulletmlDTDViolationException("not support action.") |> raise )

  /// XmlNode to Bulletml.Bulletml
  ///
  /// DTD :
  /// <!ELEMENT bulletml (bullet | fire | action)*>
  /// <!ATTLIST bulletml xmlns CDATA #IMPLIED>
  /// <!ATTLIST bulletml type (none|vertical|horizontal) "none">
  let internal createBulletml xml getChildren = 
    match xml with
    | Element(name, attrs, children) ->
      let tryFindBulletmlAttrs = maybe {
        let toShootingDirection (s:string) = 
          match s.ToLower () with
          | "none"       -> ShootingDirection.BulletNone 
          | "vertical"   -> ShootingDirection.BulletVertical 
          | "horizontal" -> ShootingDirection.BulletHorizontal 
          | x -> new BulletmlDTDViolationException(sprintf "not support ShootingDirection.：[%s]" x) |> raise
        let xmlns = tryFindAttrValue attrs "xmlns"
        match tryFindAttrValue attrs "type" with
        | Some shootingDirection ->
          return { bulletmlXmlns = xmlns; bulletmlType = shootingDirection |> toShootingDirection |> Some }
        | None ->
          return { bulletmlXmlns = xmlns; bulletmlType = None  }}

      match tryFindBulletmlAttrs with
      | Some attrs ->
        let commands : Bulletml list= getChildren xml children
        let bulletmlElements = 
          commands |> List.filter(function
            | Bulletml.Bullet _ -> true
            | Bulletml.Fire _ -> true
            | Bulletml.Action _ -> true
            | command -> new BulletmlDTDViolationException(sprintf "not support child element：[%s]" <| string command) |> raise)
          |> List.map (function
            | Bulletml.Bullet (a,b,c,d) -> BulletmlElm.Bullet (a,b,c,d)
            | Bulletml.Fire (a,b,c,d) -> BulletmlElm.Fire(a,b,c,d)
            | Bulletml.Action (a,b) -> BulletmlElm.Action(a,b)
            | _ -> new BulletmlDTDViolationException("convert error") |> raise)
        Bulletml.Bulletml(attrs, bulletmlElements ) 
      | None -> new BulletmlDTDViolationException("this element should have ShootingDirection attribute.") |> raise
    | _ -> new BulletmlDTDViolationException("not support element.") |> raise

  /// XmlNode to Bulletml.Action
  ///
  /// DTD :
  /// <!ELEMENT action (changeDirection | accel | vanish | changeSpeed | repeat | wait | (fire | fireRef) | (action | actionRef))*>
  /// <!ATTLIST action label CDATA #IMPLIED>
  let internal createAction factory xml getChildren = 
    match xml with
    | Element(name, attrs, children) ->
      let attrs = { actionLabel = tryFindLabelValue attrs }
      let commands = getChildren xml children

      let actionElements = 
        commands |> List.filter(function
          | Bulletml.ChangeDirection _ -> true
          | Bulletml.Accel _ -> true
          | Bulletml.Vanish _ -> true
          | Bulletml.ChangeSpeed _ -> true
          | Bulletml.Repeat _ -> true
          | Bulletml.Wait _ -> true
          | Bulletml.Fire _ -> true
          | Bulletml.FireRef _ -> true
          | Bulletml.Action _ -> true
          | Bulletml.ActionRef _ -> true
          | _ -> false)
        |> List.map (function 
          | Bulletml.ChangeDirection (a,b) -> Action.ChangeDirection (a,b)
          | Bulletml.Accel (a,b,c) -> Action.Accel(a,b,c)
          | Bulletml.Vanish -> Action.Vanish 
          | Bulletml.ChangeSpeed (a,b) -> Action.ChangeSpeed (a,b)
          | Bulletml.Repeat (a,b) -> Action.Repeat (a,b)
          | Bulletml.Wait (a) -> Action.Wait (a)
          | Bulletml.Fire (a,b,c,d) -> Action.Fire(a,b,c,d)
          | Bulletml.FireRef (a,b) -> Action.FireRef (a,b)
          | Bulletml.Action (a,b) -> Action.Action(a,b)
          | Bulletml.ActionRef (a,b) -> Action.ActionRef (a,b)
          | _ -> new BulletmlDTDViolationException("convert error") |> raise)

      factory(attrs, actionElements) 
    | _ -> new BulletmlDTDViolationException("not support element.") |> raise

  /// XmlNode to Bulletml.ActionRef
  ///
  /// DTD :
  /// <!ELEMENT actionRef (param* )>
  /// <!ATTLIST actionRef label CDATA #REQUIRED>
  let internal createActionRef factory xml = 
    match xml with
    | Element(_ , attrs, _) ->
      let tryFindActionRefAtts = maybe {
        let! label = tryFindLabelValue attrs
        return { actionRefLabel = label } }

      match tryFindActionRefAtts with
      | Some attrs -> factory(attrs, getParam xml)
      | _ -> new BulletmlDTDViolationException("ActionRef element should have label attribute.") |> raise 
    | _ -> new BulletmlDTDViolationException("not support element.") |> raise 

  let internal tryFindActionOrActionRef (children:XmlNode list) getChildren = 
    let f xml = 
      match xml with 
      | Element(name, attrs, children) -> 
        match name.ToLower() with
        | "action" -> createAction (ActionElm.Action) xml getChildren |> Some
        | "actionref" -> createActionRef (ActionElm.ActionRef) xml |> Some
        | _ -> None
      | _ -> new BulletmlDTDViolationException("not support element.") |> raise

    let result = children |> List.filter (function
                                          | Element(name, attrs, children) -> 
                                            match name.ToLower() with
                                            | "action"  | "actionref" -> true
                                            | _ -> false
                                          | _ -> false)

    if result |> List.length > 1 then
      new BulletmlDTDViolationException("repeat element cannot have multiple elements of (Action|ActionRef).") |> raise
    elif result |> List.length = 0 then
      new BulletmlDTDViolationException("repeat element should have Action or ActionRef.") |> raise
    result.[0] |> f

  /// XmlNode to Bulletml.Bullet
  ///
  /// DTD :
  /// <!ELEMENT bullet (direction?, speed?, (action | actionRef)* )>
  /// <!ATTLIST bullet label CDATA #IMPLIED>
  let internal createBullet factory xml getChildren = 
    match xml with 
    | Element(_ , attrs, children) -> 
      let text = getTextDefault children "0"
      let attr = { bulletLabel = tryFindLabelValue attrs }
      let commands = getChildren xml children

      let actions = getActions commands
      factory(attr, tryFindDirection children, tryFindSpeed children, actions)
    | _ -> new BulletmlDTDViolationException("not support element.") |> raise

  /// XmlNode to Bulletml.BulletRef
  ///
  /// DTD :
  /// <!ELEMENT bulletRef (param* )>
  /// <!ATTLIST bulletRef label CDATA #REQUIRED>
  let internal createBulletRef factory xml = 
    match xml with
    | Element(_ , attrs, _) ->
      let tryFindBulletRefAttrs = maybe {
        let! label = tryFindLabelValue attrs
        let bulletRefAttrs = { bulletRefLabel = label }
        return bulletRefAttrs
        }
      match tryFindBulletRefAttrs with
      | Some attrs ->
        factory(attrs, getParam xml)
      | None -> new BulletmlDTDViolationException("BulletRef element should have label attribute.") |> raise 
    | _ -> new BulletmlDTDViolationException("not support element.") |> raise

  let internal tryFindBulletOrBulletRef (children:XmlNode list) getChildren = 
    let f xml = 
      match xml with 
      | Element(name, attrs, children) -> 
        let text = getTextDefault children "0"
        match name.ToLower()  with
        | "bullet"    -> createBullet (BulletElm.Bullet) xml getChildren |> Some
        | "bulletref" -> createBulletRef (BulletElm.BulletRef) xml  |> Some
        | _ -> None
      | _ -> new BulletmlDTDViolationException("not support element.") |> raise
    children |> List.tryPick f

  /// XmlNode to Bulletml.Fire
  ///
  /// DTD :
  /// <!ELEMENT fire (direction?, speed?, (bullet | bulletRef))>
  /// <!ATTLIST fire label CDATA #IMPLIED>
  let internal createFire xml getChildren = 
    match xml with
    | Element(_ , attrs, children) ->
      let fireattrs = { FireAttrs.fireLabel = tryFindLabelValue attrs }
      match tryFindBulletOrBulletRef children getChildren with
      | Some bullet ->
        Bulletml.Fire(fireattrs, tryFindDirection children, tryFindSpeed children, bullet )
      | None -> new BulletmlDTDViolationException("Fire element should have Bullet or BulletRef element.") |> raise 
    | _ -> new BulletmlDTDViolationException ("not support element.") |> raise

  /// XmlNode to Bulletml.FireRef
  ///
  /// DTD :
  /// <!ELEMENT fireRef (param* )>
  /// <!ATTLIST fireRef label CDATA #REQUIRED>
  let internal createFireRef xml = 
    match xml with
    | Element(_ , attrs, _) ->
      let tryFindFireAttrs = maybe {
        let! label = tryFindLabelValue attrs
        return { fireRefLabel = label } }
      
      match tryFindFireAttrs with
      | Some attrs ->
        Bulletml.FireRef(attrs, getParam xml)
      | _ -> new BulletmlDTDViolationException("FireRef element should have label attribute.") |> raise 
    | _ -> new BulletmlDTDViolationException("not support element.") |> raise 

  /// XmlNode to Bulletml.Accel
  ///
  /// DTD :
  /// <!ELEMENT accel (horizontal?, vertical?, term)>  
  let internal createAccel = function
    | Element(elementName , attrs, children) ->
      match existsAttribute attrs (fun _ _ -> true) with
      | true ->
        new BulletmlDTDViolationException (sprintf "this element has no attributes.:[%s]" elementName) |> raise
      | false -> 
        let horizontal = tryFindHorizontal children
        let vertical = tryFindVertical children
        Bulletml.Accel(horizontal, vertical , createTerm children "accel")
    | _ -> new BulletmlDTDViolationException ("not support element.") |> raise

  /// XmlNode to ChangeSpeed
  ///
  /// DTD :
  /// <!ELEMENT changeSpeed (speed, term)>
  let internal createChangeSpeed = function
    | Element(elementName , attrs, children) ->
      match existsAttribute attrs (fun _ _ -> true) with
      | true ->
        new BulletmlDTDViolationException (sprintf "this element has no attributes.:[%s]" elementName) |> raise
      | false -> 
        let speed = 
          match tryFindSpeed children with
          | Some speed -> speed
          | None -> new BulletmlDTDViolationException(sprintf "this element should have Speed element.:[%s]" elementName) |> raise

        Bulletml.ChangeSpeed(speed, createTerm children "changeSpeed")
    | _ -> new BulletmlDTDViolationException ("not support element.") |> raise

  /// XmlNode to ChangeDirection
  ///
  /// DTD :
  /// <!ELEMENT changeDirection (direction, term)>
  let internal createChangeDirection = function
    | Element(elementName , attrs, children) ->
      match existsAttribute attrs (fun _ _ -> true) with
      | true ->
        new BulletmlDTDViolationException (sprintf "this element has no attributes.:[%s]" elementName) |> raise
      | false -> 
        let direction = 
          match tryFindDirection children with
          | Some direction -> direction
          | None -> new BulletmlDTDViolationException(sprintf "this element should have Direction element.:[%s]" elementName) |> raise
        Bulletml.ChangeDirection(direction, createTerm children "changeDirection")
    | _ -> new BulletmlDTDViolationException ("not support element.") |> raise

  /// XmlNode to Bulletml.Wait
  ///
  /// DTD :
  /// <!ELEMENT wait (#PCDATA)>
  let internal createWait = function 
    | Element(elementName, attrs, children) ->
      match existsAttribute attrs (fun _ _ -> true) with
      | true ->
        new BulletmlDTDViolationException (sprintf "this element has no attributes.:[%s]" elementName) |> raise
      | false -> 
        match tryFindPCData children with
        | Some text -> Bulletml.Wait(text)
        | None -> new BulletmlDTDViolationException (sprintf "[%s] element should have #PCDATA." elementName) |> raise
    | _ -> new BulletmlDTDViolationException ("not support element.") |> raise 

  /// XmlNode to Bulletml.Vanish
  ///
  /// DTD :
  /// <!ELEMENT vanish (#PCDATA)>
  let internal createVanish = function 
    | Element (elementName, attrs, children) -> 
      match existsAttribute attrs (fun _ _ -> true) with
      | true ->
        new BulletmlDTDViolationException (sprintf "this element has no attributes.:[%s]" elementName) |> raise
      | false -> 
        match tryFindPCData children with
        | Some text -> new BulletmlDTDViolationException (sprintf "this element cannot have #PCDATA.:[%s]" elementName) |> raise
        | None -> Bulletml.Vanish
    | _ -> new BulletmlDTDViolationException ("not support element.") |> raise 

  /// XmlNode to Bulletml.Repeat
  ///
  /// DTD :
  /// <!ELEMENT repeat (times, (action | actionRef))>
  let internal createRepeat xml getChildren = 
    match xml with
    | Element(elementName, attrs, children) ->
      match existsAttribute attrs (fun _ _ -> true) with
      | true ->
        new BulletmlDTDViolationException (sprintf "this element has no attributes.:[%s]" elementName) |> raise
      | false -> 
        let actionOrActionRef =
          match tryFindActionOrActionRef children getChildren with
          | Some actionOrActionRef -> actionOrActionRef
          | None -> new BulletmlDTDViolationException("repeat element should have Action or ActionRef.") |> raise
        Bulletml.Repeat(createTimes children "repeat", actionOrActionRef)
    | _ -> new BulletmlDTDViolationException ("not support element.") |> raise

  let internal (|Command|) command xml (getChildren:XmlNode -> XmlNode list -> Bulletml list) : Bulletml =
    match  (command:string).ToLower() with
    | "bulletml"        -> createBulletml xml getChildren
    | "action"          -> createAction (fun x -> Bulletml.Action(x)) xml getChildren
    | "actionref"       -> createActionRef (fun x -> Bulletml.ActionRef(x)) xml
    | "fire"            -> createFire xml getChildren
    | "fireref"         -> createFireRef xml
    | "changespeed"     -> createChangeSpeed xml
    | "changedirection" -> createChangeDirection xml 
    | "accel"           -> createAccel xml
    | "wait"            -> createWait xml
    | "vanish"          -> createVanish xml
    | "bullet"          -> createBullet (Bulletml.Bullet) xml getChildren
    | "bulletref"       -> createBulletRef (Bulletml.BulletRef) xml
    | "repeat"          -> createRepeat xml getChildren 
    | _ -> NotCommand

  let rec internal xmlToCommandList topXml xml = 
    let rec xmlToCommandList' topXml xml (list:Bulletml list) = 
      let getChildren topXml children = List.fold (fun tl child -> tl@xmlToCommandList topXml child) [] children 
      match xml with
      | PCData s -> list 
      | Element (name, attrs, children) -> 
        let command = name |> function Command func -> func xml getChildren 
        match command  with
        | Bulletml _  
        | Bulletml.ActionRef _ 
        | Bulletml.Repeat _ 
        | Bulletml.FireRef _ 
        | Bulletml.ChangeDirection _ 
        | Bulletml.ChangeSpeed _ 
        | Bulletml.Accel _ 
        | Bulletml.Wait _ 
        | Bulletml.Vanish 
        | Bulletml.BulletRef _ 
        | Bulletml.Action _ 
        | Bulletml.Fire _ 
        | Bulletml.Bullet _ ->
          command::list 
        | NotCommand -> list 
    xmlToCommandList' topXml xml []

  let internal xmlToBulletml topXml xml = 
    xmlToCommandList topXml xml 
    |> function
    | [] -> NotCommand
    | lst -> lst |> List.head

  [<CompiledName("ConvertBulletmlFromXmlNode")>]
  let convertBulletmlFromXmlNode xml =
    match xml with
    | PCData s -> NotCommand 
    | Element(name, _, _) ->
      if name.ToLower() = "bulletml" then
        xmlToBulletml xml xml
      else
        NotCommand 

  [<CompiledName("TryBulletmlFromXmlNode")>]
  let tryBulletmlFromXmlNode xml = 
    try
      xml |> convertBulletmlFromXmlNode |> Some
    with | ex -> None

  /// BulletElm DU to Bulletml DU
  let internal bulletElmToBulletml = function
    | BulletmlElm.Action(a,b) -> Bulletml.Action(a,b)
    | BulletmlElm.Bullet(a,b,c,d) -> Bulletml.Bullet(a,b,c,d)
    | BulletmlElm.Fire(a,b,c,d) -> Bulletml.Fire(a,b,c,d)

  /// Action DU to Bulletml DU
  let internal actionToBulletml = function
    | Action.ChangeDirection (a,b) -> Bulletml.ChangeDirection (a,b)
    | Action.Accel (a,b,c) -> Bulletml.Accel(a,b,c)
    | Action.Vanish -> Bulletml.Vanish 
    | Action.ChangeSpeed (a,b) -> Bulletml.ChangeSpeed (a,b)
    | Action.Repeat (a,b) -> Bulletml.Repeat (a,b)
    | Action.Wait (a) -> Bulletml.Wait (a)
    | Action.Fire (a,b,c,d) -> Bulletml.Fire(a,b,c,d)
    | Action.FireRef (a,b) -> Bulletml.FireRef (a,b)
    | Action.Action (a,b) -> Bulletml.Action(a,b)
    | Action.ActionRef (a,b) -> Bulletml.ActionRef (a,b)

  /// Bulletml DU to Action DU
  let internal bulletmlToAction = function
    | Bulletml.ChangeDirection (a,b) -> Action.ChangeDirection (a,b)
    | Bulletml.Accel (a,b,c) -> Action.Accel(a,b,c)
    | Bulletml.Vanish -> Action.Vanish 
    | Bulletml.ChangeSpeed (a,b) -> Action.ChangeSpeed (a,b)
    | Bulletml.Repeat (a,b) -> Action.Repeat (a,b)
    | Bulletml.Wait (a) -> Action.Wait (a)
    | Bulletml.Fire (a,b,c,d) -> Action.Fire(a,b,c,d)
    | Bulletml.FireRef (a,b) -> Action.FireRef (a,b)
    | Bulletml.Action (a,b) -> Action.Action(a,b)
    | Bulletml.ActionRef (a,b) -> Action.ActionRef (a,b)
    | _ -> new BulletmlDTDViolationException("convert error.") |> raise

  /// ActionElm DU to Bulletml DU
  let internal actionElmToBulletml = function
    | ActionElm.Action (a,b) -> Bulletml.Action (a,b) 
    | ActionElm.ActionRef (a,b) -> Bulletml.ActionRef (a,b)

  /// Bulletml to ActionElm DU
  let internal bulletmlToActionElm = function
    | Bulletml.Action (a,b) -> ActionElm.Action (a,b) 
    | Bulletml.ActionRef (a,b) -> ActionElm.ActionRef (a,b)
    | _ -> new BulletmlDTDViolationException("convert error.") |> raise

  /// BulletElm DU to Bullet DU
  let internal bulletElmToBullet = function
    | BulletElm.Bullet (a,b,c,d) -> Bulletml.Bullet (a,b,c,d)
    | BulletElm.BulletRef (a,b) -> Bulletml.BulletRef (a,b)

  /// Bulletml DU to BulletElm DU
  let internal bulletmlToBulletElm = function
    | Bulletml.Bullet (a,b,c,d) -> BulletElm.Bullet (a,b,c,d)
    | Bulletml.BulletRef (a,b) -> BulletElm.BulletRef (a,b)
    | _ -> new BulletmlDTDViolationException("convert error.") |> raise

  /// Bulletml to BulletmlElm DU
  let internal bulletmlToBulletmlElm = function
    | Bulletml.Action (a,b) -> BulletmlElm.Action (a,b)
    | Bulletml.Bullet (a,b,c,d) -> BulletmlElm.Bullet (a,b,c,d)
    | Bulletml.Fire (a,b,c,d) -> BulletmlElm.Fire (a,b,c,d)
    | _ -> new BulletmlDTDViolationException("convert error.") |> raise

  let internal convertDirectionOption  = fun prams -> function
    | Some (Direction(attrs,s)) -> Direction(attrs, Param.replace s prams) |> Some
    | None -> None

  let internal convertDirection  = fun prams -> function Direction(attrs,s) -> Direction(attrs, Param.replace s prams) 

  let internal convertSpeedOption = fun prams -> function
    | Some (Speed(attrs,s)) -> Speed(attrs, Param.replace s prams) |> Some
    | None -> None

  let internal convertSpeed = fun prams -> function Speed(attrs,s) -> Speed(attrs, Param.replace s prams) 
  let internal convertTerm = fun prams -> function Term(s) -> Term(Param.replace s prams)
  let internal convertTimes = fun prams -> function | Times(s) -> Times(Param.replace s prams)
  let internal convertParam = fun prams -> List.map (fun s -> Param.replace s prams) 
  let internal convertWait = fun prams -> function | s -> Param.replace s prams

  let internal convertHorizontalOption = fun prams -> function 
    | Some(Horizontal(attrs,s)) -> Horizontal(attrs,Param.replace s prams) |> Some
    | None -> None
  let internal convertVerticalOption = fun prams -> function 
    | Some(Vertical(attrs,s)) -> Vertical(attrs,Param.replace s prams) |> Some
    | None -> None
 
  let private convertRecBulletml' bulletml test = 
    let toStr single = if test then single |> string else (single:float32).ToString("F10")
    let rep s x (y:Lazy<'T>) = if (s:string).Contains("$") then x else y.Force()
    let repDir direction = direction |> function
      | Some d -> d |> function 
        | Direction(a,x) -> rep x direction (lazy (Some (Direction(a,TryParse.eval x |> toStr))))
      | None -> direction

    let repSpd speed = speed |> function
      | Some s -> s |> function 
        | Speed(a,x) -> rep x speed (lazy (Some (Speed(a,TryParse.eval x |> toStr))))
      | None -> speed

    let repTimes times = times |> function 
      | Times(x) -> rep x times (lazy (Times(TryParse.eval x |> toStr)))

    let repTerm term = term |> function 
      | Term(x) -> rep x term (lazy (Term(TryParse.eval x |> toStr)))

    let repHorizontal horizontal = horizontal |> function
      | Some h -> h |> function 
        | Horizontal(a,x) -> rep x horizontal (lazy (Some (Horizontal(a,TryParse.eval x |> toStr))))
      | None -> horizontal

    let repVertical vertical = vertical |> function
      | Some v -> v |> function 
        | Vertical(a,x) -> rep x vertical (lazy (Some (Vertical(a,TryParse.eval x |> toStr))))
      | None -> vertical

    let rec convert = function 
    | Bulletml.Bulletml (attrs, bulletmls) -> 
      let newbulletmls = bulletmls |> List.map (bulletElmToBulletml >> convert)
      RecBulletml.Bulletml(attrs, newbulletmls)
    | Bulletml.Bullet (attrs, direction, speed, actions) -> 
      let newActions = actions |> List.map (actionElmToBulletml >> convert) 
      RecBulletml.Bullet(attrs, repDir direction, repSpd speed, newActions)
    | Bulletml.Action (attrs, actions) -> 
      let newActions = actions |> List.map (actionToBulletml >> convert) 
      RecBulletml.Action(attrs, newActions)
    | Bulletml.Fire (attrs, direction, speed, bullet) -> 
      let newBullet = bullet |> (bulletElmToBullet >> convert)
      RecBulletml.Fire (attrs, repDir direction, repSpd speed, newBullet)
    | Bulletml.Repeat (times, action) -> 
      let newAction = action |> (actionElmToBulletml >> convert)
      RecBulletml.Repeat (repTimes times, newAction)
    | Bulletml.Accel (horizontal, vertical, term) ->
      RecBulletml.Accel(repHorizontal horizontal, repVertical vertical, repTerm term)
    | Bulletml.ActionRef (attrs, prams) ->
      RecBulletml.ActionRef (attrs, prams)
    | Bulletml.BulletRef (attrs, prams) ->
      RecBulletml.BulletRef (attrs, prams)
    | Bulletml.ChangeDirection (direction, term) ->
      let direction' = direction |> function 
        | Direction(a,x) -> rep x direction (lazy (Direction(a,TryParse.eval x |> toStr)))
      RecBulletml.ChangeDirection(direction', repTerm term)
    | Bulletml.ChangeSpeed (speed, term) ->
      let speed' = speed |> function 
        | Speed(a,x) -> rep x speed (lazy (Speed(a,TryParse.eval x |> toStr)))
      RecBulletml.ChangeSpeed (speed', repTerm term)
    | Bulletml.FireRef (attrs, prams) ->
      RecBulletml.FireRef (attrs, prams)
    | Bulletml.Vanish -> RecBulletml.Vanish 
    | Bulletml.Wait(times) -> 
      let times' = rep times times (lazy (TryParse.eval times |> toStr))
      RecBulletml.Wait(times') 
    | Bulletml.NotCommand -> RecBulletml.NotCommand 
    convert bulletml

  let internal convertRecBulletml bulletml= 
    convertRecBulletml' bulletml false

  let internal convertRecBulletmlForTest bulletml = 
    convertRecBulletml' bulletml true

  let internal getAction (recBulletml:RecBulletml) = 
    let rec xmlToCommandList2 topRecBulletml recBulletml = 
      let rec xmlToCommandList2' topRecBulletml recBulletml (list:RecBulletml list) = 
        let getChildren2 topRecBulletml children = List.fold (fun tl child -> tl@xmlToCommandList2 topRecBulletml child) [] children 
        match recBulletml with
        | RecBulletml.Bulletml (attrs, children) ->  
          list@getChildren2 recBulletml children
        | RecBulletml.Bullet (attrs, direction, speed, children) ->  
          list@getChildren2 recBulletml children
        | RecBulletml.Fire (a,b,c, child) ->  
          list@getChildren2 recBulletml [child]
        | RecBulletml.Repeat (a,child) -> 
          list@getChildren2 recBulletml [child]
        | RecBulletml.Action (attrs, children) -> 
          match attrs.actionLabel with
          | Some label ->
            list@[recBulletml]@getChildren2 recBulletml children
          | _ ->
            list@getChildren2 recBulletml children
        | RecBulletml.ActionRef _  
        | RecBulletml.FireRef _  
        | RecBulletml.ChangeSpeed _  
        | RecBulletml.ChangeDirection _  
        | RecBulletml.Accel _   
        | RecBulletml.Wait _  
        | RecBulletml.Vanish  
        | RecBulletml.BulletRef _  
        | RecBulletml.NotCommand ->
          list
      xmlToCommandList2' topRecBulletml recBulletml []
    xmlToCommandList2 recBulletml recBulletml

  let internal tryFindAction recBulletml targetLabel = 
    let actions = getAction recBulletml
    actions |> List.tryFind (function
      | RecBulletml.Action(attrs, _) ->
        match attrs.actionLabel with
        | Some v ->   
          match tryFindLabelValue [("label",v)] with
          | Some label -> if label = targetLabel then true else false
          | None -> false
        | None -> false
      | _ -> false)

  let internal getFire (recBulletml:RecBulletml) = 
    let rec xmlToCommandList2 topRecBulletml recBulletml = 
      let rec xmlToCommandList2' topRecBulletml recBulletml (list:RecBulletml list) = 
        let getChildren2 topRecBulletml children = List.fold (fun tl child -> tl@xmlToCommandList2 topRecBulletml child) [] children 
        match recBulletml with
        | RecBulletml.Bulletml (attrs, children) ->  
          list@getChildren2 recBulletml children
        | RecBulletml.Bullet (attrs, direction, speed, children) ->  
          list@getChildren2 recBulletml children
        | RecBulletml.Action (attrs, children) -> 
          list@getChildren2 recBulletml children
        | RecBulletml.Repeat (a,child) -> 
          list@getChildren2 recBulletml [child]
        | RecBulletml.Fire (attrs,b,c, child) -> 
          match attrs.fireLabel with
          | Some label ->
            list@[recBulletml]@getChildren2 recBulletml [child]
          | _ ->
            list@getChildren2 recBulletml [child]
        | RecBulletml.ActionRef _  
        | RecBulletml.FireRef _  
        | RecBulletml.ChangeSpeed _  
        | RecBulletml.ChangeDirection _  
        | RecBulletml.Accel _   
        | RecBulletml.Wait _ 
        | RecBulletml.Vanish  
        | RecBulletml.BulletRef _  
        | RecBulletml.NotCommand ->
          list
      xmlToCommandList2' topRecBulletml recBulletml []
    xmlToCommandList2 recBulletml recBulletml

  let internal tryFindFire recBulletml targetLabel = 
    let fires = getFire recBulletml
    fires |> List.tryFind (function
      | RecBulletml.Fire(attrs, _, _, _) ->
        match attrs.fireLabel with
        | Some v ->   
          match tryFindLabelValue [("label", v)] with
          | Some label -> if label = targetLabel then true else false
          | None -> false
        | None -> false
      | _ -> false)

  let internal getBullet (recBulletml:RecBulletml) = 
    let rec xmlToCommandList2 topRecBulletml recBulletml = 
      let rec xmlToCommandList2' topRecBulletml recBulletml (list:RecBulletml list) = 
        let getChildren2 topRecBulletml children = List.fold (fun tl child -> tl@xmlToCommandList2 topRecBulletml child) [] children 
        match recBulletml with
        | RecBulletml.Bulletml (attrs, children) ->  
          list@getChildren2 recBulletml children
        | RecBulletml.Action (atts, children) ->  
          list@getChildren2 recBulletml children
        | RecBulletml.Fire (a,b,c, child) ->  
          list@getChildren2 recBulletml [child]
        | RecBulletml.Repeat (a,child) -> 
          list@getChildren2 recBulletml [child]
        | RecBulletml.Bullet (attrs, direction, speed, children) -> 
          match attrs.bulletLabel with
          | Some label ->
            list@[recBulletml]@getChildren2 recBulletml children
          | _ ->
            list@getChildren2 recBulletml children
        | RecBulletml.ActionRef _   
        | RecBulletml.FireRef _  
        | RecBulletml.ChangeSpeed _  
        | RecBulletml.ChangeDirection _  
        | RecBulletml.Accel _   
        | RecBulletml.Wait _  
        | RecBulletml.Vanish  
        | RecBulletml.BulletRef _  
        | RecBulletml.NotCommand ->
          list
      xmlToCommandList2' topRecBulletml recBulletml []
    xmlToCommandList2 recBulletml recBulletml

  let internal tryFindBullet recBulletml targetLabel = 
    let bullets = getBullet recBulletml
    bullets |> List.tryFind (function
      | RecBulletml.Bullet(attrs, _, _, _) ->
        match attrs.bulletLabel with
        | Some v ->   
          match tryFindLabelValue [("label", v)] with
          | Some label -> if label = targetLabel then true else false
          | None -> false
        | None -> false
      | _ -> false)

  let internal existRandomParam recBulletml =
    let getRandomParamRef (recBulletml:RecBulletml) = 
      let rec xmlToCommandList2 topRecBulletml recBulletml = 
        let rec xmlToCommandList2' topRecBulletml recBulletml (list:RecBulletml list) = 
          let getChildren2 topRecBulletml children = List.fold (fun tl child -> tl@xmlToCommandList2 topRecBulletml child) [] children 
          let judge (param:Params) = 
            if param |> List.exists(fun x -> x.Contains("$rand")) then list@[recBulletml] else list
          match recBulletml with
          | RecBulletml.Bulletml (attrs, children) ->  
            list@getChildren2 recBulletml children
          | RecBulletml.Action (atts, children) ->  
            list@getChildren2 recBulletml children
          | RecBulletml.Fire (a,b,c, child) ->  
            list@getChildren2 recBulletml [child]
          | RecBulletml.Repeat (a,child) -> 
            list@getChildren2 recBulletml [child]
          | RecBulletml.Bullet (attrs, direction, speed, children) -> 
              list@getChildren2 recBulletml children
          | RecBulletml.ActionRef (attrs, param)  -> judge param
          | RecBulletml.FireRef (attrs, param) -> judge param
          | RecBulletml.BulletRef  (attrs, param) -> judge param
          | RecBulletml.Bulletml _ 
          | RecBulletml.Bullet _
          | RecBulletml.Fire _  
          | RecBulletml.Repeat _ 
          | RecBulletml.Action _ 
          | RecBulletml.ChangeSpeed _  
          | RecBulletml.ChangeDirection _  
          | RecBulletml.Accel _   
          | RecBulletml.Wait _  
          | RecBulletml.Vanish  
          | RecBulletml.NotCommand ->
            list
        xmlToCommandList2' topRecBulletml recBulletml []
      xmlToCommandList2 recBulletml recBulletml
    getRandomParamRef recBulletml |> List.length > 0

  let internal refBulletml (target) label prams =
    let prams = prams |> Param.ofList 
    let rec convert bulletml =
      match bulletml with
      | RecBulletml.Bullet (attrs, direction, speed, actions) -> 
        let newActions = actions |> List.map (convert)
        RecBulletml.Bullet(attrs, convertDirectionOption prams direction, convertSpeedOption prams speed, newActions)
      | RecBulletml.Action (attrs, actions) -> 
        let newActions = actions |> List.map (convert)
        RecBulletml.Action(attrs, newActions)
      | RecBulletml.ActionRef (attrs, param) ->
        RecBulletml.ActionRef (attrs, convertParam prams param)
      | RecBulletml.Fire (attrs, direction, speed, bullet) -> 
        let newBullet = bullet |> (convert)
        RecBulletml.Fire (attrs, convertDirectionOption prams direction, convertSpeedOption prams speed, newBullet)
      | RecBulletml.Repeat (times, action) -> 
        let newAction = action |> convert
        RecBulletml.Repeat (convertTimes prams times, newAction)
      | RecBulletml.FireRef (attrs, param) ->
        RecBulletml.FireRef (attrs, convertParam prams param)
      | RecBulletml.BulletRef (attrs, param) ->
        RecBulletml.BulletRef (attrs, convertParam prams param)
      | RecBulletml.ChangeSpeed (speed, term) ->
        RecBulletml.ChangeSpeed (convertSpeed prams speed, convertTerm prams term)
      | RecBulletml.ChangeDirection (direction, term) ->
        RecBulletml.ChangeDirection (convertDirection prams direction, convertTerm prams term)
      | RecBulletml.Wait (s) ->
        RecBulletml.Wait (convertWait prams s)
      | RecBulletml.Accel(horizontal, vertical, term) ->
        RecBulletml.Accel (convertHorizontalOption prams horizontal,convertVerticalOption prams vertical, convertTerm prams term)
      | x -> x 
    match target with
    | RecBulletml.Action(attrs, _) -> 
      if attrs.actionLabel = label then
        convert target 
      else
        target
    | RecBulletml.Bullet(attrs, _, _, _) -> 
      if attrs.bulletLabel = label then
        convert target 
      else
        target
    | RecBulletml.Fire(attrs, _, _, _) -> 
      if attrs.fireLabel = label then
        convert target 
      else
        target
    | _ -> new BulletmlDTDViolationException("convert error.") |> raise

  let rec internal convertBulletml recbulletml = 
    let rec convertBulletml' recbulletml = 
      let rec convert recbulletml list =  
        let getChildren children : Bulletml list = List.fold (fun tl child -> tl@convertBulletml' child) [] children 
        match recbulletml with
        | RecBulletml.Bulletml (attrs,bulletmls) -> 
          let newBulletElms = getChildren bulletmls |> List.map (bulletmlToBulletmlElm)
          Bulletml.Bulletml(attrs, newBulletElms)::list
        | RecBulletml.Action (attrs, actions) -> 
          let newActions = getChildren actions |> List.map (bulletmlToAction)
          Bulletml.Action (attrs, newActions) ::list
        | RecBulletml.Accel (horizontal, vertical, term) -> Bulletml.Accel(horizontal, vertical, term)::list
        | RecBulletml.BulletRef (attrs, prams) -> Bulletml.BulletRef (attrs, prams)::list
        | RecBulletml.FireRef (attrs, prams) ->
           Bulletml.FireRef (attrs, prams)::list
        | RecBulletml.ChangeDirection (direction, term) -> 
          Bulletml.ChangeDirection (direction, term)::list
        | RecBulletml.ChangeSpeed (speed, term) -> 
          Bulletml.ChangeSpeed (speed, term)::list
        | RecBulletml.Fire (attrs, direction, speed, bullet) -> 
          let newBullet = convertBulletml bullet |> bulletmlToBulletElm
          Bulletml.Fire(attrs, direction, speed, newBullet)::list
        | RecBulletml.Bullet (attrs, direction, speed, actions) ->
          let newActions = getChildren actions |> List.map (bulletmlToActionElm)
          Bulletml.Bullet (attrs, direction, speed, newActions)::list
        | RecBulletml.ActionRef (attrs,prams) -> Bulletml.ActionRef (attrs, prams)::list
        | RecBulletml.Repeat (times, action) ->
          let newAction = convertBulletml action |> bulletmlToActionElm
          Bulletml.Repeat(times, newAction)::list 
        | RecBulletml.Vanish -> Bulletml.Vanish::list 
        | RecBulletml.Wait (s) -> Bulletml.Wait (s)::list
        | RecBulletml.NotCommand -> Bulletml.NotCommand::list
      convert recbulletml [] 
    convertBulletml' recbulletml |> List.head 

  let rec internal convertRefBulletml topRecBulletml recBulletml = 
    let mapEval expr = List.map (fun x -> (Processable.getValue x).ToString("F10")) expr
    let rec convert recBulletml =
      match recBulletml with 
      | RecBulletml.ActionRef (attrs, prams) ->
        match tryFindAction topRecBulletml attrs.actionRefLabel with
        | Some action ->
          let newAction = refBulletml action (Some(attrs.actionRefLabel)) (mapEval prams)
          convertRefBulletml topRecBulletml newAction
        | None -> new BulletmlDTDViolationException(sprintf "not found target Action element:%s" attrs.actionRefLabel) |> raise
      | RecBulletml.FireRef (attrs, prams) ->
        match tryFindFire topRecBulletml attrs.fireRefLabel with
        | Some fire ->
          let newFire = refBulletml fire (Some(attrs.fireRefLabel))  (mapEval prams)
          convertRefBulletml topRecBulletml newFire
        | None -> new BulletmlDTDViolationException(sprintf "not found target Fire element:%s" attrs.fireRefLabel) |> raise
      | RecBulletml.BulletRef (attrs, prams) ->
        match tryFindBullet topRecBulletml attrs.bulletRefLabel with
        | Some bullet ->
          let newBullet = refBulletml bullet (Some(attrs.bulletRefLabel))  (mapEval prams)
          convertRefBulletml topRecBulletml newBullet
        | None -> new BulletmlDTDViolationException(sprintf "not foun target Bullet element:%s" attrs.bulletRefLabel) |> raise
      | RecBulletml.Bulletml (attrs,bulletmls) -> 
        let newbulletmls = bulletmls |> List.map convert
        RecBulletml.Bulletml(attrs, newbulletmls)
      | RecBulletml.Bullet (attrs, direction, speed, actions) -> 
        let newActions = actions |> List.map convert 
        RecBulletml.Bullet(attrs, direction, speed, newActions)
      | RecBulletml.Action (attrs, actions) -> 
        let newActions = actions |> List.map convert 
        RecBulletml.Action(attrs, newActions)
      | RecBulletml.Fire (attrs, direction, speed, bullet) -> 
        let newBullet = bullet |> convert
        RecBulletml.Fire (attrs, direction, speed, newBullet)
      | RecBulletml.Repeat (times, action) -> 
        let newAction = action |> convert
        RecBulletml.Repeat (times, newAction)
      | RecBulletml.Accel (horizontal, vertical, term) -> 
        RecBulletml.Accel (horizontal, vertical, term) 
      | RecBulletml.ChangeDirection _
      | RecBulletml.ChangeSpeed _
      | RecBulletml.Vanish 
      | RecBulletml.Wait _
      | RecBulletml.NotCommand -> recBulletml
    convert recBulletml

  let rec internal convertRecBulletmlEx recbulletml = 
    let rec convertBulletml' recbulletml : ProcessableBulletml list = 
      let rec convert recbulletml list : ProcessableBulletml list =  
        let getChildren children = List.fold (fun tl child -> tl@convertBulletml' child) [] children 
        match recbulletml with
        | RecBulletml.Bulletml (attrs,bulletmls) -> 
          let newBulletElms = getChildren bulletmls 
          ProcessableBulletml.Bulletml(attrs, newBulletElms)::list
        | RecBulletml.Action (attrs, actions) -> 
          let newActions = getChildren actions 
          ProcessableBulletml.Action ({ finish = false; stop = false; attribute = attrs }, newActions) ::list
        | RecBulletml.Accel (horizontal, vertical, t) -> 
          let h =
            match horizontal with
            | None ->  { horizontalType=HorizontalType.Absolute; horizontalValue = "0" }
            | Some (Horizontal(attrs,s)) -> 
              match attrs with
              | Some attrs -> 
                { horizontalType=attrs.horizontalType; horizontalValue= s }
              | None ->  { horizontalType=HorizontalType.Absolute; horizontalValue = s } 

          let v =
            match vertical with
            | None -> { verticalType=VerticalType.Absolute; verticalValue = "0" } 
            | Some (Vertical(attrs,s)) -> 
              match attrs with
              | Some attrs -> { verticalType=attrs.verticalType ; verticalValue= s }
              | None -> { verticalType=VerticalType.Absolute; verticalValue = s } 

          let term = t |> function | Term s -> s
          let processableAccel =
            { horizontal = h
              vertical = v
              initTerm = term
              term = 1.f
              first = false
              horizontalAccel = 0.f
              verticalAccel = 0.f
              finish = false
            }

          ProcessableBulletml.Accel( processableAccel )::list
        | RecBulletml.BulletRef (attrs, prams) -> ProcessableBulletml.BulletRef (attrs, prams)::list
        | RecBulletml.FireRef (attrs, prams) -> ProcessableBulletml.FireRef (attrs, prams)::list
        | RecBulletml.ChangeDirection (d, t) -> 
          let dt,dv = 
            match d with
            | Direction(attrs,s) -> 
              let v = s   
              match attrs with
              | Some attrs -> attrs.directionType, v
              | None -> DirectionType.Aim, v 
          let term = t |> function | Term s -> s 
          let pd = { direction= { directionType=dt; directionValue=dv }; initTerm=term; term=1.f; first=true; changeDir = 0.f; finish=false }
          ProcessableBulletml.ChangeDirection (pd)::list
        | RecBulletml.ChangeSpeed (s, t) -> 
          let st,sv = 
            match s with
            | Speed(attrs, v) -> 
              match attrs with
              | Some attrs -> attrs.speedType, v
              | None -> SpeedType.Absolute, v 
          let term = t |> function | Term s -> s 
          let sd = { speed= {speedType=st; speedValue=sv}; initTerm=term; term=1.f; first=true; changeSpeed = 0.f; finish=false }
          ProcessableBulletml.ChangeSpeed (sd)::list
        | RecBulletml.Fire (attrs, d, s, bullet) -> 
          ProcessableBulletml.Fire( 
            let direction = 
              match d with
              | None -> None
              | Some (Direction(attrs,s)) -> 
                match attrs with
                | Some attrs -> Some {directionType= attrs.directionType; directionValue= s} 
                | None -> Some { directionType =DirectionType.Aim; directionValue= s } 
            let speed = 
              match s with
              | None -> None
              | Some(Speed(attrs,s)) -> 
                match attrs with
                | Some attrs -> Some {speedType=attrs.speedType; speedValue= s}
                | None -> Some {speedType=SpeedType.Absolute; speedValue= s} 
            { fireLabel= attrs.fireLabel; direction= direction; speed=speed; changeSpeed = 0.f; changeDir= 0.f; finish = false}, convertRecBulletmlEx bullet)::list
        | RecBulletml.Bullet (attrs, direction, speed, actions) ->
          let newActions = getChildren actions 
          ProcessableBulletml.Bullet (attrs, direction, speed, newActions)::list
        | RecBulletml.ActionRef (attrs,prams) -> ProcessableBulletml.ActionRef (attrs, prams)::list
        | RecBulletml.Repeat (times, action) ->
          let newAction = convertRecBulletmlEx action 
          ProcessableBulletml.Repeat({ finish = false; stop = false; cont = false; repeatNum = 0; times = times }, newAction)::list 
        | RecBulletml.Vanish -> ProcessableBulletml.Vanish( { finish = false })::list 
        | RecBulletml.Wait (s) -> 
          let processableWait =
            { initTerm = s
              term = Processable.getValue s
              finish = false }
          ProcessableBulletml.Wait (processableWait)::list
        | RecBulletml.NotCommand -> ProcessableBulletml.NotCommand::list
      convert recbulletml [] 
    convertBulletml' recbulletml |> List.head 

  let rec internal convertProcessableBulletml processableBulletml = 
    let rec convertBulletml' recbulletml : RecBulletml list = 
      let rec convert recbulletml list : RecBulletml list =  
        let getChildren children = List.fold (fun tl child -> tl@convertBulletml' child) [] children 
        match recbulletml with
        | ProcessableBulletml.Bulletml (attrs,bulletmls) -> 
          RecBulletml.Bulletml(attrs, bulletmls |> List.map convertProcessableBulletml)::list
        | ProcessableBulletml.Action (pa, actions) -> 
          let pa = pa.attribute
          RecBulletml.Action (pa, actions |> List.map convertProcessableBulletml) ::list
        | ProcessableBulletml.Accel (pa) -> 
          let horizontal = Horizontal( Some { horizontalType = pa.horizontal.horizontalType }, pa.horizontal.horizontalValue) |> Some
          let vertical = Vertical( Some { verticalType = pa.vertical.verticalType }, pa.vertical.verticalValue) |> Some
          let term = Term(pa.initTerm)
          RecBulletml.Accel(horizontal,vertical,term)::list
        | ProcessableBulletml.BulletRef (attrs, prams) -> 
          RecBulletml.BulletRef (attrs, prams)::list
        | ProcessableBulletml.FireRef (attrs, prams) -> 
          RecBulletml.FireRef (attrs, prams)::list
        | ProcessableBulletml.ChangeDirection (pd) ->
          let direction = Direction( Some { directionType = pd.direction.directionType }, pd.direction.directionValue)  
          let term = Term(pd.initTerm)
          RecBulletml.ChangeDirection (direction,term)::list
        | ProcessableBulletml.ChangeSpeed (ps) -> 
          let speed = Speed( Some { speedType = ps.speed.speedType }, ps.speed.speedValue)
          let term = Term(ps.initTerm)
          RecBulletml.ChangeSpeed (speed, term)::list
        | ProcessableBulletml.Fire (pf,pb) ->
          let attrs =  { DTD.FireAttrs.fireLabel = pf.fireLabel }
          let direction = pf.direction |> Option.map (fun x -> Direction(Some { directionType = x.directionType }, x.directionValue) )
          let speed = pf.speed |> Option.map (fun x -> Speed(Some { speedType = x.speedType }, x.speedValue) )
          RecBulletml.Fire(attrs, direction, speed, pb |> convertProcessableBulletml)::list
        | ProcessableBulletml.Bullet (attrs, direction, speed, actions) ->
          RecBulletml.Bullet (attrs, direction, speed, actions |> List.map convertProcessableBulletml)::list
        | ProcessableBulletml.ActionRef (attrs, prams) -> 
          RecBulletml.ActionRef (attrs, prams)::list
        | ProcessableBulletml.Repeat (pr, pb) ->
          RecBulletml.Repeat(pr.times, pb |> convertProcessableBulletml)::list 
        | ProcessableBulletml.Vanish(pv) -> 
          RecBulletml.Vanish::list 
        | ProcessableBulletml.Wait (pw) -> 
          RecBulletml.Wait (pw.initTerm)::list
        | ProcessableBulletml.NotCommand -> 
          RecBulletml.NotCommand::list
      convert recbulletml [] 
    convertBulletml' processableBulletml |> List.head 
