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
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + currentDate + "SavedBeat.json",masterManager);
    }

    public MasterManagerData GetData(string json)
    {
        MasterManagerData data = JsonUtility.FromJson<MasterManagerData>(json);
        Debug.Log(data.nodeManagersData[0].drumType);
        return data;
        
    }
    
    public string ReplaceInvalidChars(string filename)
    {
        return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));    
    }
}

[System.Serializable]
public class MasterManagerData
{
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