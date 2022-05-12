using System;
using System.Collections.Generic;
using System.Linq;
using Custom_Buttons.Did_Stuff_Buttons;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using UnityEngine;

namespace Managers
{
    public class EffectsManager : MonoBehaviour
    {
        public DrumType drumType;
        private List<DidStuffSliderKnob> sliders = new List<DidStuffSliderKnob>();
        private DidStuffSoloButton _soloButton;


        private void Awake()
        {
            sliders = GetComponentsInChildren<DidStuffSliderKnob>().ToList();
            _soloButton = GetComponentInChildren<DidStuffSoloButton>();
        }

        public void SendEffectToAudioManager(int sliderIndex,float value)
        {
            if(sliderIndex != 0)MasterManager.Instance.audioManager.SetEffect((int)drumType , sliderIndex, value);
            else MasterManager.Instance.SetBpm((int)value, this);
            
        }

        public void ForceSoloOff() => _soloButton.ForceDeactivate();
        
        public void SetBpmSlider(int value)
        {
            sliders[0].SetCurrentValue(value);
        }
        
        public void InitialiseSoloButton() => _soloButton.drumTypeIndex = (int)drumType;

        public void InitialiseBpm(float value)
        {
            sliders[0].InitialiseBpm(value);
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
