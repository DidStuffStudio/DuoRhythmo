using ctsalidis;
using Mirror;
using UnityEngine.UI;

namespace ctsalidis {
    public class Effect : CustomSyncBehaviour<float> {
        private Slider _slider;


        protected override void Initialize() {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }
        
        private void ValueChangeCheck() => SendToServer(_slider.value);
        
        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(float newValue) => Value.Value = newValue;

        protected override void UpdateValue(float newValue) {
            base.UpdateValue(newValue);
            _slider.value = newValue;
        }
    }
}