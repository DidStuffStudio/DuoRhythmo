using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class InteractionSetter : OneOption
    {
        private bool _preferredMethod;
        protected override void OnEnable()
        {
            base.OnEnable();
            if (InteractionManager.Instance.Method == localInteractionMethod) _preferredMethod = true;
            else _preferredMethod = false;
        }

        protected override void Start()
        {
            base.Start();
            if (_preferredMethod)
            {
                ActivateButton();
                SetPreferredInteraction();
            }
        }
        

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            SetPreferredInteraction();
        }

        private void SetPreferredInteraction()
        {
            SetInteractionMethod(localInteractionMethod);
            PlayerPrefs.SetInt("InteractionMethod", (int)localInteractionMethod);
            ActivateCollider(localInteractionMethod == InteractionMethod.Tobii);
        }
        
    }
}
