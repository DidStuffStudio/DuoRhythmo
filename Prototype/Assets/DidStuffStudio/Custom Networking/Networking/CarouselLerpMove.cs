using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Mirror;
using UnityEngine;

namespace DidStuffLab {
    public class CarouselLerpMove : NetworkBehaviour {
        public static CarouselLerpMove Instance { get; private set; }
        [SerializeField] private float duration = 2f;
        private float _rotation = 0, _offset = 0;
        private CarouselManager _carouselManager;
        private bool _isPlaying = false;

        public override void OnStartClient() {
            base.OnStartClient();
            if (Instance == null) Instance = this;
            _carouselManager = MasterManager.Instance.carouselManager;
            _offset = 360.0f / (MasterManager.Instance.numberInstruments * 2);
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
            CalculateNewRotation(forward);
        }

        public void RotateCarouselSolo(bool forward) {
            CalculateNewRotation(forward);
            _carouselManager.BlurSolo(forward);
        }

        private void CalculateNewRotation(bool forward) {
            var degreesToMove = _offset * (forward ? 1 : -1);
            _rotation += degreesToMove;
        }
        
        private void Update() {
            var target = Quaternion.Euler(0, _rotation, 0);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, target, duration * Time.deltaTime);
        }
    }
}