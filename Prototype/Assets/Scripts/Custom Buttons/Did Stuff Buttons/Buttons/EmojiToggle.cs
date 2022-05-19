using System;
using System.Collections.Generic;
using PlayFab.MultiplayerModels;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons {
    public class EmojiToggle : AbstractDidStuffButton {
        [SerializeField] private List<Emoji> emojis = new List<Emoji>();

        protected override void ChangeToActiveState() {
            base.ChangeToActiveState();
            print("Active state");
            foreach (var emoji in emojis) emoji.Enabled = true;
        }

        protected override void ChangeToInactiveState() {
            base.ChangeToInactiveState();
            print("Inactive state");
            foreach (var emoji in emojis) emoji.Enabled = false;
        }
    }
}