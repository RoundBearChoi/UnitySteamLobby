using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_RemovedHandShakenPlayer : Message
    {
        public Message_RemovedHandShakenPlayer()
        {
            _messageType = MessageType.REMOVED_HAND_SHAKEN_PLAYER;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}