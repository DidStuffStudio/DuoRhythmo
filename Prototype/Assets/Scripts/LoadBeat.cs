using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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


    // Get the list of files from persistent data path and create toggles for them
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
        
        template.SetActive(false);
    }

    // The process of creating toggles
    public void CreateNewToggle(string saveFileName)
    {
        GameObject beat = Instantiate(template, listWindow);
        ToggleSaveData saveData = beat.GetComponent<ToggleSaveData>();
        saveData.fileName = saveFileName;

        // Get the saved screenshot from the save file
        byte[] bytes = File.ReadAllBytes(saveFileName.Substring(0, saveFileName.IndexOf('.')) + ".png");
        Texture2D texture = new Texture2D(480, 270);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0,0,480, 270), new Vector2(0.5f,0.0f), 1.0f);
        saveData.image.sprite = sprite;

        DateTime dateTime = File.GetCreationTime(saveFileName);
        string saveFileCreationDate = dateTime.ToString("ddd d MMM HH:mm");
        
        // Format the save file name so it looks nicer
        string formattedFileName = saveFileName.Substring(saveFileName.IndexOf("DuoRhythmo")+10);
        formattedFileName = formattedFileName.Replace(@"\","");
        formattedFileName = formattedFileName.Replace("_", ":");
        formattedFileName = formattedFileName.Substring(0,formattedFileName.IndexOf('.'));
        formattedFileName = formattedFileName.Substring(0, formattedFileName.IndexOf(':') - 2);

        saveData.DateCreated.text = saveFileCreationDate;
        saveData.label.text = formattedFileName;
    }
    
    // Somewhat self-explanitory
    public void LoadData()
    {
        _loadedManagerData = GetData(currentlySelectedSaveFile);
        StartCoroutine(SetValues());
    }
    
    //Method that returns a master manager data structure
    public MasterManagerData GetData(string file)
    {
        string json = System.IO.File.ReadAllText(file);
        MasterManagerData data = JsonUtility.FromJson<MasterManagerData>(json);
        //Debug.Log(data.nodeManagersData[0].drumType);
        return data;
    }

    // Set all the values loaded to the canvas 
    public IEnumerator SetValues()
    {
        // Wait for duorythmo to fully load (strange things will occur if you don't wait)
        yield return new WaitForSeconds(1);
        MasterManager masterManager = MasterManager.Instance;

        List<NodeManager> nodeManagers = new List<NodeManager>();
        masterManager.bpm = _loadedManagerData.BPM;
        
        // Same as in SaveIntoJson just in reverse
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
                nodeManagers[i].sliders[j].SetCurrentValue(_loadedManagerData.nodeManagersData[i].sliderValues[j]);
            }
        }

        yield return null;
    }

    // Method for saving the currently selected save file string
    public void ChangeCurrentlySelectedFile()
    {
        currentlySelectedSaveFile =
            toggleGroup.GetFirstActiveToggle().gameObject.GetComponent<ToggleSaveData>().fileName;
    }
}

// <3
