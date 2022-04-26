using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccumulatorSnapshot : Snapshot
{
    // variables to save within the snapshot
    public string buttonTapped;
    public int score;
    
    // time variables
    public float starttime;
    public float timesincelastbutton;
    public float remainingTime;

    public AccumulatorSnapshot(string buttonTapped, int score, float starttime, float timesincelastbutton, float remainingTime)
    {
        this.buttonTapped = buttonTapped;
        this.score = score;
        this.starttime = starttime;
        this.timesincelastbutton = timesincelastbutton;
        this.remainingTime = remainingTime;
    }
}
