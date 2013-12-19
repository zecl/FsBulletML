namespace FsBulletML.Sample
open FsBulletML
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type IBullet =
  inherit IBulletmlObject
  abstract Update : unit -> unit  
  abstract TargetEnemy : IBullet with get,set
  abstract Pos : Vector2 with get,set
  abstract Radius : float32 with get,set
