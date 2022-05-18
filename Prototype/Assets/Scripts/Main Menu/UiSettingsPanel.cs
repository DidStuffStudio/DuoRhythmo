using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSettingsPanel : UIPanel
{
    public GameObject logInOrSignUp;
    public GameObject logOut;
    public override void ExecuteSpecificChanges()
    {
        if (logInOrSignUp.activeInHierarchy)
        {
            logInOrSignUp.SetActive(false);
            logOut.SetActive(true);
        }
        else
        {
            logInOrSignUp.SetActive(true);
            logOut.SetActive(false);
        }
    }
}
