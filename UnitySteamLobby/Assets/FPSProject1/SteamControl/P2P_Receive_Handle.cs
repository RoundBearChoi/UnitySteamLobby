using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.Network;
using Steamworks;

namespace RB.SteamIntegration
{
    [System.Serializable]
    public class P2P_Receive_Handle
    {
        IGameInitializer _initializer = null;

        public P2P_Receive_Handle(IGameInitializer initializer)
        {
            _initializer = initializer;
        }

        public void Handle(SteamId senderID, BytePacket bytePacket, ref PacketType latestPacketReceived)
        {
            int packetType = bytePacket.ReadInt();

            latestPacketReceived = (PacketType)packetType;

            if (latestPacketReceived == PacketType.HELLO)
            {
                uint randomKey = bytePacket.ReadUInt();

                SteamDebug.Log("received random key: " + randomKey);

                Message message = new Message_HelloReceived();
                message.steamIDs = new List<SteamId>();
                message.uInts = new List<uint>();
                message.steamIDs.Add(senderID);
                message.uInts.Add(randomKey);
                message.Register();
            }

            else if (latestPacketReceived == PacketType.HELLO_BACK)
            {
                uint randomKey = bytePacket.ReadUInt();
                string senderName = bytePacket.ReadString();

                SteamDebug.Log("received hello_back from: " + senderName);
                SteamDebug.Log("sender ID: " + senderID);
                SteamDebug.Log("received random key: " + randomKey);

                Message message = new Message_HelloBackReceived();
                message.steamIDs = new List<SteamId>();
                message.uInts = new List<uint>();
                message.strings = new List<string>();
                message.steamIDs.Add(senderID);
                message.uInts.Add(randomKey);
                message.strings.Add(senderName);
                message.Register();
            }

            else if (latestPacketReceived == PacketType.NEW_HAND_SHAKEN_PLAYER)
            {
                ulong newPlayerID = bytePacket.ReadULong();
                string name = bytePacket.ReadString();

                SteamId steamID = new SteamId();
                steamID = newPlayerID;

                Message m = new Message_NewHandShakenPlayer();
                m.steamIDs = new List<SteamId>();
                m.strings = new List<string>();
                m.steamIDs.Add(steamID);
                m.strings.Add(name);
                m.Register();
            }

            else if (latestPacketReceived == PacketType.REMOVED_HAND_SHAKEN_PLAYER)
            {
                ulong newPlayerID = bytePacket.ReadULong();
                string name = bytePacket.ReadString();

                SteamId steamID = new SteamId();
                steamID = newPlayerID;

                Message m = new Message_RemovedHandShakenPlayer();
                m.steamIDs = new List<SteamId>();
                m.strings = new List<string>();
                m.steamIDs.Add(steamID);
                m.strings.Add(name);
                m.Register();
            }

            else if (latestPacketReceived == PacketType.HAND_SHAKEN_PLAYERS_LIST)
            {
                List<SteamId> listHandShakenPlayers = new List<SteamId>();

                int length = bytePacket.ReadInt();

                for (int i = 0; i < length; i++)
                {
                    ulong id = bytePacket.ReadULong();

                    SteamId steamID = new SteamId();
                    steamID = id;

                    listHandShakenPlayers.Add(steamID);
                }

                Message message = new Message_HandShakenPlayersListReceived();
                message.steamIDs = listHandShakenPlayers;
                message.Register();
            }

            else if (latestPacketReceived == PacketType.CHAT)
            {
                string sender = _initializer.STEAM_CONTROL.GetMemberName(senderID.Value);
                SteamDebug.Log("received chat from : " + sender);

                if (senderID.Value != _initializer.STEAM_CONTROL.STEAM_ID.Value)
                {
                    string text = bytePacket.ReadString();

                    SteamDebug.Log("chat: " + text);

                    Message message = new Message_ChatReceived();
                    message.steamIDs = new List<SteamId>();
                    message.strings = new List<string>();
                    message.steamIDs.Add(senderID);
                    message.strings.Add(text);
                    message.Register();
                }
            }

            else if (latestPacketReceived == PacketType.BLANK_MESSAGE)
            {
                string sender = _initializer.STEAM_CONTROL.GetMemberName(senderID.Value);
                //SteamDebug.Log("blank received from: " + sender);
            }

            else if (latestPacketReceived == PacketType.ORIGINAL_HOST_LEFT)
            {
                SteamDebug.Log("server left.. transitioning to disconnected stage..");

                Message m = new Message_StageTransition(typeof(DisconnectedStage));
                m.Register();
            }

            else if (latestPacketReceived == PacketType.GAME_STARTING_COUNTDOWN)
            {
                int countdown = bytePacket.ReadInt();
                SteamDebug.Log("game starting in " + countdown + "..");

                Message m = new Message_GameStarting_CountDown();
                m.ints = new List<int>();
                m.ints.Add(countdown);
                m.Register();
            }

            else if (latestPacketReceived == PacketType.START_GAME)
            {
                SteamDebug.Log("start game packet received.. transitioning to test stage 1..");

                Message m = new Message_StageTransition(typeof(SteamSyncTestStage1));
                m.Register();
            }

            else if (latestPacketReceived == PacketType.SPAWN_POINT)
            {
                Vector3 position = bytePacket.ReadVector3();

                SteamDebug.Log("my spawn point received: " + position);

                Message m = new Message_SpawnPositionReceived();
                m.vec3s = new List<Vector3>();
                m.vec3s.Add(position);
                m.Register();
            }

            else if (latestPacketReceived == PacketType.PLAYER_POSITION)
            {
                Vector3 position = bytePacket.ReadVector3();

                Message m = new Message_PlayerPositionReceived();
                m.steamIDs = new List<SteamId>();
                m.vec3s = new List<Vector3>();
                m.steamIDs.Add(senderID);
                m.vec3s.Add(position);
                m.Register();
            }

            else
            {
                SteamDebug.Log("undefined packet type: " + packetType);
            }

            bytePacket.Dispose();
        }
    }
}