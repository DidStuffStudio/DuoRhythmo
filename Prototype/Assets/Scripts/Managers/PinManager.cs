using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private MainMenuManager mainMenuManager;
        private List<PasswordButton> _pinButtons = new List<PasswordButton>();

        private void Start()
        {
            _pinButtons.AddRange(GetComponentsInChildren<PasswordButton>());
        }

        public void ClearPin()
        {
            foreach (var btn in _pinButtons)
            {
                btn.Clear();
            }
            pinIntegers.Clear();
        }

        public void SetPinCharacter(int value)
        {
            _currentIndex++;
            pinIntegers.Add(value);
        
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

 