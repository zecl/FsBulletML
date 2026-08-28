using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public static class BulletEcsRuntime
{
    public static Player Player;
    public static Enemy Enemy;
    public static float PlayerRadius = 0.15f;
    public static float EnemyRadius = 0.25f;
}

public class BulletEcsBootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AutoCreate()
    {
        if (FindAnyObjectByType<BulletEcsBootstrap>() != null)
        {
            return;
        }

        var go = new GameObject("BulletEcsBootstrap");
        DontDestroyOnLoad(go);
        go.AddComponent<BulletEcsBootstrap>();
    }

    void Awake()
    {
        Configure();
    }

    public void Configure()
    {
        UrpPlayModeCompat.Apply();

        var player = FindAnyObjectByType<Player>();
        var enemy = FindAnyObjectByType<Enemy>();
        BulletEcsRuntime.Player = player;
        BulletEcsRuntime.Enemy = enemy;

        if (player != null)
        {
            var col = player.GetComponent<Collider2D>();
            if (col != null)
            {
                var e = col.bounds.extents;
                BulletEcsRuntime.PlayerRadius = Mathf.Max(0.05f, Mathf.Max(e.x, e.y));
            }
        }

        if (enemy != null)
        {
            var col = enemy.GetComponent<Collider2D>();
            if (col != null)
            {
                var e = col.bounds.extents;
                BulletEcsRuntime.EnemyRadius = Mathf.Max(0.08f, Mathf.Max(e.x, e.y));
            }
        }

        SpriteRenderer enemySr = null;
        SpriteRenderer playerSr = null;
        if (enemy != null && enemy.BulletPrefab != null)
        {
            enemySr = enemy.BulletPrefab.GetComponent<SpriteRenderer>();
        }

        if (player != null && player.Bullet != null)
        {
            playerSr = player.Bullet.GetComponent<SpriteRenderer>();
        }

        BulletEntityFactory.Configure(enemySr, playerSr);

        var cam = Camera.main;
        if (cam == null)
        {
            cam = FindAnyObjectByType<Camera>();
        }

        if (cam != null)
        {
            cam.GetUniversalAdditionalCameraData();
        }

        if (GraphicsSettings.defaultRenderPipeline == null)
        {
            Debug.LogWarning("BulletEcsBootstrap: no URP asset assigned. Open the project once in the Editor so EnsureUrpPipeline can create Assets/Settings/URP_Pipeline.asset.");
        }
    }
}
