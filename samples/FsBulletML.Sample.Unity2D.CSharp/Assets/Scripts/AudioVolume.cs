using UnityEngine;
using System;
using R3;

[Serializable]
public class AudioVolume
{
    public SerializableReactiveProperty<float> Bgm = new(1.0f);
    public SerializableReactiveProperty<float> Se = new(1.0f);
    public SerializableReactiveProperty<float> Voice = new(1.0f);
    public SerializableReactiveProperty<bool> Mute = new(false);

    public void Init()
    {
        Bgm.Value = 1.0f;
        Se.Value = 1.0f;
        Voice.Value = 1.0f;
        Mute.Value = false;
    }
}
