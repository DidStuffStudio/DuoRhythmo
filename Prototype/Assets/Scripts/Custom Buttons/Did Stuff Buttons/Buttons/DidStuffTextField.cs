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
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Image caret;
        [SerializeField] private float caretBlinkRate;
        [SerializeField] private Color caretColor;
        private Color _caretColorNoAlpha = Color.clear;
        private Color _caretCurrentColor = Color.clear;
        private bool _caretShowing;
        private string _currentText;
        private int _caretPosition = 0;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] 
        protected override void OnEnable()
        {
            base.OnEnable();
            if (PlayerPrefs.GetInt("InteractionMethod") != 0) return;
            transform.gameObject.SetActive(false);
            
        }

        private void Start()
        {
            _dwellGfx.transform.SetParent(transform.parent);
            _dwellGfx.transform.SetSiblingIndex(0);
            inputField.targetGraphic.raycastTarget = false;
            
        }

        protected override void SetScaleOfChildren() { }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            inputField.ActivateInputField();
        }
    
        protected override void ChangeToActiveState()
        {
            SetCanHover(false);
            caret.gameObject.SetActive(true);
        }

        public void InputCharacter(string character)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(character, @"[a-zA-Z]") && 
                System.Text.RegularExpressions.Regex.IsMatch(character, @"[0-9]"))
            {
                _currentText += _currentText;
                _caretPosition++;
            }
            else   
            {
                MainMenuManager.Instance.SpawnErrorToast("Please only use letters or numbers", 0.1f);
            }
            
        }

        private void MoveCaret(bool forward)
        {
            if (forward)
            {

                _caretPosition++;
            }
            else
            {

                _caretPosition--;
            }
        }
        
        protected override void Update()
        {
           TextUpdate();
            base.Update();
        }

        private void TextUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Delete))
            {
                if (_caretPosition < _currentText.Length - 1)
                {
                    _currentText.Remove(_caretPosition);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveCaret(false);
            if (Input.GetKeyDown(KeyCode.RightArrow)) MoveCaret(true);

            if (_isActive)
            {
                foreach (char c in Input.inputString)
                {
                    if (c == '\b') // has backspace/delete been pressed?
                    {
                        if (_currentText.Length != 0)
                        {
                            _currentText = _currentText.Substring(0, _currentText.Length - 1);
                        }
                    }
                    else if ((c == '\n') || (c == '\r')) // enter/return
                    {
                        Enter();
                        DeactivateInputField();
                        print("User entered their name: " + _currentText);
                    }
                    else
                    {
                        _currentText += c;
                    }
                }
                
                UpdateText();
            }
        }

        private void Backspace()
        {
            
        }

        private void Enter()
        {
            
        }

        private void UpdateText()
        {
            
        }
        private bool CheckForOtherInput()
        {
            if(KeyCode.)
        }

        public void DeactivateInputField()
        {
            DeactivateButton();
            SetCanHover(true);
            caret.gameObject.SetActive(false);
        }

        protected override void StartInteractionCoolDown() { }

        private IEnumerator DisplayCaret()
        {
            _caretCurrentColor = _caretShowing ? caretColor : _caretColorNoAlpha;
            yield return new WaitForSeconds(caretBlinkRate);
        }
    }
    
    
}
