namespace FsBulletML.Sample.Unity2D.FSharp

open System
open System.Collections.Generic
open System.Diagnostics 
open UnityEngine
open FsBulletML
open FsBulletML.Unity2D 

type Bomb =
  static member GenerateBomb(bombType:GameObject, position:Vector3) = 
    InstanceManager.InstantiatePrefab (bombType, position, Quaternion.identity) |> ignore
