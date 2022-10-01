using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DidStuffLab.Scripts.Main_Menu;
using DidStuffLab.Scripts.Managers;
using UnityEngine;
using PlayFab;
using PlayFab.DataModels;
using PlayFab.MultiplayerModels;
using SimpleJSON;
using EntityKey = PlayFab.MultiplayerModels.EntityKey;
using JsonArray = PlayFab.Json.JsonArray;
using JsonObject = PlayFab.Json.JsonObject;

/*
  references:
  - https://docs.microsoft.com/en-us/gaming/playfab/features/multiplayer/matchmaking/quickstart
  - https://www.youtube.com/watch?v=Y3dKKJwQ8hU&list=PLS6sInD7ThM1aUDj8lZrF4b4lpvejB2uB&index=24
 */

namespace DidStuffLab {
    public class Matchmaker : MonoBehaviour {
        // private static Matchmaker _instance;

        public static Matchmaker Instance { get; private set; }
        
        // TODO --> Switch this with JammingSessionDetails
        [SerializeField] private ClientStartup
            _clientStartup; // to start the client (connect and join the server) once matchmaking is done

        private string ticketId;
        private Coroutine pollTicketCoroutine;
        private Coroutine pollFriendMatchInvites;

        private string _playerToMatchWithId = "";
        private string _selectedDrumToPlayWith = "";
        public string DrumToPlayWith => _selectedDrumToPlayWith;

        private string matchmakerId = "";
        private string _friendToJoinId = null;
        private string _friendToJoinUsername = null;

        private static string QueueName = "DefaultQueue";
        public bool isRandom = true;

        private List<string> _matchmakingTicketIdsToIgnore = new List<string>();
        private string currentMatchmakingTicketId = string.Empty;
        private bool isInitialized = false;

