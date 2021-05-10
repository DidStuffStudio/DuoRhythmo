using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyDwell : MonoBehaviour
{
    private static DontDestroyDwell _instance;
    public float dwellTimeSpeed = 100.0f;
    public static DontDestroyDwell Instance {
        get {
            if (_instance != null)return _instance;
            var dontDestroyGameObject = new GameObject();
            _instance = dontDestroyGameObject.AddComponent<DontDestroyDwell>();
            dontDestroyGameObject.name = typeof(DontDestroyDwell).ToString();
            return _instance;
        }
    }
    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
