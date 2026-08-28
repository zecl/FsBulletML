namespace FsBulletML.Sample.Unity2D.FSharp

open UnityEngine
open FsBulletML.Unity2D 

type ParticleSortingLayer () = 
  inherit MonoBehaviour ()

  member this.Start () = 
    let r = this.GetComponent<ParticleSystemRenderer>()
    r.sortingLayerName <- "Bomb"
    r.sortingOrder <- 2

  member this.Update () =
    let ps = this.GetComponent<ParticleSystem>()
    if (ps.IsAlive() |> not) then
      InstanceManager.Destroy(this.gameObject)
