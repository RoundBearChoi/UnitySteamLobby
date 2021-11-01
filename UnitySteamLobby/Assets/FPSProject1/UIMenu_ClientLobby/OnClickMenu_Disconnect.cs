using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class OnClickMenu_Disconnect : OnClickMenu
    {
        public OnClickMenu_Disconnect()
        {
            _actionName = "disconnect";
        }

        public override void Do()
        {
            UIDebug.Log("clicked on: disconnect");

            Message message = new Message_StageTransition(typeof(DisconnectedStage));
            message.Register();
        }
    }
}