namespace FsBulletML.Sample

open System
open FsBulletML

type BulletFunctions () =
  static let rand = new Random() 
  interface IBulletMLManager with
    member this.GetRandom() = Math.Round(rand.NextDouble() * 10000.) / 10000. |> float32
    member this.GetRank () = 0.f 
    member this.GetPlayerPosX () = FsBulletMLSampleGame.Player.pos.X
    member this.GetPlayerPosY () = FsBulletMLSampleGame.Player.pos.Y
