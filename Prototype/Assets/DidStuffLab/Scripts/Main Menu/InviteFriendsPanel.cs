using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

namespace DidStuffLab.Scripts.Main_Menu
{
    public class InviteFriendsPanel : UiFriendsManager {

        [SerializeField] private GameObject inviteFriendsMenu;
        [SerializeField] private GameObject rotatingSpinner;
        private static bool isUpdated = false;
        
        public static void SubscribeToEvents(bool subscribe) {
            if (subscribe) {
                // subscribe to the event when we've received all friends details including avatars
                UiFriendsDetailsInstance.OnInitializedFriendsDetails += FriendsUpdated;
            }
            else {
                // unsubscribe to the event when we've received all friends details including avatars
                UiFriendsDetailsInstance.OnInitializedFriendsDetails -= FriendsUpdated;
            }
        }

        private static void FriendsUpdated() => isUpdated = true;
    
        /*
    protected override void OnEnable() {
        FriendsManager.OnReceivedFriendsDetails += FriendsManagerOnOnReceivedFriendsDetails;
        if (FriendsManager.Instance == null) return;
        if (FriendsManager.Instance.receivedAllFriendsDetails) FriendsManagerOnOnReceivedFriendsDetails();
        else {
            rotatingSpinner.SetActive(true);
            inviteFriendsMenu.SetActive(false);
        }

        base.OnEnable();
    }
    */

        protected override void OnEnable() {
            _numberOfCards = friendCards.Count;
            friendListToLoop = new List<Friend>();
            friendListToLoop.AddRange(confirmedFriends);
            base.OnEnable();
            EnablePanel();
        }

        private void EnablePanel() {
            
           
            
            if (!isUpdated) {
                print("It's NOT updated, so show the rotating spinner in invite friends panel");
                rotatingSpinner.SetActive(true);
                inviteFriendsMenu.SetActive(false);
                
            }
            else {
                print("It's updated, so hide the rotating spinner in invite friends panel");
                print("Enabled again");
                rotatingSpinner.SetActive(false);
                inviteFriendsMenu.SetActive(true);
            }
        }

        protected override void ChangeGraphics() {
            base.ChangeGraphics();
            for (int i = 0; i < _displayedFriends; i++) {
                friendCards[i].ChangeFriend(friendListToLoop[_currentListIndex+i], true);
            }
        }
        
    
    }
}