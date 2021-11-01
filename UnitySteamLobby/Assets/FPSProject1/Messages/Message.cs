using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

namespace RB
{
    public abstract class Message
    {
        protected MessageType _messageType = MessageType.NONE;

        public MessageType MESSAGE_TYPE { get { return _messageType; } }

        public abstract void Register();

        public List<int> ints = null;
        public List<bool> bools = null;
        public List<uint> uInts = null;
        public List<Vector3> vec3s = null;
        public List<string> strings = null;
        public List<System.Net.Sockets.TcpClient> _tcpClients = null;
        public List<System.Type> types = null;
        public List<SteamId> steamIDs = null;
        public List<Friend> friends = null;
        public List<Lobby> lobbies = null;
    }
}