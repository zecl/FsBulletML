using Unity.Entities;

public enum BulletKind : byte
{
    Enemy = 0,
    Player = 1
}

public struct BulletTag : IComponentData
{
    public BulletKind Kind;
    public float Radius;
}
