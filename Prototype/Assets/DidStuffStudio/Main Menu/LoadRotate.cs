using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRotate : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float rotationAmount = 30.0f;
    private RectTransform _rectTransform;
    private float _timeLeft;
    private void Awake()
    {
        _timeLeft = 1 / speed;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_timeLeft <= Time.deltaTime) {
            _rectTransform.Rotate(Vector3.forward, -rotationAmount);
            _timeLeft = 1/speed;
        }
        else {
            // update the timer
            _timeLeft -= Time.deltaTime;
        }
    }
    
}
