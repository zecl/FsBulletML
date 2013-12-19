namespace FsBulletML.Sample
open FsBulletML

type IEnemy =
  inherit IBullet
  abstract Life : int32 with get,set
  abstract Shoot : unit -> unit
