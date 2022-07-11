using System.Collections;
using DidStuffLab.Scripts.Custom_Buttons.Did_Stuff_Buttons;
using DidStuffLab.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace DidStuffLab.Scripts.Main_Menu
{
    public class DidStuffInvite : MonoBehaviour
    {
        [SerializeField] private string invitationMessage = " has invited you to a jam!";
        private string _username = "ctsalidis";
        [SerializeField] private TextMeshProUGUI textField;
        private Animator _uiAnimator;
        private float _animationTime = 1.0f; // Change for different animation length or come up with better way
        [SerializeField] private AbstractDidStuffButton acceptButton;
        [SerializeField] private AbstractDidStuffButton rejectButton;
        private MainMenuManager mainMenuManager;
        private string _matchID;

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

        public void SetInviter(string username, string id) {
            _username = username;
            //Playfab ID stuff
            textField.text = _username + invitationMessage;
            _matchID = id;

        }

        public void AcceptRequest()
        {
            _uiAnimator.Play("InvitePopOut");
            mainMenuManager.AcceptInvite();
        }
    
        public void PlayDeclineRequest()
        {
            mainMenuManager.DeclineInvite(_username);
            _uiAnimator.Play("InvitePopOut");
            StartCoroutine(Destroy());
        }

        public void DestroyForNew() =>  Destroy(gameObject);

        IEnumerator Destroy()
        {
            yield return new WaitForSeconds(_animationTime + 0.1f);
            Matchmaker.Instance.RemoveIdFromIgnore(_matchID);
            Destroy(gameObject);
        }
    
        private void OnDisable()
        {
            acceptButton.OnClick -= AcceptRequest;
            rejectButton.OnClick -= PlayDeclineRequest;
        }
    }
}
