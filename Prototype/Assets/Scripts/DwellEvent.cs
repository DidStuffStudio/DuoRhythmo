using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using UnityEngine;

public class DwellEvent : MonoBehaviour
{
    private AbstractDidStuffButton _btn;

    private void Awake()
    {
        _btn = GetComponentInParent<AbstractDidStuffButton>();
    }

    private void PassEvent() => _btn.DwellFilled();
    
}
