using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRooms : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    [SerializeField] private float radius;


    void Start()
    {
        
        for (var index = 0; index < numberOfRooms; index++)
        {
            var angleInDegrees = index*(360/numberOfRooms);
            float x = (float)(radius * Mathf.Cos((angleInDegrees * Mathf.PI / 180F)+Mathf.PI/2));
            float y = (float)(radius * Mathf.Sin((angleInDegrees * Mathf.PI / 180F)+Mathf.PI/2));

            print("x = " + x + ", y = " + y);
        }
    }
}
