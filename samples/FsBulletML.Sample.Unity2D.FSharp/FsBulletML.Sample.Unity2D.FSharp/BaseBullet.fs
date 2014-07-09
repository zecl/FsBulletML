namespace FsBulletML.Sample.Unity2D.FSharp

open UnityEngine
open System
open System.Collections.Generic 
open FsBulletML
open FsBulletML.Unity2D 

[<AbstractClass>]
type BaseBullet () =
  inherit MonoBehaviour () 

  [<DefaultValue>]val mutable private defaultBullet : IDefaultBullet
  [<DefaultValue>]val mutable public bulletObject : GameObject

  member this.Awake () =
    let impl transform = {
      new DefaultBullet (transform) with
        override __.GetBulletPrefubInstance () = 
          let bullet = this.GetBulletPrefubInstance ()
          let self = bullet.GetComponent(typeof<IBullet>) :> obj :?> IBullet
          let bullet = self.GetDefaultBullet ()
          bullet.Init()
          bullet
    }
    this.defaultBullet <- impl this.transform 
    this.defaultBullet.Init()

  abstract member GetBulletPrefubInstance: unit -> GameObject
  default this.GetBulletPrefubInstance () = null

  member this.GetDefaultBullet () = this.defaultBullet

  interface IBullet with
     member this.GetDefaultBullet() = 
      this.defaultBullet  :> IBulletmlObject

  abstract member Update : unit -> unit
  default this.Update () = this.defaultBullet.Update()
 