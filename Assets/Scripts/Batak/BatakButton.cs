using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;


// This script handles a button interaction between the object and 
// VR camera controller. 
// Created by: Ryan Young - 28/03/2022
public class BatakButton : MonoBehaviour
{
    private Animator m_animator;
    private AudioSource m_pressedAudio;
    private bool active;
    public bool canActivate;
    private float pressedCooldown;
    private float lastPressed;

    [SerializeField] private ButtonManager _buttonManager;
    
    
    [SerializeField] private ParticleSystem _pressedParticle;
    
    [Header("Haptic Properties")]
    [Range(0, 1)] public float hapticIntensity;
    [Range(0, 5)] public float hapticDuration;

    [Header("Batak Button Properties")]
    [SerializeField] private Material buttonOnMaterial;
    [SerializeField] private Material buttonOffMaterial;
    private MeshRenderer buttonMeshRenderer;
    
    [SerializeField] private Component buttonSwitch;

    [Header("Events")]
    public UnityEvent onActivated;
    public UnityEvent onDeActivated;
    public UnityEvent onPressed;

    public bool isButtonActive;
    
    private void Start()
    {
        InitButton();
    }

    private void InitButton()
    {
        buttonMeshRenderer = buttonSwitch.GetComponent<MeshRenderer>();
        m_animator = GetComponent<Animator>();
        m_pressedAudio = GetComponent<AudioSource>();
        pressedCooldown = 1f;
        lastPressed = 0f;
        active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        SendHaptic(other.GetComponent<XRBaseController>(), hapticIntensity, hapticDuration);
        
        ButtonPressed();
    }

    public void ButtonPressed()
    {
        Debug.Log("Button Pressed");


        // prevent double pressing
        
        Debug.Log($"Last pressed: {lastPressed}");
        
        if (lastPressed + pressedCooldown > Time.time)
        {
            Debug.Log("Preventing double tap");
            return;
        }
            

        _pressedParticle.Play();
        m_pressedAudio.pitch = Random.Range(1f, 1.5f);
        
        m_pressedAudio.Play();
        m_animator.SetTrigger("Pressed");
        
        lastPressed = Time.time;

        // Change the model and colour of the button
        if(active)
            DeActivateButton();
        else
            ActivateButton();
        
        onPressed.Invoke();
        
        active = !active;
    }

    public void SendHaptic(XRBaseController controller, float intensity, float duration)
    {
        controller.SendHapticImpulse(intensity, duration);
    }

    virtual public void ActivateButton()
    {
        if (!canActivate)
            return;

        if (isButtonActive)
            return;

        onActivated.Invoke();
        Debug.Log("invoking button" + this);
        isButtonActive = true;
        
        buttonMeshRenderer.material = buttonOnMaterial;
    }

    public void DeActivateButton()
    {
        if (!isButtonActive)
            return;
        
        onDeActivated.Invoke();
        isButtonActive = false;
        buttonMeshRenderer.material = buttonOffMaterial;
    }
}
