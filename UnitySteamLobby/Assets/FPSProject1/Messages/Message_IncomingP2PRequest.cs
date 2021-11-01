using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_IncomingP2PRequest : Message
    {
        public Message_IncomingP2PRequest()
        {
            _messageType = MessageType.INCOMING_P2P_REQUEST;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}