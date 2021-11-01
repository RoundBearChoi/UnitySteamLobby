using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_MemberJoined : Message
    {
        public Message_MemberJoined()
        {
            _messageType = MessageType.MEMBER_JOINED;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}