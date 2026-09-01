namespace FsBulletML.Sample.Unity2D.FSharp

open System
open System.Collections.Generic
open System.Diagnostics
open UnityEngine
open FsBulletML

/// BulletFunctions が要るのは Player という型そのものではなく、
/// 位置を返せる何か 1 つ（player.transform.position.x / .y だけ）。
/// Player 側がコンストラクタで自分（this）を渡す形にすることで、
/// BulletFunctions は Player 型を知らずに済み、循環参照が切れる。
type IPlayerPosition =
  abstract PlayerPosX : unit -> float32
  abstract PlayerPosY : unit -> float32

type BulletFunctions (player: IPlayerPosition) =
  static let rand = new System.Random()

  interface IBulletMLManager with
    member this.GetRandom() = Math.Round(rand.NextDouble() * 10000.) / 10000. |> float32
    member this.GetRank () = 0.f
    member this.GetPlayerPosX () = player.PlayerPosX()
    member this.GetPlayerPosY () = player.PlayerPosY()
