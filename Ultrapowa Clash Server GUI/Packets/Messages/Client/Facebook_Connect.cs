namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Logic;

    #endregion

    internal class Facebook_Connect : Message
    {
        public const ushort PacketID = 14201;

        public bool _Compressed;

        public string _Identifier = string.Empty;

        public string _Token = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Facebook_Connect" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Facebook_Connect(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Facebook_Connect.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this._Compressed = this.Reader.ReadBoolean();
            this._Identifier = this.Reader.ReadString();
            this._Token = this.Reader.ReadString();
            Debug.Write("_Compressed: " + this._Compressed);
            Debug.Write("_Identifier: " + this._Identifier);
            Debug.Write("_Token: " + this._Token);
        }
    }
}