using System;
using DidStuffLab;
using Managers;
using Mirror;
using UnityEngine;

namespace DidStuffLab {
    public class Player : CustomSyncBehaviour<Vector3> {
        private Transform _transform;
        private Camera _camera;
        private GameObject _otherPlayerAvatar;
        private EyeFollow _eyeFollow;

        [SerializeField] private GameObject[] avatarPrefabs;

        private Transform _finalDestination;

        [SyncVar(hook = nameof(OnEmojiChanged))]
        public byte emojiIndex;

        private bool _localplayerIsInPosition;
        private float _journeyLength, _startTime;
        [SerializeField] private float positionSpeed = 1.0f;
        private bool _canStartMoving;

        private PlayerPositionSync _playerPositionSync;

        private void SetPlayerPosition() {
            _journeyLength = Vector3.Distance(MasterManager.Instance.startTransform.position,
                MasterManager.Instance.destinationTransform.position);
            _startTime = Time.time;

            _finalDestination = MasterManager.Instance.destinationTransform;

            JamSessionDetails.Instance.AddPlayer(this);

            CheckForCorrespondingPositioning();
        }

        private void SwapToOppositeSide() {
            print("Swap to opposite side because there's already another player here");
            _localplayerIsInPosition = false;
            _finalDestination = MasterManager.Instance.oppositeDestinationTransform;
            foreach (var spawner in MasterManager.Instance.carouselManager.emojiSpawners) {
                spawner.transform.RotateAround(Vector3.zero, Vector3.up, 180);
            }
        }

        private void Update() {
            if (!isLocalPlayer || !_canStartMoving) return;
            if (!_localplayerIsInPosition) LerpPlayer();
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
            _camera.transform.position = position;

            _transform.position = position;

            // update the rotation
            var rotation = _camera.transform.rotation;
            rotation = Quaternion.Lerp(rotation,
                _finalDestination.rotation, fractionOfJourney * Time.deltaTime);
            _camera.transform.rotation = rotation;

            _transform.rotation = rotation;

            if (!(Vector3.Distance(_transform.position, _finalDestination.position) < 0.1f)) return;
            print("Local player camera is in position");
            _localplayerIsInPosition = true;
            MasterManager.Instance.PlayerReachedDestination();
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
        }

        protected override void CmdUpdateValue(Vector3 newValue) {
            throw new NotImplementedException();
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
                // if its not the local player, then locally instantiate an avatar with the specified avatar name
                // and make this follow the other players
                _otherPlayerAvatar = Instantiate(avatarPrefabs[0], JamSessionDetails.Instance.otherPlayer._transform);
            }

            if (_playerPositionSync) _canStartMoving = true;
            else Debug.LogError("Player Position sync not found when checking for player's corresponding position");
        }
    }
}