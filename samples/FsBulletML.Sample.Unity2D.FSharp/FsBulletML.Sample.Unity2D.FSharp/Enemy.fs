namespace FsBulletML.Sample.Unity2D.FSharp

open System
open System.Collections.Generic
open System.Runtime.Serialization
open UnityEngine
open FsBulletML
open FsBulletML.Bullets 
open FsBulletML.Unity2D 
 
type Enemy () =
  inherit BaseBullet ()
  [<DefaultValue>]val mutable public bullets : BulletmlInfo list
  [<DefaultValue>]val mutable public bombType : GameObject
  [<DefaultValue>]val mutable public BulletName : string
  [<DefaultValue>]val mutable public BulletmlInfo : BulletmlInfo 
  [<DefaultValue>]val mutable public Bullet : EnemyBullet 
  [<DefaultValue>]val mutable public Life : int
  [<DefaultValue>]val mutable public MaxLife : int
  [<DefaultValue>]val mutable public isBomb : bool
  [<DefaultValue>]val mutable private BulletIndex : int
  [<DefaultValue>]val mutable private Second : bool

  member this.Awake () =
    base.Awake ()
    Manager.addEnemy(this.GetDefaultBullet())

    let self = this.GetDefaultBullet ()
    self.Init()
    self.IsBullet <- false
    self.BulletType <- BulletType.Enemy 
    this.BulletName <- ""

  member this.Start () = 
    this.bullets <- this.GetBulletml() |> Seq.toList  
    this.SetBulletmlInfo()

  member this.OnTriggerEnter2D(collier:Collider2D) =
    if (this.isBomb) then Bomb.GenerateBomb(this.bombType, this.transform.position)
    this.Life <- this.Life - 1
    if (this.Life <= 0) then
      this.Next()

  override this.Update () = 
    if (Input.GetKeyDown(KeyCode.Return)) then
        this.Next()

    if (not this.Second || this.IsFinish()) then
        this.Second <- true
        this.Shoot()

    base.Update()

  override this.GetBulletPrefubInstance () =
    let bullet = InstanceManager.InstantiatePrefab(this.bulletObject, this.transform.position, Quaternion.identity)
    let b = bullet.GetComponent<BaseBullet>() 
    let b = b.GetDefaultBullet ()
    b.Init()
    bullet

  member this.IsFinish () =
    if (this.Bullet :> obj = null) then
      false
    else
      let bullet = this.Bullet.GetDefaultBullet ()
      let task = bullet.Task
      match task with
      | None -> false
      | Some x -> 
        if task.Value.Finish then
          InstanceManager.Destroy(this.Bullet.gameObject)
        task.Value.Finish

  member this.Shoot () = 
    let self = this.GetDefaultBullet ()
    if (self.Used) then
      let bullet = this.GetBulletPrefubInstance()
      this.Bullet <- bullet.GetComponent<EnemyBullet>()
      let task = FsBulletML.BulletRunner.convertBulletmlTaskOption(this.BulletmlInfo.Bulletml)
      this.Bullet.GetDefaultBullet().Root <- true
      this.Bullet.SetTask(task)    
      


  member this.Next () = 
    this.DestroyEnemyBullet()
    if (this.BulletIndex + 1 >= this.bullets.Length) then
      this.BulletIndex <- 0
    else
      this.BulletIndex <- this.BulletIndex + 1

    this.Life <- this.MaxLife
    this.Second <- false
    this.SetBulletmlInfo();

  member this.Prev () = 
    this.DestroyEnemyBullet()
    if (this.BulletIndex = 0) then
      this.BulletIndex <- this.bullets.Length - 1
    else
      this.BulletIndex <- this.BulletIndex - 1

    this.Life <- this.MaxLife
    this.Second <- false
    this.SetBulletmlInfo();


  member this.DestroyEnemyBullet () =
    let bullets = GameObject.FindGameObjectsWithTag("EnemyBullet")
    bullets |> Seq.iter (fun bullet -> InstanceManager.Destroy(bullet))

  member this.SetBulletmlInfo () =
    let bulletmlInfo = this.bullets.[this.BulletIndex]
    this.BulletName <- bulletmlInfo.Name
    this.BulletmlInfo <- bulletmlInfo

  member this.GetBulletml() : seq<BulletmlInfo> =
    seq {
        yield FsBulletML.Bullets.EnemyBullet.Sdmkun.SilverGun.b4D_boss_PENTA
        yield FsBulletML.Bullets.EnemyBullet.Sdmkun.Strikers1999.hanabi
        yield FsBulletML.Bullets.EnemyBullet.Sdmkun.DragonBlaze.nebyurosu_2
        yield FsBulletML.Bullets.EnemyBullet.Sdmkun.GWange._roll_gara
        yield FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.knight_2
        yield FsBulletML.Bullets.EnemyBullet.Sdmkun.GWange.round_trip_bit
        yield FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.b88way
        yield FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.bit
        yield FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.rollbar
    }

