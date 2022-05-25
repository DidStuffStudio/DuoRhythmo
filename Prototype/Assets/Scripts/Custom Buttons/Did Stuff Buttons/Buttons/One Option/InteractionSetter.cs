using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class InteractionSetter : OneOption
    {
        private bool _preferredMethod;
        [SerializeField] private InteractionManager interactionManager;
        protected override void OnEnable()
        {
            base.OnEnable();
            _preferredMethod = interactionManager.Method == localInteractionMethod;
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
            interactionManager.Method = localInteractionMethod;
            ActivateCollider(localInteractionMethod == InteractionMethod.Tobii);
        }
        
    }
}
