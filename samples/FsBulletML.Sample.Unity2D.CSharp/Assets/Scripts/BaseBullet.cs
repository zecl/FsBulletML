using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using FsBulletML;
using Microsoft.FSharp.Core;
using R3;

public abstract class BaseBullet : MonoBehaviour, FsBulletML.Processable.IBulletmlObject
{
    [SerializeField]
    protected GameObject bulletObject;
    [field: SerializeField]
    public bool Root { get; set; }
    private GameObject TargetEnemy;
    public abstract GameObject GetBulletPrefubInstance();
    public GameObject BulletPrefab => bulletObject;

    public BaseBullet() : base() {}

    IDisposable simSub;

    protected virtual void OnEnable()
    {
        simSub = Observable.EveryUpdate(destroyCancellationToken)
            .Subscribe(_ => RunTask());
    }

    protected virtual void OnDisable()
    {
        simSub?.Dispose();
        simSub = null;
    }

    protected void RunTask()
    {
        var self = this as FsBulletML.Processable.IBulletmlObject;

        Monad.OptionExtentions.Action<Processable.BulletmlTask>(self.Task,
            x =>
            {
                var result = BulletRunner.Run(this);
                self.X = self.X + (result.X / 100);
                self.Y = self.Y - (result.Y / 100);
                var angle = -(self.Dir) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

                if (result.Processed)
                {
                    x.Init();
                }
                return;
            },
            _ =>
            {
                return;
            });
    }

    public float X
    {
        get { return transform.position.x; }
        set
        {
            var newPosition = this.transform.position;
            newPosition.x = value;
            this.transform.position = newPosition;
        }
    }
    public float Y
    {
        get { return transform.position.y; }
        set
        {
            var newPosition = this.transform.position;
            newPosition.y = value;
            this.transform.position = newPosition;
        }
    }

    public virtual void Init()
    {
        this.Root = false;
        this.Used = true;
        this.BulletRoot = false;
        this.AccelerationX = 0;
        this.AccelerationY = 0;
        this.Speed = 0;
        this.Dir = 0;
        this.IsBullet = true;

        Monad.OptionExtentions.Action<Processable.BulletmlTask>(this.Task,
            x =>
            {
                x.Init();
                return;
            },
            _ =>
            {
                return;
            });

    }

    public void Vanish()
    {
        this.Used = false;
    }

    public float GetAimDir()
    {
        return Mathf.Atan2((Processable.BulletMLManager.GetPlayerPosX() - this.X), (Processable.BulletMLManager.GetPlayerPosY() - this.Y));
    }

    public float GetEnemyAimDir()
    {
        if (this.TargetEnemy != null)
        {
            return Mathf.Atan2(this.TargetEnemy.transform.position.x - this.X, this.TargetEnemy.transform.position.y - this.Y);
        }
        else
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (!enemies.Any())
            {
                return 0;
            }
            else
            {
                GameObject near = enemies[0];
                float nearDist = Vector2.Distance(this.transform.position, near.transform.position);
                for (int i = 1; i < enemies.Length; i++)
                {
                    var d = Vector2.Distance(this.transform.position, enemies[i].transform.position);
                    if (d < nearDist)
                    {
                        nearDist = d;
                        near = enemies[i];
                    }
                }
                this.TargetEnemy = near;
                return Mathf.Atan2(this.TargetEnemy.transform.position.x - this.X, this.TargetEnemy.transform.position.y - this.Y);
            }
        }
    }

    public Processable.IBulletmlObject GetNewBullet()
    {
        this.BulletRoot = true;
        return BulletEntityFactory.SpawnFromEmitter(this);
    }

    public float AccelerationX { get; set; }
    public float AccelerationY { get; set; }
    public bool BulletRoot { get; set; }
    public Processable.BulletType BulletType { get; set; }
    public float Dir { get; set; }
    public bool IsBullet { get; set; }
    public DTD.ShootingDirection ShootingDirection { get; set; }
    public float Speed { get; set; }
    public Microsoft.FSharp.Core.FSharpOption<Processable.BulletmlTask> Task { get; set; }
    public bool Used { get; set; }
}
