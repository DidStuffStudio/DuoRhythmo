using System;
using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiFriendsManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> navigationArrows; //0 = Left, 1 = Right
    [SerializeField] private GameObject logInToAddViewFriends;
    
    
    [SerializeField] protected List<GameObject> friendCards = new List<GameObject>();
    [SerializeField] private List<Image> friendImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> friendTexts = new List<TextMeshProUGUI>();
    
    [Header("Dots")]
    [SerializeField] private GameObject dotParent, dot;
    [SerializeField] private float dotSpacing;
    [SerializeField] private Color dotActive, dotInactive;

    private List<Friend> _friends;
    protected List<string> _allFriendUsernames = new List<string>();
    protected List<string> _confirmedFriendUsernames = new List<string>();
    protected List<string> _allFriendAvatars = new List<string>();
    protected List<string> _confirmedFriendAvatars = new List<string>();
    protected Dictionary<string, FriendStatus> _friendStatusMap = new Dictionary<string, FriendStatus>();

    protected List<string> listToLoopUsernames = new List<string>();
    protected List<string> listToLoopAvatars = new List<string>();

    protected int _numberOfCards = 2;
    protected int _numberFriends = 0;
    protected int _displayedFriends = 2;
    private int _currentListIndex = 0;
    private int _page;
    private int _numberOfPages;
    private int _numberOfCardsOnLastPage;
    private List<Image> _dots;
    

    private bool _even; //Set from Child Class
    protected string[] _currentUsernames = new string[2];
    protected string[] _currentAvatars = new string[2];

    protected virtual void OnEnable()
    {
        if (MainMenuManager.Instance.IsGuest) DisableAllInteraction();
        
        if (MainMenuManager.Instance.LoggedIn)
        {
            FriendsManager.Instance.GetFriends();
            GetFriendDetails();
            Initialise();
            InitialiseGraphics();
            
        }
    }

    protected virtual void Initialise()
    {
        navigationArrows[0].SetActive(false);
        _numberOfPages = (int)(listToLoopUsernames.Count / _numberOfCards);
        _numberOfCardsOnLastPage = listToLoopUsernames.Count % _numberOfCards;
        _numberFriends = listToLoopUsernames.Count;
        if(_numberFriends <= 2) DisableArrowsAndDots();
        _even = _numberFriends % 2 == 0;
    }
    
    private void InitialiseGraphics()
    {
        DoTheDots();
        if (_numberFriends == 0) return;
        if (_numberFriends <= _numberOfCards) _displayedFriends = _numberFriends;
        else _displayedFriends = _numberOfCards;
        ChangeGraphics();
    }


    public void Navigate(bool right)
    {
        if (right)
        {
            _page++;
            _currentListIndex+=_numberOfCards;
        }
        else
        {
            _page--;
            _currentListIndex-=_numberOfCards;
        }
        
        navigationArrows[0].SetActive(_page != 0);
        navigationArrows[1].SetActive(_page != _numberOfPages);
        _displayedFriends = _numberOfCards;
        if (_page == _numberOfPages)
        {
            ActivateLastCards(_numberOfCardsOnLastPage, false);
            _displayedFriends = _numberOfCardsOnLastPage;
        }
        else ActivateLastCards(_numberOfCardsOnLastPage, true);
        
        ChangeGraphics();
        ChangeDots();
    }


    protected virtual void ChangeGraphics()
    {
        for (int i = 0; i < _displayedFriends; i++)
        {
            _currentUsernames[i] = listToLoopUsernames[_currentListIndex + i];
            _currentAvatars[i] = listToLoopAvatars[_currentListIndex + i];
            friendTexts[i].text = _currentUsernames[i];
            friendImages[i].sprite = Resources.Load<Sprite>("Avatars/" + _currentAvatars[i]);
        }
    }
    
    private void ActivateLastCards(int indexToKeepActive, bool activate)
    {
        if (indexToKeepActive == 0) return;
        for (var index = _numberOfCards-1; index < indexToKeepActive; index--)
        { 
            friendCards[index].SetActive(activate);
        }
    }

    protected virtual void DisableAllInteraction()
    {
        
        foreach (var card in friendCards)card.SetActive(false);
        logInToAddViewFriends.SetActive(true);
        Instantiate(logInToAddViewFriends, transform);
        foreach (var arrow in navigationArrows)
        {
            arrow.SetActive(false);
        }
    }

    
    void ChangeDots()
    {
        if (_numberFriends < _numberOfCards) return;
        foreach (var d in _dots)
        {
            d.color = dotInactive;
        }
        var i = 0;
        if (_even) i = _currentListIndex / _numberOfCards;
        else i = (int)(_currentListIndex/_numberOfCards);
        _dots[i].color = dotActive;
    }

        private void DisableArrowsAndDots()
    {
        dotParent.SetActive(false);
        foreach (var arrow in navigationArrows)
        {
            arrow.SetActive(false);
        }
    }
