namespace UCS.Packets.Messages.Server
{
    #region Usings

    using UCS.Logic;

    #endregion

    internal class Top_Global_Players_Data : Message
    {
        public const ushort PacketID = 24403;

        /// <summary>
        ///     Initialize a new instance of the
        ///     <see cref="Top_Global_Players_Data" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Top_Global_Players_Data(Device _Device)
            : base(_Device)
        {
            this.ID = PacketID;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            /* this.Writer.AddString("Berkan");    // Name
            this.Writer.Add(1);                 // Unknown
            this.Writer.AddVInt(10000);         // Score
            this.Writer.Add(23);                // Unknown
            this.Writer.AddInt(1);              // Level
            this.Writer.AddInt(0);              // Unknown
            this.Writer.AddInt(0);              // Unknown
            this.Writer.AddInt(0);              // Unknown
            this.Writer.AddInt(0);              // Unknown
            this.Writer.AddInt(0);              // Unknown
            this.Writer.AddString("FR");        // Region */
        }
    }
}