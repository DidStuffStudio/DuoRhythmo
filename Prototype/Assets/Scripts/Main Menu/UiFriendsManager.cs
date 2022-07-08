using System;
using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UiFriendsManager : MonoBehaviour {
    [SerializeField] private List<GameObject> navigationArrows; //0 = Left, 1 = Right
    [SerializeField] private GameObject logInToAddViewFriends;


    [SerializeField] protected List<FriendCard> friendCards = new List<FriendCard>();
 [Header("Dots")] [SerializeField] private GameObject dotParent, dot;
    [SerializeField] private float dotSpacing;
    [SerializeField] private Color dotActive, dotInactive;
    
    private Dictionary<Friend, FriendStatus> _friendStatusMap = new Dictionary<Friend, FriendStatus>();
    
    protected List<Friend> allFriends = new List<Friend>();
    protected List<Friend> confirmedFriends = new List<Friend>();
    
    protected List<Friend> friendListToLoop = new List<Friend>();


    protected int _numberOfCards = 2;
    protected int _numberFriends = 0;
    protected int _displayedFriends = 2;
    protected int _currentListIndex = 0;
    private int _page;
    private int _numberOfPages;
    private int _numberOfCardsOnLastPage;
    private List<Image> _dots = new List<Image>();
    private bool _initialised;

    private bool _even; //Set from Child Class
    
    
    public Dictionary<Friend, FriendStatus> FriendStatusMap {
        get => _friendStatusMap;
        set => _friendStatusMap = value;
    }

    public bool Initialised {
        get => _initialised;
        set => _initialised = value;
    }
    
    public List<Friend> AllFriends
    {
        get => allFriends;
        set => allFriends = value;
    }

    public List<Friend> ConfirmedFriends
    {
        get => confirmedFriends;
        set => confirmedFriends = value;
    }

    protected virtual void OnEnable() {
        if (!_initialised) return;
        if (PlayFabLogin.Instance.IsLoggedInAsGuest) DisableAllInteraction();

        if (PlayFabLogin.Instance.IsLoggedInToAccount) {
            Initialise();
            InitialiseGraphics();
        }
    }

    protected virtual void Initialise() {
        
        _currentListIndex = 0;
        _page = 1;

        _numberFriends = friendListToLoop.Count;

        _numberOfPages = Mathf.CeilToInt((float) _numberFriends / (float) _numberOfCards);
        _numberOfCardsOnLastPage = friendListToLoop.Count % _numberOfCards;

        ActivateArrowsAndDots(_numberFriends > _numberOfCards);
        navigationArrows[0].SetActive(false);

        _even = _numberFriends % 2 == 0;
    }

    private void InitialiseGraphics() {
        DoTheDots();
        if (_numberFriends == 0) return;
        if (_numberFriends <= _numberOfCards) {
            _displayedFriends = _numberFriends;
            for (int i = 0; i < _numberFriends; i++) friendCards[i].gameObject.SetActive(true);
            for(int i = _numberFriends; i < _numberOfCards; i++) friendCards[i].gameObject.SetActive(false);
        }
        else {
            _displayedFriends = _numberOfCards;
            for (int i = 0; i < friendCards.Count; i++) friendCards[i].gameObject.SetActive(true);
        }


        ChangeGraphics();
    }
    
    public void Navigate(bool right) {
        if (right) {
            _page++;
            _currentListIndex += _numberOfCards;
        }
        else {
            _page--;
            _currentListIndex -= _numberOfCards;
        }

        navigationArrows[0].SetActive(_page != 1);
        navigationArrows[1].SetActive(_page != _numberOfPages);
        _displayedFriends = _numberOfCards;
        if (_page == _numberOfPages && _numberOfCardsOnLastPage > 0) {
            print(" Called from here!!!!!");
            ActivateLastCards(_numberOfCardsOnLastPage, false);
            _displayedFriends = _numberOfCardsOnLastPage;
        }
        else ActivateLastCards(_numberOfCardsOnLastPage, true);

        ChangeGraphics();
        ChangeDots();
    }


    protected virtual void ChangeGraphics() {

    }

    private void ActivateLastCards(int numberToRemove, bool activate) {
        if (numberToRemove == 0) return;

        for (var index = numberToRemove; index < _numberOfCards; index++) {
            friendCards[index].gameObject.SetActive(activate);
        }
    }

    protected virtual void DisableAllInteraction() {
        foreach (var card in friendCards) card.gameObject.SetActive(false);
        logInToAddViewFriends.SetActive(true);
        Instantiate(logInToAddViewFriends, transform);
        foreach (var arrow in navigationArrows) {
            arrow.SetActive(false);
        }
    }


    void ChangeDots() {
        if (_numberFriends < _numberOfCards) return;
        foreach (var d in _dots) {
            d.color = dotInactive;
        }

        _dots[_page - 1].color = dotActive;
    }

    private void ActivateArrowsAndDots(bool activate) {
        dotParent.SetActive(activate);
        foreach (var arrow in navigationArrows) {
            arrow.SetActive(activate);
        }
    }

    private void DoTheDots() {
        if (_numberFriends <= _numberOfCards) return;
        var numberOfDots = Mathf.Ceil((float) _numberFriends / (float) _numberOfCards);
        print("Number of dots is " + numberOfDots);
        var xShift = ((numberOfDots - 1) * dotSpacing) / 2;
        for (int i = 0; i < numberOfDots; i++) {
            var dotClone = Instantiate(dot, dotParent.transform);
            dotClone.transform.Translate(-xShift + dotSpacing * i, 0, 0);
            var dotImage = dotClone.transform.GetComponent<Image>();
            dotImage.color = dotInactive;
            _dots.Add(dotImage);
        }

        _dots[0].color = dotActive;
    }


    private void ClearLists() {
        foreach (var d in _dots) Destroy(d.gameObject);
        _dots.Clear();
        // _friendStatusMap.Clear();
    }

    protected virtual void OnDisable() => ClearLists();
}