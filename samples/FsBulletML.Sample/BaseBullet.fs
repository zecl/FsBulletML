namespace FsBulletML.Sample

open System
open System.Collections.Generic 
open Microsoft.Xna.Framework
open Microsoft.FSharp.Core.Operators.Unchecked
open FsBulletML

type BaseBullet () as this =
  [<DefaultValue>]val mutable pos : Vector2
  [<DefaultValue>]val mutable private self : IBullet
  
  interface IBullet with 
    member this.Pos with get () = this.pos 
                     and set (v) = this.pos <- v  
    member this.X with get () = this.pos.X 
                    and set (v) = this.pos.X <- v  
    member this.Y with get () = this.pos.Y
                    and set (v) = this.pos.Y <- v  
    member val AccelerationX = 0.f with get, set
    member val AccelerationY = 0.f with get, set
    member val Speed = 0.f with get, set
    member val Dir = 0.f with get, set
    member val Used = false with get, set
    member val IsBullet = false with get, set
    member val BulletRoot = false with get, set
    member val BulletType = BulletType.Enemy with get, set
    member val ShootingDirection = ShootingDirection.BulletHorizontal with get, set
    member val Task = None with get, set
    member val TargetEnemy = defaultof<IBullet> with get, set
    member val Radius = 0.f with get, set
    member this.Vanish () = this.self.Used <- false
    member this.GetNewBullet() = 
      this.self.BulletRoot <- true

      let newBullet = new BaseBullet() :> IBullet
      newBullet.IsBullet <- true
      newBullet.Radius <- 4.5f
      newBullet.BulletType <- this.self.BulletType
      match newBullet.BulletType with
      | Player -> Manager.addPlayerBullet(newBullet)
      | Enemy -> Manager.addEnemyBullet(newBullet)
      newBullet :> IBulletmlObject

    member this.GetAimDir () : float32 =
      let dir = Math.Atan2( float (BulletMLManager.GetPlayerPosX() - this.self.X),float -(BulletMLManager.GetPlayerPosY() - this.self.Y))
      float32 dir

    member this.GetEnemyAimDir() : float32 = 
      let mutable md = Single.MaxValue 
      if this.self.TargetEnemy :> obj <> null then
        let dir = Math.Atan2( float (this.self.TargetEnemy.X - this.self.X), -1. * float (this.self.TargetEnemy.Y - this.self.Y))
        float32 dir
      else
        for enemy in Manager.enemys do
          let d = Vector2.Distance (Vector2(this.self.X, this.self.Y), Vector2(enemy.X, enemy.Y))
          if md > d then
            this.self.TargetEnemy <- enemy
            md <- d
        let dir = Math.Atan2( float (this.self.TargetEnemy.X - this.self.X), -1. * float (this.self.TargetEnemy.Y - this.self.Y))
        float32 dir

    member this.Init () = 
      this.self.Used <- true
      this.self.BulletRoot <- false

    member this.Update () = this.RunTask()

  member this.RunTask() =
    let apply x y = this.self.X <- x; this.self.Y <- y

    match this.self.Task with
    | None -> ()
    | Some task -> 
      match BulletRunner.run this with
      | true,(x,y) -> 
        apply x y
        this.self.Task |> Option.iter (fun x -> x.Init())
      | false,(x,y) -> apply x y

    if (this.pos.X < 0.f || this.pos.X > Settings.Display.Width || this.pos.Y < 0.f || this.pos.Y > Settings.Display.Height) then
      this.self.Used <- false

  do
    this.self <- this :> IBullet
    this.self.Init()
