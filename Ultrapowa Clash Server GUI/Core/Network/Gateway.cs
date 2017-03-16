using UCS.Extensions.Binary;

namespace UCS.Core.Network
{
    #region Usings

    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using Packets;
    using Core.Settings;
    #endregion Usings

    class Gateway
    {
        public static Thread _Thread = null;
        public static ManualResetEvent Wait = new ManualResetEvent(false);
        const int
            kPort = 9339,
            kHostConnectionBacklog = 30;
        IPAddress ip;
        private Socket m_vServerSocket;
        private readonly byte[] Buffer = new byte[Constants.MaxBuffer];
        private readonly IncomingReadHandler RHandler;
        public delegate void IncomingReadHandler(Reader _Read, byte[] _Data);
        public Gateway()
        {
            _Thread = new Thread(() =>
            {
                //IPEndPoint _EndPoint    = new IPEndPoint(IPAddress.Parse(Constants.ServerAddr), Constants.ServerPort);
                IPEndPoint _EndPoint = new IPEndPoint(IPAddress.Any, Constants.ServerPort);
                Socket _Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    _Server.Bind(_EndPoint);
                    _Server.Listen(200);

                    Console.WriteLine("GameServer started on " + _EndPoint + ".\n");

                    while (true)
                    {
                        Wait.Reset();

                        _Server.BeginAccept(this.AcceptCallback, _Server);

                        Wait.WaitOne();
                    }
                }
                catch (Exception ex)
                {
                    UCS.Core.Debug.Write("Gateway cannot start because no IP can be used.");
                    UCS.Core.Debug.Write(ex.Message);
                }
            });
            _Thread.Start();
        }
        public Gateway(Socket _Socket, IncomingReadHandler _RHandler)
        {
            this.m_vServerSocket = _Socket;
            this.RHandler = _RHandler;
            this.m_vServerSocket.BeginReceive(this.Buffer, 0, Constants.MaxBuffer, 0, this.OnReceive, this);
        }
        public void Start()
        {
            if (Host(kPort))
            {
                Debug.Write(@"Gateway started on port " + kPort);
            }
        }
        public bool Host(int port)
        {
            //Console.WriteLine("Hosting on port " + port);

            m_vServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                m_vServerSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                m_vServerSocket.Listen(kHostConnectionBacklog);
                m_vServerSocket.BeginAccept(new System.AsyncCallback(OnClientConnect), m_vServerSocket);
            }
            catch (System.Exception e)
            {
                Debug.Write("Exception when attempting to host (" + port + "): " + e);

                m_vServerSocket = null;

                return false;
            }

            return true;
        }
        void OnClientConnect(IAsyncResult _Ar)
        {
            try
            {
                Wait.Set();

                Socket _Listener    = (Socket)_Ar.AsyncState;
                Socket _Handler     = _Listener.EndAccept(_Ar);

                Console.WriteLine("Hello from " + _Handler.RemoteEndPoint + ".");

                ResourcesManager.Add(_Handler);

                new Reader(_Handler, this.ProcessPacket);

                _Listener.BeginAccept(this.AcceptCallback, _Listener);
            }
            catch (Exception e)
            {
                Debug.Write(@"Exception when accepting incoming connection: " + e);
            }
            try
            {
                m_vServerSocket.BeginAccept(new AsyncCallback(OnClientConnect), m_vServerSocket);
            }
            catch (Exception e)
            {
                Debug.Write(@"Exception when starting new accept process: " + e);
            }
        }
        //void OnReceive(SocketRead read, byte[] data)
        //{

        //    try
        //    {
        //        long socketHandle = read.Socket.Handle.ToInt64();
        //        Client c = ResourcesManager.GetClient(socketHandle);
        //        c.DataStream.AddRange(data);

        //        Message p;

        //        while (c.TryGetPacket(out p))
        //        {
        //            PacketManager.ProcessIncomingPacket(p);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //Client may not exist anymore
        //        Debug.Write(e);
        //    }
        //}
        //private void OnReceive(IAsyncResult _Ar)
        //{
        //    try
        //    {
        //        SocketError _Status = 0;
        //        int _Readed = this.m_vServerSocket.EndReceive(_Ar, out _Status);

        //        if (_Status == SocketError.Success && _Readed > 0)
        //        {
        //            byte[] _Read = new byte[_Readed];
        //            Array.Copy(this.Buffer, 0, _Read, 0, _Readed);
        //            this.RHandler(this, _Read);
        //            this.m_vServerSocket.BeginReceive(this.Buffer, 0, Constants.MaxBuffer, 0, this.OnReceive, this);
        //        }
        //    }
        //    catch (Exception)
        //    {
                //Client _Client = ResourcesManager.GetClient([this.m_vServerSocket.Handle]);

                //if (_Client != null)
                //{
                //    _Client.Errors = _Client.Errors + 1;

                //    if (_Client.Level != null)
                //    {
                //        Console.WriteLine("The player " + _Client.Level.PlayerID + " [" + _Client.Level.Name + "] throwed an exception.");
                //    }
                //    else
                //    {
                //        Console.WriteLine("The player with IP " + this.m_vServerSocket.RemoteEndPoint + " throwed an exception.");
                //    }
                //}
                //else
                //{
                    //Console.WriteLine("The player with IP " + this.m_vServerSocket.RemoteEndPoint + " throwed an exception.");
                //}
        //    }
        //}
        void OnReceiveError(SocketRead read, System.Exception exception)
        {
            Debug.Write("Error received: " + exception);
        }

        void OnEndHostComplete(System.IAsyncResult result)
        {
            m_vServerSocket = null;
        }
        /// <summary>
        /// Disconnect the specified socket.
        /// </summary>
        /// <param name="_Handler">The socket.</param>
        void Disconnect()
        {
            if (m_vServerSocket != null)
            {
                m_vServerSocket.BeginDisconnect(false, new System.AsyncCallback(OnEndHostComplete), m_vServerSocket);
            }
        }
        //public static void Disconnect(Socket _Handler)
        //{
        //    try
        //    {
        //        _Handler.Shutdown(SocketShutdown.Both);
        //        _Handler.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        UCS.Core.Debug.Write(ex.Message);
        //    }
        //}

        private void AcceptCallback(IAsyncResult _Ar)
        {
            try
            {
                Wait.Set();

                Socket _Listener = (Socket)_Ar.AsyncState;
                Socket _Handler = _Listener.EndAccept(_Ar);

                Debug.Write("Hello from " + _Handler.RemoteEndPoint + ".");

                ResourcesManager.AddClient(new Client(_Handler));

                new Reader(_Handler, this.ProcessPacket);

                _Listener.BeginAccept(this.AcceptCallback, _Listener);
            }
            catch (Exception ex)
            {
                UCS.Core.Debug.Write(ex.Message);
                ////Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Process the packet.
        /// </summary>
        /// <param name="_Read">The reader.</param>
        /// <param name="_Data">The data.</param>
        private void ProcessPacket(Reader _Read, byte[] _Data)
        {
            try
            {
                Client _Client = ResourcesManager.Devices[_Read.Socket.Handle];
                _Client.DataStream.AddRange(_Data);
                _Client.Process(_Data.Length);
            }
            catch (Exception ex)
            {
                UCS.Core.Debug.Write("Exception when getting the data of a packet :\n" + ex);
                ////Console.WriteLine("Exception when getting the data of a packet :\n" + ex);
            }
        }
    }
}