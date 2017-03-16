namespace UCS.Packets
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UCS.Extensions;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Library.Sodium;
    using UCS.Logic;
    using UCS.Logic.Enums;

    #endregion

    internal class Message : IDisposable
    {
        private byte[] m_vData;

        private int m_vLength;

        private ushort m_vMessageVersion;

        private ushort m_vType;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Message" /> class.
        /// </summary>
        public Message()
        {
            this.Client = null;

            this.ID = 0;
            this.Length = 0;
            this.Version = 0;

            this.Data = null;
            this.Reader = null;
            this.Writer = null;
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="Message" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Message(Device _Device)
        {
            this.Client = _Device;

            this.ID = 0;
            this.Length = 0;
            this.Version = 0;

            this.Data = null;
            this.Reader = null;
            this.Writer = new List<byte>();

            this.Direction = Direction.CLIENT;
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="Message" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Message(Device _Device, Reader _Reader, int[] _Header)
        {
            this.Client = _Device;

            this.ID = (ushort)_Header[0];
            this.Length = (ushort)_Header[1];
            this.Version = (ushort)_Header[2];

            this.Data = _Reader.ReadBytes(_Header[1]);
            this.Reader = new Reader(this.Data);
            this.Writer = null;

            this.Direction = Direction.SERVER;
        }

        /// <summary>
        ///     Get or set the client data.
        /// </summary>
        /// <value>
        ///     The client.
        /// </value>
        public Device Client { get; set; }

        /// <summary>
        ///     Get or set the message payload.
        /// </summary>
        /// <value>
        ///     The data.
        /// </value>
        public byte[] Data { get; set; }

        /// <summary>
        ///     Get or set the packet direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        public Direction Direction { get; set; }

        /// <summary>
        ///     Get or set the message identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public ushort ID { get; set; }

        /// <summary>
        ///     Get or set the payload length.
        /// </summary>
        /// <value>
        ///     The length.
        /// </value>
        public int Length { get; set; }

        /// <summary>
        ///     Get or set the reader.
        /// </summary>
        /// <value>
        ///     The reader.
        /// </value>
        public Reader Reader { get; set; }

        /// <summary>
        ///     Get or set the message version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        public ushort Version { get; set; }

        /// <summary>
        ///     Get or set the writer.
        /// </summary>
        /// <value>
        ///     The writer.
        /// </value>
        public List<byte> Writer { get; set; }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public virtual void Decode()
        {
            // Decode.
        }

        /// <summary>
        ///     <see cref="Decrypt" /> this instance.
        /// </summary>
        public virtual void Decrypt()
        {
            if (this.Client.State >= State.LOGGED)
            {
                this.Client.SNonce.Increment();

                this.Data = Sodium.Decrypt(
                    new byte[16].Concat(this.Data).ToArray(),
                    this.Client.SNonce,
                    this.Client.PublicKey);
                this.Reader = new Reader(this.Data);
                this.Length = this.Data.Length;
            }
        }

        public void Dispose()
        {
            if (this.Reader != null)
            {
                this.Reader.Dispose();
                this.Reader = null;
            }
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public virtual void Encode()
        {
            // Encode.
        }

        /// <summary>
        ///     <see cref="Encrypt" /> this instance.
        /// </summary>
        public virtual void Encrypt()
        {
            if (this.Client.State >= State.LOGGED)
            {
                this.Client.RNonce.Increment();

                this.Data =
                    Sodium.Encrypt(this.Writer.ToArray(), this.Client.RNonce, this.Client.PublicKey).Skip(16).ToArray();
                this.Length = this.Data.Length;
            }
            else
            {
                this.Data = this.Writer.ToArray();
                this.Length = this.Data.Length;
            }
        }

        public byte[] GetData() => this.m_vData;

        public int GetLength()
        {
            return this.m_vLength;
        }

        public ushort GetMessageType()
        {
            return this.m_vType;
        }

        public ushort GetMessageVersion()
        {
            return this.m_vMessageVersion;
        }

        /// <summary>
        ///     Get the packet, in bytes array.
        /// </summary>
        /// <returns>
        ///     The packet, in bytes array, header included.
        /// </returns>
        public byte[] GetPacket()
        {
            List<byte> _Packet = new List<byte>();

            _Packet.AddUShort(this.ID);
            _Packet.Add(0);
            _Packet.AddUShort((ushort)this.Length);
            _Packet.AddUShort(this.Version);

            _Packet.AddRange(this.Data.ToArray());

            return _Packet.ToArray();
        }

        public byte[] GetRawData()
        {
            List<byte> encodedMessage = new List<byte>();

            encodedMessage.AddRange(BitConverter.GetBytes(this.m_vType).Reverse());
            encodedMessage.AddRange(BitConverter.GetBytes(this.m_vLength).Reverse().Skip(1));
            encodedMessage.AddRange(BitConverter.GetBytes(this.m_vMessageVersion).Reverse());
            encodedMessage.AddRange(this.m_vData);

            return encodedMessage.ToArray();
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public virtual void Process(Level level)
        {
        }

        public void SetMessageType(ushort type)
        {
            this.m_vType = type;
        }

        public string ToHexString()
        {
            string hex = BitConverter.ToString(this.m_vData);
            return hex.Replace("-", " ");
        }
    }
}