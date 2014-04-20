namespace FsBulletML.MonoGame

open System
open System.Collections.Generic
open System.Runtime.Serialization
open Microsoft.Xna.Framework
open FsBulletML
 
type EnemyBullet () as this =
  inherit BaseBullet () 

  do 
    let self = this :> IBulletmlObject
    self.Init()
    self.IsBullet <- true
    self.BulletType <- BulletType.Enemy 
    
  member this.SetTask(bulletmlTask) = 
    (this :> IBullet).Task <- bulletmlTask
