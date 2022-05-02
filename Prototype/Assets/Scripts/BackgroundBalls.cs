using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BackgroundBalls : MonoBehaviour
{
    
    private float _speed = 10.0f;
    private Vector3 _navPoint;
    private float _spread;
    private Vector3 _centerPosition;
    private Material _material;
    public BackgroundBallsManager manager;


    private void Start()
    {

        _spread = manager.spread;
        var scale = Random.Range(manager.minSize, manager.maxSize);
        var transform1 = transform;
        transform1.localScale = new Vector3(scale,scale,scale);
        _centerPosition = transform1.parent.position;
        _speed = Random.Range(manager.minSpeed, manager.maxSpeed);
        _material = GetComponent<MeshRenderer>().material;
        _material.color = manager.colours[Random.Range(0, manager.colours.Count)];
        GetRandomPointInRange();
    }

    private void Update()
    {
            Movement();
    }
    
    private void Movement()
    {
        var position = transform.position;
        
        //Change fly toward point
        transform.position = Vector3.MoveTowards(position, _navPoint, Time.deltaTime*_speed);
        
        var distanceToTarget = position - _navPoint;

        if (distanceToTarget.magnitude < 0.1f) GetRandomPointInRange();
    }
    
    

    public void GetRandomPointInRange()
    {
        var point = new Vector3(
            Random.Range(_centerPosition.x-_spread/2,_centerPosition.x+_spread/2),
            Random.Range(_centerPosition.y-_spread/2,_centerPosition.y+_spread/2),
            Random.Range(_centerPosition.z-_spread/2,_centerPosition.z+_spread/2));
        _navPoint = point;
    }
    
}
  
