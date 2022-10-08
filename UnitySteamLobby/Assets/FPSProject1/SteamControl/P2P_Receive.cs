using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.Network;
using Steamworks;
using Steamworks.Data;

namespace RB.SteamIntegration
{
    [System.Serializable]
    public class P2P_Receive
    {
        [SerializeField] PacketType _latestPacketReceived = PacketType.NONE;
        [SerializeField] P2P_Receive_Handle _p2pReceiveHandle;

        IGameInitializer _initializer = null;

        public P2P_Receive(IGameInitializer initializer)
        {
            _initializer = initializer;
            _p2pReceiveHandle = new P2P_Receive_Handle(initializer);
        }

        public void OnFixedUpdate()
        {
            if (SteamClient.IsValid)
            {
                while (SteamNetworking.IsP2PPacketAvailable())
                {
                    //SteamDebug.Log("found available packet.. attempting to read..");

                    var p2packet = SteamNetworking.ReadP2PPacket();

                    if (p2packet.HasValue)
                    {
                        //SteamDebug.Log("packet has value");
                        OnReceive(p2packet.Value);
                    }
                    else
                    {
                        SteamDebug.Log("packet has no value");
                    }
                }
            }
        }

        void OnReceive(P2Packet p2packet)
        {
            try
            {
                /*BytePacket p =*/ ReadData(p2packet);
                //p.Dispose();
            }
            catch(System.Exception e)
            {
                SteamDebug.Log("system error while reading packet: " + e);
            }
        }

        void ReadData(P2Packet p2packet)
        {
            int packetLength = 0;

            BytePacket bytePacket = new BytePacket();
            bytePacket.SetBytes(p2packet.Data);

            if (bytePacket.UnreadLength() >= 4)
            {
                // If client's received data contains a packet
                packetLength = bytePacket.ReadInt();

                if (packetLength <= 0)
                {
                    SteamDebug.Log("packet length is 0");

                    bytePacket.Dispose();
                    return;
                    //return bytePacket;
                }
            }

            while (packetLength > 0 && packetLength <= bytePacket.UnreadLength())
            {
                // While packet contains data AND packet data length doesn't exceed the length of the packet we're reading
                byte[] bytes = bytePacket.ReadBytes(packetLength);

                using (BytePacket p = new BytePacket(bytes))
                {
                    _p2pReceiveHandle.Handle(p2packet.SteamId, p, ref _latestPacketReceived);
                    p.Dispose();
                }

                packetLength = 0;

                if (bytePacket.UnreadLength() >= 4)
                {
                    packetLength = bytePacket.ReadInt();

                    if (packetLength <= 0)
                    {
                        bytePacket.Dispose();
                        return;
                        //return bytePacket;
                    }
                }
            }

            if (packetLength <= 1)
            {
                bytePacket.Dispose();
                return;
                //return bytePacket;
            }
        }
    }
}