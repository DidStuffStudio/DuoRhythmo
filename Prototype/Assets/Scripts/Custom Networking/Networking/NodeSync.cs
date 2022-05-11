using Mirror;
using UnityEngine;

namespace ctsalidis {
    public class NodeSync : CustomSyncBehaviour<bool> {
        private DidStuffNode _didStuffNode;
        public void Toggle(bool value) {
            if(Value.Value == value) return;
            SendToServer(value);
        }

        protected override void Initialize() => _didStuffNode = GetComponent<DidStuffNode>();

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(bool newValue) => Value.Value = newValue;

        protected override void UpdateValue(bool newValue) {
            base.UpdateValue(newValue);
            if(newValue) _didStuffNode.SetActiveFromServer();
            else _didStuffNode.SetInactiveFromServer();
        }
    }
}