using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR;
using UnityEngine.XR;

public class HMBInfoManager : MonoBehaviour
{
    private bool _deviceActive;
    private string _loadedDeviceName;
    
    // Start is called before the first frame update
    void Start()
    {
        _deviceActive = XRSettings.isDeviceActive;
        _loadedDeviceName = XRSettings.loadedDeviceName;
        
        Debug.Log("Device Active: " + _deviceActive);
        Debug.Log("Device name: " + _loadedDeviceName);
        
        if(!_deviceActive)
            Debug.Log("No device detected! Plug in headset!");
        
        if(_deviceActive && (_loadedDeviceName == "Mock HMD" || _loadedDeviceName == "MockHMDDisplay"))
            Debug.Log("Virtual Device");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
