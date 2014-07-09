namespace FsBulletML.Sample.Unity2D.FSharp

open UnityEngine
open FsBulletML.Unity2D 

type ParticleSortingLayer () = 
  inherit MonoBehaviour ()

  member this.Start () = 
    this.particleSystem.renderer.sortingLayerName <- "Bomb"
    this.particleSystem.renderer.sortingOrder <- 2

  member this.Update () =
    if (this.particleSystem.IsAlive() |> not) then
      InstanceManager.Destroy(this.gameObject)
