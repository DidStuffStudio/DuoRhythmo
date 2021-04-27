using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwellSpeedMaster : MonoBehaviour
{
    
    
    
    public DwellSpeedButton[] _dwellSpeedButton = new DwellSpeedButton[5];

    private int activeButton; 
    void Start()
    {
        //_dwellSpeedButton[2].activateOnStart = true;
        
        
    }
    
    public void UpdateButtons()
    {
        
        activeButton = IsItActive();
        Debug.Log(activeButton);
        for (int i = 0; i < _dwellSpeedButton.Length; i++)
        {
            _dwellSpeedButton[i].Deactivate();
        }
        _dwellSpeedButton[activeButton].setThisOne();
    }

    int IsItActive()
    {
        int j = 0;
        for (int i = 0; i < _dwellSpeedButton.Length; i++)
        {
            if (_dwellSpeedButton[i].isDwellActive)
            {
                j = i;
            }
        }

        return j;
    }
}
