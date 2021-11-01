using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_GameStarting_CountDown : Message
    {
        public Message_GameStarting_CountDown()
        {
            _messageType = MessageType.GAME_STARTING_COUNTDOWN;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}