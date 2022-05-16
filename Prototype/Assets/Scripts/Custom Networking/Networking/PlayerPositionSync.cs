using System;
using System.Collections;
using System.Collections.Generic;
using ctsalidis;
using Mirror;
using UnityEngine;

namespace ctsalidis {
    public class PlayerPositionSync : CustomSyncBehaviour<bool> {
        public SyncVar<bool> Position1Occupied => Value.Value;

        protected override void Initialize() => JamSessionDetails.Instance.PlayerPositionSync = this;

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(bool newValue) => Value.Value = newValue;

        public void SetCorrespondingPosition() {
            // if Position1Occupied is still false on the server, then it means that there is currently no players occupying that space
            // so I'm the first player --> so occupy that position
            print("The server says that position 1 is occupied --> " + Value.Value);
            if(!Value.Value) SendToServer(true);
        }

        protected override void UpdateValue(bool newValue) {
            base.UpdateValue(newValue);
            print("Position1Occupied has changed from the server to: " + newValue);
        }
    }
}