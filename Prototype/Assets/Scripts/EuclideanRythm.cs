using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EuclideanRythm : MonoBehaviour {
    public int pulses = 3; // 
    private int totalSteps = 8; // euclidean cycle
    [SerializeField] private Text feedbackText;
    [SerializeField] private GameObject decrementButton, incrementButton;
    public List<int> _euclideanValues = new List<int>();
    private int _rotate;
    private NodeManager _nodeManager;


    private void Start()
    {
        _nodeManager = GetComponent<NodeManager>();
        feedbackText.text = pulses.ToString();
    }

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

    public void ChangePulse(bool increase)
    {
        if(increase && pulses < totalSteps) pulses++;
        else if (!increase && pulses > 1) pulses--;

        feedbackText.text = pulses.ToString();
        _nodeManager.StartEuclideanRhythmRoutine(true);
    }

    public void TurnOnEuclideanInterface(bool turnOn)
    {
        incrementButton.SetActive(turnOn);
        decrementButton.SetActive(turnOn);
        feedbackText.gameObject.SetActive(turnOn);
        incrementButton.layer = decrementButton.layer = LayerMask.NameToLayer("RenderPanel");
    }
}