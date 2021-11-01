using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_HandShakenPlayersListUpdated : Message
    {
        public Message_HandShakenPlayersListUpdated()
        {
            _messageType = MessageType.HAND_SHAKEN_PLAYERS_LIST_UPDATED;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}