using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RB
{
    public class Message_EnteredLobby : Message
    {
        public Message_EnteredLobby()
        {
            _messageType = MessageType.ENTERED_LOBBY;
        }

        public override void Register()
        {
            MessageHandler_Network.handler.AddMessage(this);
        }
    }
}