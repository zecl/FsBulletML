using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using FsBulletML;

public class BulletFunctions : FsBulletML.Processable.IBulletMLManager
{
    private static GameObject player;

    public BulletFunctions()
    {
        player = GameObject.Find("player");
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
        return player.transform.position.x;
    }

    public float GetPlayerPosY()
    {
        return player.transform.position.y;
    }
}