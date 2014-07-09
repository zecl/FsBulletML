using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class InstanceManager : MonoBehaviour
{
    static InstanceManager self;

    public ObjectData[] caches;
    Dictionary<string, bool> activeCachedObjects;

    [System.Serializable]
    public class ObjectData
    {
        public GameObject prefab;
        public int cacheSize = 10;

        private GameObject[] objects;
        private int cacheIndex = 0;

        public void Initialize()
        {
            objects = new GameObject[cacheSize];

            for (int i = 0; i < cacheSize; i++)
            {
                var prefub = MonoBehaviour.Instantiate(prefab) as GameObject;
                objects[i] = prefub;
                objects[i].SetActive(false);
                objects[i].name = objects[i].name.Replace("(Clone)", "") + i.ToString();
            }
        }

        public GameObject GetNextObjectInCache()
        {
            if (cacheSize <= 0)
            {
                Debug.LogError("the size of cache is required one or more.");
            }

            GameObject obj = null;
            for (int i = 0; i < cacheSize; i++)
            {
                obj = objects[cacheIndex];

                if (!obj.activeSelf)
                {
                    break;
                }

                var ci = (cacheIndex + 1) % cacheSize;
                if (ci == 0)
                {
                    cacheIndex = ci;
                    continue;
                }
                cacheIndex += 1;
            }

            if (obj.activeSelf)
            {
                Debug.LogWarning(
                    "Spawn of " + prefab.name +
                    " exceeds cache size of " + cacheSize +
                    "! Reusing already active object.", obj);
                InstanceManager.Destroy(obj);
            }
            var index = (cacheIndex + 1) % cacheSize;
            if (index == 0)
            {
                cacheIndex = index;
                return obj;
            }

            cacheIndex += 1;
            return obj;
        }
    }

    void Awake()
    {
        self = this;
        int amount = 0;

        foreach (var cache in caches)
        {
            cache.Initialize();
            amount += cache.cacheSize;
        }
        activeCachedObjects = new Dictionary<string, bool>(amount);
    }

    static public GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        var cache = self.caches.Where(x => x.prefab.tag == prefab.tag).FirstOrDefault();
        if (cache == null)
        {
            return MonoBehaviour.Instantiate(prefab, position, rotation) as GameObject;
        }
        GameObject obj = cache.GetNextObjectInCache();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        self.activeCachedObjects[obj.name] = true;
        return obj;
    }

    static public void Destroy(GameObject objectToDestroy)
    {
        if (self && self.activeCachedObjects.ContainsKey(objectToDestroy.name))
        {
            objectToDestroy.SetActive(false);
            self.activeCachedObjects[objectToDestroy.name] = false;
            return;
        }
        GameObject.DestroyObject(objectToDestroy);
    }
}