using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Runtime safety net so GameObject sprites (player/enemy/bg/bomb/HUD) and the
/// Main Camera actually draw under URP Forward+/Entities Graphics.
/// Persistent asset conversion is done by FixBlackScreen in the Editor.
/// </summary>
public static class UrpPlayModeCompat
{
    static bool _applied;

    public static void Apply()
    {
        if (_applied)
        {
            return;
        }

        _applied = true;
        EnsureCameras();
        EnsureDirectionalLight();
        UpgradeSceneRenderers();
        BindSpriteTextures();
    }

    public static Shader FindUrpUnlit()
    {
        return Shader.Find("Universal Render Pipeline/Unlit")
            ?? Shader.Find("Universal Render Pipeline/Unlit/Unlit");
    }

    public static Shader FindUrpParticleUnlit()
    {
        return Shader.Find("Universal Render Pipeline/Particles/Unlit")
            ?? FindUrpUnlit();
    }

    public static void ConfigureUrpUnlit(Material mat, Texture texture, Color color, bool transparent, bool additive)
    {
        if (mat == null)
        {
            return;
        }

        if (mat.HasProperty("_BaseMap"))
        {
            mat.SetTexture("_BaseMap", texture);
        }

        if (mat.HasProperty("_MainTex"))
        {
            mat.SetTexture("_MainTex", texture);
        }

        mat.mainTexture = texture;

        if (mat.HasProperty("_BaseColor"))
        {
            mat.SetColor("_BaseColor", color);
        }

        if (mat.HasProperty("_Color"))
        {
            mat.SetColor("_Color", color);
        }

        if (mat.HasProperty("_Cull"))
        {
            mat.SetFloat("_Cull", 0f);
        }

        mat.doubleSidedGI = true;

        if (transparent)
        {
            if (mat.HasProperty("_Surface"))
            {
                mat.SetFloat("_Surface", 1f);
            }

            var dst = additive ? BlendMode.One : BlendMode.OneMinusSrcAlpha;
            if (mat.HasProperty("_Blend"))
            {
                mat.SetFloat("_Blend", additive ? 2f : 0f);
            }

            if (mat.HasProperty("_SrcBlend"))
            {
                mat.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
            }

            if (mat.HasProperty("_DstBlend"))
            {
                mat.SetFloat("_DstBlend", (float)dst);
            }

            if (mat.HasProperty("_ZWrite"))
            {
                mat.SetFloat("_ZWrite", 0f);
            }

            mat.SetOverrideTag("RenderType", "Transparent");
            mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            if (additive)
            {
                mat.EnableKeyword("_ALPHAMODULATE_ON");
            }

            mat.renderQueue = (int)RenderQueue.Transparent;
        }
        else
        {
            if (mat.HasProperty("_Surface"))
            {
                mat.SetFloat("_Surface", 0f);
            }

            if (mat.HasProperty("_ZWrite"))
            {
                mat.SetFloat("_ZWrite", 1f);
            }

            mat.SetOverrideTag("RenderType", "Opaque");
            mat.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
            mat.renderQueue = (int)RenderQueue.Geometry;
        }

        mat.enableInstancing = true;
    }

    public static bool IsUrpShader(Shader shader)
    {
        if (shader == null)
        {
            return false;
        }

        var n = shader.name ?? string.Empty;
        return n.IndexOf("Universal Render Pipeline", System.StringComparison.OrdinalIgnoreCase) >= 0
            || n.IndexOf("Shader Graphs", System.StringComparison.OrdinalIgnoreCase) >= 0;
    }

