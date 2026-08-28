using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour
{
    void Start()
    {
        var r = GetComponent<ParticleSystemRenderer>();
        r.sortingLayerName = "Bomb";
        r.sortingOrder = 2;
    }

    void Update()
    {
        var ps = GetComponent<ParticleSystem>();
        if (!ps.IsAlive())
        {
            InstanceManager.Destroy(gameObject);
        }
    }
}
