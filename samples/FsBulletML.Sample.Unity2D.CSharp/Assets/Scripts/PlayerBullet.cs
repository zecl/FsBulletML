// Leftover GameObject bullet. Spawned shots are ECS entities (BulletSim).
// Kept so the prefab still compiles if opened; nothing instantiates this at runtime.
using UnityEngine;
using Microsoft.FSharp.Core;
using FsBulletML;
using R3;
using R3.Triggers;

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

    void Start()
    {
        var self = this as FsBulletML.Processable.IBulletmlObject;
        Observable.EveryUpdate(destroyCancellationToken)
            .Subscribe(_ =>
            {
                if (!this.Root && self.BulletRoot && !self.Used)
                {
                    InstanceManager.Destroy(gameObject);
                    return;
                }

                var p = this.transform.position;
                if (p.x < 0 || p.x > 4.8 || p.y < -6.4 || p.y > 0)
                {
                    self.Used = false;
                    InstanceManager.Destroy(gameObject);
                }
            });

        this.OnTriggerEnter2DAsObservable()
            .Where(col => col.gameObject.tag == "Enemy")
            .Subscribe(_ => InstanceManager.Destroy(gameObject))
            .AddTo(this);
    }

    public override GameObject GetBulletPrefubInstance()
    {
        return null;
    }
}
