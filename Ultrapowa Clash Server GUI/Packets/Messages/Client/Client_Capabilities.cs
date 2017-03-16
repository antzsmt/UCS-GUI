namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Client_Capabilities : Message
    {
        public const ushort PacketID = 10107;

        public Client_Capabilities(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Client_Capabilities.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Client.Ping = this.Reader.ReadVInt();
            this.Client.Interface = this.Reader.ReadString();
            Debug.Write("Client.Ping: " + this.Client.Ping);
            Debug.Write("Client.Interface: " + this.Client.Interface);
        }
    }
}