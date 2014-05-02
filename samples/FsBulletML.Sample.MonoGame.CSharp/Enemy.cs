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

namespace FsBulletML.Sample.MonoGame.CSharp
{
    public class Enemy : FsBulletML.MonoGame.BaseBullet, IEnemy 
    {
        private FsBulletML.MonoGame.IBullet self = null;
        private int Timer { get; set; }
        string BulletName { get; set; }
        BulletmlInfo BulletmlInfo { get; set; }
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
            this.Bullet.SetTask(this.BulletmlInfo.BulletmlTaskOption());
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
            base.RunTask((x, y) => { this.self.X = this.self.X + x; this.self.Y = this.self.Y + y; });
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

        public void SetMoveBulletmlInfo(BulletmlInfo bulletmlInfo) 
        {
            ((IEnemy)this).Task = bulletmlInfo.BulletmlTaskOption();
        }

        public void SetBulletmlInfo(string bulletName, BulletmlInfo bulletmlInfo) 
        {
            this.BulletName = bulletName;
            this.BulletmlInfo = bulletmlInfo;
        }

    }
}
