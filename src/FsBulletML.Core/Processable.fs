namespace FsBulletML

open System
open System.Diagnostics 
open System.IO 
open System.Text 
open System.Xml
open System.Text.RegularExpressions

[<AutoOpen>]
module ProcessableAttr = 
  type DirectionNode =
    {directionType: DirectionType
     directionValue: string }

  type ProcessableDirection = 
    { direction : DirectionNode
      initTerm : string
      mutable term : float32
      mutable first : bool 
      mutable changeDir : float32 
      mutable finish : bool}  

  type SpeedNode =
    {speedType: SpeedType
     speedValue: string }

  type ProcessableSpeed = 
    { speed : SpeedNode
      initTerm : string
      mutable term : float32  
      mutable first : bool 
      mutable changeSpeed : float32 
      mutable finish : bool}  

  type ProcessableFire = 
    { fireLabel : string option
      direction : DirectionNode option
      speed : SpeedNode option
      mutable changeDir : float32
      mutable changeSpeed : float32 
      mutable finish : bool }

  type HorizontalNode =
    { horizontalType : HorizontalType
      horizontalValue : string
    }

  type VerticalNode =
    { verticalType : VerticalType
      verticalValue : string
    }

  type ProcessableAccel =
    { horizontal : HorizontalNode
      vertical : VerticalNode
      initTerm : string
      mutable term : float32  
      mutable first : bool
      mutable horizontalAccel : float32
      mutable verticalAccel : float32
      mutable finish : bool }

  type ProcessableWait = 
    { initTerm : string
      mutable term : float32 
      mutable finish : bool }

  type ProcessableVanish = 
    { mutable finish : bool }

  type ProcessableAction = 
    { mutable finish : bool 
      mutable stop : bool
      attribute : ActionAttrs }

  type ProcessableRepeat = 
    { mutable finish : bool
      mutable stop : bool
      mutable cont : bool
      mutable repeatNum : int
      times : Times }

