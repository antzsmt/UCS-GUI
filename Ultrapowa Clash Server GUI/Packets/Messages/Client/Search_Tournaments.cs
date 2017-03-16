using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.Messages.Client {
    #region Usings

    using Extensions.Binary;
    using Extensions.List;
    using Packets;

    #endregion Usings

    internal class Search_Tournaments : Message {
        public const ushort PacketID = 16113;

        public string Name = string.Empty;

        /// <summary>
        /// Initialize a new instance of the <see cref="Search_Tournaments"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Search_Tournaments(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header) {
            // Search_Tournaments.
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {
            this.Name = this.Reader.ReadString();

            var test = this.Reader.ReadVInt();
            var test2 = this.Reader.ReadVInt();
            var test3 = this.Reader.ReadInt32();
            Debug.Write("Name: " + this.Name);
            Debug.Write("UserID: " + test);
            Debug.Write("UserID: " + test2);
            Debug.Write("UserID: " + test3);
        }
    }
}