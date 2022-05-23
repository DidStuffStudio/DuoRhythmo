using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace DidStuffLab {
    public class EyeFollow : MonoBehaviour {
        public Transform otherPlayerToFollow;

        private void Update() {
            // TODO --> Only change value on server if value changed is higher than a threshold
            // if(_playerFollowSync) _playerFollowSync.ChangeValueOnServer(localFollowPosition);
            // if(otherPlayerToFollow) transform.LookAt(otherPlayerToFollow.position);
            
            /*
             IDEA on how to sync positions:
             Create an empty PlayerEyeFollow gameobject with a network transform as a child of player prefab
             When instantiated on the scene, get the other player, and set its child pos as otherPlayerFollowPosition
             transform.LookAt(otherPlayerFollowPosition); // <-- set otherPlayerAvatar to this
             transform.position = localFollowPosition;
             */
        }

        // public void SetFollowSync(PlayerFollowSync playerFollowSync) => this._playerFollowSync = playerFollowSync;
        /*
        public void SetFollowPositionFromServer(Vector3 newValue) {
            otherPlayerFollowPosition = newValue;
        }
        */
    }
}