[<AutoOpen>]
module Processable = 

  type BulletType =
    | Enemy
    | Player

  type RunState =
    | Continue 
    | End 
    | Stop 

  open Microsoft.FSharp.Core.Operators.Unchecked 
  
  type IBulletMLManager = 
    abstract GetRandom : unit -> float32
    abstract GetRank : unit -> float32
    abstract GetPlayerPosX : unit -> float32
    abstract GetPlayerPosY : unit -> float32

  type BulletMLManager ()=
    static let mutable ib : IBulletMLManager = defaultof<IBulletMLManager>
    static member Init(ib1:IBulletMLManager) =
      ib <- ib1
    static member GetRandom() =  ib.GetRandom() 
    static member GetRank() = ib.GetRank()
    static member GetPlayerPosX() = ib.GetPlayerPosX()
    static member GetPlayerPosY() = ib.GetPlayerPosY()

  let getValue (s:string) = 
    let rand = BulletMLManager.GetRandom()
    let rank = BulletMLManager.GetRank()
    let s = s.Replace("$rand", rand.ToString())
             .Replace("$rank", rank.ToString())
    let s = System.Text.RegularExpressions.Regex.Replace(s,"\$d*","0")
    TryParse.eval s

  /// FireData
  type FireData () = 
    [<DefaultValue>]val mutable srcSpeed : float32
    member this.SrcSpeed with get () = this.srcSpeed
                          and set (v) = this.srcSpeed <- v 
    [<DefaultValue>]val mutable srcDir : float32
    member this.SrcDir with get () = this.srcDir
                        and set (v) = this.srcDir <- v 
    [<DefaultValue>]val mutable speedInit : bool
    member this.SpeedInit with get () = this.speedInit
                           and set (v) = this.speedInit <- v 

  type IBulletmlObject =
    abstract AccelerationX : float32 with get, set
    abstract AccelerationY : float32 with get, set
    abstract X : float32 with get,set
    abstract Y : float32 with get,set
    abstract Speed : float32 with get,set
    abstract Dir : float32 with get,set
    abstract Vanish : unit -> unit  
    abstract GetNewBullet : unit -> IBulletmlObject
    abstract GetAimDir : unit -> float32
    abstract GetEnemyAimDir : unit -> float32
    abstract Init : unit -> unit
    abstract Task : BulletmlTask option with get,set
    abstract BulletType : BulletType with get,set
    abstract ShootingDirection : ShootingDirection with get,set
    abstract Used : bool with get,set
    abstract IsBullet : bool with get,set
    abstract BulletRoot : bool with get,set

  and [<RequireQualifiedAccess>] ProcessableBulletml =
  /// BulletML DTD
  /// <!ELEMENT bulletml (bullet | fire | action)*>
  /// <!ATTLIST bulletml xmlns CDATA #IMPLIED>
  /// <!ATTLIST bulletml type (none|vertical|horizontal) "none">
    | Bulletml of BulletmlAttrs * ProcessableBulletml list 
  /// BulletML DTD
  /// <!ELEMENT action (changeDirection | accel | vanish | changeSpeed | repeat | wait | (fire | fireRef) | (action | actionRef))*>
  /// <!ATTLIST action label CDATA #IMPLIED>
    | Action of ProcessableAction * ProcessableBulletml list 
  /// BulletML DTD
  /// <!ELEMENT actionRef (param* )>
  /// <!ATTLIST actionRef label CDATA #REQUIRED>
    | ActionRef of ActionRefAttrs * Params
  /// BulletML DTD
  /// <!ELEMENT fire (direction?, speed?, (bullet | bulletRef))>
  /// <!ATTLIST fire label CDATA #IMPLIED>
    | Fire of ProcessableFire * ProcessableBulletml  
  /// BulletML DTD
  /// <!ELEMENT fireRef (param* )>
  /// <!ATTLIST fireRef label CDATA #REQUIRED>
    | FireRef of FireRefAttrs * Params
  /// BulletML DTD
  /// <!ELEMENT wait (#PCDATA)>
    | Wait of ProcessableWait
  /// BulletML DTD
  /// <!ELEMENT vanish (#PCDATA)>
    | Vanish of ProcessableVanish
  /// BulletML DTD
  /// <!ELEMENT changeSpeed (speed, term)>
    | ChangeSpeed of ProcessableSpeed
  /// BulletML DTD
  /// <!ELEMENT changeDirection (direction, term)>
    | ChangeDirection of ProcessableDirection
  /// BulletML DTD
  /// <!ELEMENT accel (horizontal?, vertical?, term)>  
    | Accel of ProcessableAccel //Horizontal option * Vertical option * Term
  /// BulletML DTD
  /// <!ELEMENT bullet (direction?, speed?, (action | actionRef)* )>
  /// <!ATTLIST bullet label CDATA #IMPLIED>
    | Bullet of BulletAttrs * Direction option * Speed option * ProcessableBulletml list 
  /// BulletML DTD
  /// <!ELEMENT bulletRef (param* )>
  /// <!ATTLIST bulletRef label CDATA #REQUIRED>
    | BulletRef of BulletRefAttrs * Params
  /// BulletML DTD
  /// <!ELEMENT repeat (times, (action | actionRef))>
    | Repeat of ProcessableRepeat * ProcessableBulletml 
    | NotCommand

  /// 各初期化処理
    with member this.Init() = this |> function
          | ProcessableBulletml.Action(pa,children) ->
            pa.finish <- false
            pa.stop <- false
            children |> Seq.iter (fun t -> t.Init()) 
          | ProcessableBulletml.Accel (pa) ->
            pa.first <- true
            pa.finish <- false
          | ProcessableBulletml.Fire(pf, children) ->  
            pf.finish <- false
            children.Init()
          | ProcessableBulletml.ChangeDirection (pd) -> 
            pd.first <- true
            pd.finish <- false
            pd.term <- getValue pd.initTerm
          | ProcessableBulletml.ChangeSpeed (ps) ->
            ps.first <- true
            ps.finish <- false
            ps.term <- getValue ps.initTerm 
          | ProcessableBulletml.Wait (pw) ->
            pw.finish <- false
            pw.term <- getValue pw.initTerm + 1.f
          | ProcessableBulletml.Vanish (pv) -> 
            pv.finish <- false
          | ProcessableBulletml.Repeat(pr,actionElm) ->
            pr.finish <- false
            pr.stop <- false
            pr.cont <- false
            pr.repeatNum <- 0

            match actionElm with
            | ProcessableBulletml.Action(pa,children) ->
              pa.finish <- false
              pa.stop <- false
              actionElm.Init()
            | _ -> ()
          | ProcessableBulletml.Bullet (attrs,direction, speed,actions) ->
            actions |> Seq.iter (fun t -> t.Init())
          | _ -> ()

  and BulletmlTask internal (toProcessable) as this =
    [<DefaultValue>]val mutable private fireData : System.Collections.Generic.List<FireData>
    [<DefaultValue>]val mutable private activeTaskIndex : int
    [<DefaultValue>]val mutable private tasks : ProcessableBulletml list
    [<DefaultValue>]val mutable private finish : bool
    [<DefaultValue>]val mutable private shootingDirection : ShootingDirection
    [<DefaultValue>]val mutable private original : Bulletml option

    member internal this.FireData with get () = this.fireData 
                                   and set (v) = this.fireData <- v
    member internal this.ShootingDirection with get () = this.shootingDirection 
                                            and set (v) = this.shootingDirection <- v
    member internal this.ActiveTaskIndex with get () = this.activeTaskIndex 
                                          and set (v) = this.activeTaskIndex <- v
    member internal  this.Tasks with get () = this.tasks 
                                 and set (v) = this.tasks <- v
    member this.Finish with get () = this.finish 
                        and set (v) = this.finish <- v
    member this.Original with get () = this.original 
                          and set (v) = this.original <- v
    member this.GetFireData () = this.fireData.[this.activeTaskIndex]
    do
      this.finish <- false

    member this.Init () = this.Original |> function
      | Some x -> this.Tasks <- toProcessable x
      | None -> this.tasks |> Seq.iter (fun p -> p.Init())