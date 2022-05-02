namespace Custom_Buttons.Did_Stuff_Buttons.Buttons.One_Option
{
    public class InteractionSetter : OneOption
    {
        protected override void Extras()
        {
            base.Extras();
            SetInteractionMethod(localInteractionMethod);
            print("Called from here");
        }
    }
}
