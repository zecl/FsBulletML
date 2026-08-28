using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using FsBulletML;
using Microsoft.FSharp.Core;
using R3;
using R3.Triggers;

public class Enemy : BaseBullet
{
    public GameObject BombType;

    private static List<BulletmlInfo> bullets;
    public readonly ReactiveProperty<int> BulletIndexRp = new(0);
    public readonly ReactiveProperty<int> LifeRp = new(2000);
    public readonly ReactiveProperty<string> BulletNameRp = new("");
    private BulletmlInfo BulletmlInfo { get; set; }
    private BulletSim RootSim { get; set; }
    public int MaxLife = 2000;
    public bool isBomb = true;

    public int BulletIndex => BulletIndexRp.Value;
    public string BulletName => BulletNameRp.Value;
    public int Life
    {
        get => LifeRp.Value;
        set => LifeRp.Value = value;
    }

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
        var update = Observable.EveryUpdate(destroyCancellationToken);

        BulletIndexRp
            .Subscribe(_ => ApplyPattern())
            .AddTo(this);

        BulletIndexRp
            .Select(i => bullets[i].Name)
            .Subscribe(name => BulletNameRp.Value = name)
            .AddTo(this);

        var shootOnPattern = BulletIndexRp
            .Select(_ => update.Take(1));

        var shootOnFinish = update
            .Where(_ => IsFinish());

        shootOnPattern
            .Switch()
            .Merge(shootOnFinish)
            .Subscribe(_ => Shoot());

        LifeRp
            .Pairwise()
            .Where(p => p.Current == p.Previous - 1)
            .Subscribe(_ =>
            {
                if (isBomb) Bomb.GenerateBomb(BombType, transform.position);
            })
            .AddTo(this);

        LifeRp
            .Where(x => x <= 0)
            .Subscribe(_ => Next())
            .AddTo(this);

        update
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            .Subscribe(_ => Next());

        this.OnTriggerEnter2DAsObservable()
            .Subscribe(_ => HitByPlayerBullet())
            .AddTo(this);
    }

    public void HitByPlayerBullet()
    {
        LifeRp.Value -= 1;
    }

    public override GameObject GetBulletPrefubInstance()
    {
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

    public void Next()
    {
        var n = bullets.Count;
        BulletIndexRp.Value = (BulletIndexRp.Value + 1) % n;
    }

    public void Prev()
    {
        var n = bullets.Count;
        BulletIndexRp.Value = (BulletIndexRp.Value + n - 1) % n;
    }

    private void ApplyPattern()
    {
        DestroyEnemyBullet();
        var bulletmlInfo = bullets[BulletIndexRp.Value];
        BulletmlInfo = bulletmlInfo;
        LifeRp.Value = MaxLife;
    }

    private void DestroyEnemyBullet()
    {
        BulletEntityFactory.DestroyAllEnemy();
        this.RootSim = null;
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

    void OnDestroy()
    {
        BulletIndexRp.Dispose();
        BulletNameRp.Dispose();
        LifeRp.Dispose();
    }
}
