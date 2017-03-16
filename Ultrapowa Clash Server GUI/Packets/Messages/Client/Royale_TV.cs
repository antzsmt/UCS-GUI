#region Usings

using UCS.Core;
using UCS.Core.Network;

#endregion

namespace UCS.Packets.Messages.Client
{
    #region Usings

    using Extensions.Binary;
    using Extensions.List;

    using Logic;

    using Packets;

    using Server;

    #endregion

    internal class Royale_TV : Message
    {
        public const ushort PacketID = 14402;

        public int Arena = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Check_Inbox" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Royale_TV(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header)
        {
            // Royale_TV.
        }

        /// <summary>
        /// <see cref="Decode"/> this instance.
        /// </summary>
        public override void Decode()
        {
            var test = this.Reader.ReadVInt();
            this.Arena = this.Reader.ReadVInt();
            Debug.Write("test: " + test);
            Debug.Write("arena: " + this.Arena);
        }

        /// <summary>
        /// <see cref="Process"/> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Royale_TV_Data(this.Client) { Arena = this.Arena }.Send();
        }
    }
}