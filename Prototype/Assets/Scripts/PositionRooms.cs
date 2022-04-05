using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionRooms : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    [SerializeField] private float radius;


    void OnEnable()
    {

        var oneOpButs = GetComponentsInChildren<OneOptionButton>();
        for (int i = 0; i < numberOfRooms ; i++)
        {
            var newArray = oneOpButs;
            var newArrayCopy = newArray.ToList();
            newArrayCopy.Remove(oneOpButs[i]);
            oneOpButs[i].otherButtonsToDisable = newArrayCopy.ToArray();
            
        }
      
        
        
        for (var index = 0; index < numberOfRooms; index++)
        {
            var angleInDegrees = index*(-360/numberOfRooms);
            float x = (float)(radius * Mathf.Cos((angleInDegrees+90) * Mathf.PI / 180F));
            float y = (float)(radius * Mathf.Sin((angleInDegrees+90) * Mathf.PI / 180F));
            transform.GetChild(index + 2).GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
        }
    }
}
