#region Usings



#endregion

namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core.Network;
    using UCS.Extensions.Binary;
    using UCS.Logic;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Check_Inbox : Message
    {
        public const ushort PacketID = 10905;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Check_Inbox" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Check_Inbox(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Check_Inbox.
        }

        /// <summary>
        ///     <see cref="Check_Inbox.Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Inbox_Data(this.Client).Send();
        }
    }
}