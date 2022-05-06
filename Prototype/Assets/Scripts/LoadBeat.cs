using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadBeat : MonoBehaviour
{
    public GameObject template;
    public Transform listWindow;

    public string currentlySelectedSaveFile;
    public ToggleGroup toggleGroup;

    public List<GameObject> retrievedBeats = new List<GameObject>();


    public List<string> fileList = new List<string>();
    [SerializeField] private MasterManagerData _loadedManagerData = new MasterManagerData();

    private void Awake()
    {
        GetFileList();
        ChangeCurrentlySelectedFile();
    }



    public void GetFileList()
    {
        //Clear the file list so we dont have any duplicates
        fileList.Clear();
        
        // Get all files that have any number of characters infront of .json
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.json");

        //Add each file to the fileList
        foreach (var file in files)
        {
            fileList.Add(file);
            Debug.Log(file);
            CreateNewToggle(file);
        }
    }

    public void CreateNewToggle(string saveFileName)
    {
        GameObject beat = Instantiate(template, listWindow);
        ToggleSaveData saveData = beat.GetComponent<ToggleSaveData>();
        saveData.fileName = saveFileName;
        
        string formattedFileName = saveFileName.Substring(saveFileName.IndexOf("DuoRhythmo")+10);
        formattedFileName = formattedFileName.Replace(@"\","");
        formattedFileName = formattedFileName.Replace("_", ":");
        formattedFileName = formattedFileName.Substring(0,formattedFileName.IndexOf('.'));
        
        Debug.Log(formattedFileName);
        saveData.label.text = formattedFileName;
    }

    public void LoadData()
    {
        _loadedManagerData = GetData(currentlySelectedSaveFile);
        StartCoroutine(SetValues());
    }
    public MasterManagerData GetData(string file)
    {
        string json = System.IO.File.ReadAllText(file);
        MasterManagerData data = JsonUtility.FromJson<MasterManagerData>(json);
        //Debug.Log(data.nodeManagersData[0].drumType);
        return data;
    }

    public IEnumerator SetValues()
    {
        yield return new WaitForSeconds(1);
        MasterManager masterManager = MasterManager.Instance;

        List<NodeManager> nodeManagers = new List<NodeManager>();
        masterManager.bpm = _loadedManagerData.BPM;
        
        for (int i = 0; i < MasterManager.Instance._nodeManagers.Count; i++)
        {
            nodeManagers.Add(MasterManager.Instance._nodeManagers[i]);
        }

        for (int i = 0; i < nodeManagers.Count; i++)
        {
            for (int j = 0; j < nodeManagers[i]._nodes.Count; j++)
            {
                if (_loadedManagerData.nodeManagersData[i].nodesData[j].isActive)
                {
                    nodeManagers[i]._nodes[j].ToggleState();
                }
            }

            for (int j = 0; j < nodeManagers[i].sliders.Length; j++)
            {
                nodeManagers[i].sliders[j].currentValue = _loadedManagerData.nodeManagersData[i].sliderValues[j];
                /*_masterManagerData.nodeManagersData[i].sliderValues.Add(new float());
                _masterManagerData.nodeManagersData[i].sliderValues[j] = nodeManagers[i].sliders[j].currentValue;*/
            }
        }

        yield return null;
    }

    public void ChangeCurrentlySelectedFile()
    {
        currentlySelectedSaveFile =
            toggleGroup.GetFirstActiveToggle().gameObject.GetComponent<ToggleSaveData>().fileName;
    }
}
