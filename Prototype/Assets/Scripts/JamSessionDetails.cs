using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class JamSessionDetails : MonoBehaviour
{
    private static JamSessionDetails _instance;

    public static JamSessionDetails Instance {
        get {
            if (_instance != null) return _instance;
            var JamSessionDetailsGO = new GameObject();
            _instance = JamSessionDetailsGO.AddComponent<JamSessionDetails>();
            JamSessionDetailsGO.name = typeof(JamSessionDetails).ToString();
            return _instance;
        }
    }

    public int DrumTypeIndex { get; set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}
