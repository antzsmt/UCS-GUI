namespace UCS.Core
{
    #region Usings

    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    using Logic;

    using Packets;

    #endregion

    /// <summary>
    /// The message manager.
    /// </summary>
    internal class MessageManager
    {
        /// <summary>
        /// The m_v packets.
        /// </summary>
        private static ConcurrentQueue<Message> m_vPackets;

        /// <summary>
        /// The m_v wait handle.
        /// </summary>
        private static EventWaitHandle m_vWaitHandle = new AutoResetEvent(false);

        /// <summary>
        /// The m_v is running.
        /// </summary>
        private bool m_vIsRunning;

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="MessageManager" />
        /// </para>
        /// <para>class.</para>
        /// </summary>
        public MessageManager()
        {
            m_vPackets = new ConcurrentQueue<Message>();
            this.m_vIsRunning = false;
        }

        /// <summary>
        /// The packet processing delegate.
        /// </summary>
        private delegate void PacketProcessingDelegate();

        /// <summary>
        /// The process packet.
        /// </summary>
        /// <param name="p">The p.</param>
        public static void ProcessPacket(Message p)
        {
            m_vPackets.Enqueue(p);
            m_vWaitHandle.Set();
        }

        /// <exception cref="System.Exception">
        /// A <see langword="delegate" /> callback throws an exception.
        /// </exception>
        public void Start()
        {
            try
            {
                PacketProcessingDelegate packetProcessing = new PacketProcessingDelegate(this.PacketProcessing);
                packetProcessing.BeginInvoke(null, null);

                this.m_vIsRunning = true;
            }
            catch (Exception e)
            {
                Debug.Write(Debug.FlattenException(e));
                throw;
            }

            Debug.Write("Message Manager started");
        }

        /// <summary>
        /// The packet processing.
        /// </summary>
        private void PacketProcessing()
        {
            while (this.m_vIsRunning)
            {
                m_vWaitHandle.WaitOne();

                Message p;
                while (m_vPackets.TryDequeue(out p))
                {
                    Level pl = p.Client.GetLevel();
                    string player = string.Empty;
                    if (pl != null)
                    {
	                    player += " (" + pl.GetPlayerAvatar().GetId() + ", " + pl.GetPlayerAvatar().GetAvatarName() + ")";
                    }
	                try
                    {
                        Debug.Write($"[R] {p.GetMessageType()} {p.GetType().Name}{player}");
                        p.Decode();
                        p.Process(pl);
                        Debug.Write($"finished processing of message {p.GetType().Name}{player}");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Debug.Write($"An exception occured during processing of message {p.GetType().Name}{player}{Debug.FlattenException(ex)}");
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}