using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    
    // speed
    private Vector3 velocity = Vector3.zero;
    public float maxSpeed;
    public float smoothTime;

    // position
    public GameObject targetPos;
    private Transform _startpos;
    private Transform _transform;
    private Vector3 incrementedPos;

    private Vector3 activePos;

    public SineWave sineWave;
    
    private bool active;

    public GameStateManager _gameStateManager;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _startpos = _transform;
        active = false;
        sineWave.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        active = _gameStateManager.GetCurrentGame() != GameStateManager.Games.None;
        

        activePos = active ? targetPos.transform.position : _startpos.transform.position;

        if (Vector3.Distance(_transform.position, activePos) > 0.01f && !sineWave.enabled)
        {
            incrementedPos = Vector3.SmoothDamp(_transform.position, activePos, ref velocity, smoothTime, maxSpeed, Time.deltaTime);
            _transform.position = incrementedPos;
        }
        else
        {
            sineWave.enabled = true;
        }

        if (_gameStateManager.GetCurrentGame() == GameStateManager.Games.None)
            sineWave.enabled = false;


    }
}
