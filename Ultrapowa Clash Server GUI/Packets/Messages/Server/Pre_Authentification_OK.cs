namespace UCS.Packets.Messages.Server
{
    #region Usings

    using UCS.Extensions.List;
    using UCS.Logic;
    using UCS.Logic.Enums;

    #endregion

    internal class Pre_Authentification_OK : Message
    {
        public const ushort PacketID = 20100;

        private byte[] Key;

        /// <summary>
        ///     Initialize a new instance of the
        ///     <see cref="Pre_Authentification_OK" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Pre_Authentification_OK(Device _Device)
            : base(_Device)
        {
            this.ID = PacketID;
            this.Key = new byte[24];

            this.Client.State = State.SESSION_OK;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddInt(this.Key.Length);
            this.Writer.AddRange(this.Key);
        }

        /// <summary>
        ///     <see cref="Encrypt" /> this instance.
        /// </summary>
        public override void Encrypt()
        {
            this.Data = this.Writer.ToArray();
            this.Length = this.Data.Length;
        }
    }
}