using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    #region Usings

    using Packets;

    using UCS.Extensions.List;

    #endregion Usings

    internal class Name_Verification : Message
    {
        public const ushort PacketID = 20300;

        /// <summary>
        /// Initialize a new instance of the <see cref="Name_Verification"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Name_Verification(Device _Device) : base(_Device)
        {
            this.ID     = PacketID;
            Debug.Write("ID: " + this.ID);
        }

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.Add(0);
            this.Writer.AddInt(0);
            this.Writer.AddString(string.Empty);
        }
    }
}