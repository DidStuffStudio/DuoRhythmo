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

        [SerializeField] private GameObject[] avatarPrefabs;
        // private int playerNumber;
        // private GameObject _localPlayer;

        [SerializeField] private Transform start, destination, oppositeDestination;

        private Transform _localPlayerfinalDestination, _otherPlayerAvatarFinalDestination;
        // [SerializeField] private int numberPlayers;

        [SyncVar(hook = nameof(OnEmojiChanged))]
        public byte emojiIndex;

        private bool _localplayerIsInPosition, _otherplayerAvatarIsInPosition;
        private float _journeyLength, _startTime;
        private bool _isInitialized = true;
        [SerializeField] private float positionSpeed = 1.0f;
        private bool canStartMoving;

        private PlayerPositionSync _playerPositionSync;

        private void SetPlayerPosition() {
            _journeyLength = Vector3.Distance(start.position, destination.position);
            _startTime = Time.time;

            // TODO --> Swap to opposite side should be done for both the avatar and the local player
            // meaning that both the player and the avatar should both have different endPos (opposite to each other)

            _localPlayerfinalDestination = destination;
            _otherPlayerAvatarFinalDestination = oppositeDestination;

            JamSessionDetails.Instance.AddPlayer(this);
            
            CheckForCorrespondingPositioning();
            // CheckForCorrespondingPositioning();
            // StartCoroutine(WaitToCheckForPositioning());

            // if (UnityNetworkServer.Instance.numPlayers > 1) SwapToOppositeSide();
            // if (numberPlayers > 1) SwapToOppositeSide(); // TODO --> CHANGE TO SERVER NUMBER NUMBER OF PLAYERS
        }

        private void SwapToOppositeSide() {
            print("Swap to opposite side because there's already another player here");
            _localplayerIsInPosition = false;
            _localPlayerfinalDestination = oppositeDestination;
            _otherPlayerAvatarFinalDestination = destination;
        }

        private void Update() {
            if (!canStartMoving) return;
            if (isLocalPlayer && !_localplayerIsInPosition) LerpPlayer(_camera.transform, _localPlayerfinalDestination);
            if (!_otherplayerAvatarIsInPosition && _otherPlayerAvatar != null)
                LerpPlayer(_otherPlayerAvatar.transform, _otherPlayerAvatarFinalDestination);
        }

        private void LerpPlayer(Transform t, Transform end) {
            // Distance moved equals elapsed time times speed..
            var distCovered = (Time.time - _startTime) * positionSpeed;
            // Fraction of journey completed equals current distance divided by total distance.
            var fractionOfJourney = distCovered / _journeyLength;
            t.position = Vector3.Lerp(t.position,
                end.position, fractionOfJourney * Time.deltaTime);
            t.rotation = Quaternion.Lerp(t.rotation,
                end.rotation, fractionOfJourney * Time.deltaTime);

            if (!(Vector3.Distance(t.position, end.position) < 0.1f)) return;
            if (t == _camera.transform) {
                print("Local player camera is in position");
                _localplayerIsInPosition = true;
                MasterManager.Instance.PlayerReachedDestination();
            }

            if (_otherPlayerAvatar != null && t == _otherPlayerAvatar.transform) {
                print("Other player avatar is in position");
                _otherplayerAvatarIsInPosition = true;
            }
        }

        private void OnEmojiChanged(byte oldValue, byte newValue) {
            // if (isLocalPlayer) return;
            print("New emoji value: " + newValue);
            // TODO --> Instantiate emoji animation
        }

        protected override void Initialize() {
            _transform = transform;
            _camera = MasterManager.Instance.playerCamera;
            print(_camera.name);
            SetPlayerPosition();
            _isInitialized = true;
        }

        protected override void CmdUpdateValue(Vector3 newValue) {
            throw new NotImplementedException();
        }

        public void CheckForCorrespondingPositioning() {
            print("Checking for positioning");
            _playerPositionSync = JamSessionDetails.Instance.PlayerPositionSync == null
                ? FindObjectOfType<PlayerPositionSync>()
                : JamSessionDetails.Instance.PlayerPositionSync;
            
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
                // if its not the local player, then locally instantiate an avatar with the specified avatar name
                // and make this follow the other players
                _otherPlayerAvatar = Instantiate(avatarPrefabs[0], start.position, Quaternion.identity);
            }

            if(_playerPositionSync) canStartMoving = true;
            else Debug.LogError("Player Position sync not found when checking for player's corresponding position");
        }

        /*
        private IEnumerator WaitToCheckForPositioning() {
            yield return new WaitForSeconds(1.0f);
            CheckForCorrespondingPositioning();
        }
        */
    }
}