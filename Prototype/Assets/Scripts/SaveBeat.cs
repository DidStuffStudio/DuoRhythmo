using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveBeat : MonoBehaviour
{
    [SerializeField] private MasterManagerData _masterManagerData = new MasterManagerData();




    public List<NodeManager> nodeManagers = new List<NodeManager>();
    

    public void SaveIntoJson()
    {
        int currentDrumIndex = MasterManager.Instance.currentDrumKitIndex;
        _masterManagerData.drumIndex = currentDrumIndex;
        _masterManagerData.BPM = MasterManager.Instance.bpm;
        
        for (int i = 0; i < MasterManager.Instance._nodeManagers.Count; i++)
        {
            nodeManagers.Add(MasterManager.Instance._nodeManagers[i]);
        }

        for (int i = 0; i < nodeManagers.Count; i++)
        {
            
            _masterManagerData.nodeManagersData.Add(new NodeManagerData());
            _masterManagerData.nodeManagersData[i].drumType = nodeManagers[i].drumType;
            
            for (int j = 0; j < nodeManagers[i]._nodes.Count; j++)
            {
                _masterManagerData.nodeManagersData[i].nodesData.Add(new NodeData());
                _masterManagerData.nodeManagersData[i].nodesData[j].isActive = nodeManagers[i]._nodes[j].IsActive;
            }

            for (int j = 0; j < nodeManagers[i].sliders.Length; j++)
            {
                _masterManagerData.nodeManagersData[i].sliderValues.Add(new float());
                _masterManagerData.nodeManagersData[i].sliderValues[j] = nodeManagers[i].sliders[j].currentValue;
            }
        }
        
        string masterManager = JsonUtility.ToJson(_masterManagerData, prettyPrint:true);
        string currentDate = System.DateTime.Now.ToString();
        currentDate = ReplaceInvalidChars(currentDate);
        string savedBeatName = MasterManager.Instance.drumKitNames[currentDrumIndex] + " " + currentDate + ".json";
        System.IO.File.WriteAllText(Application.persistentDataPath + Path.AltDirectorySeparatorChar + savedBeatName,masterManager);
        
        Debug.Log("Beat saved successfully! file name is: " + savedBeatName);
    }


    
    public string ReplaceInvalidChars(string name)
    {
        return string.Join("_", name.Split(Path.GetInvalidFileNameChars()));    
    }
}



[System.Serializable]
public class MasterManagerData
{
    public int drumIndex;
    public int BPM;
    public List<NodeManagerData> nodeManagersData = new List<NodeManagerData>();
}

[System.Serializable]
public class NodeManagerData
{
    public DrumType drumType;
    public List<NodeData> nodesData = new List<NodeData>();
    public List<float> sliderValues = new List<float>();
}

[System.Serializable]
public class NodeData
{
    public bool isActive;
}