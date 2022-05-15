using System;
using System.Collections.Generic;
using System.Linq;
using Custom_Buttons.Did_Stuff_Buttons;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class EffectsManager : MonoBehaviour
    {
        public DrumType drumType;
        private List<DidStuffSliderKnob> sliders = new List<DidStuffSliderKnob>();
        private DidStuffSoloButton _soloButton;
        [SerializeField] private TextMeshProUGUI panelTitle;
        [SerializeField] private List<AbstractDidStuffButton> colorCodedButtons = new List<AbstractDidStuffButton>();

        private Color drumColor { get; set; }
        private Color defaultColor { get; set; }

        private void Awake()
        {
            sliders = GetComponentsInChildren<DidStuffSliderKnob>().ToList();
            _soloButton = GetComponentInChildren<DidStuffSoloButton>();
            GetComponentInParent<Canvas>().worldCamera = Camera.main;
        }

        public void SendEffectToAudioManager(int sliderIndex, float value)
        {
            if (sliderIndex != 0) MasterManager.Instance.audioManager.SetEffect((int) drumType, sliderIndex, value);
            else MasterManager.Instance.SetBpm((int) value, this);
        }

        public void InitialisePanel(int i, Color defaultCol, Color activeCol, float bpm, int numberOfInstruments)
        {
            drumType = (DrumType) i;
            drumColor = activeCol;
            defaultColor = defaultCol;
            SetUpNamesAndColours();
            InitialiseSliders();
            InitialiseSoloButton();
            if (i == numberOfInstruments - 1) InitialiseBpm(bpm);
        }

        private void SetUpNamesAndColours()
        {
            panelTitle.color = drumColor;
            panelTitle.text = drumType + " Effects";
            foreach (AbstractDidStuffButton btn in colorCodedButtons)
                btn.SetActiveColoursExplicit(drumColor, defaultColor);
            foreach (var slider in sliders)
            {
                if (slider.isBpmSlider) continue;
                slider.SetColors(drumColor, defaultColor);
            }
        }

        public void ForceSoloOff() => _soloButton.ForceDeactivate();

        public void SetBpmSlider(int value)
        {
            sliders[0].SetCurrentValue(value);
        }

        private void InitialiseSoloButton() => _soloButton.drumTypeIndex = (int) drumType;

        private void InitialiseBpm(float value)
        {
            sliders[0].InitialiseBpm(value);
        }

        private void InitialiseSliders()
        {
            foreach (var slider in sliders) slider.InitialiseSlider();
        }

        private float Map(float value, float min1, float max1, float min2, float max2)
        {
            return min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
        }
    }
}