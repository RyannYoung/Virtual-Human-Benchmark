using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : SaveManager
{
    // Implement variables to save
    protected override string gameType { get; set; } = "Unspecified";

    public void SetGameType(string game)
    {
        gameType = game;
    }
}
