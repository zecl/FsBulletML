using UnityEngine;

public class Bomb : MonoBehaviour
{
    const float SeCooldown = 0.12f;
    const float SeVolume = 0.1f;
    const int EmitCount = 12;

    static ParticleSystem s_ps;
    static float s_lastSe = -999f;

    public static void GenerateBomb(GameObject bombType, Vector3 position)
    {
        Ensure(bombType);
        if (s_ps == null)
        {
            return;
        }

        var emit = new ParticleSystem.EmitParams
        {
            position = position,
            applyShapeToPosition = true
        };
        s_ps.Emit(emit, EmitCount);
        PlaySeQuiet();
    }

    static void Ensure(GameObject bombType)
    {
        if (s_ps != null)
        {
            return;
        }
        if (bombType == null)
        {
            return;
        }

        var go = Object.Instantiate(bombType);
        go.name = "BombVfxPool";
        Object.DontDestroyOnLoad(go);

        var sorting = go.GetComponent<ParticleSortingLayer>();
        if (sorting != null)
        {
            sorting.enabled = false;
        }

        var audio = go.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.enabled = false;
            audio.playOnAwake = false;
        }

        s_ps = go.GetComponent<ParticleSystem>();
        if (s_ps == null)
        {
            return;
        }

        var main = s_ps.main;
        main.playOnAwake = false;
        main.loop = false;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.stopAction = ParticleSystemStopAction.None;
        s_ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        go.SetActive(true);
    }

    static void PlaySeQuiet()
    {
        if (Time.unscaledTime - s_lastSe < SeCooldown)
        {
            return;
        }
        s_lastSe = Time.unscaledTime;
        AudioManager.PlaySE(0, SeVolume);
    }
}
