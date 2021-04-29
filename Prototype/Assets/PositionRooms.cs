using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRooms : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    [SerializeField] private float radius;


    void Start()
    {
        
        for (var index = 0; index < 10; index++)
        {
            var angleInDegrees = index*36;
            float x = (float)(radius * Mathf.Cos(angleInDegrees * Mathf.PI / 180F));
            float y = (float)(radius * Mathf.Sin(angleInDegrees * Mathf.PI / 180F));

            print("x = " + x + ", y = " + y);
        }
    }
}
