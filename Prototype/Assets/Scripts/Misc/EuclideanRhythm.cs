using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EuclideanRhythm : MonoBehaviour {
    public int pulses = 3; // 
    private int _totalSteps = 8; // euclidean cycle
    [SerializeField] private TextMeshProUGUI feedbackText;
    public List<int> euclideanValues = new List<int>();
    private int _rotate;
    private NodeManager _nodeManager;
    


    private void Start()
    {
        _nodeManager = GetComponent<NodeManager>();
        //feedbackText.text = pulses.ToString();
    }

    // gcd recursive calculation of euclidean algorithm
    private int CalculateEuclideanAlgorithm(int a, int b) {
        if (b == 0) return a;
        return CalculateEuclideanAlgorithm(b, a % b);
    }
    
    
    // calculate a euclidean rhythm
    // ref --> https://www.computermusicdesign.com/simplest-euclidean-rhythm-algorithm-explained/
    public void GetEuclideanRhythm() {
        _totalSteps = MasterManager.Instance.numberOfNodes;
        
        //the length of the array is equal to the number of steps
        //a value of 1 for each array element indicates a pulse
        euclideanValues.Clear();

        var bucket = 0; // out variable to add pulses together for each step

        for (int i = 0; i < _totalSteps; i++) {
            bucket += pulses;
            if (bucket >= _totalSteps) {
                bucket -= _totalSteps;
                euclideanValues.Add(1); //'1' indicates a pulse on this beat
            }
            else {
                euclideanValues.Add(0); //'0' indicates no pulse on this beat
            }
        }

        if (_rotate > 0) RotateSeq(_rotate);
        
    }

    /// <summary>
    /// Rotate the euclidean rhythm by a set amount
    /// </summary>
    /// <param name="amount">Number of steps to rotate around</param>
    /// <param name="direction">// direction -1 to go left or 1 to go right</param>
    
    public void UpdateRotation(int amount, int direction) {
        _rotate += amount * direction;
        _rotate %= _totalSteps;
    }
    private void RotateSeq(int rotate) {
        var output = new int[euclideanValues.Count]; // new array to store shifted rhythm
        var val = euclideanValues.Count - rotate;

        for (int i = 0; i < euclideanValues.Count; i++) {
            output[i] = euclideanValues[Mathf.Abs((i + val) % euclideanValues.Count)];
        }

        euclideanValues = output.ToList();
    }

    //  find out if there is a pulse on the current beat
    private bool IsCurrentBeatPulse(int currentBeat) {
        var curStep = currentBeat % _totalSteps; // wraps beat around if it is higher than the number of steps
        return euclideanValues[curStep] == 1;
    }

    public void ChangePulse(bool increase)
    {
        if(increase && pulses < _totalSteps) pulses++;
        else if (!increase && pulses > 1) pulses--;

        feedbackText.text = pulses.ToString();
        _nodeManager.StartEuclideanRhythmRoutine(true);
    }
    
}