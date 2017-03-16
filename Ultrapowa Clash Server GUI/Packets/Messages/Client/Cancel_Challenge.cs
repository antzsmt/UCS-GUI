namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core.Network;
    using UCS.Extensions.Binary;
    using UCS.Logic;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Cancel_Challenge : Message
    {
        public const ushort PacketID = 14111;

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="Cancel_Challenge" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Cancel_Challenge(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Cancel_Challenge.
        }

        /// <summary>
        ///     <see cref="Cancel_Challenge.Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Cancel_Battle_OK(this.Client).Send();
        }
    }
}