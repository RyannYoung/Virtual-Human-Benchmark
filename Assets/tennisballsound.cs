using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tennisballsound : MonoBehaviour
{
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Playing tennis ball bounce audio");
        _audioSource.Play();
    }
}
