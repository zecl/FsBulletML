using UnityEngine;
using R3;

public class BgScroll : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed1 = 0.1f;
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Start()
    {
        Observable.EveryUpdate(destroyCancellationToken)
            .Subscribe(_ =>
            {
                var offset = rend.material.mainTextureOffset;
                offset.y -= Time.deltaTime * scrollSpeed1;
                rend.material.mainTextureOffset = offset;
            });
    }
}
