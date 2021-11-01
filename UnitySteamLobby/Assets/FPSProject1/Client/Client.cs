using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Client
{
    [System.Serializable]
    public class Client
    {
        [SerializeField] bool _clientStarted = false;
        [SerializeField] Client_Listener _clientListener;
        [SerializeField] Network.HandShakenPlayer[] _handShakenPlayers;
        [Space(10)]
        [SerializeField] Steamworks.SteamId _serverHostID;
        [SerializeField] ulong _serverHostIDValue;

        public bool CLIENT_STARTED { get { return _clientStarted; } }

        IGameInitializer _initializer = null;

        public Client(IGameInitializer initializer)
        {
            _initializer = initializer;
            _clientListener = new Client_Listener(initializer);
            _handShakenPlayers = new Network.HandShakenPlayer[0];
            _clientStarted = true;
        }

        public void SetServerHostID(Steamworks.SteamId serverHostID)
        {
            _serverHostID = serverHostID;
            _serverHostIDValue = serverHostID.Value;
        }

        public void SetHandShakenPlayersList(Network.HandShakenPlayer[] arrPlayers)
        {
            _handShakenPlayers = arrPlayers;
        }

        public Network.HandShakenPlayer[] GetAllHandShakenPlayers()
        {
            return _handShakenPlayers;
        }

        public void EndClient()
        {
            if (_initializer != null)
            {
                _initializer.STEAM_CONTROL.LeaveLobby();
            }

            if (_clientListener != null)
            {
                _clientListener.REMOVE = true;
            }

            _clientStarted = false;
        }
    }
}