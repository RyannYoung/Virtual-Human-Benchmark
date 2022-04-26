using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using static GameStateManager;
using static Logger;
using Random = UnityEngine.Random;

public class ReactionTest : BaseGame
{
    // Reaction Test Parameters (seen within Unity)
    [Header("Reaction Test Params")]
    
    [Range(1, 10)] [Tooltip("The minimum time (in seconds) the buttons can activate upon start")]
    public float minRunTime;
    [Range(3, 30)] [Tooltip("The maximum time (in seconds) the buttons can activate upon start")]
    public float maxRunTime;

    public float gameAttempts;
    private int currentAttempts;

    public AudioClip reactionTriggerAudio;

    public InputActionProperty stopTrigger;
    
    private float _targetTime;
    private float _startTime;
    private float _totalTime;
    private float _reactionTime;
    private float _currentTime;

    private bool _firstGame;

    public ParticleSystem OnReactParticleSystem;

    private GameSaveManager _gameSaveManager;

    private bool _needReset;

    private GameStateManager _gameStateManager;
    
    public ReactionTest(float minRunTime, float maxRunTime)
    {
        this.minRunTime = minRunTime;
        this.maxRunTime = maxRunTime;
    }

    // Start the game
    public override void StartGame()
    {
        if (isGameRunning)
        {
            RunGame();
            return;
        }

        gameStateManager.saveManager.folderName = "ReactionTest";
        gameStateManager.saveManager.UpdateSaveInfo();

        Debug.Log($"Reaction: Start Game");
        audioSource.PlayOneShot(audioDescription);
        
        gameStateManager.timer.SetTimer("Reaction Test");
        // Set the score for the current game
        gameStateManager.scoreText.text = "<pos=20%>ATTEMPT<pos=40%>RESULT<pos=60%>COMMENT\n";
        gameStateManager.bodyText.text =
            "Welcome to the human reaction test. This test aims to evaluate your ability and quickness to react to a certain stimulus. In this test, the buttons on the Batak board will light up green. Quickly press the top left trigger on your controller to evaluate your reaction time. Goodluck.\nBeginning test in 3... 2... 1...";
        
        gameStateManager.buttonManager.EnableAllButtons();

        if (_needReset)
            ResetGame();

        isGameRunning = true;
        _needReset = true;

        if (_firstGame)
        {
            _startTime = Time.time + audioDescription.length;
            _targetTime = Random.Range(minRunTime, maxRunTime);
            _totalTime = _startTime + _targetTime;  
        }
        
        
        Log($"Reaction-time Test started.");
        Log($"Start Time: {_startTime}, Total Time: {_totalTime}");
    }

    public override void StopGame()
    {
        _reactionTime = Time.time - _totalTime;
        _reactionTime = _reactionTime < 0 ? 0 : _reactionTime;
        
        gameStateManager.timer.SetTimer(Math.Round(_reactionTime, 3).ToString());
        gameStateManager.buttonManager.DeActivateAllButtons();
        gameStateManager.boardManager.UnhighlightAllBoardObjects();
        
        Log($"Reaction Time: {Math.Round(_reactionTime, 3)}");
        UpdateScoreboard();

        gameStateManager.saveManager.TakeSnapshot(_currentTime, new ReactionSnapshot(
            _targetTime,
            _startTime,
            _totalTime,
            _reactionTime,
            minRunTime,
            maxRunTime
        ));

        if (gameAttempts > 1)
        {
            gameAttempts--;
            ResetGame();
        }
        
        else
        {
            gameStateManager.SetCurrentGame(Games.None);
            
            gameStateManager.saveManager.playSaveAudio();
            gameStateManager.saveManager.Save();

            //update board information
            gameStateManager.bodyText.text = $"Export located under\n<color=#91BED4>{gameStateManager.saveManager.fullFolder}<color=\"white\">";
            gameStateManager.bodyText.text +=
                "\n\n Did you know the average reaction time is around 200-250ms according to humanbenchmark.com?";
            
            // finish audio
            gameStateManager.boardaudio.PlayOneShot(audioFinish);
            
            _firstGame = true;
            isGameRunning = false;
        }
    }


    public override void ExitGame()
    {
        throw new NotImplementedException();
    }

    public override void DebugMode()
    {
        throw new NotImplementedException();
    }
    

    public override void UpdateTimer()
    {
        // Get time passed and formatted date
        float timePassed = Time.time - _totalTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timePassed);
        string timeOutput = $"{timeSpan.Minutes}:{timeSpan.Seconds}:{timeSpan.Milliseconds}";

        gameStateManager.timer.SetTimer(timeOutput);
    }

    public override void RunGame()
    {
        if (!isGameRunning || !(_totalTime < Time.time)) return;
        
        Log("Game is Running");
        
        _gameStateManager.buttonManager.ActivateAllButtons();
        _gameStateManager.boardManager.HighlightAllBoardObjects();
        UpdateTimer();

        stopTrigger.action.performed += context =>
        {
            if (gameStateManager.GetCurrentGame() != Games.Reaction) return;
            gameStateManager.controllerManager.SendHapticAll();
            OnReactParticleSystem.Play();
            gameStateManager.boardaudio.PlayOneShot(reactionTriggerAudio);
            StopGame();
        };
    }

    private void UpdateScoreboard()
    {
        // Set the score for the current game
        currentAttempts++;
        var comment = _reactionTime < 0.5f ? "Good" : "Too slow";
        var scoreText = $"<pos=20%>{currentAttempts}<pos=40%>{Math.Round(_reactionTime, 3)}<pos=60%>{comment}\n";
        gameStateManager.scoreText.text += scoreText;
    }

    private void Update()
    {
        _currentTime = Time.time;
    }

    public override void ResetGame()
    {

        gameStateManager.timer.SetTimer($"Good!\nSpeed:{_reactionTime}\n{gameAttempts} attempts remaining");

        Debug.Log("Resetting Game: " + gameAttempts);

        // Reset the game parameters
        _startTime = Time.time;
        _targetTime = Random.Range(minRunTime, maxRunTime);
        _totalTime = _startTime + _targetTime;
        _firstGame = false;
    }

    private void OnEnable() => stopTrigger.EnableDirectAction();
    private void OnDisable() => stopTrigger.DisableDirectAction();

    private void Start()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        _firstGame = true;
    }
}
