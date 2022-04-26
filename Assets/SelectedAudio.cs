using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedAudio : MonoBehaviour
{
    public AudioClip _audioClip;
    public AudioSource _audioSource;

    public void selected()
    {
        _audioSource.PlayOneShot(_audioClip);
    }
}
