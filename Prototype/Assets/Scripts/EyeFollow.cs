using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    [SerializeField] private Camera Camera;

    void Start () {

    }
     
    void Update ()
   {
       
      
       Vector3 mousePos = Input.mousePosition;
       mousePos.z = Camera.nearClipPlane;
       Vector3 mouseWorld= Camera.ScreenToWorldPoint(mousePos);
       transform.LookAt(mouseWorld);
     





   }
}

