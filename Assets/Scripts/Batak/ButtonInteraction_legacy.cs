
using UnityEngine;


public class ButtonInteractionOLD : MonoBehaviour
{

    private Animator m_animator;
    [SerializeField] private MeshRenderer mesh;
    private AudioSource m_pressedAudio;
    [SerializeField] private Light lightEnabled;
    private bool active;

    [Range(0, 1)] public float HapticIntensity;
    [Range(0, 5)] public float HapticDuration;
    
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_pressedAudio = GetComponent<AudioSource>();
        active = false;
    
    }
    
    private void OnTriggerEnter(Collider collider)
    {
    
        // switch (collider.name)
        // {
        //     case "LeftHandCollider":
        //         left.SendHapticImpulse(HapticIntensity, HapticDuration);
        //         break;
        //     case "RightHandCollider":
        //         right.SendHapticImpulse(HapticIntensity, HapticDuration);
        //         break;
        // }
    
        m_pressedAudio.Play();
        m_animator.SetTrigger("Pressed");
    
        if (active)
            DeActivateButton();
        else
            ActivateButton();
    
        active = !active;
    }
    
    public void ActivateButton()
    {
        mesh.material.color = Color.green;
        lightEnabled.enabled = true;
    }

    void DeActivateButton()
    {
        mesh.material.color = Color.red;
        lightEnabled.enabled = false;
    }
}
