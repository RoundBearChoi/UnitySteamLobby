using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_PlayerPositionReceived : Message
    {
        public Message_PlayerPositionReceived()
        {
            _messageType = MessageType.PLAYER_POSITION_RECEIVED;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}