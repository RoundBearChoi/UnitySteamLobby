using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public enum UIElementType
    {
        NONE,

        VERSION,
        PRESS_ENTER,
        FAILED_TO_SYNC_WITH_STEAM,

        CREATING_LOBBY,
        JOINING_LOBBY,
        CONNECTING,
        DISCONNECTED,

        PLAYERS_LIST,
        CHAT_BOX,
        CHAT_INPUT,
    }
}