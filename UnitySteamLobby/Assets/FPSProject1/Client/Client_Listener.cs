using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

namespace RB
{
    public class Client_Listener : IListener
    {
        IGameInitializer _initializer = null;
        bool _remove = false;

        public bool REMOVE { get { return _remove; } set { _remove = value; } }

        public Client_Listener(IGameInitializer initializer)
        {
            MessageDebug.Log("adding listener: client");
            _initializer = initializer;
            MessageHandler_Network.listeners.Add(this);
        }

        public void OnNotify(Message message)
        {
            if (message.MESSAGE_TYPE == MessageType.ENTERED_LOBBY)
            {
                Lobby lobby = message.lobbies[0];
                _initializer.STEAM_CONTROL.SetLobbyInfo(lobby);

                SteamDebug.Log("entered lobby.. waiting for server to say hello..");
            }

            else if (message.MESSAGE_TYPE == MessageType.HELLO_RECEIVED)
            {
                SteamDebug.Log("received hello from server");

                uint randomKey = message.uInts[0];
                SteamId serverSteamID = message.steamIDs[0];

                _initializer.CLIENT.SetServerHostID(serverSteamID);
                _initializer.STEAM_CONTROL.Send_HelloBack(message.steamIDs[0], randomKey);

                Message stageMessage = new Message_StageTransition(typeof(ClientLobbyStage));
                stageMessage.Register();
            }

            else if (message.MESSAGE_TYPE == MessageType.HAND_SHAKEN_PLAYERS_LIST_RECEIVED)
            {
                List<SteamId> listPlayers = message.steamIDs;

                List<Friend> listFriends = new List<Friend>();

                foreach (SteamId id in listPlayers)
                {
                    Friend friend = _initializer.STEAM_CONTROL.GetFriend(id);
                    listFriends.Add(friend);
                }

                Network.HandShakenPlayer[] arr = new Network.HandShakenPlayer[listFriends.Count];

                //MessageDebug.Log("sending blanks to everyone in the handshaken list");

                for (int i = 0; i < listFriends.Count; i++)
                {
                    arr[i] = new Network.HandShakenPlayer(listFriends[i].Id, listFriends[i].Name);

                    if (arr[i].mSteamID.Value != _initializer.STEAM_CONTROL.STEAM_ID.Value)
                    {
                        //MessageDebug.Log("sending blank to: " + arr[i].mSteamName);
                        _initializer.STEAM_CONTROL.Send_Blank(arr[i].mSteamID);
                    }
                }

                _initializer.CLIENT.SetHandShakenPlayersList(arr);

                //message to update ui
                Message m = new Message_HandShakenPlayersListUpdated();
                m.Register();
            }

            else if (message.MESSAGE_TYPE == MessageType.MEMBER_LEFT)
            {
                Friend friend = message.friends[0];

                MessageDebug.Log("member left: " + friend.Name);
            }
        }
    }
}