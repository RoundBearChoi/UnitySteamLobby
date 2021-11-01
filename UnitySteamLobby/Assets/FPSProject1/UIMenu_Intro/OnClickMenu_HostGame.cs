using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class OnClickMenu_HostGame : OnClickMenu
    {
        public OnClickMenu_HostGame()
        {
            _actionName = "host game";
        }

        public override void Do()
        {
            UIDebug.Log("clicked on: host game");

            Message message = new Message_StageTransition(typeof(CreatingLobbyStage));
            message.Register();
        }
    }
}