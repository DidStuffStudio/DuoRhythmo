using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using PlayFab.Networking;
using UnityEngine;

public class ClientStartup : MonoBehaviour {
    [SerializeField] private string buildId;
    public string BuildId => buildId;
    [SerializeField] private string ipAddress;
    [SerializeField] private ushort port;
    [SerializeField] private bool matchmaking = true;

    public string entityId;


    // if the server has been setup by the matchmaking process, I should call this function
    public void SetServerInstanceDetails(string ipAddress, ushort port) {
        this.ipAddress = ipAddress;
        this.port = port;
        ConnectUserToServer();
    }
    
#if !UNITY_SERVER    
    private void Start() {
        // LoginRemoteUser(); // --> Done after matchmaking via the MatchMaker.cs file
        // ConnectUserToServer();
        if(!string.IsNullOrEmpty(JamSessionDetails.Instance.ServerIpAddress)) SetServerInstanceDetails(JamSessionDetails.Instance.ServerIpAddress, JamSessionDetails.Instance.ServerPort);
    }
#endif
    
    private void LoginRemoteUser() {
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest() {
            TitleId = PlayFabSettings.TitleId,
            CreateAccount = true,
            CustomId = SystemInfo.deviceUniqueIdentifier
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnPlayfabLoginSuccess, OnLoginError);
    }

    private void OnPlayfabLoginSuccess(LoginResult response) {
        entityId = response.EntityToken.Entity.Id;
        Debug.Log("Login result: " + response + " - Entity Id: " + entityId);
        if (matchmaking) return;
        if(!matchmaking) ConnectUserToServer(); // only call this if we're not trying to matchmake first
    }

    private void ConnectUserToServer() {
        // if we haven't manually added the IP address then we have to make a request for a multiplayer server - otherwise, we can just connect to the saved one
        if (string.IsNullOrEmpty(ipAddress)) {
            // We need to grab an IP and Port from a server based on the buildId
            RequestMultiplayerServer();
        }
        else {
            ConnectRemoteClient();
        }
    }

    // ref --> https://docs.microsoft.com/en-us/rest/api/playfab/multiplayer/multiplayer-server/request-multiplayer-server?view=playfab-rest
    private void RequestMultiplayerServer() {
        Debug.Log("[ClientStartup].RequestMultiplayerServer");
        RequestMultiplayerServerRequest requestData = new RequestMultiplayerServerRequest {
            BuildId = this.buildId,
            SessionId = System.Guid.NewGuid().ToString(),
            PreferredRegions = new List<string>() {"NorthEurope", "EastUs"}
        };
        PlayFabMultiplayerAPI.RequestMultiplayerServer(requestData, OnRequestMultiplayerServer,
            OnRequestMultiplayerServerError);

    }
    
    private void OnRequestMultiplayerServerError(PlayFabError error) {
        Debug.LogError("An error has occured OnRequestMultiplayerServer - " + error);
    }

    private void OnRequestMultiplayerServer(RequestMultiplayerServerResponse response) {
        // transition server from standby state to active state - then the client will be able to fully connect
        ConnectRemoteClient(response);
    }

    private void OnLoginError(PlayFabError error) {
        Debug.LogError("Login failed - " + error);
    }
    
    private void ConnectRemoteClient(RequestMultiplayerServerResponse response = null) {
        if (response == null) {
            UnityNetworkServer.Instance.networkAddress = ipAddress;
            UnityNetworkServer.Instance.GetComponent<kcp2k.KcpTransport>().Port = port;
            // telepathyTransport.port = configuration.port;
            // apathyTransport.port = configuration.port;
        }
        else {
            Debug.Log("**** THESE ARE OUR DETAILS **** --- IP: " + response.IPV4Address + " Port: " +
                      (ushort) response.Ports[0].Num + " --- \n Copy ip address and port to ClientStartup.cs fields. Then try logging in again");

            UnityNetworkServer.Instance.networkAddress = response.IPV4Address;
            UnityNetworkServer.Instance.GetComponent<kcp2k.KcpTransport>().Port = (ushort) response.Ports[0].Num;
            // telepathyTransport.port = (ushort) response.Ports[0].Num;
            // apathyTransport.port = (ushort) response.Ports[0].Num;
        }

        UnityNetworkServer.Instance.StartClient();
    }
}