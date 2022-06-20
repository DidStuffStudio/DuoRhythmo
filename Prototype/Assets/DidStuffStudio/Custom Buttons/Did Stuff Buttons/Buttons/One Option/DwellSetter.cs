using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class DwellSetter : OneOption
    {

        private bool _preferredDwellSpeed;
        private bool _initialisedDwellButton;
        protected override void OnEnable()
        {
            base.OnEnable();
            if (!_initialisedDwellButton) return;
            if (Mathf.Approximately(localDwellTime, InteractionData.Instance.DwellTime)) _preferredDwellSpeed = true;
            else _preferredDwellSpeed = false;
        }

        protected override void Start()
        {
            base.Start();
            if (!_preferredDwellSpeed) return;
            ActivateButton();
            SetDwellTime();
        }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            SetDwellTime();
        }

        private void SetDwellTime()
        {
            DwellTime = localDwellTime;
            SetNewDwellTime();
        }

        protected override void OnDisable()
        {
            _initialisedDwellButton = true;
            base.OnDisable();
        }
    }
}