using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
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
        private void Awake()
        {
            LoadAudioSamples();
            effectConstraints.Add(1,minMaxVolume);
            effectConstraints.Add(2,minMaxFlange);
            effectConstraints.Add(3,minMaxReverb);
            effectConstraints.Add(4,minMaxLowPass);
        }

        private float[] ShiftNumber(float[] values, float amount)
        {
            float[] shiftedValues = new float[values.Length];
            for (var index = 0; index < values.Length; index++)
            {
                var value = values[index] += amount;
                shiftedValues[index] = amount;
            }

            return shiftedValues;
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

        public void MuteOthers(int drumIndex)
        {
            for (int i = 0; i < mixers.Count; i++)
            {
                if (i != drumIndex) mixers[i].audioMixer.SetFloat( EffectStrings[0], -20.0f);
            }
        }

        public void SetEffect(int drumType, int effectIndex, float value)
        {
            var minMax = effectConstraints[effectIndex];
            var val = Map(value, 0, 101, minMax[0], minMax[1]);
            if (effectIndex == 4) val *= -1; // Need positive frequencies for the low pass
            mixers[drumType].audioMixer.SetFloat(EffectStrings[effectIndex-1], val);
        }
        
        
        private float Map(float value, float a, float b, float c, float d)
        {
            if (Mathf.Approximately(a, b)) return 0.0f;
            if (a < 0)
            {
                (a, b) = (b, a);

                a = Mathf.Abs(a);
                b = Mathf.Abs(b);
                return (c + ((d - c) / (b - a)) * (value - a)) * -1;
            }
            
            
            return c + ((d - c)/(b - a)) * (value - a);
        }


    }
}
