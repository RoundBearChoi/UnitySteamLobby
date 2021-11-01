using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.UI;

namespace RB
{
    public class UIMenuLoader : GameResources<UIMenuType>
    {
        public override void InitLoader()
        {
            LoaderDebug.Log("loading ui menus..");

            LoadObj<UIMenu_Empty>(UIMenuType.NONE, "UIMenu_Empty");
            LoadObj<UIMenu_Intro>(UIMenuType.UIMENU_INTRO, "UIMenu_Intro");
            LoadObj<UIMenu_FailedToSyncWithSteam>(UIMenuType.UIMENU_FAILED_TO_SYNC_WITH_STEAM, "UIMenu_FailedToSyncWithSteam");
            LoadObj<UIMenu_ServerLobby>(UIMenuType.UIMENU_SERVER_LOBBY, "UIMenu_ServerLobby");
            LoadObj<UIMenu_JoinGame>(UIMenuType.UIMENU_JOIN_GAME, "UIMenu_JoinGame");
            
            LoadObj<UIMenu_Client_Lobby>(UIMenuType.UIMENU_CLIENT_LOBBY, "UIMenu_ClientLobby");
            LoadObj<UIMenu_Disconnected>(UIMenuType.UIMENU_DISCONNECTED, "UIMenu_Disconnected");
            LoadObj<UIMenu_Connecting>(UIMenuType.UIMENU_CONNECTING, "UIMenu_Connecting");
        }
    }
}