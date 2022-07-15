using System.Collections.Generic;
using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons.Buttons;
using UnityEngine;

namespace DidStuffLab.Scripts.Main_Menu
{
    public class ManageFriendsPanel : UiFriendsManager
    {


        [Header("Add friend Card")] [SerializeField]
        private GameObject addFriendCard;
        [SerializeField] private DidStuffTextField inputField;
        
        [Header("FriendsMenu/Spinner")]
        [SerializeField] private GameObject friendsMenuList;
        [SerializeField] private GameObject rotatingSpinner;
        private static bool _isUpdated = false;
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
        private static void FriendsUpdated() => _isUpdated = true;
        
        protected override void OnEnable() {
            friendListToLoop = new List<Friend>();
            friendListToLoop.AddRange(allFriends);
            _numberOfCards = friendCards.Count;
            base.OnEnable();
            if (!_isUpdated) {
                print("It's NOT updated, so show the rotating spinner");
                friendsMenuList.SetActive(false);
                rotatingSpinner.SetActive(true);
            }
            
            else {
                print("It's updated, so show the friends list");
                friendsMenuList.SetActive(true);
                rotatingSpinner.SetActive(false);
            }
            if (!PlayFabLogin.Instance.IsLoggedInAsGuest) EnableAddFriend();
        }
    
        private void FriendsManagerOnOnReceivedFriendsDetails() {
            print("Friends updated, so hide the rotating spinner");
            friendsMenuList.SetActive(true);
            rotatingSpinner.SetActive(false);
        }
    
        protected override void ChangeGraphics()
        {
            base.ChangeGraphics();
            for (int i = 0; i < _displayedFriends; i++) {
                friendCards[i].ChangeFriend(friendListToLoop[_currentListIndex+i], false);
            }
        }

        protected override void DisableAllInteraction()
        {
            addFriendCard.SetActive(false);
            base.DisableAllInteraction();
        }

        private void EnableAddFriend()
        {
            addFriendCard.SetActive(true);
        }
        
        public void AddFriend() {
            var stringToPass = inputField.text;
            FriendsManager.Instance.SendFriendRequest(stringToPass);
            inputField.text = "";
        }
    
        
        private void OnApplicationQuit() {
            FriendsManager.OnReceivedFriendsDetails -= FriendsManagerOnOnReceivedFriendsDetails;
        }
    }
}
