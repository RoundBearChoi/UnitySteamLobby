using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class OnClickMenu_Quit : OnClickMenu
    {
        public OnClickMenu_Quit()
        {
            _actionName = "quit";
        }

        public override void Do()
        {
            UIDebug.Log("clicked on: quit");

            Application.Quit();
        }
    }
}