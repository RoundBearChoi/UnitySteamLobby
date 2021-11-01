using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_ChatSent : Message
    {
        public Message_ChatSent()
        {
            _messageType = MessageType.CHAT_SENT;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}