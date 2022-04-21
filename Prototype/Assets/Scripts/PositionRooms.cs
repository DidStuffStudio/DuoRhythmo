using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using UnityEngine;

[ExecuteInEditMode]

public class PositionRooms : MonoBehaviour
{
    public int _numberOfRooms;
    [SerializeField] private float radius;


    void Update()
    {
        if (!Application.isPlaying)
        {
            
            for (var index = 0; index < _numberOfRooms; index++)
            {
                var angleInDegrees = index * (-360 / _numberOfRooms);
                float x = (float) (radius * Mathf.Cos((angleInDegrees + 90) * Mathf.PI / 180F));
                float y = (float) (radius * Mathf.Sin((angleInDegrees + 90) * Mathf.PI / 180F));
                transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            }
        }

    }

    void OnEnable()
    {
        for (var index = 0; index < _numberOfRooms; index++)
        {
            var angleInDegrees = index*(-360/_numberOfRooms);
            float x = (float)(radius * Mathf.Cos((angleInDegrees+90) * Mathf.PI / 180F));
            float y = (float)(radius * Mathf.Sin((angleInDegrees+90) * Mathf.PI / 180F));
            transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
        }
        }
    }

