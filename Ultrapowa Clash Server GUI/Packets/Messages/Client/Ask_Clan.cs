namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Core.Network;
    using UCS.Extensions.Binary;
    using UCS.Logic;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Ask_Clan : Message
    {
        public const ushort PacketID = 14302;

        public long ClanID;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Ask_Clan" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Ask_Clan(Device _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Ask_Clan.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.ClanID = this.Reader.ReadInt64();
            Debug.Write("ClanID: " + this.ClanID);
        }

        public override void Process(Level level)
        {
            new Clan_Data(this.Client) { ClanID = this.ClanID }.Send();
        }
    }
}