namespace UCS.Core.Network.Ancient
{
    #region Usings

    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using Core.Settings;

    using Logic;

    using Extensions.Binary;

    #endregion

	/// <summary>
	/// The gateway.
	/// </summary>
	internal class Gateway
    {
	    /// <summary>
	    /// The wait.
	    /// </summary>
	    private static readonly ManualResetEvent Wait = new ManualResetEvent(false);

	    /// <summary>
	    /// The gateway.
	    /// </summary>
	    /// <exception cref="ThreadStateException">
	    /// Ya se ha iniciado el subproceso. 
	    /// </exception>
	    public Gateway()
        {
	        try
	        {  
	        var thread = new Thread(() =>
		        {
			        // IPEndPoint _EndPoint    = new IPEndPoint(IPAddress.Parse(Constants.ServerAddr), Constants.ServerPort);
			        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, Constants.ServerPort);
			        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			        try
			        {
				        server.Bind(endPoint);
				        server.Listen(200);

				        Console.WriteLine(@"GameServer started on " + endPoint + @".");

				        while (true)
				        {
					        Wait.Reset();

					        server.BeginAccept(this.AcceptCallback, server);

					        Wait.WaitOne();
				        }
			        }
			        catch (Exception ex)
			        {
				        Debug.Write("Gateway cannot start because no IP can be used.");
				        Debug.Write(ex.Message);
			        }
		        });
	        thread.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

        /// <summary>
        /// <see cref="Gateway.Disconnect" /> the specified socket.
        /// </summary>
        /// <param name="_Handler">The socket.</param>
        public static void Disconnect(Socket _Handler)
        {
            try
            {
                _Handler.Shutdown(SocketShutdown.Both);
                _Handler.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        private void AcceptCallback(IAsyncResult _Ar)
        {
            try
            {
                Wait.Set();

                Socket _Listener = (Socket)_Ar.AsyncState;
                Socket _Handler = _Listener.EndAccept(_Ar);

                Debug.Write("Hello from " + _Handler.RemoteEndPoint + ".");

                ResourcesManager.Add(_Handler);

                new Reader(_Handler, this.ProcessPacket);

                _Listener.BeginAccept(this.AcceptCallback, _Listener);
            }
            catch (Exception ex)
            {
                Debug.Write(Debug.FlattenException(ex));
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
                //Device _Device = ResourcesManager.GetClient(_Read.Socket.Handle.ToInt64());
                Device _Device = ResourcesManager.Devices[_Read.Socket.Handle];
                _Device.Stream.AddRange(_Data);
                _Device.Process(_Data.Length);
            }
            catch (Exception ex)
            {
                Debug.Write("Exception when getting the data of a packet :\n" + Debug.FlattenException(ex));
            }
        }
    }
}