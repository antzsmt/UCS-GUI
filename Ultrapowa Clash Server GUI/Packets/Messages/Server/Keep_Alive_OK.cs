namespace UCS.Packets.Messages.Server
{
    #region Usings

    using UCS.Logic;

    #endregion

    internal class Keep_Alive_OK : Message
    {
        public const ushort PacketID = 20108;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Keep_Alive_OK" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Keep_Alive_OK(Device _Device)
            : base(_Device)
        {
            this.ID = PacketID;
        }
    }
}