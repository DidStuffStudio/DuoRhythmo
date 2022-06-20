using Mirror;

namespace DidStuffLab {
    public class NodeSync : CustomSyncBehaviour<bool> {
        private DidStuffNode _didStuffNode;
        public void Toggle(bool value) {
            if(Value.Value == value) return;
            SendToServer(value);
        }

        protected override void Initialize() {
            _didStuffNode = GetComponentInParent<DidStuffNode>();
            _didStuffNode.SetNodeSync(this);
            Toggle(_didStuffNode.IsActive);
        }

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(bool newValue) => Value.Value = newValue;

        protected override void UpdateValueLocally(bool newValue) {
            base.UpdateValueLocally(newValue);
            // print("Value has changed from the server: " + newValue);
            if(newValue) _didStuffNode.SetActiveFromServer();
            else _didStuffNode.SetInactiveFromServer();
        }
    }
}