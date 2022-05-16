using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using DidStuffLab;
using Managers;
using PlayFab.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationButton : OneShotButton
{
    private CarouselManager _carouselManager;
    [SerializeField] private bool forward;

    private NavigationVoteSync _navigationVoteSync;
    private void Start()
    {
        _carouselManager = MasterManager.Instance.carouselManager; 
    }

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        if (UnityNetworkServer.Instance.isOffline) {
            _carouselManager.PlayAnimation(forward);
        }
        else {
            // if the user is playing multiplayer, then vote instead of playing the move carousel animation directly
            // TODO --> This behaviour should actually be in a toggle navigation button instead
            _navigationVoteSync.Toggle();
        }
    }

    public void SetNavigationVoteSync(NavigationVoteSync navigationVoteSync) =>
        this._navigationVoteSync = navigationVoteSync;

    public void ShowVoteFromServer() {
        print("A player has voted to move. They want to move forward: " + forward);
        // show message saying that player wants to move
    }

    public void VotingCompleteFromServer() {
        print("Both players have voted to move. Move forward: " + forward);
        _carouselManager.PlayAnimation(forward);
        // reset the navigation sync value to 0
        _navigationVoteSync.ResetVoting();
    }
}
