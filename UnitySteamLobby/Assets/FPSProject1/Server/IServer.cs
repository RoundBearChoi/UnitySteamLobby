using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Server
{
    public interface IServer
    {
        public bool SERVER_STARTED { get; }
        public uint RANDOM_KEY { get; }
        public void OpenPort(IGameInitializer initializer);
        public void StartServer(IGameInitializer initializer);
        public void AddHandShakenPlayer(Network.HandShakenPlayer player);
        public Network.HandShakenPlayer[] GetAllHandShakenPlayers();
        public void RemoveHandShakenPlayer(Steamworks.Friend friend);
        public void EndServer();
    }
}