using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;
using RB.Network;

namespace RB.SteamIntegration
{
    [System.Serializable]
    public class SteamControl : ISteamControl
    {
        IGameInitializer _initializer = null;

        SteamId _steamID;
        
        [SerializeField] string _personalName;
        [SerializeField] ulong _accountID;
        
        [SerializeField] P2P_Send _p2pSend;
        [SerializeField] P2P_Receive _p2pReceive;
        [SerializeField] LobbyControl _lobbyControl;

        public SteamId STEAM_ID { get { return _steamID; } }
        public string STEAM_NAME { get { return _personalName; } }

        public bool InitSteamClient(IGameInitializer initializer)
        {
            _initializer = initializer;

            _p2pSend = new P2P_Send(initializer);
            _p2pReceive = new P2P_Receive(initializer);
            _lobbyControl = new LobbyControl(initializer);

            SteamDebug.Log("initializing SteamClient");

            SetCallbacks();

            if (SteamClient.IsValid)
            {
                SteamDebug.Log("SteamClient already initialized");
                _SetLocalSteamAccountInfo();

                return true;
            }
            else
            {
                try
                {
                    SteamClient.Init(480, false);
                    SteamDebug.Log("SteamClient initialized successfully!");
                    _SetLocalSteamAccountInfo();

                    return true;
                }
                catch (System.Exception e)
                {
                    SteamDebug.Log("system error while initializing SteamClient: " + e);

                    return false;
                }
            }
        }

        void SetCallbacks()
        {
            LobbyCallbacks.SetInitializer(_initializer);
            SteamMatchmaking.OnLobbyCreated += LobbyCallbacks.LobbyCreated;
            SteamMatchmaking.OnLobbyInvite += LobbyCallbacks.InvitedToLobby;
            SteamMatchmaking.OnLobbyEntered += LobbyCallbacks.LobbyEntered;
            SteamMatchmaking.OnLobbyMemberJoined += LobbyCallbacks.MemberJoined;
            SteamMatchmaking.OnLobbyMemberDisconnected += LobbyCallbacks.MemberDisconnected;
            SteamMatchmaking.OnLobbyMemberLeave += LobbyCallbacks.MemberLeft;
            SteamFriends.OnGameLobbyJoinRequested += LobbyCallbacks.RequestingLobbyJoin;
            SteamNetworking.OnP2PSessionRequest += LobbyCallbacks.RequestingP2PSession;
        }

        void _SetLocalSteamAccountInfo()
        {
            if (SteamClient.IsValid)
            {
                _steamID = SteamClient.SteamId;
                _personalName = SteamClient.Name;
                _accountID = SteamClient.SteamId.Value;
            }
        }

        public void CreateSteamLobby()
        {
            _lobbyControl.CreateSteamLobby();
        }

        public void SetLobbyInfo(Lobby lobby)
        {
            _lobbyControl.SaveLobby(lobby);
        }

        public bool IsLobbyHost(SteamId steamID)
        {
            return _lobbyControl.IsLobbyHost(steamID);
        }

        public bool IsLobbyMember(SteamId steamID)
        {
            return _lobbyControl.IsLobbyMember(steamID);
        }

        public string GetMemberName(SteamId steamID)
        {
            return _lobbyControl.GetMemberName(steamID);
        }

        public Friend GetFriend(SteamId steamID)
        {
            return _lobbyControl.GetFriend(steamID);
        }

        public void SetLobbyPublic(bool isPublic)
        {
            _lobbyControl.SetLobbyPublic(isPublic);
        }

        public void SetLobbyJoinable(bool joinable)
        {
            _lobbyControl.SetLobbyJoinable(joinable);
        }

        public void Send_Hello(SteamId steamID)
        {
            _p2pSend.Send_Hello(steamID);
        }

        public void Send_HelloBack(SteamId steamID, uint randomKey)
        {
            _p2pSend.Send_HelloBack(steamID, randomKey);
        }

        public void Send_NewHandShakenPlayer(HandShakenPlayer player)
        {
            _p2pSend.Send_NewHandShakenPlayer(player);
        }

        public void Send_RemovedHandShakenPlayer(HandShakenPlayer player)
        {
            _p2pSend.Send_RemovedHandShakenPlayer(player);
        }

        public void Send_HandShakenPlayersList()
        {
            _p2pSend.Send_HandShakenPlayersList();
        }

        public void Send_Chat(SteamId receiverID, string text)
        {
            _p2pSend.Send_Chat(receiverID, text);
        }

        public void Send_Blank(SteamId receiverID)
        {
            _p2pSend.Send_Blank(receiverID);
        }

        public void Send_OriginalHostLeft()
        {
            _p2pSend.Send_OriginalHostLeft();
        }

        public void Send_GameStartingCountDown(int countdown)
        {
            _p2pSend.Send_GameStartingCountDown(countdown);
        }

        public void Send_StartGame()
        {
            _p2pSend.Send_StartGame();
        }

        public void Send_SpawnPoint(SteamId steamID, Vector3 spawnPoint)
        {
            _p2pSend.Send_SpawnPoint(steamID, spawnPoint);
        }

        public void Send_Player_Position(Vector3 position)
        {
            _p2pSend.Send_Player_Position(position);
        }

        public void OnFixedUpdate()
        {
            _p2pReceive.OnFixedUpdate();
        }

        public void OnUpdate()
        {

        }

        public void OnLateUpdate()
        {
            if (SteamClient.IsValid)
            {
                SteamClient.RunCallbacks();
            }
        }

        public void LeaveLobby()
        {
            _lobbyControl.LeaveLobby();
        }

        public void ShutDown()
        {
            try
            {
                SteamDebug.Log("shutting down steamworks");

                SteamClient.Shutdown();
            }
            catch (System.Exception e)
            {
                SteamDebug.Log("system error while shutting down steamworks: " + e);
            }
        }
    }
}