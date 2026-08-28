using UnityEngine;
using System;
using System.Text;
using R3;

[ExecuteInEditMode()]
public class Informations : MonoBehaviour
{
    public bool show = true;
    public bool showInEditor = false;

    private Enemy enemy;
    private Player player;
    private readonly ReactiveProperty<string> StatusTextRp = new("");

    void Awake()
    {
        Application.targetFrameRate = 40;
        enemy = FindAnyObjectByType<Enemy>();
        player = FindAnyObjectByType<Player>();
        useGUILayout = false;
    }

    public void Start()
    {
        if (!Application.isPlaying || enemy == null || player == null)
        {
            return;
        }

        var token = destroyCancellationToken;
        var stats = Observable.Return((enemy: 0, player: 0, fps: 0f))
            .Concat(
                Observable.Interval(TimeSpan.FromSeconds(0.5), token)
                    .Select(_ => (
                        enemy: BulletEntityFactory.EnemyCount,
                        player: BulletEntityFactory.PlayerCount,
                        fps: 1f / Time.unscaledDeltaTime
                    )));

        enemy.BulletNameRp
            .CombineLatest(enemy.LifeRp, (name, life) => (name, life))
            .CombineLatest(player.DamageRp, (x, dmg) => (name: x.name, life: x.life, dmg: dmg))
            .CombineLatest(stats, (x, s) => FormatStatus(x.name, x.life, x.dmg, s.fps, s.enemy, s.player))
            .Subscribe(text => StatusTextRp.Value = text)
            .AddTo(this);
    }

    public void OnGUI()
    {
        if ((Application.isPlaying && show) || (!Application.isPlaying && showInEditor))
        {
            GUI.Box(new Rect(5, 30, 475, 96), "");
            GUI.Label(new Rect(10, 30, 1000, 200), GetShowText());
        }

        if (GUI.Button(new Rect(445, 35, 25, 22), show ? "-" : "+"))
        {
            this.show = !this.show;
        }

        if (GUI.Button(new Rect(5, 5, 25, 22), "<") && enemy != null)
        {
            enemy.Prev();
        }

        if (GUI.Button(new Rect(445, 5, 25, 22), ">") && enemy != null)
        {
            enemy.Next();
        }
    }

    private string GetShowText()
    {
        if (enemy == null || player == null)
        {
            return "";
        }
        if (!Application.isPlaying)
        {
            return FormatStatus(enemy.BulletName, enemy.Life, player.Damage, 0f, 0, 0);
        }
        return StatusTextRp.Value;
    }

    private static string FormatStatus(string name, int life, int damage, float fps, int enemyBullets, int playerBullets)
    {
        var sb = new StringBuilder();
        sb.Append(string.Format("FPS:{0:F2}fps\n", fps));
        sb.Append(string.Format("Name:{0}\n", name));
        sb.Append(string.Format("Boss Life:{0}\n", life));
        sb.Append(string.Format("Player Damages:{0}\n", damage));
        sb.Append(string.Format("EnemyBullets:{0}\n", enemyBullets));
        sb.Append(string.Format("PlayerBullets:{0}\n", playerBullets));
        return sb.ToString();
    }

    void OnDestroy()
    {
        StatusTextRp.Dispose();
    }
}
