using UnityEngine;
using System.Collections;
using Microsoft.FSharp.Core;
using FsBulletML;

public class EnemyBullet : BaseBullet
{
    public EnemyBullet()
        : base()
    {
        var self = this as FsBulletML.Processable.IBulletmlObject;
        self.Init();
        this.Root = true;

        self.IsBullet = true;
        self.BulletRoot = true;
        self.BulletType = Processable.BulletType.Enemy;
    }

    protected new void Update()
    {
        base.Update();

        var self = this as FsBulletML.Processable.IBulletmlObject;
        if (!this.Root && self.BulletRoot && !self.Used)
        {
            InstanceManager.Destroy(gameObject);
        }

        if (this.transform.position.x < 0 || this.transform.position.x > 4.8)
        {
            self.Used = false;
            InstanceManager.Destroy(gameObject);
        }


        if (this.transform.position.y < -6.4 || this.transform.position.y > 0)
        {
            self.Used = false;
            InstanceManager.Destroy(gameObject);
        }
    }

    public override GameObject GetBulletPrefubInstance()
    {
        return InstanceManager.InstantiatePrefab(this.bulletObject, this.transform.position, this.transform.rotation);
    }

    public void SetTask(FSharpOption<Processable.BulletmlTask> bulletmlTask)
    {
        var self = this as FsBulletML.Processable.IBulletmlObject;
        self.Task = bulletmlTask;
    }

    void OnTriggerEnter2D(Collider2D collier)
    {
        if (collier.gameObject.tag == "Player")
        {
            if (!this.Root)
            {
                InstanceManager.Destroy(gameObject);
            }
        }
    }

}
