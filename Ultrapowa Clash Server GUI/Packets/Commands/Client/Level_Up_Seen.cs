namespace UCS.Packets.Commands.Client {
    using Extensions.Binary;
    using Extensions.List;

    using Logic;

    using Packets;

    class Level_Up_Seen : Command {
        public int Tick = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Level_Up_Seen"/> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Level_Up_Seen(Reader _Reader, Device _Client, int _ID) : base(_Reader, _Client, _ID) {
            // Level_Up_Seen.
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