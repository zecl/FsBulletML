namespace FsBulletML.Sample

open System
open System.Collections.Generic
open System.Runtime.Serialization
open Microsoft.Xna.Framework
open FsBulletML
 
type PlayerBullet () as this =
  inherit BaseBullet ()

  do 
    let self = this :> IBulletmlObject
    self.Init()
    self.IsBullet <- true
    self.BulletType <- BulletType.Player 

  member this.SetTask(bulletmlTask) =
    (this :> IBullet).Task <- bulletmlTask
