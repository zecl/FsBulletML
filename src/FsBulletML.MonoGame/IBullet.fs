namespace FsBulletML.MonoGame
open FsBulletML
open Microsoft.Xna.Framework

type IBullet =
  inherit IBulletmlObject
  abstract Update : unit -> unit  
  abstract TargetEnemy : IBullet with get,set
  abstract Pos : Vector2 with get,set
  abstract Radius : float32 with get,set
