using UnityEngine;

public class SequenceSnapshot : Snapshot
{
    // save data for the sequence game
    public int score;
    public string sequenceArray; // note: this is the array of batakbutton in string form.
    public bool completed;

    public float timeStarted;
    public float timeFinished;
    public float timeDuration;

    public SequenceSnapshot(int score, string sequenceArray, bool completed, float timeStarted, float timeFinished, float timeDuration)
    {
        this.score = score;
        this.sequenceArray = sequenceArray;
        this.completed = completed;
        this.timeStarted = timeStarted;
        this.timeFinished = timeFinished;
        this.timeDuration = timeDuration;
    }
}
