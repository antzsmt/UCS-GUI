using UCS.Logic;

namespace UCS.Packets.Commands.Battles {
    #region Usings

    using Core;

    using Extensions.List;

    using Packets;

    #endregion Usings

    internal class Place_Unit : Command {
        public const int CommandID = 3;

        public long Sender = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Place_Unit"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        public Place_Unit(Device _Client) : base(_Client) {
            this.ID = CommandID;
            Debug.Write("ID: " + this.ID);
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {}

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode() {
            this.Writer.AddRange("01-A8-01-7F".HexaToBytes());
            this.Writer.AddVInt(this.Sender); // 11-BB-AE-F2-06
            this.Writer.AddRange("05-83-EA-E5-18-01".HexaToBytes());
            this.Writer.AddRange("1A-03".HexaToBytes());
            this.Writer.AddRange("00-00-01-00-09-00-94-46-8C-EF-02".HexaToBytes());
        }
    }
}