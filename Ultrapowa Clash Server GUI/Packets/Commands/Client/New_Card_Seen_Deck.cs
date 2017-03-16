namespace UCS.Packets.Commands.Client {
    using Core;

    using Extensions.Binary;
    using Extensions.List;

    using Logic;

    using Packets;

    class New_Card_Seen_Deck : Command {
        public int Tick = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="New_Card_Seen_Deck"/> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public New_Card_Seen_Deck(Reader _Reader, Device _Client, int _ID) : base(_Reader, _Client, _ID) {
            // New_Card_Seen_Deck.
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();
            Debug.Write("Tick: " + this.Tick);
        }
    }
}