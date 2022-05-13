using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class InteractionSetter : OneOption
    {
        private bool _preferredMethod;
        protected override void OnEnable()
        {
            base.OnEnable();
            if (PlayerPrefs.GetInt("InteractionMethod") == (int) localInteractionMethod) _preferredMethod = true;
            else _preferredMethod = false;
        }

        protected override void Start()
        {
            base.Start();
            if(_preferredMethod) ActivateAndCallEvents();
        }

        protected override void Extras()
        {
            SetInteractionMethod(localInteractionMethod);
            SetPreferredInteraction();
        }

        private void SetPreferredInteraction()
        {
            PlayerPrefs.SetInt("InteractionMethod", (int)localInteractionMethod);
            ActivateCollider(localInteractionMethod == InteractionMethod.Tobii);
        }
    }
}
