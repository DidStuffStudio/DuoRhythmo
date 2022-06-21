using System;
using System.Collections.Generic;
using DidStuffLab;
using Managers;
using Mirror;
using UnityEngine;

namespace DidStuffLab {
    public class Player : NetworkBehaviour {
        private Transform _transform;
        private Camera _camera;
        private GameObject _otherPlayerAvatar;
        private EyeFollow _eyeFollow;

        [SerializeField] private GameObject[] avatarPrefabs;

        private Transform _finalDestination;

        private bool _swapSides = false;
        private bool _localplayerIsInPosition;
        private float _journeyLength, _startTime;
        [SerializeField] private float positionSpeed = 1.0f;
        private bool _canStartMoving;

        private PlayerPositionSync _playerPositionSync;

        // [SyncVar(hook = nameof(SetPlayerUsernameLocally))] public string playerUsername;
        // [SyncVar(hook = nameof(SetPlayerAvatarNameLocally))] public string playerAvatarName;
        public List<string> usernames = new List<string>();
        public List<string> avatarNames = new List<string>();

        public override void OnStartClient() {
            base.OnStartClient();
            _transform = transform;
            _camera = MasterManager.Instance.playerCamera;
            print(_camera.name);
            print("Player's net id: " + netId);
            SetPlayerPosition();
        }

        private void SetPlayerPosition() {
            _journeyLength = Vector3.Distance(MasterManager.Instance.startTransform.position,
                MasterManager.Instance.destinationTransform.position);
            _startTime = Time.time;

            _finalDestination = MasterManager.Instance.destinationTransform;

            JamSessionDetails.Instance.AddPlayer(this);
            // if (isLocalPlayer) {
                // SendUserDetailsToServer();
                UpdateOtherPlayerUsername(PlayFabLogin.Instance.Username);
                UpdateOtherPlayerAvatarName(PlayFabLogin.Instance.UserAvatar);
            // }

            /*
            // send this local player's username and avatar to the other player
            if (!isLocalPlayer) {
                UpdateOtherPlayerUsername(PlayFabLogin.Instance.Username);
                UpdateOtherPlayerAvatarName(PlayFabLogin.Instance.UserAvatar);
            }
            */

            CheckForCorrespondingPositioning();
        }

        private void SwapToOppositeSide() {
            print("Swap to opposite side because there's already another player here");
            _swapSides = true;
            _localplayerIsInPosition = false;
            MasterManager.Instance.destinationTransform.parent.Rotate(0, 180, 0);
            MasterManager.Instance.carouselManager.SwapBlurForPlayer(5, 5, 0, 0);
            // _finalDestination = MasterManager.Instance.oppositeDestinationTransform;
            foreach (var spawner in MasterManager.Instance.carouselManager.emojiSpawners) {
                spawner.transform.RotateAround(Vector3.zero, Vector3.up, 180);
            }
        }

        private void Update() {
            if (!isLocalPlayer || !_canStartMoving) return;
            if (!_localplayerIsInPosition) LerpPlayer();
            if (JamSessionDetails.Instance.otherPlayer &&
                !string.IsNullOrEmpty(JamSessionDetails.Instance.otherPlayerAvatarName) &&
                JamSessionDetails.Instance.otherPlayerEyeFollowTransform) {
                JamSessionDetails.Instance.otherPlayer.transform.GetChild(1).GetChild(0).transform
                    .LookAt(JamSessionDetails.Instance.otherPlayerEyeFollowTransform);
            }
        }

        private void LerpPlayer() {
            // Distance moved equals elapsed time times speed..
            var distCovered = (Time.time - _startTime) * positionSpeed;
            // Fraction of journey completed equals current distance divided by total distance.
            var fractionOfJourney = distCovered / _journeyLength;

            // update the position
            var position = _camera.transform.position;
            position = Vector3.Lerp(position,
                _finalDestination.position, fractionOfJourney * Time.deltaTime);
            var transform1 = _camera.transform;
            transform1.position = position;

            _transform.position = position;

            // update the rotation
            var rotation = transform1.rotation;
            rotation = Quaternion.Lerp(rotation,
                _finalDestination.rotation, fractionOfJourney * Time.deltaTime);
            _camera.transform.rotation = rotation;

            _transform.rotation = rotation;

            if (!(Vector3.Distance(_transform.position, _finalDestination.position) < 0.1f)) return;
            print("Local player camera is in position");
            _localplayerIsInPosition = true;
            MasterManager.Instance.PlayerReachedDestination();
        }

        private void CheckForCorrespondingPositioning() {
            print("Checking for positioning");
            _playerPositionSync = FindObjectOfType<PlayerPositionSync>();

            if (_playerPositionSync.Position1Occupied && isLocalPlayer) {
                print("Position 1 is already occupied");
                SwapToOppositeSide();
            }
            else print(name + " says --> position 1 is not occupied");

            if (isLocalPlayer) {
                print("Is local player, so check if position is occupied");
                _playerPositionSync.SetCorrespondingPosition();
            }
            else {
                // TODO --> Instantiate appropriate avatar (other user's avatar name)
                // if its not the local player, then locally instantiate an avatar with the specified avatar name
                // and make this follow the other players
                // _otherPlayerAvatar = Instantiate(avatarPrefabs[0], JamSessionDetails.Instance.otherPlayer._transform);
            }

            if (_playerPositionSync) _canStartMoving = true;
            else Debug.LogError("Player Position sync not found when checking for player's corresponding position");
        }

