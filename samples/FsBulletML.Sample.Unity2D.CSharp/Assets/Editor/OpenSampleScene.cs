using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
static class OpenSampleScene
{
    const string Path = "Assets/Senes/FsBulletML.Sample.Unity2D.unity";

    static OpenSampleScene()
    {
        EditorApplication.delayCall += EnsureScene;
    }

    static void EnsureScene()
    {
        var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(Path);
        if (asset != null)
            EditorSceneManager.playModeStartScene = asset;

        if (EditorApplication.isPlayingOrWillChangePlaymode)
            return;
        if (EditorSceneManager.GetActiveScene().path == Path)
            return;
        if (!string.IsNullOrEmpty(EditorSceneManager.GetActiveScene().path) &&
            EditorSceneManager.GetActiveScene().isDirty)
            return;
        EditorSceneManager.OpenScene(Path);
    }
}
