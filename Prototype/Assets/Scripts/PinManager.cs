using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    private String _pin;
    private List<int> pinIntegers = new List<int>();
    private LineRenderer _lineRenderer;
    private int _currentIndex = 0;
    
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPinCharacter(int value, Vector3 position)
    {
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_currentIndex, position);
        _currentIndex++;
        pinIntegers.Add(value);
    }

    public void SetPin()
    {
        _pin = pinIntegers.Select(i => i.ToString()).Aggregate((i, j) => i + j);
        print(_pin);
    }
}