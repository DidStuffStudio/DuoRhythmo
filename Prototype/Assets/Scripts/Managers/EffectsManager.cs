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
        public List<DidStuffSliderKnob> sliders = new List<DidStuffSliderKnob>();
        private DidStuffSoloButton _soloButton;
        [SerializeField] private TextMeshProUGUI panelTitle;
        [SerializeField] private List<AbstractDidStuffButton> colorCodedButtons = new List<AbstractDidStuffButton>();
        [SerializeField] private List<GameObject> voteSkipToasts = new List<GameObject>();
        [SerializeField] private DidStuffRecord recordButton;
        private Color drumColor { get; set; }
        private Color defaultColor { get; set; }
        
        private void Awake()
        {
            sliders = GetComponentsInChildren<DidStuffSliderKnob>().ToList();
            _soloButton = GetComponentInChildren<DidStuffSoloButton>();
            GetComponentInParent<Canvas>().worldCamera = Camera.main;
        }

        // private void OnEnable() => MasterManager.OnBpmChanged += SetBpmSlider;

        public void SendEffectToAudioManager(int sliderIndex, byte value) {
            if (sliderIndex != 0) MasterManager.Instance.audioManager.SetEffect((int) drumType, sliderIndex, value);
        }

        public void InitialisePanel(int i, Color defaultCol, Color activeCol, float bpm, int numberOfInstruments, string drumName)
        {
            drumType = (DrumType) i;
            drumColor = activeCol;
            defaultColor = defaultCol;
            SetUpNamesAndColours(drumName);
            InitialiseSliders();
            InitialiseSoloButton();
            if (i == numberOfInstruments - 1) InitialiseBpm(bpm);
        }

        private void SetUpNamesAndColours(string drumName)
        {
            panelTitle.color = drumColor;
            panelTitle.text = drumName + " Effects";
            foreach (AbstractDidStuffButton btn in colorCodedButtons)
                btn.SetActiveColoursExplicit(drumColor, defaultColor);
            foreach (var slider in sliders)
            {
                if (slider.isBpmSlider) continue;
                slider.SetColors(drumColor, defaultColor);
            }
        }

        public void ForceSoloOff() => _soloButton.ForceDeactivate();

        private void SetBpmSlider(byte value) {
            print("Change the bpm slider ui " + transform.parent.name);
            // sliders[0].UpdateSliderUi(); // we only want to change the UI of the slider, because the BPM is global in MasterManager
            // sliders[0].SetCurrentValue(value);
        }

        private void InitialiseSoloButton() => _soloButton.drumTypeIndex = (int) drumType;

        private void InitialiseBpm(float value)
        {
            sliders[0].InitialiseBpm((byte) value);
        }

        private void InitialiseSliders()
        {
            foreach (var slider in sliders) slider.InitialiseSlider();
        }

        public void RecordingAudio(bool recording)
        {
            if(recording) recordButton.ActivateButton();
            else recordButton.DeactivateButton();
        }
        
        private float Map(float value, float min1, float max1, float min2, float max2)
        {
            return min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
        }

        // private void OnDisable() => MasterManager.OnBpmChanged -= SetBpmSlider;
    }
}