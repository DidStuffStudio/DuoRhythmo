using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad _instance;
    
    public static DontDestroyOnLoad Instance {
        get {
            if (_instance != null)return _instance;
            var dontDestroyGameObject = new GameObject();
            _instance = dontDestroyGameObject.AddComponent<DontDestroyOnLoad>();
            dontDestroyGameObject.name = typeof(DontDestroyOnLoad).ToString();
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
