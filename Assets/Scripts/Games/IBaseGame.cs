using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBaseGame
{
    void StartGame();
    void StopGame();
    void RunGame();
    void ExitGame();
    void DebugMode();
    void ResetGame();
    void UpdateTimer();
}