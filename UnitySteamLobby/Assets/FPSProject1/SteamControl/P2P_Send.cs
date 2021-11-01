using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using RB.Network;

namespace RB.SteamIntegration
{
    [System.Serializable]
    public class P2P_Send
    {
        IGameInitializer _initializer;

        [SerializeField] PacketType _latestPacketSent = PacketType.NONE;

        public P2P_Send(IGameInitializer initializer)
        {
            _initializer = initializer;
        }

        public void Send_Hello(SteamId steamID)
        {
            PacketType packetType = PacketType.HELLO;
            BytePacket p = new BytePacket((int)packetType);
            p.Write(_initializer.SERVER.RANDOM_KEY);
            p.WriteLength();

            bool sent = SteamNetworking.SendP2PPacket(steamID, p.ToArray());
            string receiverName = _initializer.STEAM_CONTROL.GetMemberName(steamID);

            if (sent)
            {
                SteamDebug.Log(packetType.ToString() + " sent to " + receiverName + ".. " + steamID.Value);
                _latestPacketSent = packetType;
            }
            else
            {

                SteamDebug.Log("failed to send " + packetType.ToString() + " to " + receiverName + ".. " + steamID.Value);
            }

            p.Dispose();
        }

        public void Send_HelloBack(SteamId steamID, uint randomKey)
        {
            PacketType packetType = PacketType.HELLO_BACK;
            BytePacket p = new BytePacket((int)packetType);
            p.Write(randomKey);
            p.Write(SteamClient.Name);
            p.WriteLength();

            bool sent = SteamNetworking.SendP2PPacket(steamID, p.ToArray());
            string receiverName = _initializer.STEAM_CONTROL.GetMemberName(steamID);

            if (sent)
            {
                SteamDebug.Log(packetType.ToString() + " sent to " + receiverName + ".. " + steamID.Value + ".. with random key " + randomKey + "..");
                _latestPacketSent = packetType;
            }
            else
            {
                SteamDebug.Log("failed to send " + packetType.ToString() + " to " + receiverName + ".. " + steamID.Value);
            }

            p.Dispose();
        }

        public void Send_NewHandShakenPlayer(HandShakenPlayer player)
        {
            PacketType packetType = PacketType.NEW_HAND_SHAKEN_PLAYER;
            BytePacket p = new BytePacket((int)packetType);
            p.Write(player.mSteamID.Value);
            p.Write(player.mSteamName);
            p.WriteLength();

            HandShakenPlayer[] arr = _initializer.SERVER.GetAllHandShakenPlayers();

            for (int i = 0; i < arr.Length; i++)
            {
                bool sent = SteamNetworking.SendP2PPacket(arr[i].mSteamID, p.ToArray());

                if (sent)
                {

                }
                else
                {
                    SteamDebug.Log("failed to send new handshakenplayer to: " + arr[i].mSteamName);
                }
            }

            p.Dispose();
        }

        public void Send_RemovedHandShakenPlayer(HandShakenPlayer player)
        {
            PacketType packetType = PacketType.REMOVED_HAND_SHAKEN_PLAYER;
            BytePacket p = new BytePacket((int)packetType);
            p.Write(player.mSteamID.Value);
            p.Write(player.mSteamName);
            p.WriteLength();

            HandShakenPlayer[] arr = _initializer.SERVER.GetAllHandShakenPlayers();

            for (int i = 0; i < arr.Length; i++)
            {
                bool sent = SteamNetworking.SendP2PPacket(arr[i].mSteamID, p.ToArray());

                if (sent)
                {

                }
                else
                {
                    SteamDebug.Log("failed to send removed handshakenplayer to: " + arr[i].mSteamName);
                }
            }

            p.Dispose();
        }

        public void Send_HandShakenPlayersList()
        {
            PacketType packetType = PacketType.HAND_SHAKEN_PLAYERS_LIST;
            BytePacket p = new BytePacket((int)packetType);

            HandShakenPlayer[] arr = _initializer.SERVER.GetAllHandShakenPlayers();

            if (arr.Length > 1)
            {
                p.Write(arr.Length);

                for (int i = 0; i < arr.Length; i++)
                {
                    p.Write(arr[i].mSteamID.Value);
                }

                p.WriteLength();
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].mSteamID.Value != _initializer.STEAM_CONTROL.STEAM_ID.Value)
                {
                    bool sent = SteamNetworking.SendP2PPacket(arr[i].mSteamID, p.ToArray());

                    if (sent)
                    {

                    }
                    else
                    {
                        SteamDebug.Log("failed to send handshakenplayers list to: " + arr[i].mSteamName);
                    }
                }
            }

