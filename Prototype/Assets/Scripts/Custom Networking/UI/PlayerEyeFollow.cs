using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DidStuffLab {
    public class PlayerEyeFollow : MonoBehaviour {
        private Camera _camera;
        [SerializeField] private float multiplier;
        private Player _player;
        private void Start() {
            _camera = Camera.main;
            _player = GetComponentInParent<Player>();
        }

        private void Update() {
            // if(_player == null || !_player.isLocalPlayer) return;
            Vector3 mousePos = InteractionManager.Instance.InputPosition;
            mousePos.z = _camera.nearClipPlane * multiplier;
            var mouseWorld = _camera.ScreenToWorldPoint(mousePos);
            transform.position = mouseWorld;
        }
    }

}