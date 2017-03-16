#region Usings

using UCS.Core.Network;

#endregion

namespace UCS.Packets.Messages.Client
{
    #region Usings

    using Extensions.Binary;
    using Extensions.List;

    using Logic;

    using Packets;

    using Server;

    #endregion

    internal class Social_Connection : Message
    {
        public const ushort PacketID = 10513;

        public string[] Accounts = null;

        /// <summary>
        /// Initialize a new instance of the <see cref="Social_Connection" />
        /// class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Social_Connection(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header)
        {
            // Social_Connection.
        }

        /// <summary>
        /// <see cref="Decode"/> this instance.
        /// </summary>
        public override void Decode()
        {
            int Count = this.Reader.ReadVInt();
            this.Accounts = new string[Count];

            for (int _Index = 0; _Index < Count; _Index++)
            {
                this.Accounts[_Index] = this.Reader.ReadString();
            }
        }

        /// <summary>
        /// <see cref="Process"/> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Friends_List(this.Client).Send();
        }
    }
}