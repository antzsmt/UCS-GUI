namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Extensions.Binary;
    using UCS.Logic;

    #endregion

    internal class Link_Device : Message
    {
        public const ushort PacketID = 16002;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Link_Device" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Link_Device(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Link_Device.
        }
    }
}