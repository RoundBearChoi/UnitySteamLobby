using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_SpawnPositionReceived : Message
    {
        public Message_SpawnPositionReceived()
        {
            _messageType = MessageType.SPAWN_POSITION_RECEIVED;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}