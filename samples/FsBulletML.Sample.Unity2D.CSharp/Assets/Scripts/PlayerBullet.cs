using UnityEngine;
using System.Collections;
using Microsoft.FSharp.Core;
using FsBulletML;

public class PlayerBullet : BaseBullet
{
    public PlayerBullet() : base()
    {
        var self = this as FsBulletML.Processable.IBulletmlObject;
        self.IsBullet = true;
        self.BulletType = Processable.BulletType.Player;
    }

    public void SetTask(FSharpOption<Processable.BulletmlTask> bulletmlTask)
    {
        var self = this as FsBulletML.Processable.IBulletmlObject;
        self.Task = bulletmlTask;
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

    void OnTriggerEnter2D(Collider2D collier)
    {
        if (collier.gameObject.tag == "Enemy")
        {

            InstanceManager.Destroy(gameObject);
        }
    }
}
