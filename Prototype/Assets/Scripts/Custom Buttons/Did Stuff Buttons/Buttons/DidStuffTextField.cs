using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class DidStuffTextField : AbstractDidStuffButton
    {
        [SerializeField] private Image caret;
        [SerializeField] private float caretBlinkRate;
        [SerializeField] private Color caretColor;
        private Color _caretColorNoAlpha = Color.clear;
        private Color _caretCurrentColor = Color.clear;
        private bool _caretShowing;
        private string _currentText;
        [SerializeField] private TextMeshProUGUI tMP;
        private Vector3 _caretPos = new Vector3(15,0,0);
        [SerializeField] private float caretOffset = 1f;
        [SerializeField] private int maxNumberCharacters = 20;

        public string text
        {
            get => _currentText;
            set
            {
                _currentText = value;
                UpdateText();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            DeactivateInputField();
            text = "";
        }

        private void Start()
        {
            _dwellGfx.transform.SetParent(transform.parent);
            _dwellGfx.transform.SetSiblingIndex(0);
        }
        
   
        protected override void ChangeToActiveState()
        {
            base.ChangeToActiveState();
            SetCanHover(false);
            StartCoroutine(DisplayCaret());
        }

        
        private void MoveCaret()
        {
            if (_currentText.Length > 0)
                caret.transform.position = _caretPos + new Vector3(tMP.GetRenderedValues(true).x + caretOffset, 0, 0);
            else caret.transform.position = _caretPos;
        }
        
        protected override void Update()
        {
           if(_isActive)TextUpdate();
            base.Update();
        }

        private void TextUpdate()
        {
           if (Input.inputString == null) return;
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // has backspace/delete been pressed?
                {
                    if (_currentText.Length == 0) continue;
                    _currentText = _currentText.Substring(0, _currentText.Length - 1);
                }
                else if ((c == '\n') || (c == '\r')) // enter/return
                {
                    DeactivateInputField();
                    print("User entered their name: " + _currentText);
                }
                else if(_currentText.Length <= maxNumberCharacters)
                {
                    _currentText += c;
                }
            }
            UpdateText();
            
        }
        

        private void UpdateText()
        {
            tMP.text = _currentText;
            MoveCaret();
        }

        private void DeactivateInputField()
        {
            DeactivateButton();
            SetCanHover(true);
            caret.gameObject.SetActive(false);
        }

        protected override void StartInteractionCoolDown() { }

        private IEnumerator DisplayCaret()
        {
            caret.gameObject.SetActive(true);
            while (_isActive)
            {
                _caretCurrentColor = _caretShowing ? caretColor : _caretColorNoAlpha;
                caret.color = _caretCurrentColor;
                yield return new WaitForSeconds(caretBlinkRate);
                _caretShowing = !_caretShowing;
            }
        }
    }
    
    
}
