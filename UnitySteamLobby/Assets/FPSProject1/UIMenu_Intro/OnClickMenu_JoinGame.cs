using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class OnClickMenu_JoinGame : OnClickMenu
    {
        public OnClickMenu_JoinGame()
        {
            _actionName = "join game";
        }

        public override void Do()
        {
            UIDebug.Log("clicked on: join game");

            Message message = new Message_StageTransition(typeof(JoinGameStage));
            message.Register();
        }
    }
}