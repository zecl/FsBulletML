using UnityEngine;
using R3;

public class ParticleSortingLayer : MonoBehaviour
{
    void Start()
    {
        var r = GetComponent<ParticleSystemRenderer>();
        r.sortingLayerName = "Bomb";
        r.sortingOrder = 2;

        var ps = GetComponent<ParticleSystem>();
        Observable.EveryUpdate(destroyCancellationToken)
            .Where(_ => !ps.IsAlive())
            .Take(1)
            .Subscribe(_ => InstanceManager.Destroy(gameObject));
    }
}
