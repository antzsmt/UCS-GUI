using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    #region Usings

    using Packets;

    #endregion Usings

    internal class Royale_TV_Data : Message
    {
        public const ushort PacketID = 20000;

        public int Arena = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Royale_TV_Data"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Royale_TV_Data(Device _Device) : base(_Device)
        {
            this.ID = PacketID;
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