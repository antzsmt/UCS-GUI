using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    #region Usings

    using Packets;

    using UCS.Core.Settings;
    using UCS.Extensions.List;

    #endregion Usings

    internal class UDP_Connection_Info : Message
    {
        public const ushort PacketID = 24112;

        /// <summary>
        /// Initialize a new instance of the <see cref="UDP_Connection_Info"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public UDP_Connection_Info(Device _Device) : base(_Device)
        {
            this.ID     = PacketID;
            Debug.Write("ID: " + this.ID);
        }

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddVInt(Constants.ServerPort);
            this.Writer.AddString(Constants.ServerAddr);
            this.Writer.AddString("Session ID"); // Session ID
            this.Writer.AddString(null); // Nonce
        }
    }
}