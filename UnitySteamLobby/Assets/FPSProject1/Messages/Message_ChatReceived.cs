using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_ChatReceived : Message
    {
        public Message_ChatReceived()
        {
            _messageType = MessageType.CHAT_RECEIVED;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}