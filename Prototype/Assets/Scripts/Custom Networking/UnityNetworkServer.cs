using kcp2k;

namespace PlayFab.Networking {
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Mirror;
    using UnityEngine.Events;

    public class UnityNetworkServer : NetworkManager {
        public static UnityNetworkServer Instance { get; private set; }

        public PlayerEvent OnPlayerAdded = new PlayerEvent();
        public PlayerEvent OnPlayerRemoved = new PlayerEvent();

        public int Port = 7777; // overwritten by the code in AgentListener.cs

        public bool isOffline = false; // https://discord.com/channels/343440455738064897/343440455738064897/964186182789513278

        public List<UnityNetworkConnection> Connections {
            get { return _connections; }
            private set { _connections = value; }
        }

        private List<UnityNetworkConnection> _connections = new List<UnityNetworkConnection>();

        public class PlayerEvent : UnityEvent<string> { }

        // Use this for initialization
        public override void Awake() {
            base.Awake();
            Instance = this;
            NetworkServer.RegisterHandler<ReceiveAuthenticateMessage>(OnReceiveAuthenticate);

            if (JamSessionDetails.Instance.isSoloMode) {
                print("It's in solo mode, so don't listen to the server, and start the server automatically");
                NetworkServer.dontListen = true;
                StartHost();
            }
        }

        public void StartListen() {
            GetComponent<KcpTransport>().Port = (ushort) Port;
            NetworkServer.Listen(maxConnections);
        }

        /*
         // TODO --> Check if the players go back and there's zero left, should it shut down the server like this?
        private void OnDisable() {
            if(numPlayers == 0) OnApplicationQuit();
        }
        */

        public override void OnApplicationQuit() {
            base.OnApplicationQuit();
            NetworkServer.Shutdown();
        }

        private void OnReceiveAuthenticate(NetworkConnection nconn, ReceiveAuthenticateMessage message) {
            var conn = _connections.Find(c => c.ConnectionId == nconn.connectionId);
            if (conn != null) {
                conn.PlayFabId = message.PlayFabId;
                conn.IsAuthenticated = true;
                OnPlayerAdded.Invoke(message.PlayFabId);
            }
        }

        public override void OnServerConnect(NetworkConnectionToClient networkConnectionToClient) {
            base.OnServerConnect(networkConnectionToClient);

            Debug.LogWarning("Client Connected");
            var uconn = _connections.Find(c => c.ConnectionId == networkConnectionToClient.connectionId);
            if (uconn == null) {
                Debug.LogWarning("UnityNetworkConnection is null, so initialize a new one");
                _connections.Add(new UnityNetworkConnection() {
                    Connection = networkConnectionToClient,
                    ConnectionId = networkConnectionToClient.connectionId,
                    LobbyId = PlayFabMultiplayerAgentAPI.SessionConfig.SessionId
                });
            }
        }

        public override void OnClientConnect() {
            base.OnClientConnect();
            Debug.Log("[OnClientConnect] event triggered");
            if (PlayFabLogin.EntityKey != null) {
                NetworkClient.connection.Send(new ReceiveAuthenticateMessage() { PlayFabId = PlayFabLogin.EntityKey.Id });
            }
            else {
                Debug.LogError("Playfab Login Entity key is null");
            }
        }

        public override void OnServerError(NetworkConnectionToClient networkConnectionToClient, Exception ex) {
            base.OnServerError(networkConnectionToClient, ex);

            Debug.Log(string.Format("Unity Network Connection Status: exception - {0}", ex.Message));
        }

        public override void OnServerDisconnect(NetworkConnectionToClient networkConnectionToClient) {
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(networkConnectionToClient);

            var uconn = _connections.Find(c => c.ConnectionId == networkConnectionToClient.connectionId);
            if (uconn != null) {
                if (!string.IsNullOrEmpty(uconn.PlayFabId)) {
                    OnPlayerRemoved.Invoke(uconn.PlayFabId);
                }

                _connections.Remove(uconn);
            }
        }
    }

    [Serializable]
    public class UnityNetworkConnection {
        public bool IsAuthenticated;
        public string PlayFabId;
        public string LobbyId;
        public int ConnectionId;
        public NetworkConnection Connection;
    }

    public class CustomGameServerMessageTypes {
        public const short ReceiveAuthenticate = 900;
        public const short ShutdownMessage = 901;
        public const short MaintenanceMessage = 902;
    }

    public struct ReceiveAuthenticateMessage : NetworkMessage {
        public string PlayFabId;
    }

    public struct ShutdownMessage : NetworkMessage { }

    [Serializable]
    public struct MaintenanceMessage : NetworkMessage {
        public DateTime ScheduledMaintenanceUTC;
    }
}