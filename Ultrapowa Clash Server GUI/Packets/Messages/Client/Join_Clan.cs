namespace UCS.Packets.Messages.Client {
    #region Usings

    using Core;

    using Extensions.Binary;

    using Logic;

    using Packets;

    #endregion Usings

    internal class Join_Clan : Message {
        public const ushort PacketID = 14305;

        /// <summary>
        /// Initialize a new instance of the <see cref="Join_Clan"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Join_Clan(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header) {
            // Get_Device_Token.
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {
            var test = this.Reader.ReadInt64();
            var test2 = this.Reader.ReadInt32();
            Debug.Write("test: " + test);
            Debug.Write("test2: " + test2);
        }
    }
}