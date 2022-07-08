using DidStuffLab.Scripts.Managers;

namespace DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons.Buttons
{
   public class DidStuffSoloButton : AbstractDidStuffButton
   {
      public int drumTypeIndex = 0;
      public void ForceDeactivate()
      {
         DeactivateButton();
         Mute(false);
      }

      public void Mute(bool mute) => MasterManager.Instance.audioManager.MuteOthers(drumTypeIndex, mute);

      protected override void ButtonClicked()
      {
         base.ButtonClicked();
         if(_isActive)Mute(true);
         else Mute(false);
      }
   }
}
