using System;
using System.Collections;
using System.Collections.Generic;
using Custom_Buttons.Did_Stuff_Buttons;
using Managers;
using TMPro;
using UnityEngine;

public class DidStuffInvite : MonoBehaviour
{
    [SerializeField] private string invitationMessage = " has invited you to a jam!";
    public string username = "ctsalidis";
    [SerializeField] private TextMeshProUGUI textField;
    private Animator _uiAnimator;
    private float _animationTime = 1.0f; // Change for different animation length or come up with better way
    [SerializeField] private AbstractDidStuffButton acceptButton;
    [SerializeField] private AbstractDidStuffButton rejectButton;
    private MainMenuManager mainMenuManager;

    private void OnEnable()
    {
        acceptButton.OnClick += AcceptRequest;
        rejectButton.OnClick += PlayDeclineRequest;
    }

    private void Awake()
    {
        _uiAnimator = GetComponent<Animator>();
        mainMenuManager = GetComponentInParent<MainMenuManager>();

    }

    public void SetInviter()
    {
        //Playfab ID stuff
        textField.text = username + invitationMessage;

    }

    public void AcceptRequest()
    {
        _uiAnimator.Play("InvitePopOut");
        mainMenuManager.AcceptInvite();
    }
    
    public void PlayDeclineRequest()
    {
        mainMenuManager.DeclineInvite();
        _uiAnimator.Play("InvitePopOut");
        StartCoroutine(Destroy());
    }

    public void DestroyForNew() =>  Destroy(gameObject);

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_animationTime + 0.1f);
        Destroy(gameObject);
    }
    
    private void OnDisable()
    {
        acceptButton.OnClick -= AcceptRequest;
        rejectButton.OnClick -= PlayDeclineRequest;
    }
}
