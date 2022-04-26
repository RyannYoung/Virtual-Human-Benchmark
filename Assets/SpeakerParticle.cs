using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerParticle : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    public AudioSource audioSource;

    private void OnEnable()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            if(!_particleSystem.isPlaying) 
                _particleSystem.Play();
        }
        else
        {
            _particleSystem.Stop();
        }
    }
}
