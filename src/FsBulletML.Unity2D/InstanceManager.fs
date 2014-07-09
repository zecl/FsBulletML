namespace FsBulletML.Unity2D
open System
open System.Linq 
open System.Collections.Generic
open UnityEngine

[<Serializable>]
type ObjectData () =
  [<DefaultValue>]val mutable public prefab : GameObject
  [<DefaultValue>]val mutable public cacheSize : int
  [<DefaultValue>]val mutable private objects : GameObject[]

  member this.Initialize () = 
    this.objects <- Array.zeroCreate this.cacheSize

    for i in 0..this.cacheSize-1 do
      let prefub = MonoBehaviour.Instantiate (this.prefab) :?> GameObject
      this.objects.[i] <- prefub
      this.objects.[i].SetActive(false)
      this.objects.[i].name <- this.objects.[i].name.Replace("(Clone)", "") + i.ToString()

  member this.GetNextObjectInCache () =
    if this.cacheSize <= 0 then
      Debug.LogError("the size of cache is required one or more.")
    this.objects |> Array.find  (fun x -> x.activeSelf |> not)

type InstanceManager () =
  inherit MonoBehaviour () 
  [<DefaultValue>]static val mutable private self : InstanceManager
  [<DefaultValue>]val mutable public caches : ObjectData[]
  [<DefaultValue>]val mutable private activeCachedObjects : Dictionary<string,bool>
    
  member this.Awake () =
    InstanceManager.self <- this
    let mutable amount = 0

    for cache in this.caches do
      cache.Initialize()
      amount <- amount + cache.cacheSize
    this.activeCachedObjects <- new Dictionary<string,bool>(amount)

  static member InstantiatePrefab(prefab:GameObject, position:Vector3, rotation:Quaternion) =
    let cache = InstanceManager.self.caches |> Seq.tryFind (fun x -> x.prefab.tag = prefab.tag)
    match cache with
    | None ->
      MonoBehaviour.Instantiate(prefab, position, rotation) :?> GameObject
    | Some cache ->
      let obj = cache.GetNextObjectInCache()
      obj.transform.position <- position
      obj.transform.rotation <- rotation
      obj.SetActive(true)
      InstanceManager.self.activeCachedObjects.[obj.name] <- true
      obj
    
  static member Destroy(objectToDestroy:GameObject) = 
    if (InstanceManager.self.activeCachedObjects.ContainsKey(objectToDestroy.name)) then
      objectToDestroy.SetActive(false);
      InstanceManager.self.activeCachedObjects.[objectToDestroy.name] <- false
    else
      GameObject.DestroyObject(objectToDestroy)