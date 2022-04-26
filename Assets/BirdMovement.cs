using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float speed;
    public Vector3 scaleFactor;

    private Transform _transform;
    private Vector3 newPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _transform.localScale = scaleFactor;
        newPosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z + (speed * Time.deltaTime));
    }
}
