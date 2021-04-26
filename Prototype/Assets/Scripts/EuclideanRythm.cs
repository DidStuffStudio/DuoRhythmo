using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EuclideanRythm : MonoBehaviour {
    public int pulses = 3; // 
    private int totalSteps = 8; // euclidean cycle

    public List<int> _euclideanValues = new List<int>();
    private int _rotate;

    // gcd recursive calculation of euclidean algorithm
    private int CalculateEuclideanAlgorithm(int a, int b) {
        if (b == 0) return a;
        return CalculateEuclideanAlgorithm(b, a % b);
    }

    public List<int> ActivateEuclideanRhythm(bool activate) {
        if(!activate) _euclideanValues.Clear();
        else GetEuclideanRythm();
        return _euclideanValues;
    }

    // calculate a euclidean rhythm
    // ref --> https://www.computermusicdesign.com/simplest-euclidean-rhythm-algorithm-explained/
    public void GetEuclideanRythm() {
        totalSteps = MasterManager.Instance.numberOfNodes;
        pulses = MasterManager.Instance.numberOfNodes / 2;
        //the length of the array is equal to the number of steps
        //a value of 1 for each array element indicates a pulse
        _euclideanValues.Clear();

        var bucket = 0; // out variable to add pulses together for each step

        for (int i = 0; i < totalSteps; i++) {
            bucket += pulses;
            if (bucket >= totalSteps) {
                bucket -= totalSteps;
                _euclideanValues.Add(1); //'1' indicates a pulse on this beat
            }
            else {
                _euclideanValues.Add(0); //'0' indicates no pulse on this beat
            }
        }

        if (_rotate > 0) RotateSeq(_rotate);

        print(string.Join(",", _euclideanValues));
    }

    /// <summary>
    /// Rotate the euclidean rhythm by a set amount
    /// </summary>
    /// <param name="amount">Number of steps to rotate around</param>
    /// <param name="direction">// direction -1 to go left or 1 to go right</param>
    public void UpdateRotation(int amount, int direction) {
        _rotate += amount * direction;
        _rotate %= totalSteps;
    }
    private void RotateSeq(int rotate) {
        var output = new int[_euclideanValues.Count]; // new array to store shifted rhythm
        var val = _euclideanValues.Count - rotate;

        for (int i = 0; i < _euclideanValues.Count; i++) {
            output[i] = _euclideanValues[Mathf.Abs((i + val) % _euclideanValues.Count)];
        }

        _euclideanValues = output.ToList();
    }

    //  find out if there is a pulse on the current beat
    private bool IsCurrentBeatPulse(int currentBeat) {
        var curStep = currentBeat % totalSteps; // wraps beat around if it is higher than the number of steps
        return _euclideanValues[curStep] == 1;
    }
}