using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.UI;

namespace RB
{
    public class UIElementLoader : GameResources<UI.UIElementType>
    {
        public override void InitLoader()
        {
            LoaderDebug.Log("loading uielements..");

            LoadObj<UIElement_Version>(UIElementType.VERSION, "UIElement_Version");
            LoadObj<UIElement_PressEnter>(UIElementType.PRESS_ENTER, "UIElement_PressEnter");
            LoadObj<UIElement_FailedToSyncWithSteam>(UIElementType.FAILED_TO_SYNC_WITH_STEAM, "UIElement_FailedToSyncWithSteam");

            LoadObj<UIElement_CreatingLobby>(UIElementType.CREATING_LOBBY, "UIElement_CreatingLobby");
            LoadObj<UIElement_JoiningLobby>(UIElementType.JOINING_LOBBY, "UIElement_JoiningLobby");
            LoadObj<UIElement_Connecting>(UIElementType.CONNECTING, "UIElement_Connecting");
            LoadObj<UIElement_Disconnected>(UIElementType.DISCONNECTED, "UIElement_Disconnected");

            LoadObj<UIElement_PlayersList>(UIElementType.PLAYERS_LIST, "UIElement_PlayersList");
            LoadObj<UIElement_ChatBox>(UIElementType.CHAT_BOX, "UIElement_ChatBox");
            LoadObj<UIElement_ChatInput>(UIElementType.CHAT_INPUT, "UIElement_ChatInput");
        }
    }
}