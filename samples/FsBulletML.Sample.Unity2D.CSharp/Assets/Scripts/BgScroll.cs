using UnityEngine;

public class BgScroll : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed1 = 0.1f;
    void Update()
    {
        var newTextureOffset = this.renderer.material.mainTextureOffset;
        newTextureOffset.y = this.renderer.material.mainTextureOffset.y - Time.deltaTime * scrollSpeed1;
        this.renderer.material.mainTextureOffset = newTextureOffset;
    }
}
