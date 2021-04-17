using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField] private int roundTime;
    public float timer;
    private void Start() => StartCoroutine(Time());

    private IEnumerator Time() {
        timer = roundTime;
        while (true) {
            yield return new WaitForSeconds(1);
            timer--;
            if (timer < 0) timer = roundTime;
            MasterManager.Instance.timer = timer;
        }
    }
}