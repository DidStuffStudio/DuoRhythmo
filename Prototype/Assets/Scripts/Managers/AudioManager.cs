using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using Object = System.Object;

namespace Managers {
    public class AudioManager : MonoBehaviour {
        [SerializeField] private List<string> samplesPath;
        private Dictionary<int, AudioClip[]> _sampleDictionary = new Dictionary<int, AudioClip[]>();
        public List<AudioMixerGroup> mixers;
        public Dictionary<int, AudioClip[]> SampleDictionary => _sampleDictionary;
        private static readonly string[] EffectStrings = {"Volume", "Flange", "Reverb", "LowPass"};
        [SerializeField] private float[] minMaxVolume = {-20.0f, 0.0f};
        [SerializeField] private float[] minMaxFlange = {-80.0f, 0.0f};
        [SerializeField] private float[] minMaxReverb = {-80.0f, 0.0f};
        [SerializeField] private float[] minMaxLowPass = {-22000.0f, -150.0f};
        private Dictionary<int, float[]> effectConstraints = new Dictionary<int, float[]>();
        private float[] _volumes = new float[5];
        public bool setUp = false;

        private void Awake() {
            LoadAudioSamples();
            effectConstraints.Add(1, minMaxVolume);
            effectConstraints.Add(2, minMaxFlange);
            effectConstraints.Add(3, minMaxReverb);
            effectConstraints.Add(4, minMaxLowPass);
        }

        private float[] ShiftNumber(float[] values, float amount) {
            float[] shiftedValues = new float[values.Length];
            for (var index = 0; index < values.Length; index++) {
                var value = values[index] += amount;
                shiftedValues[index] = amount;
            }

            return shiftedValues;
        }

        private void LoadAudioSamples() {
            StartCoroutine(GetAudioClips());
            /*
            for (var i = 0; i < samplesPath.Count; i++) {
                // var audioFileEntries = GetAtPath<AudioClip>("Resources/Audio/" + samplesPath[i]);
                var audioFileEntries = GetAtPath<AudioClip>("Audio/" + samplesPath[i]);
                _sampleDictionary.Add(i, audioFileEntries);
                
                var kit = Application.dataPath + "/Resources/Audio/" + samplesPath[i];
                var dir = new DirectoryInfo(kit);
                var info = dir.GetFiles("*.wav");
                var files = info.Select(f => f.Name).ToArray();
                var samples = new AudioClip[files.Length];
                for (var j = 0; j < files.Length; j++) {
                    var p = "Audio/" + samplesPath[i] + "/" + files[j];
                    var p2 = p.Split('.')[0];
                    samples[j] = Resources.Load<AudioClip>(p2);
                }

                _sampleDictionary.Add(i, samples);
                
            }
            */
        }

        private IEnumerator GetAudioClips() {
            for (var i = 0; i < samplesPath.Count; i++) {
                var list = new List<AudioClip>();
                var p = Path.Combine(Application.streamingAssetsPath, "Audio/" + samplesPath[i]);
                var fileEntries = Directory.GetFiles(p);
                foreach (var fileName in fileEntries) {
                    if (fileName.Contains("meta")) continue;
                    var url = $"file://{fileName}";
                    using var www =
                        UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ConnectionError) {
                        Debug.LogError(www.error);
                    }
                    else {
                        var clip = DownloadHandlerAudioClip.GetContent(www);
                        list.Add(clip);
                    }
                }
                _sampleDictionary.Add(i, list.ToArray());
                print("sampleDictionary counter: " + i);
            }

            setUp = true;
        }

        /*
        private IEnumerator LoadFilesAtPath<T>(string path) where T : UnityEngine.Object {
            var list = new List<T>();
            var p = Path.Combine(Application.streamingAssetsPath, path);
            var fileEntries = Directory.GetFiles(p);
            foreach (var fileName in fileEntries) {
                if (fileName.Contains("meta")) yield break;
                string url = $"file://{fileName}";
                var www = new UnityWebRequest(url);
                yield return www;
                list.Add(www.result == UnityWebRequest.Result);
            }
            if (musicFile.Name.Contains("meta")) yield break;
            else {
                string musicFilePath = musicFile.FullName.ToString();
                string url = string.Format("file://{0}", musicFilePath);
                WWW www = new WWW(url);
                yield return www;
                musicPlayer.clip = www.GetAudioClip(false, false);
                musicPlayer.Play();
            }
        }
        */

        /// <summary>
        /// Get all files of generic type T in Application.dataPath inside the assets folder
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Array of all Objects of type T for all Assets in a folder</returns>
        ///  ref --> https://forum.unity.com/threads/how-to-get-list-of-assets-at-asset-path.18898/
        public static T[] GetAtPath<T>(string path) where T : UnityEngine.Object {
            var list = new List<T>();
            var fileEntries = Directory.GetFiles(Application.streamingAssetsPath + "/Resources/" + path);
            foreach (var fileName in fileEntries) {
                // var index = fileName.LastIndexOf("/", StringComparison.Ordinal);
                // var localPath = "Assets/" + path;
                // var localPath = path;
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                var localPath = path + "/" + fileNameWithoutExtension;
                // if (index > 0) localPath += fileNameWithoutExtension;
                // localPath += fileNameWithoutExtension;
                var t = Resources.Load<T>(Application.streamingAssetsPath + "/Resources/" + localPath);

                if (t != null) list.Add(t);
            }

            var result = new T[list.Count];
            for (var i = 0; i < list.Count; i++) result[i] = (T) list[i];

            return result;
        }

        public void MuteOthers(int drumIndex, bool solo) {
            for (int i = 0; i < mixers.Count; i++) {
                if (i != drumIndex && solo) mixers[i].audioMixer.SetFloat(EffectStrings[0], -40.0f);
                else if (!solo) mixers[i].audioMixer.SetFloat(EffectStrings[0], _volumes[i]);
            }
        }

        public void MuteAll(bool mute)
        {
            for (int i = 0; i < mixers.Count; i++) {
                if (mute) mixers[i].audioMixer.SetFloat(EffectStrings[0], -40.0f);
                else mixers[i].audioMixer.SetFloat(EffectStrings[0], _volumes[i]);
            }
        }

        public void SetEffect(int drumType, int effectIndex, float value) {
            var minMax = effectConstraints[effectIndex];
            var val = Map(value, 0, 101, minMax[0], minMax[1]);
            if (effectIndex == 4) val *= -1; // Need positive frequencies for the low pass
            mixers[drumType].audioMixer.SetFloat(EffectStrings[effectIndex - 1], val);
            if (effectIndex == 1) _volumes[drumType] = val;
        }


        private float Map(float value, float a, float b, float c, float d) {
            if (Mathf.Approximately(a, b)) return 0.0f;
            if (a < 0) {
                (a, b) = (b, a);

                a = Mathf.Abs(a);
                b = Mathf.Abs(b);
                return (c + ((d - c) / (b - a)) * (value - a)) * -1;
            }


            return c + ((d - c) / (b - a)) * (value - a);
        }
    }
}