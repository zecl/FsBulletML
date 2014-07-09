namespace FsBulletML.Sample.Unity2D.FSharp

open System
open System.Collections.Generic
open System.Runtime.Serialization
open UnityEngine
open FsBulletML
open FsBulletML.Unity2D 
 
type Player () =
  inherit MonoBehaviour ()
  [<DefaultValue>]val mutable public bulletObject : GameObject
  [<DefaultValue>]val mutable public bombType : GameObject
  [<DefaultValue>]val mutable public speed : float32
  [<DefaultValue>]val mutable public isBomb : bool
  [<DefaultValue>]val mutable public Damage : int

  [<DefaultValue>]val mutable private counter : int
  [<DefaultValue>]val mutable private b2wayLeftBulletTask : BulletmlTask option
  [<DefaultValue>]val mutable private b2wayRightBulletTask : BulletmlTask option
  [<DefaultValue>]val mutable private hommingTask : BulletmlTask option

  member this.Awake () =
    Processable.BulletMLManager.Init(new BulletFunctions())
    this.b2wayLeftBulletTask <- BulletRunner.convertBulletmlTaskOption(FsBulletML.Bullets.PlayerBullet.PlayerBullet.b2wayLeftBullet)
    this.b2wayRightBulletTask <- BulletRunner.convertBulletmlTaskOption(FsBulletML.Bullets.PlayerBullet.PlayerBullet.b2wayRightBullet)
    this.hommingTask <- BulletRunner.convertBulletmlTaskOption(FsBulletML.Bullets.PlayerBullet.PlayerBullet.homing)

  member this.X with get () = this.transform.position.x 
                 and set (v) = this.transform.position <- Vector3(v, this.transform.position.y, this.transform.position.z) 
  member this.Y with get () = this.transform.position.y
                 and set (v) = this.transform.position <- Vector3(this.transform.position.x, v, this.transform.position.z) 

  member this.Update () = 

    let x = Input.GetAxisRaw("Horizontal")
    let y = Input.GetAxisRaw("Vertical")

    let mx = this.X + x / 100.f * this.speed
    if (mx >= 0.4f && mx <= 4.4f) then
        this.X <- mx

    let my = this.Y + y / 100.f * this.speed
    if (my > -6.0f && my <= -0.4f) then
        this.Y <- my

    this.counter <- this.counter + 1
    if (Input.GetKey(KeyCode.Z)) then
      this.Shoot2WayLeftBullet()
      this.Shoot2WayRightBullet()
      this.ShootHomingBullet()

    if (this.counter > 60) then
        this.counter <- 0

  member private this.GetBulletPrefubInstance (position:Vector3, rotation:Quaternion) =
    InstanceManager.InstantiatePrefab(this.bulletObject, position, rotation)

  member this.Shoot2WayLeftBullet () =
    let position = this.transform.position + new Vector3(-0.1f,0.1f,0.f)
    let bullet = this.GetBulletPrefubInstance(position, this.transform.rotation)
    let b = bullet.GetComponent<PlayerBullet>()
    b.SetTask(this.b2wayLeftBulletTask)

  member this.Shoot2WayRightBullet () =
    let position = this.transform.position + new Vector3(0.1f, 0.1f, 0.f)
    let bullet = this.GetBulletPrefubInstance(position, this.transform.rotation)
    let b = bullet.GetComponent<PlayerBullet>()
    b.SetTask(this.b2wayRightBulletTask)
  
  member this.ShootHomingBullet () =
    if this.counter > 60 then
      let position = this.transform.position
      let bullet = this.GetBulletPrefubInstance(position, this.transform.rotation)
      let b = bullet.GetComponent<PlayerBullet>()
      (b :> IBullet).GetDefaultBullet().Init()
      b.SetTask(this.hommingTask)

  member this.OnTriggerEnter2D (collier:Collider2D) =
    if (this.isBomb) then Bomb.GenerateBomb(this.bombType, this.transform.position)
    this.Damage <- this.Damage + 1
