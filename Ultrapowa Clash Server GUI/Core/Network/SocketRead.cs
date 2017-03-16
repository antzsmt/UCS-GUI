namespace UCS.Network
{
    #region Usings

    using System.Collections;
    //using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System;

    #endregion

    public class SocketRead
    {
        public const int kBufferSize = 256;

        private byte[] buffer = new byte[SocketRead.kBufferSize];

        private IncomingReadErrorHandler errorHandler;

        private IncomingReadHandler readHandler;

        private Socket socket;

        private SocketRead(Socket socket, IncomingReadHandler readHandler, IncomingReadErrorHandler errorHandler = null)
        {
            this.socket = socket;
            this.readHandler = readHandler;
            this.errorHandler = errorHandler;

	        this.BeginReceive();
        }

        public delegate void IncomingReadErrorHandler(SocketRead read, Exception exception);

        public delegate void IncomingReadHandler(SocketRead read, byte[] data);

        public Socket Socket => this.socket;

	    public static SocketRead Begin(Socket socket,
                                       IncomingReadHandler readHandler,
                                       IncomingReadErrorHandler errorHandler = null)
        {
            return new SocketRead(socket, readHandler, errorHandler);
        }

        private void BeginReceive()
        {
	        this.socket.BeginReceive(this.buffer, 0, SocketRead.kBufferSize, SocketFlags.None, new AsyncCallback(this.OnReceive), this);
        }

        private void OnReceive(IAsyncResult result)
        {
            try
            {
                if (result.IsCompleted)
                {
                    int bytesRead = this.socket.EndReceive(result);

                    if (bytesRead > 0)
                    {
                        byte[] read = new byte[bytesRead];
                        Array.Copy(this.buffer, 0, read, 0, bytesRead);

	                    this.readHandler(this, read);
                        SocketRead.Begin(this.socket, this.readHandler, this.errorHandler);
                    }
                    else
                    {
                        // Disconnect
                    }
                }
            }
            catch (Exception e)
            {
                if (this.errorHandler != null)
                {
	                this.errorHandler(this, e);
                }
            }
        }
    }
}