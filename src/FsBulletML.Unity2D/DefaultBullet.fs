namespace FsBulletML.Unity2D

open UnityEngine
open System
open System.Collections.Generic 
open Microsoft.FSharp.Core.Operators.Unchecked
open FsBulletML

type DefaultBullet (transform:Transform) =

  abstract member GetBulletPrefubInstance: unit -> IBulletmlObject
  default this.GetBulletPrefubInstance () = defaultof<IBulletmlObject>

  interface IDefaultBullet with 
    member val Root = false with get, set
    member this.Pos with get () = transform.position
                       and set (v) = transform.position <- v  
    member this.X with get () = transform.position.x 
                    and set (v) = transform.position <- Vector3(v, transform.position.y, transform.position.z) 
    member this.Y with get () = transform.position.y
                    and set (v) = transform.position <- Vector3(transform.position.x, v, transform.position.z) 
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
    member val TargetEnemy = defaultof<IDefaultBullet> with get, set
    member val Radius = 0.1f with get, set
    member this.Vanish () = (this :> IDefaultBullet).Used <- false

    override this.GetNewBullet() = 
      (this :> IDefaultBullet).BulletRoot <- true
      this.GetBulletPrefubInstance()

    member this.GetAimDir () : float32 =
      let self = this :> IDefaultBullet
      let dir = Math.Atan2( float (BulletMLManager.GetPlayerPosX() - self.X),float (BulletMLManager.GetPlayerPosY() - self.Y))
      float32 dir

    member this.GetEnemyAimDir() : float32 = 
      let self = this :> IDefaultBullet
      let mutable md = Single.MaxValue 
      if self.TargetEnemy :> obj <> null then
        Mathf.Atan2((self.TargetEnemy.X - self.X), 1.f * (self.TargetEnemy.Y - self.Y))
      else
        if  ((Manager.enemies) :> seq<_>) |> Seq.length <= 0 then
          0.f
        else
          for enemy in Manager.enemies do
            let d = Vector3.Distance (self.Pos, Vector3(enemy.X, enemy.Y))
            if md > d then
              self.TargetEnemy <- enemy
              md <- d
          Mathf.Atan2( (self.TargetEnemy.X - self.X), 1.f * (self.TargetEnemy.Y - self.Y))

    member this.Init () = 
      let self = this :> IDefaultBullet
      self.Root <- false
      self.Used <- true
      self.BulletRoot <- false
      self.AccelerationX <- 0.f
      self.AccelerationY <- 0.f
      self.Speed <- 0.f
      self.Dir <- 0.f
      self.IsBullet <- true

      self.Task |> function
      | None -> ()
      | Some x -> x.Init()

    member this.Update () = 
      let self = this :> IDefaultBullet
      let apply x y = 
        self.X <- self.X + (x / Settings.Display.PixcelsToUnits) 
        self.Y <- self.Y - (y / Settings.Display.PixcelsToUnits)
        let angle = -(self.Dir) * Mathf.Rad2Deg
        transform.rotation <- Quaternion.AngleAxis(angle, new Vector3(0.f, 0.f, 1.f))

      this.RunTask(FSharpFunc.ToAction2 apply)

  member this.RunTask(apply:Action<_,_>) =
    let apply = Action.toFSharpFunc2 apply
    let self = this :> IDefaultBullet
    match self.Task with
    | None -> ()
    | Some task -> 
      let result = BulletRunner.run self
      if result.Processed then 
        apply result.X result.Y
        self.Task |> Option.iter (fun x -> x.Init())
      else apply result.X result.Y
