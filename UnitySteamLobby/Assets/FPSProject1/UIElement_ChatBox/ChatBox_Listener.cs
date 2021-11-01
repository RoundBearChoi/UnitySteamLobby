using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.UI;
using Steamworks;

namespace RB
{
    public class ChatBox_Listener : IListener
    {
        private IGameInitializer _initializer = null;
        private UIElement_ChatBox _chatBox = null;
        private bool _remove = false;

        public bool REMOVE { get { return _remove; } set { _remove = value; } }
        
        public ChatBox_Listener(IGameInitializer initializer, UIElement_ChatBox chatBox)
        {
            _initializer = initializer;
            MessageDebug.Log("adding listener: chatbox");
            MessageHandler_Network.listeners.Add(this);
            _chatBox = chatBox;
        }

        public void OnNotify(Message message)
        {
            if (message.MESSAGE_TYPE == MessageType.NEW_HAND_SHAKEN_PLAYER)
            {
                SteamId steamID = message.steamIDs[0];
                string name = message.strings[0];
                
                SteamDebug.Log("received new player " + name + ".. " + steamID.Value);

                if (!_initializer.STEAM_CONTROL.IsLobbyHost(steamID))
                {
                    _chatBox.AddNotification(name + " joined");
                }
            }

            else if(message.MESSAGE_TYPE == MessageType.REMOVED_HAND_SHAKEN_PLAYER)
            {
                SteamId steamID = message.steamIDs[0];
                string name = message.strings[0];

                SteamDebug.Log("received removed player " + name + ".. " + steamID.Value);

                _chatBox.AddNotification(name + " left");
            }

            else if (message.MESSAGE_TYPE == MessageType.CHAT_SENT)
            {
                string text = message.strings[0];

                _chatBox.AddChatText(_initializer.STEAM_CONTROL.STEAM_ID.Value, _initializer.STEAM_CONTROL.STEAM_NAME, text);
            }

            else if (message.MESSAGE_TYPE == MessageType.CHAT_RECEIVED)
            {
                SteamId senderID = message.steamIDs[0];
                string text = message.strings[0];
                string senderName = _initializer.STEAM_CONTROL.GetMemberName(senderID);

                _chatBox.AddChatText(senderID, senderName, text);
            }

            else if (message.MESSAGE_TYPE == MessageType.MEMBER_LEFT)
            {
                Friend friend = message.friends[0];
                _chatBox.RemoveColorIndex(friend.Id.Value);
            }

            else if (message.MESSAGE_TYPE == MessageType.GAME_STARTING_COUNTDOWN)
            {
                int countdown = message.ints[0];

                _chatBox.AddNotification("Game starting in " + countdown);
            }
        }
    }
}