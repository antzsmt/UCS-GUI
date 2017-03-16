namespace UCS.Packets.Messages.Client {
    #region Usings

    using Extensions.Binary;

    using Logic;

    using Packets;

    #endregion Usings

    internal class Avatar_Stream : Message {
        public const ushort PacketID = 14405;

        /// <summary>
        /// Initialize a new instance of the <see cref="Avatar_Stream"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Avatar_Stream(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header) {
            // Avatar_Stream.
        }
    }
}