using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class SoundFilesManager : MonoBehaviour {
    // contains and loads all the different sounds from the Resources folder

    private AudioSource _audioSource;

    // load all the resources (sound files) for the specified type of drumkit
    public static void GetDrumKit(string name) { }

    public List<List<AudioClip>> DrumKits;

    public AudioClip[] FunkAudioClips;
    public AudioClip[] JazzAudioClips;

    private void Start() {
        // GetAllAudioClipsInResourcesFolder();
        _audioSource = GetComponent<AudioSource>();
        FunkAudioClips = GetAtPath<AudioClip>("Audio/Funky Drums Samples/Funk Drums");
        JazzAudioClips = GetAtPath<AudioClip>("Audio/Funky Drums Samples/Jazz Drums");
        // _audioSource.clip = AudioClips[0];
        // _audioSource.Play();
    }

    private List<GameObject> GetAllAudioClipsInResourcesFolder() {
        List<GameObject> objectsInScene = new List<GameObject>();

        int i = 0;
        foreach (AudioClip audioClip in (AudioClip[]) Resources.FindObjectsOfTypeAll(typeof(AudioClip))) {
            Debug.Log(audioClip.name + "  " + i);
            i++;
        }

        return objectsInScene;
    }

    private static T [] GetAtPath<T>(string path) {
        List<Object> al = new List<Object>();
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path);

        foreach (string fileName in fileEntries) {
            int assetPathIndex = fileName.IndexOf("Assets", StringComparison.Ordinal);
            string localPath = fileName.Substring(assetPathIndex);

            Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));

            if (t != null) al.Add(t);
        }

        T[] result = new T[al.Count];
        for (int i = 0; i < al.Count; i++) {
            result[i] = (T) al[i];
        }

        return result;
    }
}