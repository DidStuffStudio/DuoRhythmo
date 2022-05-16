using System;
using TMPro;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class ActivateTextField : AbstractDidStuffButton
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Transform areaToInteract;
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
        }

        protected override void StartInteractionCoolDown() { }
    }
}
