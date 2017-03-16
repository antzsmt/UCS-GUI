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

    internal class Keep_Alive : Message
    {
        public const ushort PacketID = 10108;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Keep_Alive" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Keep_Alive(Device _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Keep_Alive.
        }

        /// <summary>
        ///     <see cref="Keep_Alive.Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Keep_Alive_OK(this.Client).Send();
        }
    }
}