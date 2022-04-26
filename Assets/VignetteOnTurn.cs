using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Vector2 = UnityEngine.Vector2;

public class VignetteOnTurn : MonoBehaviour
{
    public InputActionProperty turn;
    public float intensityValue;
    
    public VolumeProfile mVolumeProfile;
    public Vignette mVignette;

    private ClampedFloatParameter intensity;
    
    private Vector2 controllerValue;

    private float currentValue;
    private float currentVelocity;

    private float targetValue;

    private bool canSoundPlay;
    private float timeSoundLastPlayed;

    public AudioSource snapturnAudio;
    
    public void Awake()
    {
        canSoundPlay = true;
        timeSoundLastPlayed = 0;
        
        // get the vignette effect
        for (int i = 0; i < mVolumeProfile.components.Count; i++)
        {
            if (mVolumeProfile.components[i].name == "Vignette")
            {
                mVignette = (Vignette)mVolumeProfile.components[i];
                intensity = mVignette.intensity; 
            }
        }
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
       controllerValue = turn.action.ReadValue<Vector2>();
       
       if(Math.Abs(controllerValue.x) > 0.5f) 
           ActivateVignette();
       else if(math.abs(controllerValue.x) < 0.5f) 
           DeactivateVignette();

       intensity.value = Mathf.MoveTowards(intensity.value, targetValue, 8f * Time.deltaTime);
    }

    private void DeactivateVignette()
    {
        targetValue = 0f;
    }

    private void ActivateVignette()
    {

        if (canSoundPlay)
        {
            // play sound
            Debug.Log("Playing sound");
            snapturnAudio.Play();
            timeSoundLastPlayed = Time.time;
        }

        canSoundPlay = Time.time > timeSoundLastPlayed + 0.45f;
        
        targetValue = intensityValue;
    }

}

