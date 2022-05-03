using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.DataModels;
using PlayFab.MultiplayerModels;
using UnityEngine.UI;
using EntityKey = PlayFab.MultiplayerModels.EntityKey;
using ExecuteCloudScriptResult = PlayFab.ClientModels.ExecuteCloudScriptResult;
using JsonArray = PlayFab.Json.JsonArray;
using JsonObject = PlayFab.Json.JsonObject;

/*
  references:
  - https://docs.microsoft.com/en-us/gaming/playfab/features/multiplayer/matchmaking/quickstart
  - https://www.youtube.com/watch?v=Y3dKKJwQ8hU&list=PLS6sInD7ThM1aUDj8lZrF4b4lpvejB2uB&index=24
 */

namespace ctsalidis {
    public class Matchmaker : MonoBehaviour {
        public static Matchmaker Instance;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject leaveQueueButton;
        [SerializeField] private GameObject joinButton;
        [SerializeField] private Text queueStatusText;
        [SerializeField] private string ticketIdText; // if the user wants to join an already existing match
        [SerializeField] private GameObject authenticateAndFriendsCanvas;

        [SerializeField] private ClientStartup
            _clientStartup; // to start the client (connect and join the server) once matchmaking is done

        private string ticketId;
        private Coroutine pollTicketCoroutine;
        private Coroutine pollFriendMatchInvites;

        private string matchmakerId = "";
        private string _friendToJoin = null;

        private static string QueueName = "DefaultQueue";

        private void Awake() => Instance = this;

        public void Initialize() {
            print("Initialized");
            joinButton.SetActive(false);
            SetMatchmakingTicketIdObject(clear: true); // in case there are any previous ones, clear them
            pollFriendMatchInvites = StartCoroutine(PollFriendMatchInvites());
        }

        private IEnumerator PollFriendMatchInvites() {
            while (true) {
                GetMatchmakingTicketsFromFriends();
                yield return new WaitForSeconds(10.0f);
            }
        }

