namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Logic;

    #endregion

    internal class Top_Global_Players : Message
    {
        public const ushort PacketID = 14403;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Top_Global_Players" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Top_Global_Players(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Top_Global_Players.
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            // new Top_Global_Players_Data(this.Client).Send();
            Debug.Write("new Top_Global_Players_Data");
        }
    }
}