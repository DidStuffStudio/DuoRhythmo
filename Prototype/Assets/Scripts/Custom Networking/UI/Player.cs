using System;
using System.Collections;
using System.Collections.Generic;
using ctsalidis;
using Managers;
using Mirror;
using PlayFab.Networking;
using UnityEngine;

namespace ctsalidis {

    public class Player : CustomSyncBehaviour<Vector3> {
        private Transform _transform;
        private Camera _camera;
        private GameObject _otherPlayerAvatar;
        private EyeFollow _eyeFollow;
        [SerializeField] private GameObject [] avatarPrefabs;
        // private int playerNumber;
        // private GameObject _localPlayer;

        [SerializeField] private Transform startPos, endPos;

        [SyncVar(hook = nameof(OnEmojiChanged))] public byte emojiIndex;

        private bool _playerIsInPosition;
        private float _journeyLength, _startTime;
        [SerializeField] private float positionSpeed = 1.0f;

        public void SendNewPosToServer(Vector3 value) {
            // if(Value.Value == value) return;
            // SendToServer(value);
        }

        /*
        protected override void Initialize() {
            // when this player is instantiated, its player index = (number - 1) will be the amount of players currently in the scene
            
            if (!isLocalPlayer) {
                // TODO --> Get other player's actual avatar and instantiate in corresponding position
                // otherPlayerAvatars = Instantiate(avatarPrefabs[0]);
            }
        }
        */
        

        private void SetPlayerPosition() {
            // startPos.position = _camera.transform.position;
            // startPos.rotation = _camera.transform.rotation;
            // SwapToOppositeSide();
            // endPos = MasterManager.Instance.playerPositionDestination;
            // _transform.position = startPos.position;
            // _transform.rotation = startPos.rotation;
            _journeyLength = Vector3.Distance(startPos.position, endPos.position);
            _startTime = Time.time;
            if (isLocalPlayer) {
                if (UnityNetworkServer.Instance.numPlayers > 0) {
                    SwapToOppositeSide();
                }
            }
            else {
                // if its not the local player, then locally instantiate an avatar with the specified avatar name
                // and make this follow the other players
                _otherPlayerAvatar = Instantiate(avatarPrefabs[0]);
                // otherPlayerAvatar.transform.position = endPos.position;
            }
        }

        private void SwapToOppositeSide() {
            startPos.Rotate(0, 180, 0);
            endPos.Rotate(0, 180, 0);
            // _transform.position = startPos.position;
            // _transform.rotation = startPos.rotation;
        }

        private void Update() {
            if (!_playerIsInPosition) {
                // print("Player not in position, so lerp");
                // TODO --> Lerp avatar's position until in appropriate position
                LerpPlayer(isLocalPlayer ? _camera.transform : _otherPlayerAvatar.transform);
            }
        }

        private void LerpPlayer(Transform t) {
            // Distance moved equals elapsed time times speed..
            var distCovered = (Time.time - _startTime) * positionSpeed;
            // Fraction of journey completed equals current distance divided by total distance.
            var fractionOfJourney = distCovered / _journeyLength;
            t.position = Vector3.Lerp(t.position,
                endPos.position, fractionOfJourney * Time.deltaTime);
            t.rotation = Quaternion.Lerp(t.rotation,
                endPos.rotation, fractionOfJourney * Time.deltaTime);

            if (!(Vector3.Distance(t.position, endPos.position) < 0.1f)) return;
            _playerIsInPosition = true;
            if(isLocalPlayer) MasterManager.Instance.PlayerReachedDestination();
        }

        private void OnEmojiChanged(byte oldValue, byte newValue) {
            // if (isLocalPlayer) return;
            print("New emoji value: " + newValue);
            // TODO --> Instantiate emoji animation
        }
        
          /*
        [Command(requiresAuthority = false)]
        protected override void CmdUpdateValue(Vector3 newValue) => Value.Value = newValue;

        protected override void UpdateValue(Vector3 newValue) {
            base.UpdateValue(newValue);
            print("Value has changed from the server: " + newValue);
            // avatar.SetFollowPositionFromServer(newValue);
            _eyeFollow.SetFollowPositionFromServer(newValue);
        }
        */
          protected override void Initialize() {
              // print("Initializing player script");
              _transform = transform;
              _camera = MasterManager.Instance.playerCamera;
              // playerNumber = UnityNetworkServer.Instance.numPlayers - 1;
              //_eyeFollow = GetComponentInParent<EyeFollow>();
              // _eyeFollow.SetFollowSync(this);
              // _localPlayer = NetworkClient.localPlayer.gameObject;
              SetPlayerPosition();
          }

          protected override void CmdUpdateValue(Vector3 newValue) {
              throw new NotImplementedException();
          }
    }
}
