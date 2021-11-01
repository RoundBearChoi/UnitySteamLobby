using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class OnClickMenu_StartGame : OnClickMenu
    {
        public OnClickMenu_StartGame()
        {
            _actionName = "start game";
        }

        public override void Do()
        {
            UIDebug.Log("clicked on: start game");

            Message m = new Message_GameStartInitiated();
            m.Register();
        }
    }
}