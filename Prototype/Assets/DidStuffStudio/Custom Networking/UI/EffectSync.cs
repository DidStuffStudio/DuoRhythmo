using Mirror;

namespace DidStuffLab {
    public class EffectSync : CustomSyncBehaviour<byte> {
        private DidStuffSliderKnob _didStuffSliderKnob;

        protected override void Initialize() {
            _didStuffSliderKnob = GetComponentInParent<DidStuffSliderKnob>();
            _didStuffSliderKnob.SetEffectSync(this);
            ChangeValue(_didStuffSliderKnob.CurrentValue);
        }

        public void ChangeValue(byte newValue) {
            if (Value.Value == newValue) return;
            SendToServer(newValue);
        }

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(byte newValue) => Value.Value = newValue;

        protected override void UpdateValueLocally(byte newValue) {
            base.UpdateValueLocally(newValue);
            // print("Value has changed from the server: " + newValue);
            _didStuffSliderKnob.SetValueFromServer(newValue);
        }
    }
}