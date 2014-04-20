using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using FsBulletML;
using FsBulletML.MonoGame;
using BulletType = FsBulletML.Processable.BulletType;
using EnemyBullet = FsBulletML.MonoGame.EnemyBullet;
using IBulletmlObject = FsBulletML.Processable.IBulletmlObject;
using BulletmlTask = Microsoft.FSharp.Core.FSharpOption<FsBulletML.Processable.BulletmlTask>;

namespace FsBulletML.Sample.MonoGame.CSharp
{
    public class Enemy : FsBulletML.MonoGame.BaseBullet, IEnemy 
    {
        private FsBulletML.MonoGame.IBullet self = null;
        private int Timer { get; set; }
        string BulletName { get; set; }
        BulletmlTask BulletBulletmlTask { get; set; }
        private bool Second { get; set; }
        EnemyBullet Bullet { get; set; }
        public int Life { get; set; }

        public Enemy() : this(2000) {}
        public Enemy(int life)
        {
            this.self = this;
            this.self.BulletType = BulletType.Enemy;
            self.Init();
            this.self.IsBullet = false;
            this.self.Radius = 18;
            this.Life = life;
        }

        public void Shoot()
        {
          if (this.self.Used)
          {
            this.Bullet = new EnemyBullet();
            ((IBulletmlObject)this.Bullet).IsBullet = true;
            Manager.AddEnemyBulletPos(this.Bullet, new Vector2(self.X, self.Y));
            this.Bullet.SetTask(this.BulletBulletmlTask);
          }
        }

        public void Update()
        { 
            this.Timer += 1;

            if (!this.Second || IsFinish()) 
            {
                this.Second = true;
                this.Timer = 0;
                this.Shoot();
            }

            base.RunTask();
        }

        private bool IsFinish()
        {
            if (this.Bullet == null)
            {
                return false;
            }
            else
            {
                var task = ((IBullet)this.Bullet).Task;
                if (Microsoft.FSharp.Core.OptionModule.IsNone(task))
                {
                    return false;
                }
                return task.Value.Finish;
            }
       
        }

        public void SetMoveTask(BulletmlTask bulletmlTask) 
        {
            ((IEnemy)this).Task = bulletmlTask;
        }

        public void SetBulletTask(string bulletName, BulletmlTask bulletmlTask) 
        {
            this.BulletName = bulletName;
            this.BulletBulletmlTask = bulletmlTask;
        }

    }
}


/*
 
 
namespace FsBulletML.Sample.MonoGame.FSharp

open System
open System.Collections.Generic
open System.Runtime.Serialization
open Microsoft.Xna.Framework
open FsBulletML
open FsBulletML.MonoGame
 
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
 
 
 
 */