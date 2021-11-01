using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_NewHandShakenPlayer : Message
    {
        public Message_NewHandShakenPlayer()
        {
            _messageType = MessageType.NEW_HAND_SHAKEN_PLAYER;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}