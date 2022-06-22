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
      /*var pos = GetComponent<RectTransform>().position;
      _position = new Vector3(pos.x, pos.y, 1);*/
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
    }
    /*void GetPoint () {
      //the new bits:
      Rect r = RectTransformToScreenSpace(GetComponent<RectTransform>());
      Vector3 v3 = new Vector3(r.xMin, r.yMin, 1);

      //more or less original code:
      Vector3 val = Camera.main.ScreenToWorldPoint(v3);
      val = transform.InverseTransformPoint(val) * -1;
      _position = new Vector3(val.x, val.y, val.z);
    }

//helper function:
    public static Rect RectTransformToScreenSpace(RectTransform transform) {
      Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
      Rect rect = new Rect(transform.position.x, transform.position.y, size.x, size.y);
      rect.x -= (transform.pivot.x * size.x);
      rect.y -= ((1.0f - transform.pivot.y) * size.y);
      return rect;
    }*/
    
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
