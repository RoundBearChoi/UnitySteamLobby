using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;
using RB.Network;

namespace RB.SteamIntegration
{
    public interface ISteamControl
    {
        public SteamId STEAM_ID { get; }
        public string STEAM_NAME { get; }
        public bool InitSteamClient(IGameInitializer initializer);
        public void CreateSteamLobby();
        public void SetLobbyInfo(Lobby lobby);
        public bool IsLobbyHost(SteamId steamID);
        public bool IsLobbyMember(SteamId steamID);
        public string GetMemberName(SteamId steamID);
        public Friend GetFriend(SteamId steamID);
        public void SetLobbyPublic(bool isPublic);
        public void SetLobbyJoinable(bool joinable);
        public void Send_Hello(SteamId steamID);
        public void Send_HelloBack(SteamId steamID, uint randomKey);
        public void Send_NewHandShakenPlayer(HandShakenPlayer player);
        public void Send_RemovedHandShakenPlayer(HandShakenPlayer player);
        public void Send_HandShakenPlayersList();
        public void Send_Chat(SteamId receiverID, string text);
        public void Send_Blank(SteamId receiverID);
        public void Send_OriginalHostLeft();
        public void Send_GameStartingCountDown(int countdown);
        public void Send_StartGame();
        public void Send_SpawnPoint(SteamId steamID, Vector3 spawnPoint);
        public void Send_Player_Position(Vector3 position);
        public void LeaveLobby();
        public void ShutDown();
    }
}