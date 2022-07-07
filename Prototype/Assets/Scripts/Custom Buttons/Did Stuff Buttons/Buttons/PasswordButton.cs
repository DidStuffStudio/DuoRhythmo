using Managers;
using UnityEngine;

namespace Custom_Buttons.Did_Stuff_Buttons.Buttons
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
      GetPoint();
    }

    protected override void ButtonClicked()
    {
      base.ButtonClicked();
      _pinManager.SetPinCharacter(_value, _position);
      SetCanHover(false);
    }


    void GetPoint()
    {
      var rectT = transform.parent.GetComponent<RectTransform>();
      RectTransformUtility.ScreenPointToWorldPointInRectangle(
        rectT, RectTransformUtility.WorldToScreenPoint(null,transform.position), null,
        out var point);
      _position = new Vector3(point.x, point.y, 0);
      _position = GetComponent<RectTransform>().anchoredPosition;
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
