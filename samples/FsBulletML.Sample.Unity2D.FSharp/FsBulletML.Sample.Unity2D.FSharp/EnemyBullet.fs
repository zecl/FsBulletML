namespace FsBulletML.Sample.Unity2D.FSharp

open System
open System.Collections.Generic
open System.Runtime.Serialization
open UnityEngine
open FsBulletML
open FsBulletML.Unity2D 
 
type EnemyBullet () =
  inherit BaseBullet ()

  member this.Awake () =
    base.Awake ()
    let self = this.GetDefaultBullet ()
    self.Init()
    self.Root <- true
    self.IsBullet <- true
    self.BulletRoot <- true
    self.BulletType <- BulletType.Enemy 

  override this.Update () = 
    base.Update()
    let self = this.GetDefaultBullet ()
    if (not self.Root && self.BulletRoot && not self.Used) then
      InstanceManager.Destroy(this.gameObject)

    if (this.transform.position.x < 0.f || this.transform.position.x > 4.8f) then
      self.Used <- false
      InstanceManager.Destroy(this.gameObject)

    if (this.transform.position.y < -6.4f || this.transform.position.y > 0.f) then
      self.Used <- false
      InstanceManager.Destroy(this.gameObject)

  override this.GetBulletPrefubInstance () =
    InstanceManager.InstantiatePrefab(this.bulletObject, this.transform.position, this.transform.rotation)

  member this.SetTask(bulletmlTask) = 
    let self = this.GetDefaultBullet ()
    self.Task <- bulletmlTask

  member this.OnTriggerEnter2D(collier:Collider2D) =
    if (collier.gameObject.tag = "Player") then
      if (not <| this.GetDefaultBullet().Root) then
        InstanceManager.Destroy(this.gameObject)