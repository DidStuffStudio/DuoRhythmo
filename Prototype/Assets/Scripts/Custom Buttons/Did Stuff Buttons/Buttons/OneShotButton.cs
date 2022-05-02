namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class OneShotButton : AbstractDidStuffButton
    {
        protected override void ButtonClicked()
        {
            StartInteractionCoolDown();
            InvokeOnClickUnityEvent();
        }
    }
}
