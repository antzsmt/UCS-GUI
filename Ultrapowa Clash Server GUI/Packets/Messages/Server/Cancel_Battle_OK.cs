using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    #region Usings

    using Packets;

    #endregion Usings

    internal class Cancel_Battle_OK : Message
    {
        public const ushort PacketID = 24125;

        /// <summary>
        /// Initialize a new instance of the <see cref="Cancel_Battle_OK"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Cancel_Battle_OK(Device _Device) : base(_Device)
        {
            this.ID     = PacketID;
            Debug.Write("ID: " + this.ID);
        }

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.Add(1);
        }
    }
}