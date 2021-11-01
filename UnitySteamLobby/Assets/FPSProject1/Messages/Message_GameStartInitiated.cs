using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_GameStartInitiated : Message
    {
        public Message_GameStartInitiated()
        {
            _messageType = MessageType.GAME_START_INITIATED;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}