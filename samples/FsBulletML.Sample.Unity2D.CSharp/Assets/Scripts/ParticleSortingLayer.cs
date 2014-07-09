using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour
{
    void Start()
    {
        particleSystem.renderer.sortingLayerName = "Bomb";
        particleSystem.renderer.sortingOrder = 2;
    }

    void Update()
    {
        if (!particleSystem.IsAlive())
        {
            InstanceManager.Destroy(gameObject);
        }
    }
}