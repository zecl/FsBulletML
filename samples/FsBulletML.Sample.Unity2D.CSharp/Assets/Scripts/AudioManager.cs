using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private static AudioManager self;

    [Range(1,16)]public int maxSeSize = 8;
    [Range(1, 16)]public int maxVoiceSize = 2;
    [SerializeField]
    private AudioVolume volume = new AudioVolume();

    private AudioSource BGMsource;
    private AudioSource[] SEsources;
    private AudioSource[] VoiceSources;

    public AudioClip[] Bgm;
    public AudioClip[] Se;
    public AudioClip[] Voice;

    void Awake()
    {
        self = this;
        SEsources = new AudioSource[maxSeSize];
        VoiceSources = new AudioSource[maxVoiceSize];
        DontDestroyOnLoad(gameObject);

        BGMsource = gameObject.AddComponent<AudioSource>();
        BGMsource.loop = true;

        for (int i = 0; i < SEsources.Length; i++)
        {
            SEsources[i] = gameObject.AddComponent<AudioSource>();
        }

        for (int i = 0; i < VoiceSources.Length; i++)
        {
            VoiceSources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        BGMsource.mute = volume.Mute;
        foreach (AudioSource source in SEsources)
        {
            source.mute = volume.Mute;
        }

        foreach (AudioSource source in VoiceSources)
        {
            source.mute = volume.Mute;
        }

        BGMsource.volume = volume.Bgm;
        foreach (AudioSource source in SEsources)
        {
            source.volume = volume.Se;
        }

        foreach (AudioSource source in VoiceSources)
        {
            source.volume = volume.Voice;
        }
    }

    public static void PlayBGM(int index)
    {
        if (0 > index || self.Bgm.Length <= index)
        {
            Debug.LogError("PlayBGM:out of index");
            return;
        }

        if (self.BGMsource.clip == self.Bgm[index])
        {
            return;
        }

        self.BGMsource.Stop();
        self.BGMsource.clip = self.Bgm[index];
        self.BGMsource.Play();
    }

    public static void StopBGM()
    {
        self.BGMsource.Stop();
        self.BGMsource.clip = null;
    }

    public static void PlaySE(int index)
    {
        if (0 > index || self.Se.Length <= index)
        {
            Debug.LogError("PlaySE:out of index");
            return;
        }

        foreach (AudioSource source in self.SEsources)
        {
            if (false == source.isPlaying)
            {
                source.PlayOneShot(self.Se[index]);
                return;
            }
        }
    }

    public static void StopSE()
    {
        foreach (AudioSource source in self.SEsources)
        {
            source.Stop();
            source.clip = null;
        }
    }

    public static void PlayVoice(int index)
    {
        if (0 > index || self.Voice.Length <= index)
        {
            Debug.LogError("PlayVoice:out of index");
            return;
        }

        foreach (AudioSource source in self.VoiceSources)
        {
            if (false == source.isPlaying)
            {
                source.PlayOneShot(self.Voice[index]);
                return;
            }
        }
    }

    public static void StopVoice()
    {
        foreach (AudioSource source in self.VoiceSources)
        {
            source.Stop();
            source.clip = null;
        }
    }
}