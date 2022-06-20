using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class InteractionSetter : OneOption
    {
        private bool _preferredMethod;
        private bool _initialisedInteractionSetter;
        protected override void OnEnable()
        {
            base.OnEnable();
            if (!_initialisedInteractionSetter) return;
            _preferredMethod = InteractionData.Instance.Method == localInteractionMethod;
            if (!_preferredMethod) return;
            ActivateButton();
            SetPreferredInteraction();
        }

        protected override void ChangeToInactiveState()
        {
            base.ChangeToInactiveState();
            //_isHover = false;
        }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            SetPreferredInteraction();
        }

        private void SetPreferredInteraction()
        {
            SetInteractionMethod(localInteractionMethod);
        }

        protected override void OnDisable()
        {
            _initialisedInteractionSetter = true;
            base.OnDisable();
        }
    }
}
