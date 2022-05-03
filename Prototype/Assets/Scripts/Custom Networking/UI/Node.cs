using ctsalidis;
using UnityEngine;
using Mirror;

namespace ctsalidis {
    public class Node : CustomSyncBehaviour<bool> {
        private Material _material;
        private readonly Color _activeColor = Color.green, _inactiveColor = Color.gray;

        private void OnMouseOver() {
            // if it's active already, then return - otherwise, update on the server
            if (Value.Value) return;
            SendToServer(true);
        }

        private void OnMouseExit() {
            // if it's already not active, then return - otherwise, update on the server
            if (!Value.Value) return;
            SendToServer(false);
        }

        protected override void Initialize() => _material = GetComponent<MeshRenderer>().material;

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(bool newValue) => Value.Value = newValue;

        protected override void UpdateValue(bool newValue) {
            base.UpdateValue(newValue);
            _material.color = Value.Value ? _activeColor : _inactiveColor;
        }
    }
}