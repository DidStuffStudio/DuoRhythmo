using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using Mirror;
using UnityEngine;

namespace DidStuffLab {
    public class EmojiSync : CustomSyncBehaviour<byte> {
        public byte EmojiIndex => Value.Value;

        public void ChangeValueOnServer(byte newValue) {
            if(Value.Value != newValue) SendToServer(newValue);
        }
        
        protected override void Initialize() {
            throw new System.NotImplementedException();
        }

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(byte newValue) => Value.Value = newValue;

        protected override void UpdateValueLocally(byte newValue) {
            base.UpdateValueLocally(newValue);
            print("Emoji index has changed from the server --> " + newValue);
        }
    }
}
