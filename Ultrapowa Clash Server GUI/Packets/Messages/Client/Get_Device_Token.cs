namespace UCS.Packets.Messages.Client {
    #region Usings

    using Core;

    using Extensions.Binary;

    using Logic;

    using Packets;

    #endregion Usings

    internal class Get_Device_Token : Message {
        public const ushort PacketID = 10113;

        public string Token = string.Empty;

        /// <summary>
        /// Initialize a new instance of the <see cref="Get_Device_Token"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Get_Device_Token(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header) {
            // Get_Device_Token.
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode()
        {
            this.Token = this.Reader.ReadString();

            // Client.Level.SetPass(this.Password);
            Debug.Write("Token: " + this.Token);
        }
    }
}