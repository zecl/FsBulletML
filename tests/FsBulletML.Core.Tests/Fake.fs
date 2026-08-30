namespace FsBulletML.Core.Tests

open System.Collections.Generic
open FsBulletML
open FsBulletML.DTD
open FsBulletML.Processable

/// $rand / $rank / 自機位置を固定する。BulletMLManager は static mutable なので
/// fixture ごとに Init し直すこと。
type FixedManager(rand: float32, rank: float32, playerX: float32, playerY: float32) =
  interface IBulletMLManager with
    member _.GetRandom() = rand
    member _.GetRank() = rank
    member _.GetPlayerPosX() = playerX
    member _.GetPlayerPosY() = playerY

/// 記録するだけの弾。BulletRunner.run の相手。
///
/// 面の作り方は MonoGame の BaseBullet を写した。とくに次の 3 つは
/// run の分岐に効くので、勝手に簡単にしない。
///   Init()         Used <- true / BulletRoot <- false
///   GetNewBullet() 親の BulletRoot <- true、子の IsBullet <- true
///   Vanish()       Used <- false
type FakeBullet(id: int, born: List<FakeBullet>) =
  let mutable accelerationX = 0.0f
  let mutable accelerationY = 0.0f
  let mutable x = 0.0f
  let mutable y = 0.0f
  let mutable speed = 0.0f
  let mutable dir = 0.0f
  let mutable task : BulletmlTask option = None
  let mutable bulletType = BulletType.Enemy
  let mutable shootingDirection = ShootingDirection.BulletVertical
  let mutable used = false
  let mutable isBullet = false
  let mutable bulletRoot = false
  let mutable vanishCount = 0

  member _.Id = id
  member _.VanishCount = vanishCount
  /// 根の弾から数えて産まれた順。親子は問わず 1 本の並びに積む
  member _.Born = born

  interface IBulletmlObject with
    member _.AccelerationX with get () = accelerationX and set v = accelerationX <- v
    member _.AccelerationY with get () = accelerationY and set v = accelerationY <- v
    member _.X with get () = x and set v = x <- v
    member _.Y with get () = y and set v = y <- v
    member _.Speed with get () = speed and set v = speed <- v
    member _.Dir with get () = dir and set v = dir <- v

    member _.Vanish() =
      vanishCount <- vanishCount + 1
      used <- false

    member _.GetNewBullet() =
      bulletRoot <- true
      let child = FakeBullet(born.Count + 1, born)
      born.Add child
      let c = child :> IBulletmlObject
      c.IsBullet <- true
      c.BulletType <- bulletType
      c

    /// 自機の向き。実機では atan2 で毎フレーム変わるが、ここは固定して決定的にする
    member _.GetAimDir() = 0.0f
    member _.GetEnemyAimDir() = 0.0f

    member _.Init() =
      used <- true
      bulletRoot <- false

    member _.Task with get () = task and set v = task <- v
    member _.BulletType with get () = bulletType and set v = bulletType <- v
    member _.ShootingDirection with get () = shootingDirection and set v = shootingDirection <- v
    member _.Used with get () = used and set v = used <- v
    member _.IsBullet with get () = isBullet and set v = isBullet <- v
    member _.BulletRoot with get () = bulletRoot and set v = bulletRoot <- v
