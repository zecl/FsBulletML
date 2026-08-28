using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;
using FsBulletML;
using Microsoft.FSharp.Core;

public static class BulletEntityFactory
{
    public const float ScreenMinX = 0f;
    public const float ScreenMaxX = 4.8f;
    public const float ScreenMinY = -6.4f;
    public const float ScreenMaxY = 0f;
    public const float DefaultBulletRadius = 0.1f;

    static bool _ready;
    static RenderMeshArray _renderMeshArray;
    static RenderMeshDescription _renderDesc;
    static float _enemyRadius = DefaultBulletRadius;
    static float _playerRadius = DefaultBulletRadius;
    static int _enemyCount;
    static int _playerCount;

    public static bool IsReady => _ready;
    public static int EnemyCount => _enemyCount;
    public static int PlayerCount => _playerCount;

    public static void Configure(SpriteRenderer enemyBulletSprite, SpriteRenderer playerBulletSprite)
    {
        var enemyMesh = CreateMeshFromSprite(enemyBulletSprite != null ? enemyBulletSprite.sprite : null, "g_bullet_s_ecs");
        var playerMesh = CreateMeshFromSprite(playerBulletSprite != null ? playerBulletSprite.sprite : null, "p_bullet_s_ecs");
        var enemyMat = CreateBulletMaterial(enemyBulletSprite, "g_bullet_s_ecs");
        var playerMat = CreateBulletMaterial(playerBulletSprite, "p_bullet_s_ecs");

        _renderMeshArray = new RenderMeshArray(
            new[] { enemyMat, playerMat },
            new[] { enemyMesh, playerMesh });

        _renderDesc = new RenderMeshDescription(
            shadowCastingMode: ShadowCastingMode.Off,
            receiveShadows: false,
            motionVectorGenerationMode: MotionVectorGenerationMode.Camera,
            layer: 0);

        if (enemyBulletSprite != null && enemyBulletSprite.sprite != null)
        {
            var b = enemyBulletSprite.sprite.bounds.extents;
            _enemyRadius = math.max(0.04f, math.max(b.x, b.y));
        }

        if (playerBulletSprite != null && playerBulletSprite.sprite != null)
        {
            var b = playerBulletSprite.sprite.bounds.extents;
            _playerRadius = math.max(0.04f, math.max(b.x, b.y));
        }

        _ready = true;
    }

    public static BulletSim SpawnEnemy(Vector3 position, FSharpOption<Processable.BulletmlTask> task, bool root)
    {
        var sim = Spawn(BulletKind.Enemy, position.x, position.y, root);
        sim.SetTask(task);
        return sim;
    }

    public static BulletSim SpawnPlayer(Vector3 position, FSharpOption<Processable.BulletmlTask> task)
    {
        var sim = Spawn(BulletKind.Player, position.x, position.y, root: false);
        sim.SetTask(task);
        return sim;
    }

    public static BulletSim SpawnChild(BulletSim parent)
    {
        return Spawn(parent.Kind, parent.X, parent.Y, root: false);
    }

    public static BulletSim SpawnFromEmitter(Processable.IBulletmlObject emitter)
    {
        var kind = object.Equals(emitter.BulletType, Processable.BulletType.Player) ? BulletKind.Player : BulletKind.Enemy;
        return Spawn(kind, emitter.X, emitter.Y, root: false);
    }

    public static BulletSim Spawn(BulletKind kind, float x, float y, bool root)
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
        {
            Debug.LogError("BulletEntityFactory: DefaultGameObjectInjectionWorld is null.");
            return new BulletSim();
        }

        if (!_ready)
        {
            Debug.LogError("BulletEntityFactory: Configure() was not called before spawn.");
        }

        var em = world.EntityManager;
        var entity = em.CreateEntity();

        var sim = new BulletSim
        {
            Entity = entity,
            Kind = kind,
            X = x,
            Y = y,
            BulletType = kind == BulletKind.Player
                ? Processable.BulletType.Player
                : Processable.BulletType.Enemy
        };
        sim.Init();
        sim.Root = root;
        sim.X = x;
        sim.Y = y;

        var radius = kind == BulletKind.Player ? _playerRadius : _enemyRadius;
        em.AddComponentData(entity, LocalTransform.FromPositionRotationScale(
            new float3(x, y, 0f),
            quaternion.identity,
            1f));
        em.AddComponentData(entity, new BulletTag { Kind = kind, Radius = radius });
        em.AddComponentObject(entity, sim);

        if (_ready)
        {
            var matIndex = kind == BulletKind.Player ? 1 : 0;
            var meshIndex = kind == BulletKind.Player ? 1 : 0;
            RenderMeshUtility.AddComponents(
                entity,
                em,
                _renderDesc,
                _renderMeshArray,
                MaterialMeshInfo.FromRenderMeshArrayIndices(matIndex, meshIndex));
        }

        if (kind == BulletKind.Player)
        {
            _playerCount++;
        }
        else
        {
            _enemyCount++;
        }

