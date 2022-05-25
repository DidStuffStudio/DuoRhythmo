using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class DwellSetter : OneOption
    {

        private bool _preferredDwellSpeed;
        [SerializeField] private InteractionManager interactionManager;
        protected override void OnEnable()
        {
            base.OnEnable();
            if (Mathf.Approximately(localDwellTime, interactionManager.DwellTime)) _preferredDwellSpeed = true;
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
        
    }
}