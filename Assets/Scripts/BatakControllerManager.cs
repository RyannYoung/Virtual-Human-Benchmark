using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using XRController = UnityEngine.InputSystem.XR.XRController;

public class BatakControllerManager : MonoBehaviour
{
    public XRBaseController lefthand;
    public XRBaseController righthand;

    public float hapticIntensity;
    public float hapticDuration;
    
    public void SendHaptic(XRBaseController controller)
    {
        controller.SendHapticImpulse(hapticIntensity, hapticDuration);
    }
    
    public void SendHaptic(XRBaseController controller, float intensity, float duration)
    {
        controller.SendHapticImpulse(intensity, duration);
    }

    public void SendHapticAll()
    {
        lefthand.SendHapticImpulse(hapticIntensity, hapticDuration);
        righthand.SendHapticImpulse(hapticIntensity, hapticDuration);
    }
    
    public void SendHapticAll(float intensity, float duration)
    {
        lefthand.SendHapticImpulse(intensity, duration);
        righthand.SendHapticImpulse(intensity, duration);
    }
    
    
}
