using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using ChoETL;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static Logger;

public class SequenceTest : BaseGame
{
    
    // Sequence Test Parameters (seen within Unity)
    [Header("Sequence Test Parameters")] 
    [Range(1,10)] public int startingStackSize;

    public int patternSpeed;
    
    private bool _needReset;

    private float _startTime;
    private float _finishTime;

    private float _timeStarted;
    private float _timeFinished;

    private bool _showPattern;
    public float _onTime;
    public float _offTime;

    private bool _needNewStack;
    private int _stackSize;
    private bool _hasCoroutineFinished;

    private bool _canButtonAudioPlay;
    private bool _isActivatingAllButtons;

    private BatakButton nextButton;
    private BatakButton currentButton;
    private BatakButton buttonAudioLastPlayedOn;
    
    private  Stack<BatakButton> sequenceStack;
    private BatakButton[] sequenceArray;
    private string sequenceString;

    private float _cooldown;

    [SerializeField] private AudioClip correctAudio;
    [SerializeField] private AudioClip buttonPatternAudio;
    [SerializeField] private AudioClip beginPatternAudio;
    [SerializeField] private AudioClip gameCompleteAudio;
    
    private float pitch;

    private bool hadIntroAudioPlayed;

    private void OnEnable()
    {
        gameStateManager.buttonManager.ButtonPressedAction += HandleButton;
    }

    private void HandleButton(BatakButton obj)
    {
        if (gameStateManager.GetCurrentGame() != GameStateManager.Games.Sequence)
            return;
        
        Debug.Log("Received button " + obj.name);
        
        // handle the button press
        if (obj.name != nextButton.name)
        {
            Debug.Log($"INCORRECT! SELECTED: {obj.name} EXPECTING: {nextButton.name}");
            StopGame();
        }
        else
        {
            Debug.Log("Correct Button!");
        }
    }

    public override void StartGame()
    {
        if (isGameRunning)
        {
            RunGame();
            return;
        }

        gameStateManager.saveManager.folderName = "SequenceTest";
        gameStateManager.saveManager.UpdateSaveInfo();

        gameStateManager.scoreText.text = "I have not implemented this yet";
        gameStateManager.bodyText.text = "Starting the Sequence Test...  The purpose of this test is to assess your short and medium-term memory. In this test, the Batak board will light up a sequence of buttons. Once the pattern has been shown, the board will then light up green signifying you to start the round. Simply repeat the pattern that was shown to you. Upon successful completion of the round, the board will then play another sequence test with increasing difficulty. Goodluck. Beginning test in 3... 2... 1...";


        if (!hadIntroAudioPlayed)
        {
            audioSource.PlayOneShot(audioDescription);
            hadIntroAudioPlayed = true;
        }

        if (audioSource.isPlaying)
            return;

        if(_needReset)
            ResetGame();
        
        _cooldown = Time.time;
        isGameRunning = true;
        _needReset = true;
        _needNewStack = true;
        _showPattern = true;
        _canButtonAudioPlay = true;
        _stackSize = startingStackSize;

        Log("Starting Sequence Test");
        
    }

    public override void StopGame()
    {
        gameStateManager.timer.SetTimer($"Finished.\nScore: {_stackSize}\nThanks for playing.");
        gameStateManager.SetCurrentGame(GameStateManager.Games.None);
        
        TakeSnapshot(false);

        gameStateManager.saveManager.Save();
        gameStateManager.saveManager.playSaveAudio();

        audioSource.PlayOneShot(gameCompleteAudio);
    }

    public override void RunGame()
    {
        // Get a stack of a random sequence
        if (_needNewStack)
            GetNewStack();
        
        if(_showPattern) 
            ShowPattern();
        
        if (sequenceStack.Count == 0)
        {
            if (audioSource.isPlaying)
                return;
            
            audioSource.PlayOneShot(correctAudio);
            _needNewStack = true;
            _timeFinished = Time.time;
            
            TakeSnapshot(true);
            _stackSize++;
        }
        else
        {
            nextButton = sequenceStack.Peek();
            nextButton.onPressed.AddListener(CorrectButtonPress);
        }
    }

    private void CorrectButtonPress()
    {
        if (Time.time > _cooldown)
        {
            Log("Correct!");
            sequenceStack.Pop();
            _cooldown = Time.time + 1f;
        }
    }

    private void TakeSnapshot(bool completedPattern)
    {
        var timeDuration = _timeFinished - _timeStarted;
        
        gameStateManager.saveManager.TakeSnapshot(Time.time, 
            new SequenceSnapshot(_stackSize, sequenceString, completedPattern, _timeStarted, _timeFinished, timeDuration));
    }

    private void ShowPattern()
    {
        for (var i = 0; i < sequenceArray.Length; i++)
        {
            currentButton = sequenceArray[i];
            var activateTime = _startTime + (i*_onTime);
            var deactivateTime = activateTime + _offTime;
            
            //listen to the button turn on event
            currentButton.onActivated.AddListener(PlayActivatedSound);

            if (Time.time > deactivateTime)
            {
                currentButton.canActivate = false;
                currentButton.DeActivateButton();
            }
            else if (Time.time > activateTime)
            {
                currentButton.canActivate = true;
                currentButton.ActivateButton();
            }

            
            currentButton.onActivated.RemoveListener(PlayActivatedSound);
        }

        var timeActiveAllButtons = _startTime + (sequenceArray.Length * _onTime);
        
        if(Time.time > timeActiveAllButtons + 1)
        {
            _isActivatingAllButtons = true;
            gameStateManager.buttonManager.ActivateAllButtons();

            audioSource.pitch = 1;
            PlayBeginSound();
            
            if (Time.time > timeActiveAllButtons + 2)
            {
                _showPattern = false;
                gameStateManager.buttonManager.DeActivateAllButtons();
                _isActivatingAllButtons = false;
            }
        }
    }

    private void PlayBeginSound()
    {
        if (audioSource.isPlaying)
            return;
        
        audioSource.PlayOneShot(beginPatternAudio);

        _timeStarted = Time.time;
    }

    private void PlayActivatedSound()
    {
        if(currentButton == buttonAudioLastPlayedOn)
            return;

        buttonAudioLastPlayedOn = currentButton;
        Debug.Log("Playing button audio");
        audioSource.PlayOneShot(buttonPatternAudio);
        audioSource.pitch += 0.1f;
    }

    private void GetNewStack()
    {
        Log("Creating stack of size " + _stackSize);
        _startTime = Time.time + 1;
        _needNewStack = false;
        _showPattern = true;
        
        sequenceStack = UniqueRandom.GenerateSequence(gameStateManager.buttonManager._buttons, _stackSize);
        sequenceArray = new Stack<BatakButton>(new Stack<BatakButton>(sequenceStack)).ToArray();
        sequenceString = ConvertArrayToString(sequenceArray);
    }

    private static string ConvertArrayToString(BatakButton[] sequence)
    {
        var namesOnly = new string[sequence.Length];

        for (var i = 0; i < sequence.Length; i++)
        {
            namesOnly[i] = sequence[i].name;
        }
        
        var formattedString = String.Format("[{0}]", string.Join<string>(", ", namesOnly));

        return formattedString;
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
        throw new System.NotImplementedException();
    }
}
