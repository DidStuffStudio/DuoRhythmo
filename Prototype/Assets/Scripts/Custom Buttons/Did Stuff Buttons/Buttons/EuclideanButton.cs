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
        [SerializeField] private Animator[] animators;
        private static readonly int Speed = Animator.StringToHash("Speed");

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
            for (var index = 0; index < buttonsToToggle.Count; index++)
            {
                if (_isActive)
                {
                    animators[index].SetFloat(Speed, -1);
                    animators[index].Play("PlayEuclidean");
                }
                   
                else buttonsToToggle[index].SetActive(_isActive);
            }
        }

        public void ChangePulse(bool increment) => euclideanRhythm.ChangePulse(increment);
    
    }
}
