using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using FsBulletML;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public GameObject Bullet;
    public GameObject BombType;
    public int Damage { get; private set; }
    public bool isBomb = true;
    private int counter = 0;

    private static Microsoft.FSharp.Core.FSharpOption<Processable.BulletmlTask> b2wayLeftBulletTask;
    private static Microsoft.FSharp.Core.FSharpOption<Processable.BulletmlTask> b2wayRightBulletTask;
    private static Microsoft.FSharp.Core.FSharpOption<Processable.BulletmlTask> hommingTask;

    void Awake()
    {
        Processable.BulletMLManager.Init(new BulletFunctions());
        b2wayLeftBulletTask = BulletRunner.ConvertBulletmlTaskOption(FsBulletML.Bullets.PlayerBullet.PlayerBullet.b2wayLeftBullet);
        b2wayRightBulletTask = BulletRunner.ConvertBulletmlTaskOption(FsBulletML.Bullets.PlayerBullet.PlayerBullet.b2wayRightBullet);
        hommingTask = BulletRunner.ConvertBulletmlTaskOption(FsBulletML.Bullets.PlayerBullet.PlayerBullet.homing);
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
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        var mx = this.X + x / 100 * speed;
        if (mx >= 0.4 && mx <= 4.4)
        {
            this.X = mx;
        }

        var my = this.Y + y / 100 * speed;
        if (my > -6.0 && my <= -0.4)
        {
            this.Y = my;
        }

        counter += 1;
        if (Input.GetKey(KeyCode.Z))
        {
            Shoot2WayLeftBullet();
            Shoot2WayRightBullet();
            //ShootHomingBullet();
        };
        if (counter > 60)
            counter = 0;
    }

    private GameObject GetBulletPrefubInstance(Vector3 position, Quaternion rotation)
    {
        return InstanceManager.InstantiatePrefab(this.Bullet, position, rotation);
    }

    private void Shoot2WayLeftBullet()
    {
        var position = this.transform.position + new Vector3(-0.1f, 0.1f, 0);
        var bullet = this.GetBulletPrefubInstance(position, this.transform.rotation);
        bullet.SendMessage("SetTask", Player.b2wayLeftBulletTask);
    }

    private void Shoot2WayRightBullet()
    {
        var position = this.transform.position + new Vector3(0.1f, 0.1f, 0);
        var bullet = this.GetBulletPrefubInstance(position, this.transform.rotation);
        bullet.SendMessage("SetTask", Player.b2wayRightBulletTask);
    }

    private void ShootHomingBullet()
    {
        if (counter > 60)
        {
            var bullet = this.GetBulletPrefubInstance(this.transform.position, this.transform.rotation);
            bullet.SendMessage("Init");
            bullet.SendMessage("SetTask", Player.hommingTask);
        }
    }

    void OnTriggerEnter2D(Collider2D collier)
    {
        if (isBomb) Bomb.GenerateBomb(BombType, this.transform.position);
        this.Damage += 1;
    }
}