using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_StageTransition : Message
    {
        public Message_StageTransition(System.Type stageType)
        {
            _messageType = MessageType.STAGE_TRANSITION;

            types = new List<System.Type>();
            types.Add(stageType);
        }

        public override void Register()
        {
            MessageHandler_StageTransition.handler.AddMessage(this);
        }
    }
}