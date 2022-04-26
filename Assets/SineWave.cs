using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SineWave : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    public float phase;

    public Vector3 rotation;

    private float time;
    private float originalPosY;

    private Transform _transform;
    
    // Update is called once per frame
    private void Start()
    {
        _transform = GetComponent<Transform>();
        originalPosY = _transform.position.y;
    }

    private void FixedUpdate()
    {
        var sine = amplitude * Math.Sin(2 * Math.PI * frequency * Time.time * 2 + phase);
        var pos = transform.position;
        pos = new Vector3(pos.x, originalPosY + (float)sine, pos.z);
        transform.position = pos;
        
        transform.Rotate(rotation);
    }
}
