using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class OneOption : AbstractDidStuffButton
    {
        
        
        private List<OneOption> _otherButtonsToDisable = new  List<OneOption>();

        [SerializeField] private bool defaultOption;
        protected virtual void Start()
        {
            var otherButtons = transform.parent.GetComponentsInChildren<OneOption>().Where(btn => btn != this).ToList();

            _otherButtonsToDisable = otherButtons;
            if(defaultOption) ActivateButton();
        }
        

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            SetCanHover(false);
            
            foreach (var btn in _otherButtonsToDisable)
            {
                btn.DeactivateButton();
                btn.SetCanHover(true);
            }
            
        }
        
        protected override void StartInteractionCoolDown() { }

        protected override void ChangeToActiveState()
        {
            base.ChangeToActiveState();
            SetCanHover(false);
        }
    }
}
