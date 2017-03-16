namespace UCS.Packets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using Core;
    using Extensions;
    using Extensions.Binary;
    using Logic.Enums;
    using Logic;

    internal class Client
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="_Socket">The socket.</param>
        public Client(Socket _Socket)
        {
            Connection = _Socket;
            m_vSocketHandle = _Socket.Handle.ToInt64();
            State = State.DISCONNECTED;
            DataStream = new List<byte>();
            PublicKey = new byte[32];
            RNonce = new byte[24];
            SNonce = new byte[24];
        }

        public Socket Connection { get; set; }

        public uint ClientSeed { get; set; }

        private long m_vSocketHandle;

        private Level m_vLevel;

        public byte[] PublicKey { get; set; }

        public byte[] SharedKey { get; set; }

        //public ClientAvatar Level { get; set; }

        public byte[] RNonce { get; set; }

        public byte[] SNonce { get; set; }

        public State State { get; set; }

        public List<Byte> DataStream { get; set; }

        public int Errors { get; set; }

        public int Ping { get; set; }

        public string Interface { get; set; }

        public Level GetLevel()
        {
            return m_vLevel;
        }

        public long GetSocketHandle()
        {
            return m_vSocketHandle;
        }

        public void SetLevel(Level l)
        {
            m_vLevel = l;
        }

        public void Process(int _Range)
        {
            
            int[] _Header = new int[3];
            
            if (_Range >= 7)
            {
                using (Reader _Reader = new Reader(DataStream.ToArray()))
                {
                    _Header[0] = _Reader.ReadUInt16(); // ID

                    _Reader.BaseStream.Seek(1, SeekOrigin.Current);

                    _Header[1] = _Reader.ReadUInt16(); // Length
                    _Header[2] = _Reader.ReadUInt16(); // Version

                    if (DataStream.Count - 7 >= _Header[1])
                    {
                        if (MessageFactory.m_vMessages.ContainsKey(_Header[0]))
                        {
                            Message _Message = Activator.CreateInstance(MessageFactory.m_vMessages[_Header[0]], this, _Reader, _Header) as Message;
                            Level pl = _Message.Client.GetLevel();
                            if (_Message != null)
                            {
                                if (Core.Settings.Settings.Debug)
                                {
                                    Console.WriteLine(ConsolePad.Padding(((IPEndPoint)Connection.RemoteEndPoint).Address.ToString()) + " -> " + ConsolePad.Padding(_Message.GetType().Name) + " -> SERVER                  -> " + Interface);
                                }

                                try
                                {
                                    _Message.Decrypt();
                                    _Message.Decode();
                                    _Message.Process();
                                }
                                catch (Exception _Error)
                                {
                                    Console.WriteLine("The player " + (string.IsNullOrEmpty(pl.GetPlayerAvatar().GetAvatarName()) ? "with ID " + pl.GetPlayerAvatar().GetId() : "with name '" + pl.GetPlayerAvatar().GetAvatarName()) + " throwed an exception.");

                                    if (SNonce != null)
                                    {
                                        if (State >= State.LOGGED)
                                        {
                                            // this.SNonce.Increment();
                                        }
                                        else
                                        {
                                            // Login Failed.
                                        }
                                    }
                                    else
                                    {
                                        if (Errors > 5)
                                        {
                                            ResourcesManager.DropClient(this.m_vSocketHandle);
                                        }
                                        else
                                        {
                                            Errors = Errors + 1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            SNonce.Increment();
                            Console.WriteLine("The message " + _Header[0] + " can't be processed.");
                        }

                        DataStream.RemoveRange(0, _Header[1] + 7);
                    }
                }
            }
        }
    }
}