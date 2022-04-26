using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class ReactionSnapshot : Snapshot
{
    // Variables to save within the snapshot
    public float targetTime;
    public float startTime;
    public float totalTime;
    public float reactionTime;

    public float minRunTime;
    public float maxRunTime;

    public ReactionSnapshot(float targetTime, float startTime, float totalTime, float reactionTime,
        float minRunTime, float maxRunTime)
    {
        this.targetTime = targetTime;
        this.startTime = startTime;
        this.totalTime = totalTime;
        this.reactionTime = reactionTime;
        this.minRunTime = minRunTime;
        this.maxRunTime = maxRunTime;
    }
}
