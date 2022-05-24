using System.Collections;
using System.Collections.Generic;
using Managers;
using Mirror;
using UnityEngine;

namespace DidStuffLab {
    public class CarouselAnimation : NetworkBehaviour {
        private NetworkAnimator _networkAnimator;
        private CarouselManager _carouselManager;

        public override void OnStartClient() {
            base.OnStartClient();
            _carouselManager = MasterManager.Instance.carouselManager;
            _networkAnimator = GetComponent<NetworkAnimator>();
            // _networkAnimator.animator = _carouselManager.GetComponent<Animator>();
            _carouselManager.carouselAnimation = this;
        }

        public void UpdateAnimator(bool forward) => CmdUpdateAnimator(forward);
        
        [Command(requiresAuthority = false)]
        private void CmdUpdateAnimator(bool forward) {
            print("Server is going to tell clients to play animation forward: " + forward);
            RpcUpdateAnimator(forward);
        }

        [ClientRpc]
        private void RpcUpdateAnimator(bool forward) {
            if(!isClient) return;
            print("Received from server to move animation forward: " + forward);
            _carouselManager.PlayAnimationFromServer(_networkAnimator, forward);
            // _networkAnimator.animator.SetFloat("SpeedMultiplier", forward ? 1 : -1);
        }
    }
}
