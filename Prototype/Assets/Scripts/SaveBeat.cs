using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveBeat : MonoBehaviour
{
    [SerializeField] private MasterManagerData _masterManagerData = new MasterManagerData();


    private string fileName;

    public List<NodeManager> nodeManagers = new List<NodeManager>();
    

    public void SaveIntoJson()
    {
        // Get general info on save data
        int currentDrumIndex = MasterManager.Instance.currentDrumKitIndex;
        _masterManagerData.drumIndex = currentDrumIndex;
        _masterManagerData.BPM = MasterManager.Instance.bpm;
        
        for (int i = 0; i < MasterManager.Instance._nodeManagers.Count; i++)
        {
            nodeManagers.Add(MasterManager.Instance._nodeManagers[i]);
        }

        for (int i = 0; i < nodeManagers.Count; i++)
        {
            
            // Get master manager and node managers
            _masterManagerData.nodeManagersData.Add(new NodeManagerData());
            _masterManagerData.nodeManagersData[i].drumType = nodeManagers[i].drumType;
            
            // Get nodes activation
            for (int j = 0; j < nodeManagers[i]._nodes.Count; j++)
            {
                _masterManagerData.nodeManagersData[i].nodesData.Add(new NodeData());
                _masterManagerData.nodeManagersData[i].nodesData[j].isActive = nodeManagers[i]._nodes[j].IsActive;
            }
            
            // Get slider values
            for (int j = 0; j < nodeManagers[i].sliders.Length; j++)
            {
                _masterManagerData.nodeManagersData[i].sliderValues.Add(new float());
                _masterManagerData.nodeManagersData[i].sliderValues[j] = nodeManagers[i].sliders[j].currentValue;
            }
        }
        
        //Create a Json file with the received data
        string masterManager = JsonUtility.ToJson(_masterManagerData, prettyPrint:true);
        string currentDate = System.DateTime.Now.ToString();
        currentDate = ReplaceInvalidChars(currentDate);
        string savedBeatName = MasterManager.Instance.drumKitNames[currentDrumIndex] + " " + currentDate + ".json";
        fileName = Application.persistentDataPath + Path.AltDirectorySeparatorChar + MasterManager.Instance.drumKitNames[currentDrumIndex] + " " + currentDate;
        System.IO.File.WriteAllText(Application.persistentDataPath + Path.AltDirectorySeparatorChar + savedBeatName,masterManager);
        StartCoroutine(TakeScreenshot());
        
        Debug.Log("Beat saved successfully! file name is: " + savedBeatName);
    }


    
    public string ReplaceInvalidChars(string name)
    {
        return string.Join("_", name.Split(Path.GetInvalidFileNameChars()));    
    }
    
    private int resWidth = 480; 
    private int resHeight = 270;

    IEnumerator TakeScreenshot()
    {
        // Take a screenshot of the current panel
        yield return new WaitForEndOfFrame();
        
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24); 
        Camera.main.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = fileName + ".png";
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        yield return null;
    }
}


// Data structure
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