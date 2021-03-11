using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EuclideanRythm : MonoBehaviour {
    public int pulses = 3; // 
    public int totalSteps = 8; // euclidean cycle

    private List<int> _storedRhythm = new List<int>();
    private int _rotate;

    // gcd recursive calculation of euclidean algorithm
    private int CalculateEuclideanAlgorithm(int a, int b) {
        if (b == 0) return a;
        return CalculateEuclideanAlgorithm(b, a % b);
    }

    private void Start() {
        // print(CalculateEuclideanAlgorithm(pulses, totalSteps));
        for (int i = 0; i < totalSteps; i++) {
            GetEuclideanRythm(totalSteps, pulses);
            _rotate++;
        }
    }

    // calculate a euclidean rhythm
    // ref --> https://www.computermusicdesign.com/simplest-euclidean-rhythm-algorithm-explained/
    public List<int> GetEuclideanRythm(int steps, int pulses) {
        //the length of the array is equal to the number of steps
        //a value of 1 for each array element indicates a pulse
        _storedRhythm.Clear();

        var bucket = 0; //out variable to add pulses together for each step

        for (int i = 0; i < steps; i++) {
            bucket += pulses;
            if (bucket >= steps) {
                bucket -= steps;
                _storedRhythm.Add(1); //'1' indicates a pulse on this beat
            }
            else {
                _storedRhythm.Add(0); //'0' indicates no pulse on this beat
            }
        }

        if (_rotate > 0) RotateSeq(_rotate);

        print(string.Join(",", _storedRhythm));
        return _storedRhythm;
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
        var output = new int[_storedRhythm.Count]; // new array to store shifted rhythm
        var val = _storedRhythm.Count - rotate;

        for (int i = 0; i < _storedRhythm.Count; i++) {
            output[i] = _storedRhythm[Mathf.Abs((i + val) % _storedRhythm.Count)];
        }

        _storedRhythm = output.ToList();
    }

    //  find out if there is a pulse on the current beat
    private bool IsCurrentBeatPulse(int currentBeat) {
        var curStep = currentBeat % totalSteps; // wraps beat around if it is higher than the number of steps
        return _storedRhythm[curStep] == 1;
    }
}