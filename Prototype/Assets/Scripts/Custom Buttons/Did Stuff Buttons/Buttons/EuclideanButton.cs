using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class EuclideanButton : AbstractDidStuffButton
    {
        [SerializeField] private List<GameObject> buttonsToToggle = new List<GameObject>();
        [SerializeField] private EuclideanRhythm euclideanRhythm;
        [SerializeField] private NodeManager nodeManager;
        [SerializeField] private OneShotButton[] incrementButtons = new OneShotButton[2];
        private void Start()
        {
            ToggleButtons();
            
        }
        

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            if(_isActive)nodeManager.StoreRhythm();
            ToggleButtons();
            ActivateText(_isActive);
            nodeManager.StartEuclideanRhythmRoutine(_isActive);
        }

        private void ToggleButtons()
        {
            foreach (var btn in buttonsToToggle)
            {
                btn.SetActive(_isActive);
            }
        }

        public void ChangePulse(bool increment) => euclideanRhythm.ChangePulse(increment);
    
    }
}
