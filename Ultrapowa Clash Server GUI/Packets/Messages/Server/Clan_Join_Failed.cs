using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    #region Usings

    using Packets;

    using UCS.Extensions.List;

    #endregion Usings

    internal class Clan_Join_Failed : Message
    {
        public const ushort PacketID    = 24302;

        public int Reason               = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Clan_Join_Failed"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Clan_Join_Failed(Device _Device) : base(_Device)
        {
            this.ID     = PacketID;
            Debug.Write("ID: " + this.ID);
        }

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddInt(this.Reason);
        }
    }
}