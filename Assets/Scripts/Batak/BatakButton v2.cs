using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BatakButtonPhysics : MonoBehaviour
{

    [Header("Button Parts")]
    public Transform buttonSwitch;
    [SerializeField] private Material btnSwitchMatOn;
    [SerializeField] private Material btnSwitchMatOff;
    private MeshRenderer btnSwitchRenderer;

    [Header("Batak Properties")] 
    private bool buttonOn;
    [SerializeField] private Color OnColor;
    [SerializeField] private Color OffColor;

    
    [Header("Button Physics")]
    public Rigidbody buttonSwitchRigid;
    public Transform buttonLowerLimit;
    public Transform buttonUpperLimit;
    public float threshHold;
    public float force = 10;
    private float upperLowerDiff;
    public bool isPressed;
    private bool prevPressedState;
    public AudioSource pressedSound;
    public AudioSource releasedSound;
    public Collider[] CollidersToIgnore;
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    // Start is called before the first frame update
    void Start()
    {
        InitButton();
        InitButtonPhysics();
        
    }

    private void InitButton()
    {
        btnSwitchRenderer = buttonSwitch.GetComponent<MeshRenderer>();
        btnSwitchRenderer.material = btnSwitchMatOff;
        buttonOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonPhysics();
    }

    void InitButtonPhysics()
    {
        Collider localCollider = GetComponent<Collider>();
        if (localCollider != null)
        {
            Physics.IgnoreCollision(localCollider, buttonSwitch.GetComponentInChildren<Collider>());

            foreach (Collider singleCollider in CollidersToIgnore)
            {
                Physics.IgnoreCollision(localCollider, singleCollider);
            }
        }
        
        if (transform.eulerAngles != Vector3.zero){
            Vector3 savedAngle = transform.eulerAngles;
            transform.eulerAngles = Vector3.zero;
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
            transform.eulerAngles = savedAngle;
        }
        else
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
    }

    public void ToggleButton()
    {
        buttonOn = !buttonOn;
        
        if (buttonOn)
            ActivateButton();
        else
            DeActivateButton();
    }

    public void ActivateButton()
    {
        btnSwitchRenderer.material = btnSwitchMatOn;
    }

    public void DeActivateButton()
    {
        btnSwitchRenderer.material = btnSwitchMatOff;
    }

    void UpdateButtonPhysics()
    {
        buttonSwitch.transform.localPosition = new Vector3(0, buttonSwitch.transform.localPosition.y, 0);
        buttonSwitch.transform.localEulerAngles = new Vector3(0, 0, 0);
        if (buttonSwitch.localPosition.y >= 0)
            buttonSwitch.transform.position = new Vector3(buttonUpperLimit.position.x, buttonUpperLimit.position.y, buttonUpperLimit.position.z);
        else
            buttonSwitchRigid.AddForce(buttonSwitch.transform.up * force * Time.deltaTime);

        if (buttonSwitch.localPosition.y <= buttonLowerLimit.localPosition.y)
            buttonSwitch.transform.position = new Vector3(buttonLowerLimit.position.x, buttonLowerLimit.position.y, buttonLowerLimit.position.z);


        if (Vector3.Distance(buttonSwitch.position, buttonLowerLimit.position) < upperLowerDiff * threshHold)
            isPressed = true;
        else
            isPressed = false;

        if(isPressed && prevPressedState != isPressed)
            Pressed();
        if(!isPressed && prevPressedState != isPressed)
            Released();
    }

    void Pressed(){
        prevPressedState = isPressed;
        pressedSound.pitch = 1;
        pressedSound.Play();
        onPressed.Invoke();
    }

    void Released(){
        prevPressedState = isPressed;
        releasedSound.pitch = Random.Range(1.1f, 1.2f);
        releasedSound.Play();
        onReleased.Invoke();
    }
}