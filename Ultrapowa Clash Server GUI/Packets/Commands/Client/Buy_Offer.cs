using UCS.Logic;

namespace UCS.Packets.Commands.Client {
    using Packets;

    using UCS.Extensions.Binary;
    using UCS.Extensions.List;

    class Buy_Offer : Command {
        public int Tick = 0;
        public int Type = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Buy_Offer"/> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Buy_Offer(Reader _Reader, Device _Client, int _ID) : base(_Reader, _Client, _ID) {
            // Buy_Offer.
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();
        }
    }
}