    static void EnsureCameras()
    {
        Camera main = Camera.main;
        if (main == null)
        {
            var cameras = Object.FindObjectsByType<Camera>(FindObjectsInactive.Include);
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] != null && cameras[i].CompareTag("MainCamera"))
                {
                    main = cameras[i];
                    break;
                }
            }

            if (main == null && cameras.Length > 0)
            {
                main = cameras[0];
            }
        }

        if (main == null)
        {
            Debug.LogError("UrpPlayModeCompat: no camera found.");
            return;
        }

        main.enabled = true;
        main.gameObject.tag = "MainCamera";
        main.gameObject.SetActive(true);
        main.clearFlags = CameraClearFlags.SolidColor;
        // Original sample clear was already black; keep it. Scrolling bg mesh fills the view.
        if (main.backgroundColor.a < 0.01f)
        {
            var bg = main.backgroundColor;
            bg.a = 1f;
            main.backgroundColor = bg;
        }

        var mainData = main.GetUniversalAdditionalCameraData();
        mainData.renderType = CameraRenderType.Base;
        mainData.SetRenderer(0);
        mainData.renderPostProcessing = false;
        mainData.cameraStack.Clear();

        var camerasAll = Object.FindObjectsByType<Camera>(FindObjectsInactive.Include);
        for (int i = 0; i < camerasAll.Length; i++)
        {
            var cam = camerasAll[i];
            if (cam == null || cam == main)
            {
                continue;
            }

            cam.enabled = true;
            var data = cam.GetUniversalAdditionalCameraData();
            data.renderType = CameraRenderType.Overlay;
            data.SetRenderer(0);
            data.renderPostProcessing = false;
            if (!mainData.cameraStack.Contains(cam))
            {
                mainData.cameraStack.Add(cam);
            }
        }

        Debug.Log(
            "UrpPlayModeCompat cameras: main=" + main.name
            + " ortho=" + main.orthographic
            + " fov=" + main.fieldOfView
            + " size=" + main.orthographicSize
            + " pos=" + main.transform.position
            + " near=" + main.nearClipPlane
            + " far=" + main.farClipPlane
            + " mask=" + main.cullingMask
            + " stack=" + mainData.cameraStack.Count
            + " pipeline=" + (GraphicsSettings.defaultRenderPipeline != null ? GraphicsSettings.defaultRenderPipeline.name : "NULL"));
    }

    static void EnsureDirectionalLight()
    {
        var lights = Object.FindObjectsByType<Light>(FindObjectsInactive.Include);
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i] != null && lights[i].type == LightType.Directional)
            {
                return;
            }
        }

        var go = new GameObject("Directional Light");
        var light = go.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1f;
        light.color = Color.white;
        go.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
    }

    static void UpgradeSceneRenderers()
    {
        var unlit = FindUrpUnlit();
        var particle = FindUrpParticleUnlit();
        if (unlit == null)
        {
            Debug.LogError("UrpPlayModeCompat: URP Unlit shader not found.");
            return;
        }

        var renderers = Object.FindObjectsByType<Renderer>(FindObjectsInactive.Include);
        for (int i = 0; i < renderers.Length; i++)
        {
            var r = renderers[i];
            if (r == null)
            {
                continue;
            }

            var shared = r.sharedMaterials;
            if (shared == null)
            {
                continue;
            }

            Material[] instances = null;
            for (int m = 0; m < shared.Length; m++)
            {
                var src = shared[m];
                if (src == null || IsUrpShader(src.shader))
                {
                    continue;
                }

                if (instances == null)
                {
                    instances = r.materials;
                }

                var shaderName = src.shader != null ? src.shader.name : string.Empty;
                bool additive = shaderName.IndexOf("Particle", System.StringComparison.OrdinalIgnoreCase) >= 0
                    || shaderName.IndexOf("Additive", System.StringComparison.OrdinalIgnoreCase) >= 0;
                var shader = additive && particle != null ? particle : unlit;
                var copy = new Material(shader) { name = src.name + " (URP)" };
                var tex = src.HasProperty("_MainTex") ? src.GetTexture("_MainTex") : src.mainTexture;
                var color = src.HasProperty("_Color") ? src.GetColor("_Color") : Color.white;
                bool transparent = !(r is MeshRenderer && r.gameObject.name.IndexOf("background", System.StringComparison.OrdinalIgnoreCase) >= 0);
                ConfigureUrpUnlit(copy, tex != null ? tex : Texture2D.whiteTexture, color, transparent, additive);
                instances[m] = copy;
            }

            if (instances != null)
            {
                r.materials = instances;
            }
        }
    }

    static void BindSpriteTextures()
    {
        var sprites = Object.FindObjectsByType<SpriteRenderer>(FindObjectsInactive.Include);
        for (int i = 0; i < sprites.Length; i++)
        {
            BindSprite(sprites[i]);
        }
    }

    public static void BindSprite(SpriteRenderer sr)
    {
        if (sr == null || sr.sprite == null)
        {
            return;
        }

        var tex = sr.sprite.texture;
        if (tex == null)
        {
            return;
        }

        var block = new MaterialPropertyBlock();
        sr.GetPropertyBlock(block);
        block.SetTexture("_MainTex", tex);
        block.SetTexture("_BaseMap", tex);
        block.SetColor("_BaseColor", sr.color);
        block.SetColor("_Color", sr.color);
        sr.SetPropertyBlock(block);
    }
}
