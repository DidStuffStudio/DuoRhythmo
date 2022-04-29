using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;

public class SoloButton : DidStuffButton
{
   public int drumTypeIndex = 0;
   public void ForceDeactivate()
   {
      DeactivateButton();
   }
}
