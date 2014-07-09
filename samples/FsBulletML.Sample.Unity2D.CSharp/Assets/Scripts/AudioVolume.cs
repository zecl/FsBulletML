using UnityEngine;
using System;

[Serializable]
public class AudioVolume
{
    [Range(0, 2.0f)]
    public float Bgm = 1.0f;

    [Range(0, 2.0f)]
    public float Se = 1.0f;

    [Range(0, 2.0f)]
    public float Voice = 1.0f;

    public bool Mute = false;

    public void Init()
    {
        Bgm = 1.0f;
        Se = 1.0f;
        Voice = 1.0f;
        Mute = false;
    }
}