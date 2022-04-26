using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform transform;
    public Transform lookAt;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAt);
        transform.rotation *= Quaternion.Euler(0, 180, -0);
    }
}
