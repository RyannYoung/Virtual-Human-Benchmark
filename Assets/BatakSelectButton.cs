using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatakSelectButton : BatakButton
{

    public string gametype;
    private GameStateManager.Games selectedGame;
    public GameObject helperArrow;
    public GameObject starterArrow;

    public GameStateManager gameStateManager;

    public void SetGame()
    {
        gametype = gametype.ToLower();
        
        switch (gametype)
        {
            case "accumulator":
                selectedGame = GameStateManager.Games.Accumulator;
                break;
            case "reaction":
                selectedGame = GameStateManager.Games.Reaction;
                break;
            case "sequence":
                selectedGame = GameStateManager.Games.Sequence;
                break;
        }
        
        gameStateManager.SetSelectedGame(selectedGame);
        
        Debug.Log($"Selected game mode: {selectedGame}");

        Instantiate(starterArrow);
        Destroy(helperArrow);
    }
    
}