        /*
        [Command(requiresAuthority = false)]
        private void SendUserDetailsToServer() {
            playerUsername = PlayFabLogin.Instance.Username;
            playerAvatarName = PlayFabLogin.Instance.UserAvatar;
        }
        
        private void SetPlayerUsernameLocally(string oldUsername, string newUsername) {
            
            if (!isLocalPlayer) {
                JamSessionDetails.Instance.otherPlayerUsername = newUsername;
            }
            
        }
        private void SetPlayerAvatarNameLocally(string oldAvatarName, string newAvatarName) {
        if (!isLocalPlayer) {
                JamSessionDetails.Instance.otherPlayerAvatarName = newAvatarName;
                // instantiate other player's avatar
                InstantiateOtherPlayerAvatar(newAvatarName);
            }
            
        }
        */

        public void UpdateOtherPlayerUsername(string username) => CmdUpdateOtherPlayerUsername(/*netId + "," + */username);
        
        [Command(requiresAuthority = false)]
        private void CmdUpdateOtherPlayerUsername(string username) {
            print("Server is going to tell clients the clients the username: " + username);
            RpcUpdateOtherPlayerUsername(username);
        }
        
        [ClientRpc]
        private void RpcUpdateOtherPlayerUsername(string username) {
            /*
            // check if this player has already sent their username. If not, then update the other player's name locally
            if (username.Contains(netId.ToString()))
                return; // if it's the same as this player, then dont do anything. We want the other player's username
            print("Received other player's username from server: " + username);
            var split = username.Split(',');
            JamSessionDetails.Instance.otherPlayerUsername = split[1];
            */
            /*
            if (!usernames.Contains(username)) {
                usernames.Add(username);
                if (!username.Contains(netId.ToString())) {
                    // then it means that this is the other player's username
                    print("Received other player's username from server: " + username);
                    var split = username.Split(',');
                    JamSessionDetails.Instance.otherPlayerUsername = split[1];
                }
            }
            */
            if (JamSessionDetails.Instance.IsOtherPlayerUsernameFromServer(username)) {
                JamSessionDetails.Instance.otherPlayerUsername = username;
            }
        }
        
        public void UpdateOtherPlayerAvatarName(string avatarname) => CmdUpdateOtherPlayerAvatarName(/*netId + "," + */avatarname);
        
        [Command(requiresAuthority = false)]
        private void CmdUpdateOtherPlayerAvatarName(string avatarname) {
            print("Server is going to tell clients the avatar name: " + avatarname);
            RpcUpdateOtherPlayerAvatarName(avatarname);
        }
        
        [ClientRpc]
        private void RpcUpdateOtherPlayerAvatarName(string avatarname) {
            /*
            // check if this player has already sent their username. If not, then update the other player's name locally
            if (avatarname.Contains(netId.ToString()))
                return; // if it's the same as this player, then dont do anything. We want the other player's username
            print("Received other player's avatar name from server: " + avatarname);
            var split = avatarname.Split(',');
            JamSessionDetails.Instance.otherPlayerAvatarName = split[1];
            var avatarIndex = split[1] == "Avatar1" ? 0 : 1;
            _otherPlayerAvatar =
                Instantiate(avatarPrefabs[avatarIndex], JamSessionDetails.Instance.otherPlayer._transform);
                */
            /*
            JamSessionDetails.Instance.otherPlayerAvatarName = avatarname;
            var avatarIndex = avatarname == "Avatar1" ? 0 : 1;
            _otherPlayerAvatar =
                Instantiate(avatarPrefabs[avatarIndex], JamSessionDetails.Instance.otherPlayer._transform);
                */
            /*
            if (!usernames.Contains(avatarname)) {
                avatarNames.Add(avatarname);
                
                if (!avatarname.Contains(netId.ToString())) {
                    // then it means that this is the other player's avatar name
                    print("Received other player's avatarName from server: " + avatarname);
                    var split = avatarname.Split(',');
                    InstantiateOtherPlayerAvatar(split[1]);
                    JamSessionDetails.Instance.otherPlayerAvatarName = split[1];
                }
            }
            */
            if (JamSessionDetails.Instance.IsOtherPlayerAvatarNameFromServer(avatarname)) {
                InstantiateOtherPlayerAvatar(avatarname);
            }
        }
        
        public void InstantiateOtherPlayerAvatar(string avatarName) {
            var avatarIndex = avatarName == "Avatar1" ? 0 : 1;
            _otherPlayerAvatar = Instantiate(avatarPrefabs[avatarIndex], JamSessionDetails.Instance.otherPlayer._transform);
        }
    }
}