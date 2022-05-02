using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffToast : MonoBehaviour
{
    [SerializeField] private float secondsBeforeTurnOff = 2.0f;
    private void OnEnable() => StartCoroutine(TurnSelfOff());


    IEnumerator TurnSelfOff()
    {
        yield return new WaitForSeconds(secondsBeforeTurnOff);
        transform.gameObject.SetActive(false);
    }
}
