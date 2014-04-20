namespace FsBulletML.Sample

open System
open System.Collections.Generic
open System.Runtime.Serialization
open Microsoft.Xna.Framework
open FsBulletML
 
type Enemy (life) as this =
  inherit BaseBullet ()
  [<DefaultValue>]val mutable private self : IEnemy
  [<DefaultValue>]val mutable private timer : int
  [<DefaultValue>]val mutable private bulletName : string
  [<DefaultValue>]val mutable private bulletBulletmlTask : BulletmlTask
  [<DefaultValue>]val mutable private second : bool
  [<DefaultValue>]val mutable private bullet : EnemyBullet
  [<DefaultValue>]val mutable private life : int32
    
  new() = Enemy(2000)
  do 
    this.self <- this :> IEnemy
    this.self.BulletType <- BulletType.Enemy 
    this.self.Init()
    this.self.IsBullet <- false
    this.self.Radius <- 18.f
    this.life <- life

  member this.Timer with get () = this.timer     

  interface IEnemy with
    member this.Life with get () = this.life
                      and set (v) = this.life <- v      

    member this.Shoot () =
      let self = this :> IEnemy
      if self.Used then
        this.bullet <- new EnemyBullet()
        (this.bullet :> IBulletmlObject).IsBullet <- true
        Manager.addEnemyBulletPos(this.bullet, Vector2(self.X, self.Y))
        this.bullet.SetTask(Some this.bulletBulletmlTask) 

    member this.Update () = 
      this.timer <- this.timer + 1       

      let finish = 
        if this.bullet :> obj = null then false else
          (this.bullet:>IBullet).Task |> function
          | None -> false 
          | Some x -> x.Finish 
      if not this.second || finish then
        this.second <- true
        this.timer <- 0
        let self = this :> IEnemy
        self.Shoot()
    
      base.RunTask()

  member this.SetMoveTask(bulletmlTask) = 
    (this :> IEnemy).Task <- bulletmlTask

  member this.SetBulletTask(bulletName, bulletmlTask) = 
    this.bulletName <- bulletName
    this.bulletBulletmlTask <- bulletmlTask

module EnemyControl =
  let bullets = [ FsBulletML.Bullets.EnemyBullet.Sdmkun.Guwange.round_2_boss_circle_fire
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Psyvariar.b4_D_boss_MZIQ
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.SilverGun.b4D_boss_PENTA
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Strikers1999.hanabi
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.b10flower_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.dis_bee_1
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.dis_bee_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.dis_bee_3
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.roll_misago
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.slow_move
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.OtakuTwo.circle_fireworks2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.DragonBlaze.nebyurosu_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.GWange._roll_gara
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.knight_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.GWange.round_trip_bit
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.b88way
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.bit
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.rollbar
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Tenmado.b5_boss_1
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Tenmado.b5_boss_3
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Dodonpachi.hibachi
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Dodonpachi.kitiku_1 
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Dodonpachi.kitiku_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.hibachi_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.hibachi_3
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_5_boss_gara_4
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_5_boss_gara_3
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_5_boss_gara_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_5_boss_gara_1_a
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.EspRade.round_123_boss_izuna_hakkyou
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_1_boss
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_3_boss
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_3_boss_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_3_boss_last
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_4_boss
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.StormCalibar.last_boss_double_roll_bullets
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.acc_n_dec
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.circular_model
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.wind_cl
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.gnnnyari
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.mossari
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.MAD.double_w
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.star_in_the_sky
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.guruguru
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.backfire 
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.yokokasoku
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.entangled_space
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.ellipse_bomb
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.gyakuhunsya
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.fujin_ranbu_true
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Bulletsmorph.double_seduction
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.accusation
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.XiiStag.b3b
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.KetuiLt.b2boss_winder_crash
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.ChaosSeed.big_monkey_boss
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Guwange.round_4_boss_eye_ball
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_4_boss_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_4_boss_4
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_4_boss_5
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_5_boss_1
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_5_boss_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_1
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_2
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_3
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_4
                  FsBulletML.Bullets.EnemyBullet.Sdmkun.Daiouzyou.round_6_boss_5
                ]

  let enemyTasks = 
    bullets |> List.map (fun (name, bulletml) -> name,BulletRunner.convertBulletmlTask bulletml)

  let enemys enemyDefaultPos (tasks:(string * BulletmlTask) list) = 
    tasks |> List.map (fun task -> enemyDefaultPos, task)  