        public void StartMatchmaking() {
            if (PlayFabLogin.SelectedFriendsEntityKeys.Count > 0)
                matchmakerId = PlayFabLogin.EntityKey.Id; // this person would 'create' the match
            else if (_friendToJoin != null)
                matchmakerId =
                    _friendToJoin; // if the user hasn't selected anyone, and has an invitation from friend, join that
            var Latencies = new object[] {
                new {
                    region = "NorthEurope",
                    latency = 100
                },
            };
            var dataObjectWithoutFriends = new {Latencies};
            var dataObjectWithFriends = new {
                Latencies,
                MatchmakerId = matchmakerId,
            };

            StopCoroutine(pollFriendMatchInvites);
            authenticateAndFriendsCanvas.SetActive(false);
            playButton.SetActive(false);
            joinButton.SetActive(false);
            queueStatusText.text = "Submitting Ticket";
            queueStatusText.gameObject.SetActive(true);

            var creator = new MatchmakingPlayer {
                Entity = new EntityKey {
                    Id = PlayFabLogin.EntityKey.Id,
                    Type = PlayFabLogin.EntityKey.Type,
                },

                // Here we specify the creator's attributes.
                Attributes = new MatchmakingPlayerAttributes {
                    DataObject = !string.IsNullOrEmpty(matchmakerId)
                        ? (object) dataObjectWithFriends
                        : dataObjectWithoutFriends,
                },
            };

            var request = new CreateMatchmakingTicketRequest {
                // The ticket creator specifies their own player attributes.
                Creator = creator,
                // Cancel matchmaking if a match is not found after 120 seconds.
                GiveUpAfterSeconds = 120,

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
        public void JoinMatchmaking() {
            StartMatchmaking();
            return;
            authenticateAndFriendsCanvas.SetActive(false);
            playButton.SetActive(false);
            joinButton.SetActive(false);
            queueStatusText.text = "Joining existing Match";
            queueStatusText.gameObject.SetActive(true);

            PlayFabMultiplayerAPI.JoinMatchmakingTicket(
                new JoinMatchmakingTicketRequest {
                    TicketId = ticketIdText,
                    QueueName = QueueName,
                    Member = new MatchmakingPlayer {
                        Entity = new EntityKey {
                            Id = PlayFabLogin.EntityKey.Id,
                            Type = PlayFabLogin.EntityKey.Type,
                        },
                        Attributes = new MatchmakingPlayerAttributes {
                            DataObject = new {
                                Latencies = new object[] {
                                    new {
                                        region = "NorthEurope",
                                        latency = 100
                                    },
                                },
                            },
                        },
                    }
                },
                // Callbacks for handling success and error.
                this.OnJoinMatchmakingTicket,
                this.OnMatchmakingError
            );
        }

        public void LeaveQueue() {
            leaveQueueButton.SetActive(false);
            queueStatusText.gameObject.SetActive(false);

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
            playButton.SetActive(true);
            joinButton.SetActive(true);
            SetMatchmakingTicketIdObject(clear: true);
            pollFriendMatchInvites = StartCoroutine(PollFriendMatchInvites());
        }

        private void OnTicketCanceled(CancelMatchmakingTicketResult result) {
            print("Left queue successfully");
            playButton.SetActive(true);
            joinButton.SetActive(true);
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

            leaveQueueButton.SetActive(true);
            queueStatusText.text = "Ticket Created";
        }

        private void OnJoinMatchmakingTicket(JoinMatchmakingTicketResult result) {
            // TODO --> figure out how to join a match based on existing ticket id
            print("Joined matchmaking ticket result");
            pollTicketCoroutine = StartCoroutine(PollTicket(ticketIdText));
            leaveQueueButton.SetActive(true);
            queueStatusText.text = "Ticket Created";
        }

        private void OnMatchmakingError(PlayFabError error) {
            Debug.LogError(error.GenerateErrorReport());
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
            queueStatusText.text = $"Status: {result.Status}";
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
                    leaveQueueButton.SetActive(false);
                    queueStatusText.gameObject.SetActive(false);
                    playButton.SetActive(true);
                    SetMatchmakingTicketIdObject(clear: true);
                    pollFriendMatchInvites = StartCoroutine(PollFriendMatchInvites());
                    break;
            }
        }

        private void StartMatch(string matchId) {
            queueStatusText.text = $"Starting Match";

            PlayFabMultiplayerAPI.GetMatch(
                new GetMatchRequest {
                    MatchId = matchId,
                    QueueName = QueueName
                },
                OnGetMatch,
                OnMatchmakingError
            );
        }

        private void OnGetMatch(GetMatchResult result) {
            // ref to what to send --> https://docs.microsoft.com/en-us/gaming/playfab/features/multiplayer/matchmaking/multiplayer-servers#information-passed-to-the-game-server
            var matchId = result.MatchId; // it's the same as the Session ID
            var ipAddress = result.ServerDetails.IPV4Address;
            var ports = result.ServerDetails.Ports;
            var preferredRegions = result.RegionPreferences;
            var initialPlayers = result.Members;
            print("Match ID: " + result.MatchId);
            print("IP Address: " + result.ServerDetails.IPV4Address);
            for (int i = 0; i < result.ServerDetails.Ports.Count; i++) {
                print("Port " + i + " - " + result.ServerDetails.Ports[i]);
            }

            // clear the matchmaking request for friends, cause its already completed
            SetMatchmakingTicketIdObject(clear: true);
            queueStatusText.text = $"{result.Members[0].Entity.Id} vs {result.Members[1].Entity.Id}";

            _clientStartup.SetServerInstanceDetails(ipAddress, (ushort) ports[0].Num);
        }

        // CHRISTIAN NOTE --> I´'m using this method as an attempt to save the ticket ID to the friend's title user data
        // Hopefully, that friend can then obtain this matchmaking ticket id and connect to the same
        // reference --> https://docs.microsoft.com/en-us/gaming/playfab/features/data/entities/quickstart
        // Otherwise consider looking into makeEntityAPICall in CloudScript --> (Available in Title --> Automation, Revisions(Legacy))
        // look into DaperDino's getting and setting title player data --> https://github.com/DapperDino/PlayFab-Tutorials/blob/main/Assets/Mirror/Examples/Pong/Scripts/Player.cs 
        private void SetMatchmakingTicketIdObject(bool clear = false) {
            var friends = new string[PlayFabLogin.SelectedFriendsEntityKeys.Count];
            var count = 0;
            foreach (var friend in PlayFabLogin.SelectedFriendsEntityKeys) {
                friends[count] = friend.Id;
                count++;
            }

            var data = new Dictionary<string, object>() {
                {"MatchmakingTicketId", clear ? "" : ticketId},
                {"Owner", clear ? "" : PlayFabLogin.EntityKey.Id},
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
            if (PlayFabLogin.EntityKey == null) return;
            if (PlayFabMultiplayerAPI.IsEntityLoggedIn()) print("Logged in, so get match invitation from friends");
            
            // StartCloudHelloWorld();
            // StartCloudEntityGetObject();
            // StartCloudUpdateObjects();
            //StartCloudEntitySetObject();
            // return;
            
             // CHRISTIAN NOTE:
             // To be able to read FriendMatchmakingTicket object from a friend, I had to add an extra
             // policy on PlayFab to Title Settings -> API Features -> Entity Global Title Policy -> JSON
             //
             // {
             //     "Action": "Read",
             //     "Effect": "Allow",
             //     "Resource": "pfrn:data--*!*/Profile/Objects/FriendMatchmakingTicket",
             //     "Principal": {
             //         "Friend": "true"
             //     },
             //     "Comment": "Allow friends to access this player's objects",
             //     "Condition": null
             // } 
            
            var getRequest = new GetObjectsRequest {
                Entity = new PlayFab.DataModels.EntityKey
                    // {Id = PlayFabLogin.EntityKey.Id, Type = PlayFabLogin.EntityKey.Type}
                    {Id = PlayFabLogin.FriendsEntityKeys[0].Id, Type = PlayFabLogin.FriendsEntityKeys[0].Type}
            };
            PlayFabDataAPI.GetObjects(getRequest,
                result => {
                    var objs = result.Objects;

                    foreach (var o in objs.Where(o => o.Key == "FriendMatchmakingTicket")) {
                        if (!(o.Value.DataObject is JsonObject details) ||
                            !(details["Friends"] is JsonArray friends)) continue;
                        if (!friends.Contains(PlayFabLogin.EntityKey.Id)) continue;
                        print("Player " + getRequest.Entity.Id + " wants to play with you!");
                        print("Retrieved matchmaking Ticket id: " + details?["MatchmakingTicketId"] + " from player: " +
                              getRequest.Entity.Id);
                        print(details?["Owner"]);
                        _friendToJoin = details?["Owner"].ToString();
                        ticketIdText = details["MatchmakingTicketId"].ToString();
                        StopCoroutine(pollFriendMatchInvites);
                        joinButton.SetActive(true);
                    }
                },
                OnPlayFabError
            );
        }

        private void StartCloudUpdateObjects() {
            PlayFabCloudScriptAPI.ExecuteEntityCloudScript(new ExecuteEntityCloudScriptRequest() {
                FunctionName = "updateMatchInvitation", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new {ReceiverEntityId = PlayFabLogin.FriendsEntityKeys[0].Id}, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            }, OnCloudUpdateObjects, OnErrorShared);
        }

        private void OnCloudUpdateObjects(PlayFab.CloudScriptModels.ExecuteCloudScriptResult result) {
            // CloudScript (Legacy) returns arbitrary results, so you have to evaluate them one step and one parameter at a time
            // Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
            JsonObject jsonResult = (JsonObject) result.FunctionResult;
            object messageValue;
            jsonResult.TryGetValue("setResult",
                out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript (Legacy)
            Debug.Log((string) messageValue);
            
            if(result.FunctionResult!=null)
                Debug.Log($"function askChat called result: {result.FunctionResult.ToString()}");
            else
                Debug.Log($"function askChat called null results");
        }

        // Build the request object and access the API
        private void StartCloudHelloWorld() {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest() {
                FunctionName = "helloWorld", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new {inputValue = PlayFabLogin.EntityKey.Id}, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            }, OnCloudHelloWorld, OnErrorShared);
        }

        private void OnCloudHelloWorld(ExecuteCloudScriptResult result) {
            // CloudScript (Legacy) returns arbitrary results, so you have to evaluate them one step and one parameter at a time
            // Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
            JsonObject jsonResult = (JsonObject) result.FunctionResult;
            object messageValue;
            jsonResult.TryGetValue("messageValue",
                out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript (Legacy)
            Debug.Log((string) messageValue);
        }

        private void OnErrorShared(PlayFabError error) {
            Debug.Log(error.GenerateErrorReport());
        }

        private void StartCloudEntitySetObject() {
            PlayFabCloudScriptAPI.ExecuteEntityCloudScript(new ExecuteEntityCloudScriptRequest() {
                /*
                Entity = new PlayFab.CloudScriptModels.EntityKey() {
                    // Id = PlayFabLogin.EntityKey.Id,
                    // Type = PlayFabLogin.EntityKey.Type
                    Id = PlayFabLogin.FriendsEntityKeys[0].Id,
                    Type = PlayFabLogin.FriendsEntityKeys[0].Type
                },
                */
                FunctionName = "makeEntityAPICall", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new {prop1 = PlayFabLogin.EntityKey.Id}, // The parameter provided to your function
                // GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            }, OnCloudEntitySuccess, OnPlayFabError);
        }

        private void StartCloudEntityGetObject() {
            PlayFabCloudScriptAPI.ExecuteEntityCloudScript(new ExecuteEntityCloudScriptRequest() {
                FunctionName = "makeEntityAPICallGetObject", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new {friendId = PlayFabLogin.FriendsEntityKeys[0].Id}, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            }, OnCloudEntitySuccess, OnPlayFabError);
        }
        
        private void OnCloudEntitySuccess(PlayFab.CloudScriptModels.ExecuteCloudScriptResult result) {
            // CloudScript (Legacy) returns arbitrary results, so you have to evaluate them one step and one parameter at a time
            // Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
            JsonObject jsonResult = (JsonObject) result.FunctionResult;
            object messageValue;
            jsonResult.TryGetValue("setResult",
                out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript (Legacy)
            Debug.Log((string) messageValue);
            jsonResult.TryGetValue("getResult",
                out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript (Legacy)
            Debug.Log((string) messageValue);
            if(result.FunctionResult!=null)
                Debug.Log($"function askChat called result: {result.FunctionResult.ToString()}");
            else
                Debug.Log($"function askChat called null results");
        }

        private void OnPlayFabError(PlayFabError error) {
            Debug.LogError(error.GenerateErrorReport());
            SetMatchmakingTicketIdObject(clear: true);
        }

        private void OnApplicationQuit() {
            if (!PlayFabMultiplayerAPI.IsEntityLoggedIn()) return;
            LeaveQueue();
            CancelAllTickets();
        }
    }
}