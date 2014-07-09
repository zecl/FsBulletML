namespace FsBulletML.Sample.Unity2D.FSharp

open System
open System.Collections.Generic
open System.Diagnostics 
open UnityEngine
open FsBulletML

type BulletFunctions () =
  static let player = GameObject.Find("player")
  static let rand = new System.Random()

  interface IBulletMLManager with
    member this.GetRandom() = Math.Round(rand.NextDouble() * 10000.) / 10000. |> float32
    member this.GetRank () = 0.f 
    member this.GetPlayerPosX () = player.transform.position.x
    member this.GetPlayerPosY () = player.transform.position.y
