using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using UnityEngine;

public class PasswordButton : AbstractDidStuffButton
{
  private PinManager _pinManager;
  [SerializeField] private int _value;
  private Vector2 _position;


  private void Start()
  {
    _pinManager = GetComponentInParent<PinManager>();
    _position = GetComponent<RectTransform>().anchoredPosition;
  }

  protected override void ButtonClicked()
  {
    base.ButtonClicked();
    _pinManager.SetPinCharacter(_value, _position);
    SetCanHover(false);
  }
  
  protected override void StartInteractionCoolDown() { }
  
}
