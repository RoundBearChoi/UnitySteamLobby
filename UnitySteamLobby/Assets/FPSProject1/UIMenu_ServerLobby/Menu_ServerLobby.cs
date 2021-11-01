using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class Menu_ServerLobby : Menu
    {
        public override void Init(UIMenu uiSelectionMenu, Color selectedColor, Color unSelectedColor)
        {
            SetMenuItems(uiSelectionMenu, selectedColor, unSelectedColor);

            _listMenuItems[0]._onClickMenu = new OnClickMenu_StartGame();
            _listMenuItems[1]._onClickMenu = new OnClickMenu_Disconnect();
        }

        public override void OnFixedUpdate()
        {
            foreach (MenuItem option in _listMenuItems)
            {
                option.OnFixedUpdate();
            }
        }

        public override void OnUpdate()
        {
            foreach (MenuItem option in _listMenuItems)
            {
                option.OnUpdate();
            }
        }

        public override void OnLateUpdate()
        {
            foreach (MenuItem option in _listMenuItems)
            {
                option.OnLateUpdate();
            }
        }
    }
}