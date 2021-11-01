using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class UILoader : GameResources<UI.UIType>
    {
        public override void InitLoader()
        {
            LoaderDebug.Log("loading ui..");

            LoadObj<UI.StandardCanvas>(UI.UIType.STANDARD_CANVAS, "StandardCanvas - Unity Input System");
        }
    }
}