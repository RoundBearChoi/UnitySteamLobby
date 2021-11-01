using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_MemberLeft : Message
    {
        public Message_MemberLeft()
        {
            _messageType = MessageType.MEMBER_LEFT;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}