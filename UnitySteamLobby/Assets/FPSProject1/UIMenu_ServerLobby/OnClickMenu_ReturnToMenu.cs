using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class OnClickMenu_ReturnToMenu : OnClickMenu
    {
        public OnClickMenu_ReturnToMenu()
        {
            _actionName = "return to menu";
        }

        public override void Do()
        {
            UIDebug.Log("clicked on: return to menu");

            Message message = new Message_StageTransition(typeof(IntroStage));
            message.Register();
        }
    }
}