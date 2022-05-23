using System;
using System.Collections;
using System.Collections.Generic;
using DidStuffLab;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiFriendsManager : MonoBehaviour {
    [SerializeField] private List<GameObject> navigationArrows; //0 = Left, 1 = Right
    [SerializeField] private GameObject logInToAddViewFriends;


    [SerializeField] protected List<GameObject> friendCards = new List<GameObject>();
    [SerializeField] private List<Image> friendImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> friendTexts = new List<TextMeshProUGUI>();

    [Header("Dots")] [SerializeField] private GameObject dotParent, dot;
    [SerializeField] private float dotSpacing;
    [SerializeField] private Color dotActive, dotInactive;

    protected List<string> _allFriendUsernames = new List<string>();
    protected List<string> _confirmedFriendUsernames = new List<string>();
    protected List<string> _allFriendAvatars = new List<string>();
    public List<bool> AllFriendOnlineStatuses { get; set; } = new List<bool>();
    protected List<string> _confirmedFriendAvatars = new List<string>();
    private Dictionary<string, FriendStatus> _friendStatusMap = new Dictionary<string, FriendStatus>();

    protected List<string> listToLoopUsernames = new List<string>();
    protected List<string> listToLoopAvatars = new List<string>();

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
    protected string[] _currentUsernames = new string[2];
    protected string[] _currentAvatars = new string[2];
    protected bool[] _currentOnlineStatuses = new bool[3];

    public List<string> AllFriendUsernames {
        get => _allFriendUsernames;
        set => _allFriendUsernames = value;
    }

    public List<string> ConfirmedFriendUsernames {
        get => _confirmedFriendUsernames;
        set => _confirmedFriendUsernames = value;
    }

    public List<string> AllFriendAvatars {
        get => _allFriendAvatars;
        set => _allFriendAvatars = value;
    }

    public List<string> ConfirmedFriendAvatars {
        get => _confirmedFriendAvatars;
        set => _confirmedFriendAvatars = value;
    }

    public Dictionary<string, FriendStatus> FriendStatusMap {
        get => _friendStatusMap;
        set => _friendStatusMap = value;
    }

    public bool Initialised {
        get => _initialised;
        set => _initialised = value;
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

        _numberFriends = listToLoopUsernames.Count;

        _numberOfPages = Mathf.CeilToInt((float) _numberFriends / (float) _numberOfCards);
        _numberOfCardsOnLastPage = listToLoopUsernames.Count % _numberOfCards;

        ActivateArrowsAndDots(_numberFriends > _numberOfCards);
        navigationArrows[0].SetActive(false);

        _even = _numberFriends % 2 == 0;
    }

    private void InitialiseGraphics() {
        DoTheDots();
        if (_numberFriends == 0) return;
        if (_numberFriends <= _numberOfCards) {
            _displayedFriends = _numberFriends;
            for (int i = 0; i < _numberFriends; i++) friendCards[i].SetActive(true);
            for(int i = _numberFriends; i < _numberOfCards; i++) friendCards[i].SetActive(false);
            _currentAvatars = new string[_numberFriends];
            _currentUsernames = new string[_numberFriends];
        }
        else {
            _displayedFriends = _numberOfCards;
            for (int i = 0; i < friendCards.Count; i++) friendCards[i].SetActive(true);
            _currentAvatars = new string[_numberOfCards];
            _currentUsernames = new string[_numberOfCards];
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
        for (int i = 0; i < _displayedFriends; i++) {
            print("Displayed friends is " + _displayedFriends);
            _currentUsernames[i] = listToLoopUsernames[_currentListIndex + i];
            _currentAvatars[i] = listToLoopAvatars[_currentListIndex + i];
            friendTexts[i].text = _currentUsernames[i];
            friendImages[i].sprite = Resources.Load<Sprite>("Avatars/" + _currentAvatars[i]);
        }
    }

    private void ActivateLastCards(int numberToRemove, bool activate) {
        if (numberToRemove == 0) return;

        for (var index = numberToRemove; index < _numberOfCards; index++) {
            friendCards[index].SetActive(activate);
        }
    }

    protected virtual void DisableAllInteraction() {
        foreach (var card in friendCards) card.SetActive(false);
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
        _friendStatusMap.Clear();
    }

    protected virtual void OnDisable() => ClearLists();
}