using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_HelloReceived : Message
    {
        public Message_HelloReceived()
        {
            _messageType = MessageType.HELLO_RECEIVED;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}