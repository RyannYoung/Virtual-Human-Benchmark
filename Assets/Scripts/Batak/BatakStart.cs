using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Button script to start the selected game
/// </summary>
public class BatakStart : BatakButton
{

    public AudioClip startAudio;
    public GameStateManager GameStateManager;
    
    public void StartGame()
    {
        // start the game by setting the current game to the selected one.
        GameStateManager.SetCurrentGame(GameStateManager.GetSelectedGame());
    }

    public override void ActivateButton()
    {
        StartGame();
        GameStateManager.boardaudio.PlayOneShot(startAudio);
        Debug.Log($"Starting game: {GameStateManager.GetCurrentGame()}");
        base.ActivateButton();
    }
}
