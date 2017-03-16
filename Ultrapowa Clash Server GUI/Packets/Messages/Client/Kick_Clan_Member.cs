namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Logic;

    #endregion

    internal class Kick_Clan_Member : Message
    {
        public const ushort PacketID = 14307;

        public long UserID;

        public string Reason = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Kick_Clan_Member" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Kick_Clan_Member(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Kick_Clan_Member.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.UserID = this.Reader.ReadInt64();
            this.Reason = this.Reader.ReadString();
            Debug.Write("UserID: " + this.UserID);
            Debug.Write("Reason: " + this.Reason);
        }
    }
}