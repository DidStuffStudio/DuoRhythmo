using System;
using System.Collections.Generic;
using System.Linq;
using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons.Buttons;
using UnityEngine;

namespace DidStuffLab.Scripts.Managers
{
    public class PinManager : MonoBehaviour
    {
        private String _pin;
        private List<int> pinIntegers = new List<int>();
        private int _currentIndex = 0;
        private Vector3 _lastPos;
        [SerializeField] private float lineWidth;
        [SerializeField] private MainMenuManager mainMenuManager;
        private List<PasswordButton> _pinButtons = new List<PasswordButton>();
        [SerializeField] private GameObject line;
        private int _numberOfNumbers = 10;
        private List<GameObject> _lines = new List<GameObject>();
        public RectTransform vec;
        [SerializeField] private float buttonRadius;

        private void Start()
        {
            _pinButtons.AddRange(GetComponentsInChildren<PasswordButton>());
        }

        public void ClearPin()
        {
            foreach (var btn in _pinButtons) btn.ResetButton();
            foreach (var l in _lines) Destroy(l);
                
            pinIntegers.Clear();
            _lines.Clear();
            _currentIndex = 0;
        }

        public void SetPinCharacter(int value, Vector3 anchoredPos)
        {
            if (_currentIndex > 0)
            {
                var l = Instantiate(line).GetComponent<RectTransform>();
                l.transform.SetParent(transform);
                l.SetSiblingIndex(0);

                var length = Vector3.Distance(_lastPos, anchoredPos)-2*buttonRadius;
                var angle = Mathf.Atan2(anchoredPos.y - _lastPos.y, anchoredPos.x - _lastPos.x) * Mathf.Rad2Deg - 90;
                
                //var vec = new RectTransform();
                //vec.transform.SetParent(transform);
                vec.localRotation =  Quaternion.Euler(0,0,angle);
                vec.anchoredPosition = _lastPos + vec.up * buttonRadius;
                
                l.localScale = Vector3.one;
                l.anchoredPosition = vec.anchoredPosition;
                
                l.localRotation = Quaternion.Euler(0,0,angle);
                l.sizeDelta = new Vector2(lineWidth, length);
                _lines.Add(l.gameObject);
            }
            
            _lastPos = anchoredPos;
            pinIntegers.Add(value);
            _currentIndex++;
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

 