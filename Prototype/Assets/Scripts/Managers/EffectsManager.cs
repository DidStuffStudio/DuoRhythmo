using System;
using System.Collections.Generic;
using System.Linq;
using Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;

namespace Managers
{
    public class EffectsManager : MonoBehaviour
    {
        public DrumType drumType;
        private List<DidStuffSliderKnob> sliders = new List<DidStuffSliderKnob>();


        private void Awake()
        {
            sliders = GetComponentsInChildren<DidStuffSliderKnob>().ToList();
        }

        public void SendEffectToAudioManager(int sliderIndex,float value)
        {
            if(sliderIndex != 0)MasterManager.Instance.audioManager.SetEffect((int)drumType , sliderIndex, value);
            else
            {
                var newBpm = (int)Map(value, 0, 101, 40, 200);
                MasterManager.Instance.SetBpm(newBpm);
            }
        }

        public void InitialiseSliders()
        {
            foreach (var slider in sliders)slider.InitialiseSlider();
        }
        public void SetColours(Color activeColour, Color inactiveColour)
        {
            foreach (var slider in sliders)
            {
                if(slider.isBpmSlider) continue;
                slider.SetColors(activeColour, inactiveColour);
            }
        }
        
        private float Map(float value, float min1, float max1, float min2, float max2) {
            return min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
        }

    }
}
