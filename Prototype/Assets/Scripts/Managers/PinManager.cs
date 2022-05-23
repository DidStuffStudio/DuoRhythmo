using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Custom_Buttons.Did_Stuff_Buttons.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class PinManager : MonoBehaviour
    {
        private String _pin;
        private List<int> pinIntegers = new List<int>();
        private int _currentIndex = 0;
        private Vector2 _lastPos;
        [SerializeField] private float lineWidth;
        [SerializeField] private Color lineColor;
        [SerializeField] private Transform parent;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private MainMenuManager mainMenuManager;
        private List<PasswordButton> _pinButtons = new List<PasswordButton>();
        private List<Vector3> _transforms = new List<Vector3>();

        private void Start()
        {
            _pinButtons.AddRange(GetComponentsInChildren<PasswordButton>());
            res = Screen.currentResolution;
        }

        public void ClearPin()
        {
            foreach (var btn in _pinButtons)
            {
                btn.Clear();
            }
            pinIntegers.Clear();
            lineRenderer.positionCount = 0;
            
        }

        public void SetPinCharacter(int value, Vector3 anchoredPos)
        {
           
            if (_currentIndex >= 0)
            {
                lineRenderer.gameObject.SetActive(true);
                var positionIndex = _currentIndex + 1;
                lineRenderer.positionCount = positionIndex;
                _transforms.Add(anchoredPos);
                var correctedTransform = new Vector3(anchoredPos.x-Screen.width/2, anchoredPos.y-Screen.height/2, Screen.height); //Don't ask, I don't know
                lineRenderer.SetPosition(_currentIndex, correctedTransform);
                lineRenderer.widthMultiplier = 50.0f;
            }
            _currentIndex++;
            pinIntegers.Add(value);
        }
        
        Resolution res;
        
        // Update is called once per frame
        void Update () {
     
            if (res.width != Screen.width || res.height != Screen.height) {
         
                CalculateNewLinePositions();
                res = Screen.currentResolution;
            }
 
        }

        void CalculateNewLinePositions()
        {
            for (var index = 0; index < _transforms.Count; index++)
            {
                var t = _transforms[index];
                var correctedT = new Vector3(t.x-Screen.width/2, t.y-Screen.height/2, Screen.height);
                lineRenderer.SetPosition(index, correctedT);
            }
        }
        
        public void SetPin()
        {
            _pin = pinIntegers.Select(i => i.ToString()).Aggregate((i, j) => i + j);
            mainMenuManager.SetPin(_pin);
            ClearPin();
        }
        
        public void StorePinForCheck() => mainMenuManager.ReferencePin = _pin;

        
    }
}

 