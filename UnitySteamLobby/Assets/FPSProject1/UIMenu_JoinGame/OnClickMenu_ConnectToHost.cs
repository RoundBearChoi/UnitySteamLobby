using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class OnClickMenu_ConnectToHost : OnClickMenu
    {
        public OnClickMenu_ConnectToHost()
        {
            _actionName = "connect to host";
        }

        public override void Do()
        {
            UIDebug.Log("clicked on: connect to host");

            Message message = new Message_StageTransition(typeof(ConnectingStage));
            message.Register();
        }
    }
}