            p.Dispose();
        }

        public void Send_Chat(SteamId receiverID, string str)
        {
            PacketType packetType = PacketType.CHAT;
            BytePacket p = new BytePacket((int)packetType);
            p.Write(str);
            p.WriteLength();

            bool sent = SteamNetworking.SendP2PPacket(receiverID, p.ToArray());
            string receiverName = _initializer.STEAM_CONTROL.GetMemberName(receiverID);

            if (sent)
            {
                SteamDebug.Log("successfully sent chat to " + receiverName + ".. " + receiverID.Value + ".. " + str);
            }
            else
            {
                SteamDebug.Log("failed to send chat to: " + receiverName + ".. " + receiverID.Value);
            }

            p.Dispose();
        }

        public void Send_Blank(SteamId receiverID)
        {
            PacketType packetType = PacketType.BLANK_MESSAGE;
            BytePacket p = new BytePacket((int)packetType);
            p.WriteLength();

            bool sent = SteamNetworking.SendP2PPacket(receiverID, p.ToArray());
            string receiverName = _initializer.STEAM_CONTROL.GetMemberName(receiverID);

            if (sent)
            {

            }
            else
            {
                SteamDebug.Log("failed to send blank to " + receiverName + ".. " + receiverID.Value);
            }

            p.Dispose();
        }

        public void Send_OriginalHostLeft()
        {
            PacketType packetType = PacketType.ORIGINAL_HOST_LEFT;
            BytePacket p = new BytePacket((int)packetType);
            p.WriteLength();

            HandShakenPlayer[] arr = _initializer.SERVER.GetAllHandShakenPlayers();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].mSteamID.Value != _initializer.STEAM_CONTROL.STEAM_ID.Value)
                {
                    bool sent = SteamNetworking.SendP2PPacket(arr[i].mSteamID, p.ToArray());

                    if (!sent)
                    {
                        SteamDebug.Log("failed to send originalhostleft to " + arr[i].mSteamName);
                    }
                }
            }

            p.Dispose();
        }

        public void Send_GameStartingCountDown(int countdown)
        {
            _initializer.STEAM_CONTROL.SetLobbyPublic(false);
            _initializer.STEAM_CONTROL.SetLobbyJoinable(false);

            PacketType packetType = PacketType.GAME_STARTING_COUNTDOWN;
            BytePacket p = new BytePacket((int)packetType);
            p.Write(countdown);
            p.WriteLength();

            HandShakenPlayer[] arr = _initializer.SERVER.GetAllHandShakenPlayers();

            for (int i = 0; i < arr.Length; i++)
            {
                bool sent = SteamNetworking.SendP2PPacket(arr[i].mSteamID, p.ToArray());

                if (!sent)
                {
                    SteamDebug.Log("failed to send count down to " + arr[i].mSteamName);
                }
            }

            p.Dispose();
        }

        public void Send_StartGame()
        {
            PacketType packetType = PacketType.START_GAME;
            BytePacket p = new BytePacket((int)packetType);
            p.WriteLength();

            HandShakenPlayer[] arr = _initializer.SERVER.GetAllHandShakenPlayers();

            for (int i = 0; i < arr.Length; i++)
            {
                bool sent = SteamNetworking.SendP2PPacket(arr[i].mSteamID, p.ToArray());

                if (!sent)
                {
                    SteamDebug.Log("failed to send startgame to " + arr[i].mSteamName);
                }
            }

            p.Dispose();
        }

        public void Send_SpawnPoint(SteamId receiverID, Vector3 spawnPoint)
        {
            PacketType packetType = PacketType.SPAWN_POINT;
            BytePacket p = new BytePacket((int)packetType);
            p.Write(spawnPoint);
            p.WriteLength();

            bool sent = SteamNetworking.SendP2PPacket(receiverID, p.ToArray());
            string receiverName = _initializer.STEAM_CONTROL.GetMemberName(receiverID);

            if (sent)
            {

            }
            else
            {
                SteamDebug.Log("failed to send spawn point to " + receiverName + ".. " + receiverID.Value);
            }

            p.Dispose();
        }

        public void Send_Player_Position(Vector3 position)
        {
            PacketType packetType = PacketType.PLAYER_POSITION;
            BytePacket p = new BytePacket((int)packetType);
            p.Write(position);
            p.WriteLength();

            HandShakenPlayer[] arr;
            
            if (_initializer.SERVER.SERVER_STARTED)
            {
                arr = _initializer.SERVER.GetAllHandShakenPlayers();
            }
            else
            {
                arr = _initializer.CLIENT.GetAllHandShakenPlayers();
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].mSteamID.Value != _initializer.STEAM_CONTROL.STEAM_ID.Value)
                {
                    bool sent = SteamNetworking.SendP2PPacket(arr[i].mSteamID, p.ToArray());

                    if (!sent)
                    {
                        SteamDebug.Log("failed to send player position to " + arr[i].mSteamName);
                    }
                }
            }

            p.Dispose();
        }
    }
}