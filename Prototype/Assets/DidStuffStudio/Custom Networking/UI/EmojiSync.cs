using System.Collections.Generic;
using Managers;
using Mirror;
using UnityEngine;

namespace DidStuffLab {
    public class EmojiSync : CustomSyncBehaviour<byte> {
        private bool _sendingEmojiAnimation;

        public void ChangeValueOnServer(byte newValue) {
            if (Value.Value == newValue) return;
            _sendingEmojiAnimation = true;
            SendToServer(newValue);
        }

        protected override void Initialize() => MasterManager.Instance.carouselManager.EmojiSync = this;

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(byte newValue) => Value.Value = newValue;

        protected override void UpdateValueLocally(byte newValue) {
            base.UpdateValueLocally(newValue);
            // print("Emoji index has changed from the server --> " + newValue);
            if (!_sendingEmojiAnimation) PlayReceivingAnimation(newValue);
            else PlaySendingAnimation(newValue);
        }

        private void PlaySendingAnimation(byte newValue) {
            print("Playing send animation number " + newValue);
            _sendingEmojiAnimation = false; // update it so we can send again afterwards
            MasterManager.Instance.carouselManager.emojiParticles[0][newValue].Play();
        }

        private void PlayReceivingAnimation(byte newValue) {
            print("Playing receive animation number " + newValue);
            MasterManager.Instance.carouselManager.emojiParticles[1][newValue].Play();
        }
    }
}