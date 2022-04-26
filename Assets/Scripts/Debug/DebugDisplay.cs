using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class DebugDisplay : MonoBehaviour
{
    private Dictionary<string, string> _dLogs = new Dictionary<string, string>(); // if required: can use sorted dictionary
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI fps_text;
    public GameObject canvas;
    private Display _display;
    public bool sortOutput;

    private bool enabled;
    
    [SerializeField]
    private InputActionProperty toggleButton;

    [SerializeField] private Transform attachComponent;
    private RectTransform rectTransform;
    public int maxLines;

    private void OnEnable()
    {
        Application.logMessageReceived += WriteLog;
        toggleButton.action.performed += ctx => ToggleUIMode();
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= WriteLog;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        enabled = false;
        canvas.SetActive(enabled);
    }

    private void Update()
    {
        if (!enabled)
            return;
    }

    private void WriteLog(string log, string stacktrace, LogType type)
    {
        if (type != LogType.Log) return;

        
        if (_dLogs.Count > maxLines)
        {
            _dLogs.Clear();
        }
        
        var split = log.Split(char.Parse(":"));
        var key = split[0];

        var value ="";
        
        if (split.Length > 1)
        {
            for (var i = 1; i < split.Length; i++)
                value += split[i];
        }

        if (_dLogs.ContainsKey(key))
            _dLogs[key] = value; 
        else
            _dLogs.Add(key, value);

        var textOutput = "";

        foreach (var logItem in _dLogs)
        {
            if (logItem.Value == "")
                textOutput += $"{logItem.Key}\n";
            else
                textOutput += $"{logItem.Key}: {logItem.Value}\n";
            
        }
        text.text = textOutput;
    }

    private float GetFPS()
    {
        return (int)(1f / Time.unscaledDeltaTime);
    }

    private void ToggleUIMode()
    {
        Debug.Log("Toggling UI");
        enabled = !enabled;
        canvas.SetActive(enabled);
        
        if(enabled)
            InvokeRepeating(nameof(FPSCounterUpdate), 0, 0.2f);
    }

    private void FPSCounterUpdate()
    {
        fps_text.text = $"FPS<sub>(Last Frame)</sub>: <color=\"red\"> {GetFPS()}";
    }


}
