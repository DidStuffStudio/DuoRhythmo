using Managers;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class DidStuffSaveBeat : OneShotButton
    {
        public void SaveBeat()
        {
            MasterManager.Instance.saveBeat.SaveIntoJson();
        }

        protected override void ButtonClicked()
        {
            base.ButtonClicked();
            SaveBeat();
            //spawn toast
        }
    }
}
