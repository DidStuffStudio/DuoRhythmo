using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDidStuffButton
{
    public delegate void Clicked();

    public event Clicked OnClick;

    public delegate void Hovered();

    public event Hovered OnHover;

    public delegate void UnHovered();

    public event UnHovered OnUnHover;

    public delegate void Deactivated();

    public event Deactivated OnDeactivate;

    public delegate void Activated();

    public  event Activated OnActivate;
}
