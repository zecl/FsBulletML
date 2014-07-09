using UnityEngine;

public class Bomb : MonoBehaviour
{
    public static void GenerateBomb(GameObject BombType, Vector3 position)
    {
        InstanceManager.InstantiatePrefab(BombType, position, Quaternion.identity);
        AudioManager.PlaySE(0);
    }
}
