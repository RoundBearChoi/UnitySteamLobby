using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RB.UI
{
    public class UIElement_JoiningLobby : UIElementObj
    {
        public override void Init(IGameInitializer initializer)
        {
            _initializer = initializer;
        }
    }
}