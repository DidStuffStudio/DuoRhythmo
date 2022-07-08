using DidStuffLab.Scripts.Managers;
using UnityEngine;

namespace DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons.Buttons {
    public class NavigationMultiButton : AbstractDidStuffButton {
        private CarouselManager _carouselManager;
        [SerializeField] private bool forward;
        private bool votedToMove = false;

        [SerializeField] private AbstractDidStuffButton otherButtonToDisable;

        protected override void Start() {
            base.Start();
            _carouselManager = MasterManager.Instance.carouselManager;
        }

        protected override void ButtonClicked() {
            base.ButtonClicked();
            _carouselManager.votedToMoveLocally = true;
            _carouselManager.UpdateVoteToMoveToServer(_isActive, forward);
            votedToMove = _isActive;
            if(otherButtonToDisable._isActive && _isActive) otherButtonToDisable.ClickAndCallEvents();
        }
        
        protected override void OnEnable() {
            base.OnEnable();
            CarouselManager.OnMovedCarousel += VotingCompleteFromServer;
        }

        private void VotingCompleteFromServer() {
            if(!votedToMove) return;
            print("Both players have voted to move. Move forward: " + forward);
            // change to inactive state
            DeactivateButton();
            votedToMove = false;
        }
        
        protected override void OnDisable() {
            base.OnDisable();
            CarouselManager.OnMovedCarousel -= VotingCompleteFromServer;
        }
    }
}