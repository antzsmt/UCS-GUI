namespace UCS.Packets.Messages.Client
{
    #region Usings

    using Core.Network;

    using Extensions.Binary;
    using Extensions.List;

    using Logic;

    using Packets;

    using Server;

    #endregion

    internal class Battle_NPC : Message
    {
        public const ushort PacketID = 14104;

        /// <summary>
        /// Initialize a new instance of the <see cref="Battle_NPC" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Battle_NPC(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header)
        {
            // Battle_NPC.
        }

        /// <summary>
        /// <see cref="Decode"/> this instance.
        /// </summary>
        public override void Decode()
        {
            var test = this.Reader.ReadVInt();
            Core.Debug.Write("test: " + test);
        }

        /// <summary>
        /// <see cref="Process"/> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Sector_NPC(this.Client).Send();
        }
    }
}