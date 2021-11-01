using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Server
{
    [System.Serializable]
    public class ServerController
    {
        System.Net.Sockets.TcpListener _tcpListener = null;

        string _localIP = string.Empty;
        string _publicIP = string.Empty;

        IServer _server = null;

        public ServerController(IServer server)
        {
            _server = server;
        }

        public void OpenPort()
        {
            int tempPort = 26950;

            NetworkDebug.Log("opening port: " + tempPort);

            try
            {
                _tcpListener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Any, tempPort);
                _tcpListener.Start();
                _tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
            }
            catch(System.Exception e)
            {
                NetworkDebug.Log("system error while opening port: " + e);
            }
        }

        private void TCPConnectCallback(System.IAsyncResult result)
        {
            System.Net.Sockets.TcpClient tcpClient = _tcpListener.EndAcceptTcpClient(result);
            _tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
        }

        public void StopTCPListner()
        {
            try
            {
                if (_tcpListener != null)
                {
                    NetworkDebug.Log("stopping tcp listener");
                    _tcpListener.Stop();
                }
                else
                {
                    NetworkDebug.Log("tcp listener never started");
                }
            }
            catch (System.Exception e)
            {
                NetworkDebug.Log("system error stopping tcpListener: " + e);
            }
        }
    }
}