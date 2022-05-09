using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private List<string> samplesPath;
        private Dictionary<int, AudioClip[]> _sampleDictionary = new Dictionary<int, AudioClip[]>();
        [SerializeField] private AudioMixer audioMixer;
        public List<AudioMixerGroup> mixerGroups;

        public List<AudioClip> currentClips = new List<AudioClip>(5);

        public Dictionary<int, AudioClip[]> SampleDictionary => _sampleDictionary;


        private void Awake()
        {
            LoadAudioSamples();
            foreach(KeyValuePair<int, AudioClip[]> pair in _sampleDictionary)
            {
                //Use pair.Key to get the key
                //Use pair.Value for value
                
                
                foreach (var v in pair.Value)
                {
                    print(pair.Key + " is paired with " + v);
                }
            }
        }

        private void LoadAudioSamples()
        {
            for (var index = 0; index < samplesPath.Count; index++)
            {
                var kit = samplesPath[index];
                var dir = new DirectoryInfo(kit);
                var info = dir.GetFiles("*.wav");
                var files = info.Select(f => f.Name).ToArray();
                _sampleDictionary.Add(index,files.Select(sample => samplesPath[index] + "/" + sample).
                    Select(path => (AudioClip) AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip))).ToArray());
            }
        }

        private void MuteOthers(int drumIndex)
        {
            for (int i = 0; i < mixerGroups.Count; i++)
            {
                if(i != drumIndex) mixerGroups[i].
            }
            
        }
        


    }
}
