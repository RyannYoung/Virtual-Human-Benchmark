using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Hand : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private SkinnedMeshRenderer _renderer;
    
    private float gripCurrentValue;
    private float gripTarget;
    private float triggerCurrentValue;
    private float triggerTarget;
    
    [SerializeField] private float speed;
    [SerializeField] private InputActionProperty m_grip;
    [SerializeField] private InputActionProperty m_target;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material onHoverMaterial;
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        m_target.EnableDirectAction();
        m_grip.EnableDirectAction();
    }

    public void OnHoverEntered()
    {
        //change the hands material
        _renderer.material = onHoverMaterial;
        Debug.Log("Hover Entered");
    }

    public void OnHoverCancelled()
    {
        //change the hands material
        _renderer.material = defaultMaterial;
        Debug.Log("Hover Cancelled");
    }

    // Update is called once per frame
    void Update()
    {
        gripTarget = m_grip.action.ReadValue<float>();
        triggerTarget = m_target.action.ReadValue<float>();
        AnimateHand();
    }

    void AnimateHand()
    {
        if (gripCurrentValue != gripTarget)
        {
            gripCurrentValue = Mathf.MoveTowards(gripCurrentValue, gripTarget, Time.deltaTime * speed);
            animator.SetFloat("Grip", gripCurrentValue);
        }
        
        if (triggerCurrentValue != triggerTarget)
        { 
            triggerCurrentValue = Mathf.MoveTowards(triggerCurrentValue, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat("Trigger", triggerCurrentValue);
        }
    }
}
