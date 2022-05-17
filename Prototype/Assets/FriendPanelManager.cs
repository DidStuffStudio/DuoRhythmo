using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DidStuffLab;
using Managers;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendPanelManager : MonoBehaviour
{
    private string[] _currentUsernames = new string[2];
    private string[] _currentAvatars = new string[2];

    
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject rightArrow, leftArrow;
    [SerializeField] private GameObject logInToAddViewFriends;
    [SerializeField] private GameObject addFriendCard, friend1Card, friend2Card;
    [SerializeField] private GameObject dotParent, dot;
    [SerializeField] private float dotSpacing;
    [SerializeField] private Color dotActive, dotInactive;
    
    [SerializeField] private List<Image> friendImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> friendTexts = new List<TextMeshProUGUI>();

    [SerializeField] private List<GameObject> confirmedInteraction = new List<GameObject>();
    [SerializeField] private List<GameObject> requestInteraction = new List<GameObject>();
    [SerializeField] private List<GameObject> pendingInteraction = new List<GameObject>();

    private List<Image> _dots = new List<Image>();
    private List<string> _requesterUsernames = new List<string>(); //Friend request
    private List<string> _requesteeUsernames = new List<string>(); //Pending
    private List<string> _confirmedUsernames = new List<string>(); //Friend

    private List<string> _requesterAvatars = new List<string>(); //Friend request
    private List<string> _requesteeAvatars = new List<string>(); //Pending
    private List<string> _confirmedAvatars = new List<string>(); //Friend

    private List<string> _fullFriendsListUsernames = new List<string>();
    private List<string> _fullFriendsListAvatars = new List<string>();
    private Dictionary<string, FriendStatus> _friendStatusMap = new Dictionary<string, FriendStatus>();
    private int currentListIndex = 0;
    private bool _even;
    private List<Friend> _friends = new List<Friend>();

    public List<Friend> Friends
    {
        get => _friends;
        set => _friends = value;
    }

    private void OnEnable()
    {
        if (MainMenuManager.Instance.IsGuest) DisableAllInteraction();
        if (MainMenuManager.Instance.LoggedIn)
        {
            FriendsManager.Instance.GetFriends();
            GetFriendDetails();
            DoTheDots();
            if (_fullFriendsListUsernames.Count > 1)
            {
                ChangeGraphics(2);
                ChangeDots();
            }
            else if (_fullFriendsListUsernames.Count == 1)
            {
                ChangeGraphics(1);
                ActivateSecondFriend(false);
            }
            else
            {
                friend1Card.SetActive(false);
                friend2Card.SetActive(false);
            }
        }
    }

    public void Navigate(bool right)
    {
        if (right) currentListIndex+=2;
        else currentListIndex-=2;

        leftArrow.SetActive(currentListIndex != 0);
        
        if(_even)rightArrow.SetActive(currentListIndex != _fullFriendsListUsernames.Count);
        if(!_even)rightArrow.SetActive(currentListIndex != _fullFriendsListUsernames.Count-1);
        if(!_even) friend2Card.SetActive(currentListIndex != _fullFriendsListUsernames.Count-1);
        var index = 2;
        if (!_even && currentListIndex == _fullFriendsListUsernames.Count-1) index = 1;
        ChangeGraphics(index);
        ChangeDots();
    }

    void ChangeDots()
    {
        foreach (var dot in _dots)
        {
            dot.color = dotInactive;
        }

        var i = 0;
        if (_even) i = currentListIndex / 2;
        else i = (int)(currentListIndex/2.0f);
        _dots[i].color = dotActive;
    }
    
    

    private void ChangeGraphics(int index)
    {
        for (int i = 0; i < index; i++)
        {
            _currentUsernames[i] = _fullFriendsListUsernames[currentListIndex + i];
            _currentAvatars[i] = _fullFriendsListAvatars[currentListIndex + i];
            friendTexts[i].text = _currentUsernames[i];
            friendImages[i].sprite = Resources.Load<Sprite>("Avatars/" + _currentAvatars[i]);
            if (_friendStatusMap[_currentUsernames[i]] == FriendStatus.Confirmed)
                SwitchFriendStatusButtons(i, FriendStatus.Confirmed);
            else if (_friendStatusMap[_currentUsernames[i]] == FriendStatus.Requestee)
                SwitchFriendStatusButtons(i, FriendStatus.Requestee);
            if (_friendStatusMap[_currentUsernames[i]] == FriendStatus.Requester)
                SwitchFriendStatusButtons(i, FriendStatus.Requester);
        }
    }

    private void SwitchFriendStatusButtons(int i, FriendStatus status)
    {
        switch (status)
        {
            case FriendStatus.Confirmed:
                confirmedInteraction[i].SetActive(true);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(false);
                break;
            case FriendStatus.Requester:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(true);
                pendingInteraction[i].SetActive(false);
                break;
            case FriendStatus.Requestee:
                confirmedInteraction[i].SetActive(false);
                requestInteraction[i].SetActive(false);
                pendingInteraction[i].SetActive(true);
                break;
        }
    }

    private void DisableAllInteraction()
    {
        addFriendCard.SetActive(false);
        friend1Card.SetActive(false);
        friend2Card.SetActive(false);
        logInToAddViewFriends.SetActive(true);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }

    private void ActivateSecondFriend(bool activate)
    {
        friend2Card.SetActive(activate);
    }

    public void AcceptFriendRequest(int index) => FriendsManager.Instance.AcceptFriendRequest(_currentUsernames[index]);
    public void DeclineFriendRequest(int index) => FriendsManager.Instance.DenyFriendRequest(_currentUsernames[index]);
    public void RemoveFriend(int index) => FriendsManager.Instance.RemoveFriend(_currentUsernames[index]);

    public void AddFriend() {
        var stringToPass = inputField.text;
        FriendsManager.Instance.SendFriendRequest(stringToPass);
        inputField.text = "";
    }

    private void DisableArrowsAndDots()
    {
        dotParent.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }
private void DoTheDots()
    {
        var numberOfDots = Mathf.Ceil(_fullFriendsListUsernames.Count/2.0f);
        var xShift = (numberOfDots - 1) * dotSpacing;
        for (int i = 0; i < numberOfDots; i++)
        {
            var dotClone = Instantiate(dot, dotParent.transform);
            dotClone.transform.Translate(-xShift+dotSpacing*i,0,0);
            _dots.Add(dotClone.GetComponent<Image>());
        }
    }
    private void GetFriendDetails()
    {
        ClearLists();
        print("Getting Friends");
        _friends.AddRange(FriendsManager.Instance.FriendsDetails);
        print(_friends.Count);
        
        foreach (var friend in _friends)
        {
            switch (friend.FriendStatus)
            {
                case FriendStatus.Requestee:
                    _requesteeUsernames.Add(friend.Username);
                    _requesteeAvatars.Add(friend.AvatarName);
                    break;
                
                case FriendStatus.Requester:
                    _requesterUsernames.Add(friend.Username);
                    _requesterAvatars.Add(friend.AvatarName);                    
                    break;
                
                case FriendStatus.Confirmed:
                    _confirmedUsernames.Add(friend.Username);
                    _confirmedAvatars.Add(friend.AvatarName);
                    break;
                
                case FriendStatus.Default:
                    print("friend status default");
                    break;
                default:
                    print("Didn't get type");
                    break;
            }
        }

        for (int i = 0; i < _requesterUsernames.Count; i++)
        {
            _fullFriendsListUsernames.Add(_requesterUsernames[i]);
            _fullFriendsListAvatars.Add( _requesterAvatars[i]);
            _friendStatusMap.Add(_requesterUsernames[i], FriendStatus.Requester);
        }

        for (int i = 0; i < _confirmedUsernames.Count; i++)
        {
            var index = _requesterUsernames.Count + i;
            _fullFriendsListUsernames.Add(_confirmedUsernames[i]);
            _fullFriendsListAvatars.Add( _confirmedAvatars[i]);
            _friendStatusMap.Add(_confirmedUsernames[i], FriendStatus.Confirmed);
        }
        
        for (int i = 0; i < _requesteeUsernames.Count; i++)
        {
            var index = _requesterUsernames.Count + _confirmedUsernames.Count + i;
            _fullFriendsListUsernames.Add( _requesteeUsernames[i]);
            _fullFriendsListAvatars.Add( _requesteeAvatars[i]);
            _friendStatusMap.Add(_requesteeUsernames[i], FriendStatus.Requestee);
        }
        
        if(_fullFriendsListUsernames.Count <= 2) DisableArrowsAndDots();
        _even = _fullFriendsListUsernames.Count % 2 == 0;
    }

    private void ClearLists()
    {
        foreach (var d in _dots) Destroy(d.gameObject);
        _friends.Clear();
        _requesteeUsernames.Clear();
        _requesteeAvatars.Clear();
        _requesterUsernames.Clear();
        _requesterAvatars.Clear();
        _confirmedUsernames.Clear();
        _confirmedAvatars.Clear();
        _fullFriendsListAvatars.Clear();
        _fullFriendsListUsernames.Clear();
        _friendStatusMap.Clear();
        _dots.Clear();
    }

    
}
