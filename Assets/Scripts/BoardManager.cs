using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject[] _boardObjects;
    private MeshRenderer[] _renderers;
    
    [SerializeField] private Material boardOnMaterial;
    [SerializeField] private Material boardDefault;

    public void HighlightAllBoardObjects()
    {
        foreach (var obj in _boardObjects)
        {
            obj.GetComponent<MeshRenderer>().material = boardOnMaterial;
        }
    }
    
    public void UnhighlightAllBoardObjects()
    {
        foreach (var obj in _boardObjects)
        {
            obj.GetComponent<MeshRenderer>().material = boardDefault;
        }
    }
}
