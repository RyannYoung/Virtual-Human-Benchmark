using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ButtonManager : MonoBehaviour
{
    public BatakButton[] _buttons;
    private MeshRenderer[] _meshRenderers;
    public Stack<Component> buttonSwitches = new Stack<Component>();

    public BatakButtonPhysics[] buttonsv2;
    public Action<BatakButton> ButtonPressedAction;

    public void ButtonPressed(BatakButton btn)
    {
        Debug.Log(btn.name);
        ButtonPressedAction.Invoke(btn);
    }

    public BatakButton[] GetButtons()
    {
        return _buttons;
    }
    
    void Update()
    {
        
    }

    

    void Start()
    {
        buttonSwitches = new Stack<Component>(_buttons);
    }

    public void ActivateAllButtons()
    {
        foreach (var btn in _buttons)
        {
            btn.canActivate = true;
            btn.ActivateButton();
        }
    }

    public void DeActivateAllButtons()
    {
        foreach (var btn in _buttons)
        {
            btn.canActivate = false;
            btn.DeActivateButton();
        }
    }

    public void DisableAllButtons()
    {
        foreach (var btn in _buttons)
        {
            btn.canActivate = false;
        }
    }

    public void EnableAllButtons()
    {
        foreach (var btn in _buttons)
        {
            btn.canActivate = true;
        }
    }

    
}
