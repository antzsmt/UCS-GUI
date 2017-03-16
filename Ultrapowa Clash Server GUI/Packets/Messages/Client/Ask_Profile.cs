namespace UCS.Packets.Messages.Client
{
    #region Usings

    using Core;
    using Core.Network;

    using Extensions.Binary;

    using Logic;

    using Packets;

    using Server;

    #endregion

    internal class Ask_Profile : Message
    {
        public const ushort PacketID = 14113;

        public long UserID = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Ask_Profile" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Ask_Profile(Device _Client, Reader _Reader, int[] _Header) : base(_Client, _Reader, _Header)
        {
            // Ask_Profile.
        }

        /// <summary>
        /// <see cref="Decode"/> this instance.
        /// </summary>
        public override void Decode()
        {
            this.UserID = this.Reader.ReadInt64();
            Debug.Write("UserID: " + this.UserID);
        }

        /// <summary>
        /// <see cref="Process"/> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Profile_Data(this.Client) { UserID = this.UserID }.Send();
        }
    }
}