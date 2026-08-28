using UnityEngine;

public class BgScroll : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed1 = 0.1f;
    void Update()
    {
        var r = GetComponent<Renderer>();
        var newTextureOffset = r.material.mainTextureOffset;
        newTextureOffset.y = r.material.mainTextureOffset.y - Time.deltaTime * scrollSpeed1;
        r.material.mainTextureOffset = newTextureOffset;
    }
}
