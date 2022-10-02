using DidStuffLab.Scripts.Managers;

namespace DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class InteractionSetter : OneOption
    {
        private bool _preferredMethod;
        private bool _initialisedInteractionSetter;
        protected override void OnEnable()
        {
            base.OnEnable();
            if (!_initialisedInteractionSetter) return;
            _preferredMethod = InteractionData.Instance.interactionMethod == localInteractionMethod;
            if (!_preferredMethod) return;
            ActivateButton();
            SetPreferredInteraction();
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
