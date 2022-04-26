using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{

    private AudioSource _helperAudio;
    
    
    void OnEnable()
    {
        _helperAudio = GetComponent<AudioSource>();
    }

    public void ActivateAudio()
    {
        _helperAudio.Play();
        Debug.Log("Playing audio");
    }
    
}
