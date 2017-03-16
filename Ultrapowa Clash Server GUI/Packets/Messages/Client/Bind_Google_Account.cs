namespace UCS.Packets.Messages.Client
{
    #region Usings

    using Core.Network;

    using Extensions.Binary;

    using Logic;

    using Packets;

    using Server;

    #endregion Usings

    internal class Bind_Google_Account : Message
    {
        public const ushort PacketID    = 14262;

        /// <summary>
        /// Initialize a new instance of the <see cref="Bind_Google_Account"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Bind_Google_Account(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header)
        {
            // Device_Information.
        }

        /// <summary>
        /// Process this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Device_Already_Bound(this.Client).Send();
        }
    }
}