using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

namespace RB.SteamIntegration
{
    [System.Serializable]
    public class LobbyControl
    {
        IGameInitializer _initializer = null;

        Lobby _lobby;
        [SerializeField] ulong _lobbyID;

        public LobbyControl(IGameInitializer initializer)
        {
            _initializer = initializer;
        }

        public void CreateSteamLobby()
        {
            SteamDebug.Log("creating steam lobby");

            SteamMatchmaking.CreateLobbyAsync(10);
        }

        public void SaveLobby(Lobby lobby)
        {
            _lobby = lobby;
            _lobbyID = lobby.Id.Value;
        }

        public bool IsLobbyHost(SteamId steamID)
        {
            if (_lobby.Owner.Id.Value == steamID.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsLobbyMember(SteamId steamID)
        {
            IEnumerable<Friend> e = _lobby.Members;
            IEnumerator f = e.GetEnumerator();

            while (f.MoveNext())
            {
                Friend friend = (Friend)f.Current;

                if (friend.Id.Value == steamID.Value)
                {
                    return true;
                }
            }

            return false;
        }

        public string GetMemberName(SteamId steamID)
        {
            IEnumerable<Friend> e = _lobby.Members;
            IEnumerator f = e.GetEnumerator();

            while (f.MoveNext())
            {
                Friend friend = (Friend)f.Current;

                if (friend.Id.Value == steamID.Value)
                {
                    return friend.Name;
                }
            }

            return "non member";
        }

        public Friend GetFriend(SteamId steamID)
        {
            IEnumerable<Friend> e = _lobby.Members;
            IEnumerator f = e.GetEnumerator();

            while (f.MoveNext())
            {
                Friend friend = (Friend)f.Current;

                if (friend.Id.Value == steamID.Value)
                {
                    return friend;
                }
            }

            return new Friend();
        }

        public void SetLobbyPublic(bool isPublic)
        {
            if (isPublic)
            {
                _lobby.SetPublic();
            }
            else
            {
                _lobby.SetPrivate();
            }
        }

        public void SetLobbyJoinable(bool joinable)
        {
            _lobby.SetJoinable(joinable);
        }

        public void LeaveLobby()
        {
            try
            {
                SteamDebug.Log("leaving lobby");

                _lobby.Leave();
            }
            catch (System.Exception e)
            {
                SteamDebug.Log("system error leaving lobby: " + e);
            }

            SteamDebug.Log("left lobby");
        }
    }
}