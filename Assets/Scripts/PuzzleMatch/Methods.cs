﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Methods : MonoBehaviour
{
    private Vector2 firstPosition;
    private Vector2 finalPosition;
    
    private void Start() {
        Debug.Log("Aqu[i rto");
    }

    private void Update() {
        
    }
    void OnMouseDown() {
        firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(firstPosition);
    }

    void OnMouseUp() {
        firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(firstPosition);
    }
}