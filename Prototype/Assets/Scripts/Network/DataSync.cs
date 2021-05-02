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
    
    
    public int[,] nodesActivated = new int[5,16];
    

    public void SetConnectedPlayer(int index, bool connected)
    {
        if (connected) conectedPlayers[index] = 1;
        else conectedPlayers[index] = 0;
    }

    public void SendNodes(int drumIndex, bool sendAll)
    {
        if (!sendAll)
        {
            var nodeString = TestStringSync.MessageTypes.DRUM_NODES_SINGLE_DRUM + MasterManager.Instance.localPlayerNumber + ","  + drumIndex + ",";
            for (int i = 0; i < 16; i++) nodeString += nodesActivated[drumIndex, i];
            RealTimeInstance.Instance._testStringSync.SetMessage(nodeString);
        }
        else
        {
            var nodeString = TestStringSync.MessageTypes.DRUM_NODES_ALL_DRUM;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    nodeString += nodesActivated[i, j];
                }

                nodeString += ",";
            }
            RealTimeInstance.Instance._testStringSync.SetMessage(nodeString);
        }
    }
    

}
