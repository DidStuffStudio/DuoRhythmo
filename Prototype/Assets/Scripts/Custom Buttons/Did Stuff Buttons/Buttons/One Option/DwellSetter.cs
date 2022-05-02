using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class DwellSetter : OneOption
    {
        [SerializeField] private float localDwellTime;


        protected override void OnEnable()
        {
            base.OnEnable();
            if (Mathf.Approximately(localDwellTime, PlayerPrefs.GetFloat("DwellTime"))) return;
            ActivateButton();
            SetDwellTime();
        }

        private void SetDwellTime()
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
    }
}