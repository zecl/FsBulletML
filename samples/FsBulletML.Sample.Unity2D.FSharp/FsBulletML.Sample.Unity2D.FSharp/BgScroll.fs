namespace FsBulletML.Sample.Unity2D.FSharp

open System
open UnityEngine

type BgScroll () =
  inherit MonoBehaviour ()
  [<SerializeField;DefaultValue>]val mutable public scrollSpeed : float32
  
  member this.Update () =
    let r = this.GetComponent<Renderer>()
    let newTextureOffset = new Vector2(r.material.mainTextureOffset.x , r.material.mainTextureOffset.y - Time.deltaTime * this.scrollSpeed)
    r.material.mainTextureOffset <- newTextureOffset
