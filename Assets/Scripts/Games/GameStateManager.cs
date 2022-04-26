using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public partial class GameStateManager : MonoBehaviour
{
    
    // Manager Specific Tools
    private Games currentGame;
    private Games selectedGame;
    
    public ButtonManager buttonManager;
    public BoardManager boardManager;
    public Timer timer;

    // Games
    private ReactionTest _reactionTest;
    private AccumulatorTest _accumulatorTest;
    private SequenceTest _sequenceTest;
    
    // Controllers
    public BatakControllerManager controllerManager;
    
    // Save Manager
    public SaveManager saveManager;

    // Audio
    public BatakAudio batakAudio;
    public AudioSource boardaudio;
    
    // Description Board
    public TextMeshProUGUI bodyText;
    public TextMeshProUGUI scoreText;

    // Event system
    public UnityEvent selectedGameChangeEvent;
    public UnityEvent currentGameChangeEvent;

    private void OnEnable()
    {
        InitGames();
        selectedGame = Games.None;
        currentGame = Games.None;
    }

    public Games GetCurrentGame()
    {
        return currentGame;
    }

    public Games GetSelectedGame()
    {
        return selectedGame;
    }

    public void SetCurrentGame(Games game)
    {
        currentGame = game;
        currentGameChangeEvent.Invoke();
    }

    public void SetSelectedGame(Games game)
    {
        selectedGame = game;
        selectedGameChangeEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineGame();
    }

    private void DetermineGame()
    {
        switch (currentGame)
        {
            case Games.Reaction:
                Debug.Log("GSM: Reaction");
                _reactionTest.StartGame();
                break;
            case Games.None:
                break;
            case Games.Accumulator:
                _accumulatorTest.StartGame();
                break;
            case Games.Sequence:
                _sequenceTest.StartGame();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void InitGames()
    {
        _reactionTest = GetComponent<ReactionTest>();
        _accumulatorTest = GetComponent<AccumulatorTest>();
        _sequenceTest = GetComponent<SequenceTest>();
    }
}
