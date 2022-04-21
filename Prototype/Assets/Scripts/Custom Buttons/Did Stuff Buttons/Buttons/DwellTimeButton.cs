

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class DwellTimeButton : AbstractDidStuffButton
    {
        
        
        private List<DwellTimeButton> _otherButtonsToDisable = new  List<DwellTimeButton>();

        [SerializeField] private bool defaultOption;
        [SerializeField]
        private float localDwellTime = 1.0f;
        private void Start()
        {
            DwellTime = localDwellTime;
            var otherButtons = transform.parent.GetComponentsInChildren<DwellTimeButton>().Where(btn => btn != this).ToList();

            _otherButtonsToDisable = otherButtons;
            if(defaultOption) ActivateButton();

            if (Mathf.Approximately(PlayerPrefs.GetFloat("DwellTime"), localDwellTime))
            {
                ActivateButton();
                SetDwellTime();
            }
            
        }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            base.ToggleHoverable(false);
            foreach (var btn in _otherButtonsToDisable)
            {
                btn.DeactivateButton();
                btn.ToggleHoverable(true);
            }
        }

        public void SetDwellTime()
        {
            DwellTime = localDwellTime;
            SetNewDwellTime();
        }

        protected override void FillDwellFeedback(float speed)
        {
            var localSpeed = 1/localDwellTime;
            if (speed < 0) localSpeed *= -1;
            base.FillDwellFeedback(localSpeed);
        }

        protected override void StartInteractionCoolDown() { }
    }
}
