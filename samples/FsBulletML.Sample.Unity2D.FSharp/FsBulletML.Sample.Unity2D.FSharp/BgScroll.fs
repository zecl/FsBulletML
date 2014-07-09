namespace FsBulletML.Sample.Unity2D.FSharp

open System
open UnityEngine

type BgScroll () =
  inherit MonoBehaviour ()
  [<SerializeField;DefaultValue>]val mutable public scrollSpeed : float32
  
  member this.Update () =
    let newTextureOffset = new Vector2(this.renderer.material.mainTextureOffset.x , this.renderer.material.mainTextureOffset.y - Time.deltaTime * this.scrollSpeed)
    this.renderer.material.mainTextureOffset <- newTextureOffset
