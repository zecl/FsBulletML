using UnityEngine;
using System.Linq;
using System.Collections;
using System.Text;

[ExecuteInEditMode()]
public class Informations : MonoBehaviour
{

    public bool show = true;
    public bool showInEditor = false;

    private Enemy enemy;
    private Player player;

    private float oldTime;
    private int frame = 0;
    private float frameRate = 0.0F;
    public string info = "";
    private const float intervalTime = 0.5F;

    private int enemyBullets = 0;
    private int playerBullets = 0;

    void Awake()
    {
        Application.targetFrameRate = 40;
        enemy = GameObject.FindObjectOfType<Enemy>();
        player = GameObject.FindObjectOfType<Player>();
        useGUILayout = false;
    }

    public void Start()
    {
        oldTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if (!this.show) return;

        frame++;
        float time = Time.realtimeSinceStartup - oldTime;
        if (time >= intervalTime)
        {
            frameRate = frame / time;
            info = "FPS:" + frameRate.ToString();
            oldTime = Time.realtimeSinceStartup;
            frame = 0;

            enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet").Length;
            playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet").Length;
        }
    }

    public void OnGUI()
    {
        if ((Application.isPlaying && show) || (!Application.isPlaying && showInEditor))
        {
            GUI.Box(new Rect(5, 30, 475, 96), "");
            GUI.Label(new Rect(10, 30, 1000, 200), GetShowText());
        }

        if (GUI.Button(new Rect(445, 35, 25, 22), show ? "▲" : "▼"))
        {
            this.show = !this.show;
        }

        // Prev
        if (GUI.Button(new Rect(5, 5, 25, 22), "<"))
        {
            enemy.Prev();
        }

        // Next
        if (GUI.Button(new Rect(445, 5, 25, 22), ">"))
        {
            enemy.Next();
        }
    }

    private string GetShowText()
    {
        StringBuilder sb = new StringBuilder();
        // FPS
        sb.Append(string.Format("FPS:{0:F2}fps\n", frameRate));
        // 弾名
        sb.Append(string.Format("Name:{0}\n", enemy.BulletName));
        // ボスライフ
        sb.Append(string.Format("Boss Life:{0}\n", enemy.Life));
        // プレイヤーダメージ
        sb.Append(string.Format("Player Damages:{0}\n", player.Damage));
        // 敵弾数
        sb.Append(string.Format("EnemyBullets:{0}\n", enemyBullets));
        // 自機弾数
        sb.Append(string.Format("PlayerBullets:{0}\n", playerBullets));
        return sb.ToString();
    }
}