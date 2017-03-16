namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Extensions.Binary;
    using UCS.Logic;

    #endregion

    internal class Joinable_Tournaments : Message
    {
        public const ushort PacketID = 16103;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Joinable_Tournaments" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Joinable_Tournaments(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Joinable_Tournaments.
        }
    }
}