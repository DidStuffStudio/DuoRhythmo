namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class InteractionSetter : OneOption
    {
        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            SetInteractionMethod(localInteractionMethod);
        }
    }
}
