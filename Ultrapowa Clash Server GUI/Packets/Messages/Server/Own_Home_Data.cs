namespace UCS.Packets.Messages.Server
{
    #region Usings

    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Own_Home_Data : Message
    {
        public const ushort PacketID = 24101;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Own_Home_Data" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Own_Home_Data(Device _Device)
            : base(_Device)
        {
            this.ID = PacketID;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddRange(this.Client.GetLevel().GetPlayerAvatar().Data_Part1());
            this.Writer.AddRange(this.Client.GetLevel().GetPlayerAvatar().Data_Part2());
            this.Writer.Add(0);
            this.Writer.AddVInt(0);
        }
    }
}