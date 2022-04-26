using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{

    private ParticleSystem _particle;
    public GameObject bird;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision occured");
        Explode();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        _particle.Play();
        Destroy(bird, 0.5f);
    }
}
