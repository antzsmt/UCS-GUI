using UCS.Core;

namespace UCS.Packets.Messages.Client {
    #region Usings

    using Extensions.Binary;
    using Extensions.List;

    using Logic;

    using Packets;

    #endregion Usings

    internal class Create_Clan : Message {
        public const ushort PacketID = 14301;

        public string Name = string.Empty;
        public string Description = string.Empty;

        public int Badge = 0;
        public int Origin = 0;
        public int Required_Score = 0;
        public int Type = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Create_Clan"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Create_Clan(Device _Client, Reader _Reader, int[] _Header) : base(_Client, _Reader, _Header) {
            // Create_Clan.
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {
            this.Name = this.Reader.ReadString();
            Debug.Write("Name: " + this.Name);
            this.Description = this.Reader.ReadString();
            Debug.Write("Description: " + this.Description);
            this.Badge = this.Reader.ReadVInt();
            Debug.Write("Badge: " + this.Badge);
            this.Badge = this.Reader.ReadVInt();
            Debug.Write("Badge: " + this.Badge);
            this.Type = this.Reader.Read();
            Debug.Write("Type: " + this.Type);
            this.Required_Score = this.Reader.ReadVInt();
            Debug.Write("Required_Score: " + this.Required_Score);
            this.Origin = this.Reader.ReadVInt();
            Debug.Write("Origin: " + this.Origin);
            this.Origin = this.Reader.ReadVInt();
            Debug.Write("Origin: " + this.Origin);
        }
    }
}