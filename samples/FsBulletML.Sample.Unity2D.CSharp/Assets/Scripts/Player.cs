using UnityEngine;
using System;
using FsBulletML;
using R3;
using R3.Triggers;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public GameObject Bullet;
    public GameObject BombType;
    public bool isBomb = true;

    public readonly ReactiveProperty<int> DamageRp = new(0);
    public readonly ReactiveProperty<Vector2> PositionRp = new(Vector2.zero);
    public int Damage => DamageRp.Value;

    private static Microsoft.FSharp.Core.FSharpOption<Processable.BulletmlTask> b2wayLeftBulletTask;
    private static Microsoft.FSharp.Core.FSharpOption<Processable.BulletmlTask> b2wayRightBulletTask;
    private static Microsoft.FSharp.Core.FSharpOption<Processable.BulletmlTask> hommingTask;

    public float X
    {
        get => PositionRp.Value.x;
        set
        {
            var p = PositionRp.Value;
            p.x = value;
            PositionRp.Value = p;
        }
    }

    public float Y
    {
        get => PositionRp.Value.y;
        set
        {
            var p = PositionRp.Value;
            p.y = value;
            PositionRp.Value = p;
        }
    }

    void Awake()
    {
        Processable.BulletMLManager.Init(new BulletFunctions());
        b2wayLeftBulletTask = BulletRunner.ConvertBulletmlTaskOption(FsBulletML.Bullets.PlayerBullet.PlayerBullet.b2wayLeftBullet);
        b2wayRightBulletTask = BulletRunner.ConvertBulletmlTaskOption(FsBulletML.Bullets.PlayerBullet.PlayerBullet.b2wayRightBullet);
        hommingTask = BulletRunner.ConvertBulletmlTaskOption(FsBulletML.Bullets.PlayerBullet.PlayerBullet.homing);
    }

    void Start()
    {
        PositionRp.Value = new Vector2(transform.position.x, transform.position.y);

        PositionRp
            .Subscribe(p =>
            {
                var pos = transform.position;
                pos.x = p.x;
                pos.y = p.y;
                transform.position = pos;
            })
            .AddTo(this);

        DamageRp
            .Skip(1)
            .Subscribe(_ =>
            {
                if (isBomb) Bomb.GenerateBomb(BombType, transform.position);
            })
            .AddTo(this);

        var update = Observable.EveryUpdate(destroyCancellationToken);

        update
            .Select(_ => (x: Input.GetAxisRaw("Horizontal"), y: Input.GetAxisRaw("Vertical")))
            .Where(v => v.x != 0f || v.y != 0f)
            .Subscribe(v => ApplyMove(v.x, v.y));

        update
            .Where(_ => Input.GetKey(KeyCode.Z))
            .Subscribe(_ =>
            {
                Shoot2WayLeftBullet();
                Shoot2WayRightBullet();
            });

        this.OnTriggerEnter2DAsObservable()
            .Subscribe(_ => HitByEnemyBullet())
            .AddTo(this);
    }

    void ApplyMove(float x, float y)
    {
        var p = PositionRp.Value;
        var mx = p.x + x / 100f * speed;
        if (mx >= 0.4f && mx <= 4.4f)
        {
            p.x = mx;
        }
        var my = p.y + y / 100f * speed;
        if (my > -6.0f && my <= -0.4f)
        {
            p.y = my;
        }
        PositionRp.Value = p;
    }

    private void Shoot2WayLeftBullet()
    {
        var position = this.transform.position + new Vector3(-0.1f, 0.1f, 0);
        BulletEntityFactory.SpawnPlayer(position, Player.b2wayLeftBulletTask);
    }

    private void Shoot2WayRightBullet()
    {
        var position = this.transform.position + new Vector3(0.1f, 0.1f, 0);
        BulletEntityFactory.SpawnPlayer(position, Player.b2wayRightBulletTask);
    }

    private void ShootHomingBullet()
    {
        BulletEntityFactory.SpawnPlayer(this.transform.position, Player.hommingTask);
    }

    public void HitByEnemyBullet()
    {
        DamageRp.Value += 1;
    }

    void OnDestroy()
    {
        DamageRp.Dispose();
        PositionRp.Dispose();
    }
}
