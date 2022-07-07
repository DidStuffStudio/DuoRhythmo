using System;
using PlayFab.ClientModels;
using EntityKey = PlayFab.MultiplayerModels.EntityKey;


namespace DidStuffLab
{
    public class Friend {
        public string MasterPlayfabId { get; set; }
        public string Username { get; set; }
        public string AvatarName { get; set; }
        public FriendInfo FriendInfo { get; set; }
        public EntityKey TitleEntityKey { get; set; }
        public FriendStatus FriendStatus { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsOnline { get; set; }
    }

    public enum FriendStatus {
        Confirmed,
        Requester,
        Requestee,
        Default
    }
}
