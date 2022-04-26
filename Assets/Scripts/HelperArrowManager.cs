using UnityEngine;

public class HelperArrowManager : MonoBehaviour
{
    // get the directional arrows to manage
    public DirectionalArrow gameSelectHelper;
    public DirectionalArrow gameStartHelper;

    public GameStateManager gameStateManager;

    private void OnEnable()
    {
        gameStateManager.selectedGameChangeEvent.AddListener(CheckGame);
    }

    private void OnDestroy()
    {
        gameStateManager.selectedGameChangeEvent.RemoveListener(CheckGame);
    }

    private void CheckGame()
    {
        var currentGame = gameStateManager.GetCurrentGame();
        var selectedGame = gameStateManager.GetSelectedGame();

        // check if a game is selected
        if (selectedGame != GameStateManager.Games.None)
            ActivateArrow(gameSelectHelper, false);

        // enable the start game helper
        if (currentGame == GameStateManager.Games.None && selectedGame != GameStateManager.Games.None)
            ActivateArrow(gameStartHelper, true);

        // disable the start game helper
        if (currentGame != GameStateManager.Games.None)
            ActivateArrow(gameStartHelper, false);
    }

    private void Start()
    {
        // set the initial states
        ActivateArrow(gameSelectHelper, true);
        ActivateArrow(gameStartHelper, false);
    }

    public void ActivateArrow(DirectionalArrow arrow, bool activate)
    {
        arrow.gameObject.SetActive(activate);
        arrow.ActivateAudio();
    }

    public void RemoveArrow(DirectionalArrow arrow)
    {
        ActivateArrow(arrow, false);
    }


}
