using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BatakAudio : MonoBehaviour
{
    public enum Audio
    {
        ReactionDescription,
        BeginTest
    }

    public AudioSource _reactionTestDescription;
    public AudioSource _beginTest;

    public void Play(Audio audio)
    {
        var audioSource = GetAudio(audio);
        audioSource.pitch = Random.Range(1f, 1.5f);
        audioSource.Play();
    }

    public static void Play(AudioSource audioSource)
    {
        Logger.Log($"Playing audio: {audioSource}");
        audioSource.pitch = Random.Range(1f, 1.5f);
        audioSource.Play();
    }

    private AudioSource GetAudio(Audio audio)
    {
        AudioSource audioSource;
        switch (audio)
        {
            case Audio.ReactionDescription:
                audioSource = _reactionTestDescription;
                break;
            case Audio.BeginTest:
                audioSource = _beginTest;
                break;
            default:
                throw new NotImplementedException();
        }

        return audioSource;
    }
    
    public IEnumerator PlayandWait(Audio audio)
    {
        Logger.Log($"Waiting for audio to finish");
        var audioSource = GetAudio(audio);
        Play(audio);
        yield return new WaitWhile(() => audioSource.isPlaying);
    }
}
