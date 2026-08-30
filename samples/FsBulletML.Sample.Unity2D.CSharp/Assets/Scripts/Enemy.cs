using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FsBulletML;
using Microsoft.FSharp.Core;

public class Enemy : BaseBullet
{
    public GameObject BombType;

    private static List<BulletmlInfo> bullets;
    public string BulletName { get; private set; }
    public int BulletIndex { get; set; }
    private BulletmlInfo BulletmlInfo { get; set; }
    private BulletSim RootSim { get; set; }
    public int MaxLife = 2000;
    public int Life = 2000;
    public bool isBomb = true;
    private bool Second { get; set; }

    public Enemy()
        : base()
    {
        var self = this as FsBulletML.Processable.IBulletmlObject;
        self.BulletType = Processable.BulletType.Enemy;
        self.IsBullet = false;
        self.Used = true;
    }

    void Start()
    {
        bullets = GetBulletml().ToList();
        SetBulletmlInfo();
    }

    void OnTriggerEnter2D(Collider2D collier)
    {
        HitByPlayerBullet();
    }

    public void HitByPlayerBullet()
    {
        if (isBomb) Bomb.GenerateBomb(BombType, this.transform.position);
        this.Life -= 1;
        if (this.Life <= 0)
        {
            Next();
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Next();
        };

        if (!this.Second || this.IsFinish())
        {
            this.Second = true;
            this.Shoot();
        }

        base.Update();
    }

    public override GameObject GetBulletPrefubInstance()
    {
        // Spawned bullets are ECS entities; GameObject instantiate path is unused.
        return null;
    }

    public void Shoot()
    {
        var self = this as FsBulletML.Processable.IBulletmlObject;
        if (self.Used)
        {
            var task = FsBulletML.BulletRunner.ConvertBulletmlTaskOption(this.BulletmlInfo.Bulletml);
            this.RootSim = BulletEntityFactory.SpawnEnemy(this.transform.position, task, root: true);
        }
    }

    private bool IsFinish()
    {
        if (this.RootSim == null)
        {
            return false;
        }
        else
        {
            var task = this.RootSim.Task;
            if (Microsoft.FSharp.Core.OptionModule.IsNone(task))
            {
                return false;
            }
            if (task.Value.Finish)
            {
                BulletEntityFactory.Destroy(this.RootSim);
                this.RootSim = null;
            }
            return task.Value.Finish;
        }
    }

    public void Next()
    {
        DestroyEnemyBullet();
        if (this.BulletIndex + 1 >= bullets.Count())
        {
            this.BulletIndex = 0;
        }
        else
        {
            this.BulletIndex += 1;
        }

        this.Life = MaxLife;
        this.Second = false;
        SetBulletmlInfo();
    }

    public void Prev()
    {
        DestroyEnemyBullet();
        if (this.BulletIndex == 0)
        {
            this.BulletIndex = bullets.Count() - 1;
        }
        else
        {
            this.BulletIndex -= 1;
        }

        this.Life = MaxLife;
        this.Second = false;
        SetBulletmlInfo();
    }

    private void DestroyEnemyBullet()
    {
        BulletEntityFactory.DestroyAllEnemy();
        this.RootSim = null;
    }

    private void SetBulletmlInfo()
    {
        var bulletmlInfo = bullets[this.BulletIndex];
        this.BulletName = bulletmlInfo.Name;
        this.BulletmlInfo = bulletmlInfo;
    }

    public static IEnumerable<FsBulletML.BulletmlInfo> GetBulletml()
    {
        yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.SilverGun.b4D_boss_PENTA;
        yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Strikers1999.hanabi;
        yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.DragonBlaze.nebyurosu_2;
        yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.GWange._roll_gara;
        yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Original.knight_2;
        yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.GWange.round_trip_bit;
        yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.b88way;
        yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.bit;
        yield return FsBulletML.Bullets.EnemyBullet.Sdmkun.Noiz2sa.rollbar;
    }
}
