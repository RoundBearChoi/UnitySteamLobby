using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

namespace RB
{
    public class Server_Listener : IListener
    {
        IGameInitializer _initializer = null;
        Coroutine _gameStartRoutine = null;
        bool _remove = false;

        public bool REMOVE { get { return _remove; } set { _remove = value; } }

        public Server_Listener(IGameInitializer initializer)
        {
            MessageDebug.Log("adding listener: server");

            _initializer = initializer;

            MessageHandler_Network.listeners.Add(this);
        }

        public void OnNotify(Message message)
        {
            if (message.MESSAGE_TYPE == MessageType.ENTERED_LOBBY)
            {
                Network.HandShakenPlayer[] arr = _initializer.SERVER.GetAllHandShakenPlayers();

                //only transition once
                if (arr.Length == 0)
                {
                    Lobby lobby = message.lobbies[0];
                    _initializer.STEAM_CONTROL.SetLobbyInfo(lobby);

                    SteamId myID = _initializer.STEAM_CONTROL.STEAM_ID;
                    string myName = _initializer.STEAM_CONTROL.STEAM_NAME;

                    MessageDebug.Log("adding first handshakenplayer (server): " + myName);

                    _initializer.SERVER.AddHandShakenPlayer(new Network.HandShakenPlayer(myID, myName));

                    Message stageMessage = new Message_StageTransition(typeof(ServerLobbyStage));
                    stageMessage.Register();
                }
            }

            else if (message.MESSAGE_TYPE == MessageType.MEMBER_JOINED)
            {
                MessageDebug.Log("member joined");

                Friend friend = message.friends[0];

                MessageDebug.Log("sending hello to: " + friend.Name);
                _initializer.STEAM_CONTROL.Send_Hello(friend.Id);
            }

            else if (message.MESSAGE_TYPE == MessageType.HELLOBACK_RECEIVED)
            {
                MessageDebug.Log("helloback received");

                uint randomKey = message.uInts[0];
                string senderName = message.strings[0];
                SteamId steamID = message.steamIDs[0];

                if (randomKey == _initializer.SERVER.RANDOM_KEY)
                {
                    MessageDebug.Log("matching key received from: " + senderName);
                    MessageDebug.Log("sender ID: " + steamID);
                    MessageDebug.Log("random key: " + randomKey);
                    MessageDebug.Log("adding to hand shaken player list");

                    _initializer.SERVER.AddHandShakenPlayer(new Network.HandShakenPlayer(steamID, senderName));
                }
            }

            else if (message.MESSAGE_TYPE == MessageType.MEMBER_LEFT)
            {
                Friend friend = message.friends[0];
                MessageDebug.Log("member left server.. " + friend.Name + ".. removing from handshaken list");
                _initializer.SERVER.RemoveHandShakenPlayer(friend);
            }

            else if (message.MESSAGE_TYPE == MessageType.GAME_START_INITIATED)
            {
                if (_gameStartRoutine == null)
                {
                    _gameStartRoutine = _initializer.RunRoutine(GameStartRoutine());
                }
            }
        }

        IEnumerator GameStartRoutine()
        {
            _initializer.STEAM_CONTROL.Send_GameStartingCountDown(3);
            yield return new WaitForSeconds(1);

            _initializer.STEAM_CONTROL.Send_GameStartingCountDown(2);
            yield return new WaitForSeconds(1);

            _initializer.STEAM_CONTROL.Send_GameStartingCountDown(1);
            yield return new WaitForSeconds(1);

            _initializer.STEAM_CONTROL.Send_StartGame();
        }
    }
}