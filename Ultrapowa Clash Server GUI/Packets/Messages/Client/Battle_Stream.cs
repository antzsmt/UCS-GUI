namespace UCS.Packets.Messages.Client {
    #region Usings

    using Core;

    using Extensions.Binary;

    using Logic;

    using Packets;

    #endregion Usings

    internal class Battle_Stream : Message {
        public const ushort PacketID = 14406;

        /// <summary>
        /// Initialize a new instance of the <see cref="Battle_Stream"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Battle_Stream(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header) {
            // Battle_Stream.
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {
            int[] unk = new int[2];
            unk[0] = this.Reader.ReadInt32();
            unk[1] = this.Reader.ReadInt32();
            Debug.Write("unk[0]: " + unk[0]);
            Debug.Write("unk[1]: " + unk[1]);
        }
    }
}