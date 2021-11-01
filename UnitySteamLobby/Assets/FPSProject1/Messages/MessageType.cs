using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public enum MessageType
    {
        NONE,

        STAGE_TRANSITION,

        ENTERED_LOBBY,
        HELLO_RECEIVED,
        HELLOBACK_RECEIVED,
        MEMBER_JOINED,
        MEMBER_LEFT,
        ORIGINAL_HOST_LEFT,
        NEW_HAND_SHAKEN_PLAYER,
        REMOVED_HAND_SHAKEN_PLAYER,
        HAND_SHAKEN_PLAYERS_LIST_UPDATED,
        HAND_SHAKEN_PLAYERS_LIST_RECEIVED,
        CHAT_SENT,
        CHAT_RECEIVED,
        INCOMING_P2P_REQUEST,
        GAME_START_INITIATED,
        GAME_STARTING_COUNTDOWN,
        PLAYER_POSITION_RECEIVED,

        SPAWN_POSITION_RECEIVED,
    }
}