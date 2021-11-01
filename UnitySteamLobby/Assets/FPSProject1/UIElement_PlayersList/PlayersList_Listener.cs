using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.UI;

namespace RB
{
    public class PlayersList_Listener : IListener
    {
        private IGameInitializer _initializer = null;
        private UIElement_PlayersList _playersList = null;
        private bool _remove = false;

        public bool REMOVE { get { return _remove; } set { _remove = value; } }

        public PlayersList_Listener(IGameInitializer initializer, UIElement_PlayersList playersList)
        {
            _initializer = initializer;
            MessageDebug.Log("adding listener: playerslist");
            MessageHandler_Network.listeners.Add(this);
            _playersList = playersList;
        }

        public void OnNotify(Message message)
        {
            if (message.MESSAGE_TYPE == MessageType.HAND_SHAKEN_PLAYERS_LIST_UPDATED)
            {
                //client: just update ui
                _playersList.UpdatePlayersList();

                //server: send info to all clients
                if (_initializer.SERVER.SERVER_STARTED)
                {
                    _initializer.STEAM_CONTROL.Send_HandShakenPlayersList();
                }
            }
        }
    }
}