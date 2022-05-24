using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Mirror;
using TS;
using UnityEngine;

namespace DidStuffLab {
    public class CarouselLerpMove : NetworkBehaviour {
        public static CarouselLerpMove Instance { get; private set; }

        private TweenSharp _tween;
        [SerializeField] private float duration = 2f;
        private float _rotation = 0, _offset = 0;
        private CarouselManager _carouselManager;
        private bool _isPlaying = false;

        public override void OnStartClient() {
            base.OnStartClient();
            if (Instance == null) Instance = this;
            _carouselManager = MasterManager.Instance.carouselManager;
            _offset = 360.0f / (MasterManager.Instance.numberInstruments * 2);
            
            // action to execute when the tween has completed lerping
            // _tween.onComplete = _carouselManager.PauseAnimation;
        }

        public void UpdateLerpMoveRotation(bool forward) => CmdUpdateAnimator(forward);

        [Command(requiresAuthority = false)]
        private void CmdUpdateAnimator(bool forward) {
            print("Server is going to tell clients to play animation forward: " + forward);
            RpcUpdateAnimator(forward);
        }

        [ClientRpc]
        private void RpcUpdateAnimator(bool forward) {
            if(!isClient) return;
            print("Received from server to move animation forward: " + forward);
            _carouselManager.BlurFromServer(forward);
            StartLerpMove(forward);
        }

        public void RotateCarouselSolo(bool forward) {
            StartLerpMove(forward);
            _carouselManager.BlurSolo(forward);
        }

        private void StartLerpMove(bool forward) {
            _isPlaying = true;
            var degreesToMove = _offset * (forward ? 1 : -1);
            _rotation += degreesToMove;
            if (_rotation > 360) _rotation = degreesToMove;
            _tween = new TweenSharp(gameObject, duration, new {
                rotationY = _rotation,
                ease = Quad.EaseInOut,
            });
        }
        
        private void Update() {
            if(!_isPlaying) return;
            if(_tween.Progress >= 1) {
                print("Pause animation");
                // _tween = new TweenSharp(gameObject, 0, new {});
                _carouselManager.PauseAnimation();
                _isPlaying = false;
            }
        }

        /*
        private void Update() {
            if (Input.GetKeyUp(KeyCode.RightArrow)) StartLerpMove(true);
            else if(Input.GetKeyUp(KeyCode.LeftArrow)) StartLerpMove(false);
        }
        */
    }
}