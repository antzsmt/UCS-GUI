namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Logic;

    #endregion

    internal class Chat_Alliance : Message
    {
        public const ushort PacketID = 14315;

        public string Message = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Chat_Alliance" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Chat_Alliance(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Chat_Alliance.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Message = this.Reader.ReadString();
            Debug.Write("Message: " + this.Message);
        }
    }
}