using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuclideanManager : MonoBehaviour
{
    public int numberOfNodes = 4;
    public float radius = 3.0f;
    public GameObject nodePrefab;
    private GameObject[] _nodes;
    
    private void Start()
    {
        SpawnNodes();
    }

    public void SpawnNodes()
    {
        _nodes = new GameObject[numberOfNodes];
        
        for (int i = 0; i < numberOfNodes; i++)
        {
            //var radians = Mathf.Deg2Rad * (i * (360 / numberOfNodes));
            var radians = 2 * Mathf.PI / numberOfNodes * i;
            var y = Mathf.Sin(radians);
            var x = Mathf.Cos(radians);
            var spawnPos = new Vector2(x,y) * radius;
            
            var node = Instantiate(nodePrefab, transform) as GameObject;

            var rt = node.GetComponent<RectTransform>();
            
            rt.rotation = Quaternion.Euler(0,0,i * (360 / numberOfNodes) - 90);
            //rt.pivot = new Vector2(0.5f,0.5f);
            rt.anchoredPosition = spawnPos;
         
            rt.localScale = Vector3.one;
            
            _nodes.SetValue(node, i);

        }
        
        

        

    }

}
