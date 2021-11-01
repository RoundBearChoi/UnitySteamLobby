using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_HelloBackReceived : Message
    {
        public Message_HelloBackReceived()
        {
            _messageType = MessageType.HELLOBACK_RECEIVED;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}