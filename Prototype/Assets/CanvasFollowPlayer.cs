using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class CanvasFollowPlayer : MonoBehaviour
{
    private Transform _cam;
    
    void Start()
    {
        _cam = Camera.main.transform;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _cam.position;
        transform.rotation = _cam.rotation;
    }
}
