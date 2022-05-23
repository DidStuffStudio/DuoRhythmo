using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
{
  public class PasswordButton : AbstractDidStuffButton
  {
    private PinManager _pinManager;
    [SerializeField] private int _value;
    private Vector2 _position;


    private void Start()
    {
      _pinManager = transform.parent.GetComponentInParent<PinManager>();
      _position = GetComponent<RectTransform>().anchoredPosition;
    }

    protected override void ButtonClicked()
    {
      base.ButtonClicked();
      _pinManager.SetPinCharacter(_value, transform.position);
      SetCanHover(false);
    }

    public void Clear()
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
