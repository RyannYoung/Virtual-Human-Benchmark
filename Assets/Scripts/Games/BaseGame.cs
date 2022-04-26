using System.Collections;
using UnityEngine;

public abstract class BaseGame : MonoBehaviour, IBaseGame
{
    /// <summary>
    /// Flag which determines if the game is currently running
    /// </summary>
    public bool isGameRunning;

    public GameStateManager gameStateManager;

    [Header("Audio Parameters")]
    public AudioClip audioDescription;
    public AudioClip audioStart;
    public AudioClip audioFinish;
    public AudioSource audioSource;
    
    [Header("Save Snapshot Parameters")] 
    [Tooltip("How often the snapshot should occur")] public float snapshotInterval;

    public void PlayAudioandWait(AudioSource audioSource)
    {
        StartCoroutine(Example(audioSource));
    }

    private static IEnumerator Example(AudioSource audioSource){
        audioSource.Play ();
        yield return new WaitWhile (()=> audioSource.isPlaying);
        //do something
    }
    public abstract void StartGame();
    public abstract void StopGame();
    public abstract void RunGame();
    public abstract void ExitGame();
    public abstract void DebugMode();
    public abstract void ResetGame();
    public abstract void UpdateTimer();
}
