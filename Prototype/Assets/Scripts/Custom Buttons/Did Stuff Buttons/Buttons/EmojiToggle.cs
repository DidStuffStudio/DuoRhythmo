using System.Collections.Generic;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
    public class EmojiToggle : AbstractDidStuffButton
    {
        [SerializeField] private List<GameObject> buttonsToToggle = new List<GameObject>();
        [SerializeField] private OneShotButton[] incrementButtons = new OneShotButton[2];
        [SerializeField] private Animator[] animators;
        private static readonly int Speed = Animator.StringToHash("Speed");

        
        protected override void ChangeToActiveState()
        {
            for (var index = 0; index < buttonsToToggle.Count; index++)
            {
                animators[index].SetFloat(Speed, 1);
                animators[index].Play($"PlayEmoji");
            }
        }

        protected override void ChangeToInactiveState()
        {
            for (var index = 0; index < buttonsToToggle.Count; index++)
            {
                animators[index].SetFloat(Speed, -1);
                animators[index].Play($"PlayEmoji");
            }
        }
        
        
    
    }
}