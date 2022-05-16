using System;
using Mirror;

namespace DidStuffLab {
    public class NavigationVoteSync : CustomSyncBehaviour<byte> {
        private bool _localPlayerHasVoted = false;
        private NavigationButton _navigationButton;
        
        public void Toggle() {
            var b = (byte) (Value.Value + (_localPlayerHasVoted ? -1 : 1 ));
            SendToServer(b);
            _localPlayerHasVoted = !_localPlayerHasVoted;
        }

        public void ResetVoting() => SendToServer(0);

        protected override void Initialize() {
            _navigationButton = GetComponentInParent<NavigationButton>();
            _navigationButton.SetNavigationVoteSync(this);
        }

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(byte newValue) => Value.Value = newValue;

        protected override void UpdateValueLocally(byte newValue) {
            base.UpdateValueLocally(newValue);
            // if the value is 1, it means that one player has voted to move
            // if it's 2, it means that two players have voted, so move the carousel
            // print("Voting value has changed from server to: " + newValue);
            if(newValue == 1) _navigationButton.ShowVoteFromServer();
            else if(newValue == 2) _navigationButton.VotingCompleteFromServer();
        }
    }
}