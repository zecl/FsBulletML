using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using FsBulletML;

public class BulletFunctions : FsBulletML.Processable.IBulletMLManager
{
    private static Player player;

    public BulletFunctions()
    {
        player = UnityEngine.Object.FindAnyObjectByType<Player>();
    }

    private static System.Random rand = new System.Random();
    public float GetRandom()
    {
        return Convert.ToSingle(Math.Round(rand.NextDouble() * 10000) / 10000);
    }

    public float GetRank()
    {
        return 0;
    }

    public float GetPlayerPosX()
    {
        return player != null ? player.PositionRp.Value.x : 0f;
    }

    public float GetPlayerPosY()
    {
        return player != null ? player.PositionRp.Value.y : 0f;
    }
}