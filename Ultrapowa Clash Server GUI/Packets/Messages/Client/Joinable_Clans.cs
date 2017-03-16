namespace UCS.Packets.Messages.Client
{
    #region Usings

    using Extensions.Binary;

    using Logic;

    using Packets;

    #endregion Usings

    internal class Joinable_Clans : Message
    {
        public const ushort PacketID    = 14303;

        /// <summary>
        /// Initialize a new instance of the <see cref="Joinable_Clans"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Joinable_Clans(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header)
        {
            // Joinable_Clans.
        }
    }
}