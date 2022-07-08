using System.Collections;
using System.Collections.Generic;
using DidStuffLab.Scripts.Misc;
using Mirror;
using UnityEngine;

namespace DidStuffLab {
    public class PlayerFollowSync : CustomSyncBehaviour<Vector3> {
        private EyeFollow _eyeFollow;
        public void ChangeValueOnServer(Vector3 value) {
            if(Value.Value == value) return;
            SendToServer(value);
        }

        protected override void Initialize() {
            /*
            if (JamSessionDetails.Instance.otherPlayer) {
                _eyeFollow = GetComponentInChildren<EyeFollow>();
                _eyeFollow.SetFollowSync(this);
            }
            */
        }

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(Vector3 newValue) => Value.Value = newValue;

        /*
        protected override void UpdateValueLocally(Vector3 newValue) {
            base.UpdateValueLocally(newValue);
            // print("Value has changed from the server: " + newValue);
            // _eyeFollow.SetFollowPositionFromServer(newValue);
        }
        */
    }
}
