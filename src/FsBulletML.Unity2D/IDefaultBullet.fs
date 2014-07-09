namespace FsBulletML.Unity2D
open FsBulletML
open UnityEngine

type IDefaultBullet =
  inherit IBulletmlObject
  abstract TargetEnemy : IDefaultBullet with get,set
  abstract Pos : Vector3 with get,set
  abstract Radius : float32 with get,set
  abstract Root : bool with get,set
  abstract member Update : unit -> unit