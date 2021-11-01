using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Network
{
    [System.Serializable]
    public struct HandShakenPlayer
    {
        public Steamworks.SteamId mSteamID;
        public string mSteamName;

        public HandShakenPlayer(Steamworks.SteamId steamID, string steamName)
        {
            mSteamID = steamID;
            mSteamName = steamName;
        }
    }
}