        return sim;
    }

    public static void Destroy(BulletSim sim)
    {
        if (sim == null)
        {
            return;
        }

        Destroy(sim.Entity);
    }

    public static void Destroy(Entity entity)
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null || entity == Entity.Null)
        {
            return;
        }

        var em = world.EntityManager;
        if (!em.Exists(entity))
        {
            return;
        }

        if (em.HasComponent<BulletTag>(entity))
        {
            var kind = em.GetComponentData<BulletTag>(entity).Kind;
            if (kind == BulletKind.Player)
            {
                _playerCount = math.max(0, _playerCount - 1);
            }
            else
            {
                _enemyCount = math.max(0, _enemyCount - 1);
            }
        }

        em.DestroyEntity(entity);
    }

    public static void DestroyAllEnemy()
    {
        DestroyAll(BulletKind.Enemy);
    }

    public static void DestroyAll(BulletKind kind)
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
        {
            return;
        }

        var em = world.EntityManager;
        using (var query = em.CreateEntityQuery(typeof(BulletTag), typeof(BulletSim)))
        using (var entities = query.ToEntityArray(Unity.Collections.Allocator.Temp))
        {
            for (int i = 0; i < entities.Length; i++)
            {
                var e = entities[i];
                if (em.Exists(e) && em.GetComponentData<BulletTag>(e).Kind == kind)
                {
                    em.DestroyEntity(e);
                }
            }
        }

        if (kind == BulletKind.Player)
        {
            _playerCount = 0;
        }
        else
        {
            _enemyCount = 0;
        }
    }

    public static bool IsOffScreen(float x, float y)
    {
        return x < ScreenMinX || x > ScreenMaxX || y < ScreenMinY || y > ScreenMaxY;
    }

    static Mesh CreateMeshFromSprite(Sprite sprite, string name)
    {
        var mesh = new Mesh { name = name };
        if (sprite != null && sprite.vertices != null && sprite.vertices.Length > 0)
        {
            var src = sprite.vertices;
            var verts = new Vector3[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                verts[i] = src[i];
            }

            var tris = sprite.triangles;
            var indices = new int[tris.Length];
            for (int i = 0; i < tris.Length; i++)
            {
                indices[i] = tris[i];
            }

            mesh.SetVertices(verts);
            mesh.SetUVs(0, sprite.uv);
            mesh.SetTriangles(indices, 0);
        }
        else
        {
            const float h = 0.08f;
            mesh.SetVertices(new Vector3[]
            {
                new Vector3(-h, -h, 0),
                new Vector3(h, -h, 0),
                new Vector3(-h, h, 0),
                new Vector3(h, h, 0)
            });
            mesh.SetUVs(0, new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            });
            // Camera at z=-10 looks +Z, so wind the quad to face -Z.
            mesh.SetTriangles(new[] { 0, 1, 2, 1, 3, 2 }, 0);
        }

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }

    static Material CreateBulletMaterial(SpriteRenderer spriteRenderer, string name)
    {
        var texture = spriteRenderer != null && spriteRenderer.sprite != null
            ? spriteRenderer.sprite.texture
            : Texture2D.whiteTexture;
        var color = spriteRenderer != null ? spriteRenderer.color : Color.white;

        var shader = Shader.Find("Universal Render Pipeline/Unlit");
        if (shader == null)
        {
            shader = Shader.Find("Universal Render Pipeline/Unlit/Unlit");
        }

        if (shader == null)
        {
            shader = Shader.Find("Unlit/Transparent");
        }

        if (shader == null)
        {
            shader = Shader.Find("Sprites/Default");
        }

        if (shader == null)
        {
            shader = Shader.Find("Hidden/InternalErrorShader");
        }

        var mat = new Material(shader) { name = name };
        if (mat.HasProperty("_BaseMap"))
        {
            mat.SetTexture("_BaseMap", texture);
        }
        else
        {
            mat.mainTexture = texture;
        }

        if (mat.HasProperty("_BaseColor"))
        {
            mat.SetColor("_BaseColor", color);
        }
        else if (mat.HasProperty("_Color"))
        {
            mat.SetColor("_Color", color);
        }

        if (mat.HasProperty("_Cull"))
        {
            mat.SetFloat("_Cull", 0f);
        }

        ConfigureTransparent(mat);
        return mat;
    }

    static void ConfigureTransparent(Material mat)
    {
        if (mat.HasProperty("_Surface"))
        {
            mat.SetFloat("_Surface", 1f);
        }

        if (mat.HasProperty("_Blend"))
        {
            mat.SetFloat("_Blend", 0f);
        }

        if (mat.HasProperty("_SrcBlend"))
        {
            mat.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
        }

        if (mat.HasProperty("_DstBlend"))
        {
            mat.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
        }

        if (mat.HasProperty("_ZWrite"))
        {
            mat.SetFloat("_ZWrite", 0f);
        }

        if (mat.HasProperty("_Cull"))
        {
            mat.SetFloat("_Cull", 0f);
        }

        mat.SetOverrideTag("RenderType", "Transparent");
        mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = (int)RenderQueue.Transparent;
        mat.enableInstancing = true;
    }
}
