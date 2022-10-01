using System;
using System.Collections;
using System.Collections.Generic;
using DidStuffLab.Scripts.Managers;
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

        /*private void Update() {
#if  !UNITY_SERVER
            if (!MasterManager.Instance.isInPosition) return;
            Vector3 mousePos = InteractionData.Instance.InputPosition; //Todo --> Follow other player
            mousePos.z = _camera.nearClipPlane * multiplier;
            var mouseWorld = _camera.ScreenToWorldPoint(mousePos);
            transform.position = mouseWorld;
#endif
        }*/
    }

}