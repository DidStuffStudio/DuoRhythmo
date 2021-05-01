using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public class DataSync : MonoBehaviour
{
    
    private List<float> times = new List<float>();
    public float averagedTime = 0.0f;
    public int [] conectedPlayers = new int[10];
    public void AddTime(float time) => times.Add(time);
    

    public void SetConnectedPlayer(int index, bool connected)
    {
        if (connected) conectedPlayers[index] = 1;
        else conectedPlayers[index] = 0;
    }
    

}
