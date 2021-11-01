using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_HandShakenPlayersListReceived : Message
    {
        public Message_HandShakenPlayersListReceived()
        {
            _messageType = MessageType.HAND_SHAKEN_PLAYERS_LIST_RECEIVED;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}