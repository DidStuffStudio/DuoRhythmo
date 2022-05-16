using System;
using ctsalidis;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace ctsalidis {
    public class EffectSync : CustomSyncBehaviour<byte> {
        private DidStuffSliderKnob _didStuffSliderKnob;
        
        protected override void Initialize() {
            _didStuffSliderKnob = GetComponentInParent<DidStuffSliderKnob>();
            _didStuffSliderKnob.SetEffectSync(this);
            
        }

        public void ChangeValue(byte newValue) {
            if (Value.Value != newValue) SendToServer(newValue);
        }
        
        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(byte newValue) => Value.Value = newValue;

        protected override void UpdateValue(byte newValue) {
            base.UpdateValue(newValue);
            // print("Value has changed from the server: " + newValue);
            _didStuffSliderKnob.SetValueFromServer(newValue);
        }
    }
}