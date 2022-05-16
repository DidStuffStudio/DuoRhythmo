using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class DwellSetter : OneOption
    {

        private bool _preferredDwellSpeed;
        protected override void OnEnable()
        {
            base.OnEnable();
            if (Mathf.Approximately(localDwellTime, PlayerPrefs.GetFloat("DwellTime"))) _preferredDwellSpeed = true;
            else _preferredDwellSpeed = false;
  
        }

        protected override void Start()
        {
            base.Start();
            if (!_preferredDwellSpeed) return;
            ActivateButton();
            SetDwellTime();
        }

        private void SetDwellTime()
        {
            DwellTime = localDwellTime;
            SetNewDwellTime();
        }



        
    }
}