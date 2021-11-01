using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.Network;

namespace RB.Server
{
    [System.Serializable]
    public class Server : IServer
    {
        [SerializeField] bool _serverStarted = false;
        [SerializeField] uint _randomKey = 0;
        [SerializeField] ServerController _serverController;
        [SerializeField] Server_Listener _serverListener;
        [SerializeField] List<HandShakenPlayer> _listHandShakenPlayers = new List<HandShakenPlayer>();

        public bool SERVER_STARTED { get { return _serverStarted; } }
        public uint RANDOM_KEY { get { return _randomKey; } }

        IGameInitializer _initializer = null;

        public void OpenPort(IGameInitializer initializer)
        {
            _initializer = initializer;

            _serverController = new ServerController(this);
            _serverController.OpenPort();
        }

        public void StartServer(IGameInitializer initializer)
        {
            _initializer = initializer;
            _serverListener = new Server_Listener(initializer);
            _listHandShakenPlayers = new List<HandShakenPlayer>();
            _serverStarted = true;
            _randomKey = SomewhatRandom.GetRandomKey();

            _initializer.STEAM_CONTROL.CreateSteamLobby();
        }

        public void AddHandShakenPlayer(HandShakenPlayer player)
        {
            bool duplicate = false;

            foreach (HandShakenPlayer p in _listHandShakenPlayers)
            {
                if (p.mSteamID.Value == player.mSteamID.Value)
                {
                    duplicate = true;
                    break;
                }
            }

            if (!duplicate)
            {
                SteamDebug.Log("adding handshaken player to list: " + player.mSteamID.Value);
                _listHandShakenPlayers.Add(player);

                _initializer.STEAM_CONTROL.Send_NewHandShakenPlayer(player);
            }

            Message m = new Message_HandShakenPlayersListUpdated();
            m.Register();
        }

        public HandShakenPlayer[] GetAllHandShakenPlayers()
        {
            HandShakenPlayer[] arr = new HandShakenPlayer[_listHandShakenPlayers.Count];

            for (int i = 0; i < _listHandShakenPlayers.Count; i++)
            {
                arr[i] = _listHandShakenPlayers[i];
            }

            return arr;
        }

        public void RemoveHandShakenPlayer(Steamworks.Friend friend)
        {
            SteamDebug.Log("looking for handshaken player: " + friend.Name);

            for (int i = 0; i < _listHandShakenPlayers.Count; i++)
            {
                if (_listHandShakenPlayers[i].mSteamID.Value == friend.Id.Value)
                {
                    _initializer.STEAM_CONTROL.Send_RemovedHandShakenPlayer(_listHandShakenPlayers[i]);

                    _listHandShakenPlayers.RemoveAt(i);
                    SteamDebug.Log("handshaken player removed: " + friend.Name);

                    Message m = new Message_HandShakenPlayersListUpdated();
                    m.Register();
                    break;
                }
            }
        }

        public void EndServer()
        {
            NetworkDebug.Log("ending server..");

            if (_initializer != null)
            {
                _initializer.STEAM_CONTROL.Send_OriginalHostLeft();
                _initializer.STEAM_CONTROL.LeaveLobby();
            }

            if (_listHandShakenPlayers != null)
            {
                _listHandShakenPlayers.Clear();
            }

            if (_serverController != null)
            {
                _serverController.StopTCPListner();
            }

            if (_serverListener != null)
            {
                _serverListener.REMOVE = true;
            }

            _serverStarted = false;
        }
    }
}