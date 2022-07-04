using System;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using Managers;
using Mirror;
using TMPro;
using UnityEngine;

namespace DidStuffLab {
    public class NavigationVoteSync : CustomSyncBehaviour<byte> {
        [SerializeField] private bool forward;
        [SerializeField] private GameObject [] navigationButtonsToasts; // either left or right
        public byte VotingValue => Value.Value;
        
        public TextMeshProUGUI [] texts;

        /*
        // show the vote to move toast event
        public delegate void VoteToMoveFromServerAction(byte totalVotes, bool forward);
        public static event VoteToMoveFromServerAction OnVotedToMoveFromServer;
        */ 
        
        // play the move carousel animation and toggle buttons to inactive state
        public delegate void VotingCompletedFromServerAction(bool forward);
        public static event VotingCompletedFromServerAction OnVotingCompletedFromServerAction;

        public void ChangeValue(byte newValue) {
            if(newValue == Value.Value) return; // avoid updating the value if already done
            SendToServer(newValue);
        }

        public void ResetVoting() => ChangeValue(0);

        protected override void Initialize() { }

        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(byte newValue) => Value.Value = newValue;

        protected override void UpdateValueLocally(byte newValue) {
            base.UpdateValueLocally(newValue);
            // if the value is 1, it means that one player has voted to move
            // if it's 2 or higher than 1, it means that two players have voted, so move the carousel
            print("Voting value has changed from server to: " + newValue);
            
            if (MasterManager.Instance.carouselManager.votedToMoveLocally) {
                MasterManager.Instance.carouselManager.votedToMoveLocally = false;
                MasterManager.Instance.carouselManager.UpdateNavSyncTexts(PlayFabLogin.Instance.Username, forward);
            }
            else MasterManager.Instance.carouselManager.UpdateNavSyncTexts(JamSessionDetails.Instance.otherPlayerUsername, forward);
            
            if (newValue == 0) {
                // hide vote toast messages
                foreach (var nbt in navigationButtonsToasts) nbt.SetActive(false);
            }
            else if (newValue == 1) {
                // show vote toast messages
                foreach (var nbt in navigationButtonsToasts) nbt.SetActive(true);
            }
            else if(newValue > 1) {
                foreach (var nbt in navigationButtonsToasts) nbt.SetActive(false);
                OnVotingCompletedFromServerAction?.Invoke(forward);
            }
        }
    }
}