        private void Awake() {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Initialize() {
            if (isInitialized) return;
            isInitialized = true;
            print("Initialized matchmaker");
            SetMatchmakingTicketIdObject(clear: true); // in case there are any previous ones, clear them
            pollFriendMatchInvites = StartCoroutine(PollFriendMatchInvites());
        }


        private IEnumerator PollFriendMatchInvites() {
            while (true) {
                GetMatchmakingTicketsFromFriends();
                yield return new WaitForSeconds(15.0f);
            }
        }

        public void InviteFriendToMatchmaking(string username) {
            print("Called Invite with user " + username);
            _playerToMatchWithId = FriendsManager.Instance.IdFromUsername(username);
            // StartMatchmaking();
        }

        public void SelectDrumAndStartMatch(string drumName, bool random)
        {
            Debug.Log("Called matchmaking");
            isRandom = random;
            QueueName = isRandom ? "DefaultQueueRandom" : "DefaultQueue";
            _selectedDrumToPlayWith = drumName;
            StartMatchmaking();
        }

        private void StartMatchmaking() {
            matchmakerId = PlayFabLogin.AuthenticationContext.EntityId; // this person would 'create' the match
            if (_friendToJoinId != null) {
                matchmakerId = _friendToJoinId;
                print("Not matchmaking with a friend, so let's start random matchmaking" + matchmakerId);
            }

            var Latencies = new object[] {
                new {
                    region = "NorthEurope",
                    latency = 100
                },
            };

            // TODO --> Fix selected drums array of strings:
            // right now it would come as [{"DrumType" : "0"}, {"DrumType" : "1"}] but should be ["0", "1"] 
            // var SelectedDrums = new object[] { _selectedDrumToPlayWith, _selectedDrumToPlayWith };
            var DrumTypes = new object[] {_selectedDrumToPlayWith};

            var dataObjectWithoutFriends = new {
                Latencies,
                DrumTypes, // doing type intersection rule if playing with random users
                Username = PlayFabLogin.Instance.Username,
                AvatarName = PlayFabLogin.Instance.UserAvatar,
            };
            var dataObjectWithFriends = new {
                Latencies,
                MatchmakerId = matchmakerId,
                Username = PlayFabLogin.Instance.Username,
                AvatarName = PlayFabLogin.Instance.UserAvatar,
            };

            StopCoroutine(pollFriendMatchInvites);

            MainMenuManager.Instance.SetMatchmakingStatusText("Submitting ticket");

            var creator = new MatchmakingPlayer {
                Entity = new EntityKey {
                    Id = PlayFabLogin.EntityKey.Id,
                    Type = PlayFabLogin.EntityKey.Type,
                },

                // Here we specify the creator's attributes.
                Attributes = new MatchmakingPlayerAttributes {
                    DataObject = (!isRandom) ? (object) dataObjectWithFriends : dataObjectWithoutFriends,
                },
            };

            var request = new CreateMatchmakingTicketRequest {
                // The ticket creator specifies their own player attributes.
                Creator = creator,
                // Cancel matchmaking if a match is not found after 120 seconds.
                GiveUpAfterSeconds =
                    120, // TODO --> consider taking this down (would also need to change the matchmaking queues in playfab)

                // The name of the queue to submit the ticket into.
                QueueName = QueueName
            };
            PlayFabMultiplayerAPI.CreateMatchmakingTicket(request,
                // Callbacks for handling success and error.
                OnMatchmakingTicketCreated,
                OnMatchmakingError
            );
        }

        // https://docs.microsoft.com/en-us/gaming/playfab/features/multiplayer/lobby/lobby-matchmaking-sdks/multiplayer-unity-plugin-quickstart#join-a-matchmaking-ticket
        // https://docs.microsoft.com/en-us/gaming/playfab/features/multiplayer/matchmaking/quickstart#group-members-join-the-match-ticket
        public void JoinMatchmaking() => StartMatchmaking();

        public void LeaveQueue() {
            PlayFabMultiplayerAPI.CancelMatchmakingTicket(
                new CancelMatchmakingTicketRequest {
                    QueueName = QueueName,
                    TicketId = ticketId
                },
                OnTicketCanceled,
                OnMatchmakingError
            );
        }

        public void CancelAllTickets() {
            print("Cancelling all tickets");
            var request = new CancelAllMatchmakingTicketsForPlayerRequest {
                Entity = PlayFabLogin.EntityKey,
                QueueName = QueueName
            };
            PlayFabMultiplayerAPI.CancelAllMatchmakingTicketsForPlayer(request, OnCanceledAllTickets, OnPlayFabError);
        }

        private void OnCanceledAllTickets(CancelAllMatchmakingTicketsForPlayerResult obj) {
            print("Canceled all tickets successfully");
            SetMatchmakingTicketIdObject(clear: true);
            pollFriendMatchInvites = StartCoroutine(PollFriendMatchInvites());
        }

        private void OnTicketCanceled(CancelMatchmakingTicketResult result) {
            print("Left queue successfully");
            SetMatchmakingTicketIdObject(clear: true);
            pollFriendMatchInvites = StartCoroutine(PollFriendMatchInvites());
        }

        private void OnMatchmakingTicketCreated(CreateMatchmakingTicketResult result) {
            ticketId = result.TicketId;
            print("Matchmaking ticket created: " + ticketId);

            if (!string.IsNullOrEmpty(matchmakerId)) {
                SetMatchmakingTicketIdObject(); // attempt to send this matchmakingticketid to friends
            }

            pollTicketCoroutine = StartCoroutine(PollTicket(ticketId));

            MainMenuManager.Instance.SetMatchmakingStatusText("Ticket created");
        }

        /*
        private void OnJoinMatchmakingTicket(JoinMatchmakingTicketResult result) {
            print("Joined matchmaking ticket result");
            // pollTicketCoroutine = StartCoroutine(PollTicket(ticketIdText));
        }
        */

        private void OnMatchmakingError(PlayFabError error) {
            Debug.LogError(error.GenerateErrorReport());
            SendBackToMainMenuWithErrorMessage(error.GenerateErrorReport());
        }

        private void SendBackToMainMenuWithErrorMessage(string error) {
            MainMenuManager.Instance.DeactivatePanel(18); // hardcoded to waiting for match panel index
            MainMenuManager.Instance.ActivatePanel(6); // hardcoded to main menu panel index
            MainMenuManager.Instance.SpawnErrorToast("Match request cancelled", 0.1f); // give out error message
        }

        private IEnumerator PollTicket(string ticketId) {
            while (true) {
                PlayFabMultiplayerAPI.GetMatchmakingTicket(
                    new GetMatchmakingTicketRequest {
                        TicketId = ticketId,
                        QueueName = QueueName
                    },
                    OnGetMatchMakingTicket,
                    OnMatchmakingError
                );

                yield return new WaitForSeconds(10); // limited to up to 10 requests every minute --> 60/10=6
            }
        }

        // https://docs.microsoft.com/en-us/rest/api/playfab/multiplayer/matchmaking/get-matchmaking-ticket?view=playfab-rest#getmatchmakingticketresult
        // Possible values are: WaitingForPlayers, WaitingForMatch, WaitingForServer, Canceled and Matched
        private void OnGetMatchMakingTicket(GetMatchmakingTicketResult result) {
            MainMenuManager.Instance.SetMatchmakingStatusText($"Status: {result.Status}");
            print($"Status: {result.Status}");
            foreach (var member in result.Members) {
                print("Member that has joined this matchmaking ticket: " + member.Entity.Id);
            }

            switch (result.Status) {
                case "Matched":
                    StopCoroutine(pollTicketCoroutine);
                    StartMatch(result.MatchId);
                    break;
                case "Canceled":
                    Debug.LogError(result.CancellationReasonString);
                    StopCoroutine(pollTicketCoroutine);
                    SetMatchmakingTicketIdObject(clear: true);
                    if (result.CancellationReasonString.Contains("Timeout"))
                        SendBackToMainMenuWithErrorMessage(result.CancellationReasonString);
                    pollFriendMatchInvites = StartCoroutine(PollFriendMatchInvites());
                    break;
            }
        }

        private void StartMatch(string matchId) {
            MainMenuManager.Instance.SetMatchmakingStatusText("Starting match");

            PlayFabMultiplayerAPI.GetMatch(
                new GetMatchRequest {
                    MatchId = matchId,
                    QueueName = QueueName,
                    ReturnMemberAttributes = true,
                    EscapeObject = false
                },
                OnGetMatch,
                OnMatchmakingError
            );
        }

        private void OnGetMatch(GetMatchResult result) {
            // ref to what to send --> https://docs.microsoft.com/en-us/gaming/playfab/features/multiplayer/matchmaking/multiplayer-servers#information-passed-to-the-game-server
            var matchId = result.MatchId; // it's the same as the Session ID
            var preferredRegions = result.RegionPreferences;
            var initialPlayers = result.Members;

            // clear the matchmaking request for friends, cause its already completed
            SetMatchmakingTicketIdObject(clear: true);
            // result.Members[0].Attributes.EscapedDataObject; // TODO --> Use this to check for the drumtypes that have been set by the user (use the same for user attribute and matchmaking attribute)
            MainMenuManager.Instance.SetMatchmakingStatusText(
                $"{result.Members[0].Entity.Id} vs {result.Members[1].Entity.Id}");


            foreach (var member in result.Members.Where(member =>
                member.Entity.Id != PlayFabLogin.AuthenticationContext.EntityId)) {
                if (member.Attributes != null) {
                    if (!(member.Attributes.DataObject is JsonObject data)) continue;
                    if (data.ContainsKey("Username")) {
                        JamSessionDetails.Instance.otherPlayerUsername = data["Username"].ToString();
                        print("we have matched with username: " + JamSessionDetails.Instance.otherPlayerUsername);
                    }
                    else Debug.LogError("Match member doesn't contain username key");

                    if (data.ContainsKey("AvatarName")) {
                        JamSessionDetails.Instance.otherPlayerAvatarName = data["AvatarName"].ToString();
                        print("other player avatar name: " + JamSessionDetails.Instance.otherPlayerAvatarName);
                    }
                    else Debug.LogError("Match member doesn't contain avatarname key");
                }
                else {
                    // check escaped data object string in case attributes object is null
                    Debug.LogError("The attributes data is null");
                }
            }


            byte highestChosenDrum = 0;
            if (isRandom) {
                var drumTypes =
                    new Dictionary<byte, byte>(); // to get the intersection of player's chosen drum types - the one which is most common wins and the players will use that one
                foreach (var member in result.Members) {
                    if (member.Attributes != null) {
                        if (!(member.Attributes.DataObject is JsonObject data) ||
                            !(data["DrumTypes"] is JsonArray drums)) continue;
                        foreach (var v in drums.Select(d => (byte) int.Parse(d.ToString()))) {
                            if (!drumTypes.ContainsKey(v)) drumTypes.Add(v, 1);
                            else drumTypes[v]++;
                            print("Found drum type " + v + " of which in total we have " + drumTypes[v]);
                        }
                    }
                    else {
                        // check escaped data object string in case attributes object is null
                        Debug.LogError("The attributes data is null");
                    }
                }

                foreach (var d in drumTypes.Where(d => d.Value > highestChosenDrum)) {
                    highestChosenDrum = d.Value;
                }
            }
            else highestChosenDrum = (byte) int.Parse(DrumToPlayWith);

            // TODO --> initialize server instance details (send to JammingSessionDetails), then call _clientStartup.SetServerInstanceDetails(...)
            // TODO --> Also pass user's avatar and username to jammingsessiondetails - then add to player sync gameobject prefab, and sync Mirror once in the server


            // TODO --> UNCOMMENT ONCE WE HAVE SERVER ALLOCATION ENABLED ON THE MATCHMAKING QUEUE IN PLAYFAB (MAKE SURE THAT BUILD ID IS CORRECT)

            var ipAddress = result.ServerDetails.IPV4Address;
            var ports = result.ServerDetails.Ports;
            print("Match ID: " + result.MatchId);
            print("IP Address: " + result.ServerDetails.IPV4Address);
            for (int i = 0; i < result.ServerDetails.Ports.Count; i++) {
                print("Port " + i + " - " + result.ServerDetails.Ports[i]);
            }

            JamSessionDetails.Instance.SetMultiplayerMatchDetails(PlayFabLogin.Instance.Username,
                PlayFabLogin.Instance.UserAvatar, highestChosenDrum, ipAddress, (ushort) ports[0].Num);
        }

        // CHRISTIAN NOTE --> I´'m using this method as an attempt to save the ticket ID to the friend's title user data
        // Hopefully, that friend can then obtain this matchmaking ticket id and connect to the same
        // reference --> https://docs.microsoft.com/en-us/gaming/playfab/features/data/entities/quickstart
        // Otherwise consider looking into makeEntityAPICall in CloudScript --> (Available in Title --> Automation, Revisions(Legacy))
        // look into DaperDino's getting and setting title player data --> https://github.com/DapperDino/PlayFab-Tutorials/blob/main/Assets/Mirror/Examples/Pong/Scripts/Player.cs 
        private void SetMatchmakingTicketIdObject(bool clear = false) {
            var friends = new[] {_playerToMatchWithId};
            var count = 0;

            var data = new Dictionary<string, object>() {
                {"MatchmakingTicketId", clear ? "" : ticketId},
                {"OwnerId", clear ? "" : PlayFabLogin.AuthenticationContext.EntityId},
                {"OwnerUsername", clear ? "" : PlayFabLogin.Instance.Username},
                {"DrumType", clear ? "" : _selectedDrumToPlayWith},
                {"Friends", clear ? new string[0] : friends}
            };
            var dataList = new List<SetObject>() {
                new SetObject() {
                    ObjectName = "FriendMatchmakingTicket",
                    DataObject = data,
                },
                // A free-tier customer may store up to 3 objects on each entity
            };
            PlayFabDataAPI.SetObjects(new SetObjectsRequest() {
                Entity = new PlayFab.DataModels.EntityKey {
                    Id = PlayFabLogin.EntityKey.Id,
                    Type = PlayFabLogin.EntityKey.Type,
                    // Id = PlayFabLogin.FriendsEntityKeys[0].Id, Type = PlayFabLogin.FriendsEntityKeys[0].Type
                }, // Saved from GetEntityToken, or a specified key created from a titlePlayerId, CharacterId, etc
                Objects = dataList,
            }, (setResult) => { Debug.Log(setResult.ProfileVersion); }, OnPlayFabError);
        }

        private void GetMatchmakingTicketsFromFriends() {
            if (PlayFabLogin.EntityKey == null) {
                Debug.LogError("No entitykey available, so poll for friend matchmaking invitations");
                return;
            }

            if (PlayFabMultiplayerAPI.IsEntityLoggedIn()) print("Logged in, so get match invitation from friends");

            foreach (var friend in FriendsManager.Instance.FriendsDetails) {
                if (friend.FriendStatus != FriendStatus.Confirmed)
                    continue; // only check for invites from friends who have confirmed friendship status 
                var getRequest = new GetObjectsRequest {
                    Entity = new PlayFab.DataModels.EntityKey
                        {Id = friend.TitleEntityKey.Id, Type = friend.TitleEntityKey.Type}
                };
                PlayFabDataAPI.GetObjects(getRequest,
                    result => {
                        var objs = result.Objects;

                        foreach (var o in objs.Where(o => o.Key == "FriendMatchmakingTicket")) {
                            if (!(o.Value.DataObject is JsonObject details) ||
                                !(details["Friends"] is JsonArray friends)) continue;
                            if (!friends.Contains(PlayFabLogin.AuthenticationContext.PlayFabId)) continue;
                            print("Player " + getRequest.Entity.Id + " wants to play with you!");
                            print("Retrieved matchmaking Ticket id: " + details?["MatchmakingTicketId"] +
                                  " from player: " + getRequest.Entity.Id);
                            print(details?["OwnerId"]);
                            _friendToJoinId = details?["OwnerId"].ToString();
                            _friendToJoinUsername = details?["OwnerUsername"].ToString();
                            _selectedDrumToPlayWith = details?["DrumType"].ToString();
                            if (_matchmakingTicketIdsToIgnore.Contains(details?["MatchmakingTicketId"].ToString()))
                                continue;
                            currentMatchmakingTicketId = details?["MatchmakingTicketId"].ToString();
                            StopCoroutine(pollFriendMatchInvites);
                            MainMenuManager.Instance.ReceiveInviteToPlay(_friendToJoinUsername, currentMatchmakingTicketId);
                            AddCurrentTicketIdToIgnoreList();
                            break;
                        }
                    },
                    OnPlayFabError
                );
            }
        }

        private void OnPlayFabError(PlayFabError error) {
            Debug.LogError(error.GenerateErrorReport());
            SetMatchmakingTicketIdObject(clear: true);
            MainMenuManager.Instance.SetMatchmakingStatusText(error.GenerateErrorReport());
        }

        private void OnApplicationQuit() {
            if (!PlayFabMultiplayerAPI.IsEntityLoggedIn()) return;
            LeaveQueue();
            CancelAllTickets();
        }

        private void AddCurrentTicketIdToIgnoreList() {
            _matchmakingTicketIdsToIgnore.Add(currentMatchmakingTicketId);
        }

        public void AcceptInvite() {
            //AddCurrentTicketIdToIgnoreList();
            isRandom = false;
            JoinMatchmaking();
        }

        public void DeclineInvite(string username) {
            //AddCurrentTicketIdToIgnoreList();
            print("This is the declined current matchmaking ticket id: " + currentMatchmakingTicketId);
            pollFriendMatchInvites = StartCoroutine(PollFriendMatchInvites()); // start checking for invites again
        }

        public void RemoveIdFromIgnore(string id)
        {
            StartCoroutine(WaitForReinvite(id));
        }

        public void ResetMatchmaking()
        {
            ticketId = "";
            _playerToMatchWithId = "";
            _selectedDrumToPlayWith = "";
            matchmakerId = "";
            _friendToJoinId = "";
            _friendToJoinUsername = "";

            Instance = null;
            var old = GetComponent<Matchmaker>();
            Instance = gameObject.AddComponent<Matchmaker>();
            Destroy(old);
        }

        IEnumerator WaitForReinvite(string id)
        {
            yield return new WaitForSeconds(50.0f);
            _matchmakingTicketIdsToIgnore.Remove(id);
            
        }
    }
}