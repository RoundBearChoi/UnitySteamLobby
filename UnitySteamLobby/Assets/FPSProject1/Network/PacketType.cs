using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Network
{
    public enum PacketType
    {
        NONE = 0,

        HELLO,
        HELLO_BACK,
        NEW_HAND_SHAKEN_PLAYER,
        REMOVED_HAND_SHAKEN_PLAYER,
        HAND_SHAKEN_PLAYERS_LIST,
        CHAT,
        BLANK_MESSAGE,
        ORIGINAL_HOST_LEFT,
        GAME_STARTING_COUNTDOWN,
        START_GAME,
        SPAWN_POINT,
        PLAYER_POSITION,
    }
}