using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using static Logger;
using static GameStateManager;

public class AccumulatorTest : BaseGame
{

    [Header("Accumulator Test Options")]
    [Range(5, 60)] public float duration;

    private int score;

    private bool _needReset;
    
    private float _startTime;
    private float _finishTime;
    private float timeLeft;

    private float _timeSinceLastButton;
    private float _buttonHitTime;
    private float _prevButtonHitTime;

    private bool _needNewButton;
    private BatakButton currentButton;

    private GameStateManager _gameStateManager;
    
    // Snapshots and saving
    private bool canSnapshot;
    
    public AudioClip countdownthreesecond;
    private bool hasCountdownplayed;


    public override void StartGame()
    {
        if (isGameRunning)
        {
            RunGame();
            return;
        }

        gameStateManager.saveManager.folderName = "AccumulatorTest";
        gameStateManager.saveManager.UpdateSaveInfo();
        
        canSnapshot = false;
        
        audioSource.PlayOneShot(audioDescription);

        // @todo
        gameStateManager.scoreText.text = "<pos=15%>Name<pos=30%>Score<pos=45%>TSLB<pos=60%>Time rem.\n\n";
        //update board information
        gameStateManager.bodyText.text =
            "Starting the Accumulator Test...  The purpose of this test is to measure your ability to see, process and react to visual information. In this test the Batak board will light up a random button, you must then strike this button as fast as possible for another one to appear. Continue striking as many buttons as possible until the allocated time runs out. Goodluck. Beginning test in 3... 2... 1...";
        gameStateManager.timer.SetTimer("Accumulator Test");

        if(_needReset)
            ResetGame();

        score = 0;
        _prevButtonHitTime = 0;
        isGameRunning = true;
        _needReset = true;
        _needNewButton = true;

        _startTime = Time.time + audioDescription.length;
        Invoke("PlayStartAudio", _startTime);
        _finishTime = _startTime + duration;

        Log("Accumulator test started.");
    }

    private void PlayStartAudio()
    {
        gameStateManager.boardaudio.PlayOneShot(audioStart);
    }

    public override void StopGame()
    {
        gameStateManager.timer.SetTimer("Finished.");
        
        //save the game data
        gameStateManager.saveManager.Save();
        gameStateManager.saveManager.playSaveAudio();
        gameStateManager.SetCurrentGame(Games.None);
        
        //update board
        gameStateManager.bodyText.text = $"Export located under\n<color=#91BED4>{gameStateManager.saveManager.fullFolder}<color=\"white\">";
        gameStateManager.bodyText.text +=
            "\n\n Please report any bugs to u3188033@uni.canberra.edu.au";
        
        //play finish audio
        gameStateManager.boardaudio.PlayOneShot(audioFinish);
        
        // play haptic feedback
        gameStateManager.controllerManager.SendHapticAll(0.2f, 0.5f);
    }

    public override void RunGame()
    {
        if (Time.time < _startTime)
            return;
        
        if (Time.time > _finishTime)
            StopGame();
        
        if (_needNewButton)
        {
            Log(gameStateManager.buttonManager._buttons);

            if (score == 0)
            {
                _prevButtonHitTime = Time.time;
            }
            
            if (score != 0)
            {
                _prevButtonHitTime = _buttonHitTime == 0 ? Time.time : _buttonHitTime;

                _buttonHitTime = Time.time;
                _timeSinceLastButton = _buttonHitTime - _prevButtonHitTime;
            }



            if (canSnapshot)
            {
                TakeSnapshot();
                Debug.Log("Snapshot taken!");
                UpdateScoreboard();
            }

            currentButton = UniqueRandom.GetNewRandom(gameStateManager.buttonManager._buttons);
            Log($"Activating button: {currentButton.name}");
            currentButton.canActivate = true;
            currentButton.ActivateButton();
            canSnapshot = true;
            _needNewButton = false;
        }
        
        // Update the timer
        UpdateTimer();

        // countdown timer
        if (timeLeft < 3f && !hasCountdownplayed)
        {
            hasCountdownplayed = true;
            gameStateManager.boardaudio.PlayOneShot(countdownthreesecond);
        }
        
        currentButton.onPressed.AddListener(GetNewButton);
    }

    private void GetNewButton()
    {
        if(!_needNewButton)
            score++;

        currentButton.canActivate = false;
        currentButton.DeActivateButton();
        
        _needNewButton = true;
    }

    private void UpdateScoreboard()
    {
        var formattedString = $"<pos=15%>{currentButton.name}<pos=30%>{score}<pos=45%>{Math.Round(_timeSinceLastButton, 3)}<pos=60%>{Math.Round(timeLeft, 3)}\n";
        gameStateManager.scoreText.text += formattedString;
    }

    private void TakeSnapshot()
    {
        gameStateManager.saveManager.TakeSnapshot(Time.time, new AccumulatorSnapshot(
            currentButton.name,
            score, 
            _startTime, 
            _timeSinceLastButton, 
            timeLeft
        ));
    }

    public override void ExitGame()
    {
        throw new System.NotImplementedException();
    }

    public override void DebugMode()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetGame()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateTimer()
    {
        timeLeft = _finishTime - Time.time;
        
        gameStateManager.timer.SetTimer(timeLeft.ToString("0.0"));
    }
}
