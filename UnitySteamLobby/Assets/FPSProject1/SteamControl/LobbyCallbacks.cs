using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

namespace RB.SteamIntegration
{
    public static class LobbyCallbacks
    {
        static IGameInitializer _initializer = null;

        public static void SetInitializer(IGameInitializer initializer)
        {
            _initializer = initializer;
        }

        public static void LobbyCreated(Result result, Lobby lobby)
        {
            if (result == Result.OK)
            {
                SteamDebug.Log("callback.. steam lobby created.. waiting for lobbyenter callback..");

                //lobby.SetFriendsOnly();
                lobby.SetPublic();
                lobby.SetJoinable(true);
                lobby.MaxMembers = 10;
            }

            else
            {
                SteamDebug.Log("callback.. failed to create steam lobby.. transitioning to intro stage..");

                Message message = new Message_StageTransition(typeof(IntroStage));
                message.Register();
            }
        }

        public static void InvitedToLobby(Friend friend, Lobby lobby)
        {
            SteamDebug.Log("callback.. " + friend.Name + " invited you to lobby");
            SteamDebug.Log("lobby id: " + lobby.Id.Value);
        }

        public static void LobbyEntered(Lobby lobby)
        {
            SteamDebug.Log("callback.. lobby entered..");

            Message message = new Message_EnteredLobby();
            message.lobbies = new List<Lobby>();
            message.lobbies.Add(lobby);
            message.Register();
        }

        public static void MemberJoined(Lobby lobby, Friend friend)
        {
            SteamDebug.Log("callback.. member joined: " + friend.Id.Value);

            Message message = new Message_MemberJoined();
            message.friends = new List<Friend>();
            message.friends.Add(friend);
            message.Register();
        }

        public static void MemberDisconnected(Lobby lobby, Friend friend)
        {
            SteamDebug.Log("callback.. member disconnected: " + friend.Name);

            Message message = new Message_MemberLeft();
            message.friends = new List<Friend>();
            message.friends.Add(friend);
            message.Register();
        }

        public static void MemberLeft(Lobby lobby, Friend friend)
        {
            SteamDebug.Log("callback.. member left: " + friend.Name);

            Message message = new Message_MemberLeft();
            message.friends = new List<Friend>();
            message.friends.Add(friend);
            message.Register();
        }

        public static void RequestingLobbyJoin(Lobby lobby, SteamId steamID)
        {
            //make sure to leave previous lobby
            if (_initializer.SERVER.SERVER_STARTED || _initializer.CLIENT.CLIENT_STARTED)
            {
                Message m = new Message_StageTransition(typeof(DisconnectedStage));
                m.Register();
            }
            else
            {
                SteamDebug.Log("callback.. requesting lobby join");
                SteamDebug.Log("lobby ID: " + lobby.Id.Value);
                SteamDebug.Log("steamID: " + steamID.Value);

                _initializer.InitClient();

                Message m = new Message_StageTransition(typeof(JoiningLobbyStage));
                m.Register();

                lobby.Join();
            }
        }

        public static void RequestingP2PSession(SteamId requesterID)
        {
            SteamDebug.Log("incoming p2p request from: " + requesterID);

            if (_initializer.STEAM_CONTROL.IsLobbyHost(requesterID))
            {
                SteamDebug.Log("accepting request from server host: " + _initializer.STEAM_CONTROL.GetMemberName(requesterID));
                SteamNetworking.AcceptP2PSessionWithUser(requesterID);
            }
            else if (_initializer.STEAM_CONTROL.IsLobbyMember(requesterID))
            {
                SteamDebug.Log("accepting request from lobby member: " + _initializer.STEAM_CONTROL.GetMemberName(requesterID));
                SteamNetworking.AcceptP2PSessionWithUser(requesterID);
            }
        }
    }
}