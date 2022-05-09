using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class PanelMaster : MonoBehaviour
{
    public enum UIType
    {
        DrumNodes,
        Effects
    }

    public DrumType drumType;
    public UIType uIType;
    public Color defaultColor, activeColor, hintColor;
    private NodeManager _nodeManager;
    
    void Start()
    {

        switch (uIType)
        {
            case UIType.DrumNodes:
            {
                _nodeManager = GetComponentInChildren<NodeManager>();
                _nodeManager.drumType = drumType;
                break;
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
