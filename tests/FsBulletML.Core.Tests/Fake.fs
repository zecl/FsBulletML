namespace FsBulletML.Core.Tests

open System
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

/// 走行の途中で $rand / $rank を動かせる manager。
///
/// 焼き付けの有無は、値が動かないと割れない。FixedManager では
/// 「毎回読み直している」と「最初の 1 回を持ち回っている」が同じ控えになる。
/// 呼ばれた回数も持つ（getValue は式の中身に関わらず両方を毎回呼ぶ）。
type MutableManager(rand: float32, rank: float32, playerX: float32, playerY: float32) =
  let mutable r = rand
  let mutable k = rank
  let mutable randCalls = 0
  let mutable rankCalls = 0
  member _.Rand with get () = r and set v = r <- v
  member _.Rank with get () = k and set v = k <- v
  member _.RandCalls = randCalls
  member _.RankCalls = rankCalls
  interface IBulletMLManager with
    member _.GetRandom() =
      randCalls <- randCalls + 1
      r
    member _.GetRank() =
      rankCalls <- rankCalls + 1
      k
    member _.GetPlayerPosX() = playerX
    member _.GetPlayerPosY() = playerY

/// GetEnemyAimDir が狙う相手の位置。自機（FixedManager が持つ）と区別できる場所に置く。
/// 原点に置くと根の弾も原点なので atan2(0, -0) = π になり、
/// ライブラリの値ではなく偽の弾の副作用が控えに出る。
module FakeEnemy =
  let X = -40.0f
  let Y = -60.0f

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

    /// 自機の向き。式は BaseBullet.GetAimDir を写した。
    /// 自機の位置は FixedManager が固定して返すので、これでも決定的になる。
    member _.GetAimDir() =
      float32 (Math.Atan2(float (BulletMLManager.GetPlayerPosX() - x),
                          float -(BulletMLManager.GetPlayerPosY() - y)))

    /// 敵を狙う向き。本番は最寄りの敵を探すが、ここに敵の一覧は無いので
    /// 1 体だけ居るものとして同じ式で出す。
    ///
    /// 敵を原点に置くと、根の弾も原点なので atan2(0, -0) = π になり、
    /// 偽の弾の副作用が値に出るので、自機と区別できる位置へずらしてある。
    member _.GetEnemyAimDir() =
      float32 (Math.Atan2(float (FakeEnemy.X - x), -1.0 * float (FakeEnemy.Y - y)))

    member _.Init() =
      used <- true
      bulletRoot <- false

    member _.Task with get () = task and set v = task <- v
    member _.BulletType with get () = bulletType and set v = bulletType <- v
    member _.ShootingDirection with get () = shootingDirection and set v = shootingDirection <- v
    member _.Used with get () = used and set v = used <- v
    member _.IsBullet with get () = isBullet and set v = isBullet <- v
    member _.BulletRoot with get () = bulletRoot and set v = bulletRoot <- v
