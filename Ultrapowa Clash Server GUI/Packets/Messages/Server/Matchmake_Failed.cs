namespace UCS.Packets.Messages.Server
{
    #region Usings

    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Matchmake_Failed : Message
    {
        public const ushort PacketID = 24108;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Matchmake_Failed" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Matchmake_Failed(Device _Device)
            : base(_Device)
        {
            this.ID = PacketID;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddInt(0);
        }
    }
}