private void DoTheDots()
{
    if (_numberFriends < _numberOfCards) return;
        var numberOfDots = Mathf.Ceil(_numberFriends/_numberOfCards);
        var xShift = (numberOfDots - 1) * dotSpacing;
        for (int i = 0; i < numberOfDots; i++)
        {
            var dotClone = Instantiate(dot, dotParent.transform);
            dotClone.transform.Translate(-xShift+dotSpacing*i,0,0);
            var dotImage = dotClone.GetComponent<Image>();
            dotImage.color = dotInactive;
            _dots.Add(dotImage);
        }

        _dots[0].color = dotActive;
}
    private void GetFriendDetails()
    {
        ClearLists(); 
        _friends.AddRange(FriendsManager.Instance.FriendsDetails);
        
        var requesterUsernames = new List<string>(); //Friend request
        var requesteeUsernames = new List<string>(); //Pending
        var confirmedUsernames = new List<string>(); //Friend

        var requesterAvatars = new List<string>(); //Friend request
        var requesteeAvatars = new List<string>(); //Pending
        var confirmedAvatars = new List<string>(); //Friend
        
        foreach (var friend in _friends)
        {
            switch (friend.FriendStatus)
            {
                case FriendStatus.Requestee:
                    requesteeUsernames.Add(friend.Username);
                    requesteeAvatars.Add(friend.AvatarName);
                    break;
                
                case FriendStatus.Requester:
                    requesterUsernames.Add(friend.Username);
                    requesterAvatars.Add(friend.AvatarName);                    
                    break;
                
                case FriendStatus.Confirmed:
                    confirmedUsernames.Add(friend.Username);
                    confirmedAvatars.Add(friend.AvatarName);
                    break;
                
                case FriendStatus.Default:
                    
                    break;
            }
        }
        
        _confirmedFriendUsernames.AddRange(confirmedUsernames);
        _confirmedFriendAvatars.AddRange(confirmedAvatars);

        for (int i = 0; i < requesterUsernames.Count; i++)
        {
            _allFriendUsernames.Add(requesterUsernames[i]);
            _allFriendUsernames.Add( requesterAvatars[i]);
            _friendStatusMap.Add(requesterUsernames[i], FriendStatus.Requester);
        }

        for (int i = 0; i < confirmedUsernames.Count; i++)
        {
            var index = requesterUsernames.Count + i;
            _allFriendUsernames.Add(confirmedUsernames[i]);
            _allFriendUsernames.Add( confirmedAvatars[i]);
            _friendStatusMap.Add(confirmedUsernames[i], FriendStatus.Confirmed);
        }
        
        for (int i = 0; i < requesteeUsernames.Count; i++)
        {
            var index = requesterUsernames.Count + confirmedUsernames.Count + i;
            _allFriendUsernames.Add( requesteeUsernames[i]);
            _allFriendUsernames.Add( requesteeAvatars[i]);
            _friendStatusMap.Add(requesteeUsernames[i], FriendStatus.Requestee);
        }
    }

    private void ClearLists()
    {
        foreach (var d in _dots) Destroy(d.gameObject);
        _friends.Clear();
        _allFriendAvatars.Clear();
        _allFriendUsernames.Clear();
        _confirmedFriendUsernames.Clear();
        _confirmedFriendAvatars.Clear();
        _friendStatusMap.Clear();
        _dots.Clear();
    }

    private void DestroyAllToasts()
    {
        var toasts = GameObject.FindGameObjectsWithTag("Toast");
        foreach (var toast in toasts) DestroyImmediate(toast);
    }

    private void OnDisable()
    {
        DestroyAllToasts();
    }
}
