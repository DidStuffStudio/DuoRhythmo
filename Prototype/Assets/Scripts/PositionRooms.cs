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
            var angleInDegrees = index*(-360/numberOfRooms);
            float x = (float)(radius * Mathf.Cos((angleInDegrees+90) * Mathf.PI / 180F));
            float y = (float)(radius * Mathf.Sin((angleInDegrees+90) * Mathf.PI / 180F));
            transform.GetChild(index + 2).GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
        }
    }
}
