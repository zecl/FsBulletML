using System;

namespace UnityEngine
{
    // COMPILE-ONLY stub. Not a substitute for the Unity Editor.

    public struct Vector2
    {
        public float x, y;
        public Vector2(float x, float y) { this.x = x; this.y = y; }
        public static float Distance(Vector2 a, Vector2 b)
        {
            float dx = a.x - b.x, dy = a.y - b.y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static implicit operator Vector3(Vector2 v) => new Vector3(v.x, v.y, 0f);
    }

    public struct Vector3
    {
        public float x, y, z;
        public Vector3(float x, float y) { this.x = x; this.y = y; this.z = 0f; }
        public Vector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public static float Distance(Vector3 a, Vector3 b)
        {
            float dx = a.x - b.x, dy = a.y - b.y, dz = a.z - b.z;
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static implicit operator Vector2(Vector3 v) => new Vector2(v.x, v.y);
    }

    public struct Quaternion
    {
        public float x, y, z, w;
        public static Quaternion identity => new Quaternion();
        public static Quaternion AngleAxis(float angle, Vector3 axis) => identity;
    }

    public struct Rect
    {
        public float x, y, width, height;
        public Rect(float x, float y, float width, float height)
        {
            this.x = x; this.y = y; this.width = width; this.height = height;
        }
    }

    public static class Mathf
    {
        public const float Rad2Deg = 57.29578f;
        public const float Deg2Rad = 0.0174532924f;
        public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);
        public static float Abs(float v) => Math.Abs(v);
    }

    public enum FindObjectsInactive
    {
        Exclude,
        Include
    }

    public class Object
    {
        public string name { get; set; }
        public static Object Instantiate(Object original) => original;
        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation) => original;
        public static T Instantiate<T>(T original) where T : Object => original;
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object => original;
        public static void Destroy(Object obj) { }
        public static void DestroyObject(Object obj) { }
        public static T FindObjectOfType<T>() where T : Object => default;
        public static T FindFirstObjectByType<T>() where T : Object => default;
        public static T FindAnyObjectByType<T>() where T : Object => default;
        public static T[] FindObjectsByType<T>() where T : Object => System.Array.Empty<T>();
        public static T[] FindObjectsByType<T>(FindObjectsInactive findObjectsInactive) where T : Object => System.Array.Empty<T>();
    }

    public class GameObject : Object
    {
        public string tag { get; set; }
        public Transform transform { get; } = new Transform();
        public bool activeSelf { get; private set; } = true;
        public void SetActive(bool value) { activeSelf = value; }
        public T GetComponent<T>() => default;
        public Component GetComponent(Type type) => null;
        public static GameObject Find(string name) => null;
        public static GameObject[] FindGameObjectsWithTag(string tag) => Array.Empty<GameObject>();
        public static new T FindObjectOfType<T>() where T : Object => default;
        public static new T FindAnyObjectByType<T>() where T : Object => default;
    }

    public class Component : Object
    {
        public GameObject gameObject { get; } = new GameObject();
        public Transform transform { get; } = new Transform();
        public string tag { get => gameObject.tag; set => gameObject.tag = value; }
        public T GetComponent<T>() => default;
        public Component GetComponent(Type type) => null;
    }

    public class Behaviour : Component
    {
        public bool enabled { get; set; } = true;
    }

    public class MonoBehaviour : Behaviour
    {
        public bool useGUILayout { get; set; }
    }

    public class Transform : Component
    {
        public Vector3 position { get; set; }
        public Quaternion rotation { get; set; }
    }

    public class Renderer : Component
    {
        public Material material { get; set; } = new Material();
        public string sortingLayerName { get; set; }
        public int sortingOrder { get; set; }
    }

    public class Material : Object
    {
        public Vector2 mainTextureOffset { get; set; }
    }

    public class ParticleSystem : Component
    {
        public bool IsAlive() => false;
        public ParticleSystemRenderer GetRenderer() => new ParticleSystemRenderer();
    }

    public class ParticleSystemRenderer : Renderer { }

    public class Collider2D : Component { }

    public static class Time
    {
        public static float deltaTime => 0f;
        public static float realtimeSinceStartup => 0f;
    }

    public static class Application
    {
        public static int targetFrameRate { get; set; }
        public static bool isPlaying => false;
    }

    public static class Input
    {
        public static float GetAxisRaw(string axisName) => 0f;
        public static bool GetKey(KeyCode key) => false;
        public static bool GetKeyDown(KeyCode key) => false;
    }

    public enum KeyCode
    {
        None = 0,
        Return = 13,
        Z = 122,
    }

    public static class GUI
    {
        public static void Box(Rect position, string text) { }
        public static void Label(Rect position, string text) { }
        public static bool Button(Rect position, string text) => false;
    }

    public static class Debug
    {
        public static void LogError(object message) { }
        public static void Log(object message) { }
    }

    public class ExecuteInEditMode : Attribute { }
    public class SerializeField : Attribute { }
    public class RequireComponent : Attribute
    {
        public RequireComponent(Type type) { }
    }
}
