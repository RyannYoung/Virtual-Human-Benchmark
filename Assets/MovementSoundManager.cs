using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSoundManager : MonoBehaviour
{
    public InputActionProperty move;
    public AudioSource movementSound;
    private Vector2 movementControllerValue;

    private float lastPlayed;

    private void OnEnable()
    {
        lastPlayed = 0;
    }

    private void Update()
    {
        movementControllerValue = move.action.ReadValue<Vector2>();

        var xabs = Math.Abs(movementControllerValue.x);
        var yabs = Math.Abs(movementControllerValue.y);
        
        if (xabs > 0 || yabs > 0)
        {
            playMovementAudio();
        }
    }

    private void playMovementAudio()
    {
        if(!movementSound.isPlaying && Time.time > lastPlayed + 0.5f)
        {
            lastPlayed = Time.time;
            movementSound.Play();
        }
    }
}
