using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace DidStuffLab {
    public class PlayerEyeFollow : MonoBehaviour {
        private Camera _camera;
        [SerializeField] private float multiplier;

        private void Start() => _camera = Camera.main;

        private void Update() {
            // if(_player == null || !_player.isLocalPlayer) return;
            Vector3 mousePos = InteractionData.Instance.InputPosition; //Todo --> Follow other player
            mousePos.z = _camera.nearClipPlane * multiplier;
            var mouseWorld = _camera.ScreenToWorldPoint(mousePos);
            transform.position = mouseWorld;
        }
    }

}