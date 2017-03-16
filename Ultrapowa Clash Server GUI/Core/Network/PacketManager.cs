namespace UCS.Network
{
    #region Usings

    using System;
    using System.Threading;
    using System.Net.Sockets;
    using System.Collections.Concurrent;

    using Packets;

    using Core;

    using Logic;

    #endregion

    internal class PacketManager
    {
        private static ConcurrentQueue<Message> m_vIncomingPackets;

        private static EventWaitHandle m_vIncomingWaitHandle = new AutoResetEvent(false);

        private static ConcurrentQueue<Message> m_vOutgoingPackets;

        private static EventWaitHandle m_vOutgoingWaitHandle = new AutoResetEvent(false);

        private bool m_vIsRunning;

        public PacketManager()
        {
            m_vIncomingPackets = new ConcurrentQueue<Message>();
            m_vOutgoingPackets = new ConcurrentQueue<Message>();

            m_vIsRunning = false;
        }

        private delegate void IncomingProcessingDelegate();

        private delegate void OutgoingProcessingDelegate();

        public static void ProcessIncomingPacket(Message p)
        {
            m_vIncomingPackets.Enqueue(p);
            m_vIncomingWaitHandle.Set();
        }

        public static void ProcessOutgoingPacket(Message p)
        {
            p.Encode();
            try
            {
                Level pl = p.Client.GetLevel();
                string player = "";
                if (pl != null) player += " (" + pl.GetPlayerAvatar().GetId() + ", " + pl.GetPlayerAvatar().GetAvatarName() + ")";
                Debugger.WriteLine("[S] " + p.GetMessageType() + " " + p.GetType().Name + player);
                m_vOutgoingPackets.Enqueue(p);
                m_vOutgoingWaitHandle.Set();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Start()
        {
            IncomingProcessingDelegate incomingProcessing = new IncomingProcessingDelegate(IncomingProcessing);
            incomingProcessing.BeginInvoke(null, null);

            OutgoingProcessingDelegate outgoingProcessing = new OutgoingProcessingDelegate(OutgoingProcessing);
            outgoingProcessing.BeginInvoke(null, null);

            m_vIsRunning = true;

            Console.WriteLine("Packet Manager started");
        }

        private void IncomingProcessing()
        {
            while (this.m_vIsRunning)
            {
                m_vIncomingWaitHandle.WaitOne();
                Message p;
                while (m_vIncomingPackets.TryDequeue(out p))
                {
                    p.GetData();
                    Debug.Write("R " + p.GetMessageType().ToString() + " (" + p.GetLength().ToString() + ")");
                    Logger.WriteLine(p, "R");
                    MessageManager.ProcessPacket(p);
                }
            }
        }

        private void OutgoingProcessing()
        {
            while (this.m_vIsRunning)
            {
                m_vOutgoingWaitHandle.WaitOne();
                Message p;
                while (m_vOutgoingPackets.TryDequeue(out p))
                {
                    Logger.WriteLine(p, "S");
                    if (p.GetMessageType() == 20000)
                    {
                        byte[] sessionKey = ((SessionKeyMessage)p).Key;
                        p.Client.Encrypt(p.GetData());
                        p.Client.UpdateKey(sessionKey);
                    }
                    else
                    {
                        p.Client.Encrypt(p.GetData());
                    }

                    try
                    {
                        if (p.Client.Connection != null)
                        {
                            p.Client.Connection.Send(p.GetRawData());
                        }
                        else
                        {
                            ResourcesManager.DropClient(p.Client.GetSocketHandle());
                        }
                    }
                    catch (Exception)
                    {
                        //example: client connection closed
                        try
                        {
                            ResourcesManager.DropClient(p.Client.GetSocketHandle());
                            p.Client.Connection.Shutdown(SocketShutdown.Both);
                            p.Client.Connection.Close();
                        }
                        catch (Exception)
                        {
                            //Console.WriteLine(exs.ToString());
                        }
                    }
                }
            }
        }
    }
}