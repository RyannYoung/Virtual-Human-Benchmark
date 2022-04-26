using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public void SetTimer(string value)
    {
        timerText.text = value;
    }

    public void Clear()
    {
        timerText.text = "";
    }
}
