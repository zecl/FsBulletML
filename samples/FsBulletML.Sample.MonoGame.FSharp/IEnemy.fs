namespace FsBulletML.Sample.MonoGame.FSharp
open FsBulletML
open FsBulletML.MonoGame

type IEnemy =
  inherit IBullet
  abstract Life : int32 with get,set
  abstract Shoot : unit -> unit
