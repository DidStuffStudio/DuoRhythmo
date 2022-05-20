using DidStuffLab;
using Managers;
using PlayFab.Networking;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons {
    public class NavigationButton : AbstractDidStuffButton {
        private CarouselManager _carouselManager;
        [SerializeField] private bool forward;

        private void Start() {
            _carouselManager = MasterManager.Instance.carouselManager;
        }

        protected override void ButtonClicked() {
            base.ButtonClicked();
            _carouselManager.UpdateVoteToMove(_isActive, forward);
        }
        
        protected override void OnEnable() {
            base.OnEnable();
            CarouselManager.OnMovedCarousel += VotingCompleteFromServer;
        }

        private void VotingCompleteFromServer() {
            print("Both players have voted to move. Move forward: " + forward);
            // change to inactive state
            ChangeToInactiveState();
            ActivatedScaleFeedback();
        }
        
        protected override void OnDisable() {
            base.OnDisable();
            CarouselManager.OnMovedCarousel -= VotingCompleteFromServer;
        }
    }
}