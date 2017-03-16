using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    #region Usings

    using Packets;

    using UCS.Extensions.List;

    #endregion Usings

    internal class Matchmaking_Info : Message
    {
        public const ushort PacketID = 24107;

        /// <summary>
        /// Initialize a new instance of the <see cref="Matchmaking_Info"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Matchmaking_Info(Device _Device) : base(_Device)
        {
            this.ID     = PacketID;
            Debug.Write("ID: " + this.ID);
        }

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddInt(200);
        }
    }
}