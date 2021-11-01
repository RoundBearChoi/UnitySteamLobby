using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public enum UIMenuType
    {
        NONE,

        UIMENU_INTRO,
        UIMENU_FAILED_TO_SYNC_WITH_STEAM,
        //UIMENU_CREATING_LOBBY,
        UIMENU_SERVER_LOBBY,
        UIMENU_JOIN_GAME,

        UIMENU_CLIENT_LOBBY,
        UIMENU_DISCONNECTED,
        UIMENU_CONNECTING,
    }
}