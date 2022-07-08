using UnityEngine;

namespace DidStuffLab.Scripts.Main_Menu
{
    public class MultiplayerPanelInfo : UIPanel
    {
        [SerializeField] private UiFriendsManager _friendsManager, _inviteManager;
        public override void ExecuteSpecificChanges()
        {
            _friendsManager.Initialised = true;
            _inviteManager.Initialised = true;
        }
    }
}
