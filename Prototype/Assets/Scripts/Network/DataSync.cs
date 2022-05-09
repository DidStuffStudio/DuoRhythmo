using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Managers;
using UnityEngine;

public class DataSync : MonoBehaviour {
    private List<float> times = new List<float>();
    public float averagedTime = 0.0f;
    public int[] conectedPlayers = new int[10];
    public void AddTime(float time) => times.Add(time);

    public int[,] nodesActivated = new int[5, 16];
    public int[,] effectValues = new int[5, 4];
    
    public void SetConnectedPlayer(int index, bool connected) {
        if (connected) conectedPlayers[index] = 1;
        else conectedPlayers[index] = 0;
    }

    public void SendNodes(int drumIndex, bool sendAll) {
        if (!sendAll) {
            var nodeString = MasterManager.Instance.localPlayerNumber + "," + drumIndex + ",";
            for (int i = 0; i < 16; i++) nodeString += nodesActivated[drumIndex, i];
            RealTimeInstance.Instance.stringSync.SetDrumNodesSingle(nodeString);
        }
        else {
            var nodeString = MasterManager.Instance.localPlayerNumber + ",";
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 16; j++) {
                    nodeString += nodesActivated[i, j];
                }

                nodeString += ",";
            }
            nodeString = nodeString.TrimEnd(',');
            RealTimeInstance.Instance.stringSync.SetDrumNodesAllDrums(nodeString);
        }
    }

    public void SendEffects(int drumIndex, bool sendAll) {
        var effectsString = "";
        if (!sendAll) {
            effectsString = MasterManager.Instance.localPlayerNumber + "," + drumIndex + ",";
            for (int i = 0; i < 4; i++) effectsString += effectValues[drumIndex, i];
        }
        else {
            effectsString = MasterManager.Instance.localPlayerNumber + ",";
            for (int i = 0; i < MasterManager.Instance.numberInstruments; i++) {
                for (int j = 0; j < 4; j++) {
                    effectsString += effectValues[i, j];
                    if (j < 3) effectsString += "-";
                }
                effectsString += ",";
                
            }
            effectsString += MasterManager.Instance.bpm;
            // effectsString = effectsString.TrimEnd(',');
        }
        RealTimeInstance.Instance.stringSync.SetEffectValues(effectsString);
    }
}