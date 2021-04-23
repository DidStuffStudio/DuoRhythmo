using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwellSpeedMaster : MonoBehaviour
{
    
    DwellSpeedButton[] _dwellSpeedButton = new DwellSpeedButton[5];

    private int activeButton; 
    void Start()
    {
        _dwellSpeedButton[3].activateOnStart = true;
    }
    
    void Update()
    {
        activeButton = isItActive();
        
        for (int i = 0; i < _dwellSpeedButton.Length; i++)
        {
            _dwellSpeedButton[i].Deactivate();
        }
        _dwellSpeedButton[activeButton].activateOnStart = true;
    }

    int isItActive()
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
