using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class UIMenu_Client_Lobby : UIMenu
    {
        public override void Init(IGameInitializer baseInitializer)
        {
            _initializer = baseInitializer;

            menu = new Menu_Client_Lobby();
            menu.Init(this, _selectedColor, _unSelectedColor);
        }

        public override void OnFixedUpdate()
        {
            menu.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            OnESC();
            menu.OnUpdate();
        }

        public override void OnLateUpdate()
        {
            menu.OnLateUpdate();
        }
    }
}