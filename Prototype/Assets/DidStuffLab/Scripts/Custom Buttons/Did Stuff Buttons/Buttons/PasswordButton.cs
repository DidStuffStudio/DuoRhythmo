using DidStuffLab.Scripts.Managers;
using UnityEngine;

namespace DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons.Buttons
{
  public class PasswordButton : AbstractDidStuffButton
  {
    private PinManager _pinManager;
    [SerializeField] private int _value;
    private Vector3 _position;


    protected override void Start()
    {
      base.Start();
      _pinManager = transform.parent.GetComponentInParent<PinManager>();
      _position = GetComponent<RectTransform>().anchoredPosition;
    }

    protected override void ButtonClicked()
    {
      base.ButtonClicked();
      _pinManager.SetPinCharacter(_value, _position);
      SetCanHover(false);
    }
    
    public void ResetButton()
    {
      DeactivateButton();
      SetCanHover(true);
    }

    protected override void ChangeToActiveState()
    {
      base.ChangeToActiveState();
      SetCanHover(false);
    }

    protected override void StartInteractionCoolDown() { }
  
  }
}
