using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerPanelInfo : UIPanel
{
    [SerializeField] private UiFriendsManager _friendsManager, _inviteManager;
    public override void ExecuteSpecificChanges()
    {
        _friendsManager.Initialised = true;
        _inviteManager.Initialised = true;
    }
}
