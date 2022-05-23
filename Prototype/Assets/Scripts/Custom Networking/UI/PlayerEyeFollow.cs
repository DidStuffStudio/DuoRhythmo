using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DidStuffLab {
    public class PlayerEyeFollow : MonoBehaviour {
        private Camera _camera;
        [SerializeField] private float multiplier;
        public bool isLocalPlayer = true;
        private void Start() => _camera = Camera.main;

        private void Update() {
            if(!isLocalPlayer) return;
            var mousePos = Input.mousePosition;
            mousePos.z = _camera.nearClipPlane * multiplier;
            var mouseWorld = _camera.ScreenToWorldPoint(mousePos);
            transform.position = mouseWorld;
        }
    }

}