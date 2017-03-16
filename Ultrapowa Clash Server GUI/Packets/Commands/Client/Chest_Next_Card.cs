using UCS.Logic;

namespace UCS.Packets.Commands.Client {
    using Packets;

    using UCS.Extensions.Binary;
    using UCS.Extensions.List;

    class Chest_Next_Card : Command {
        public int Tick = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Chest_Next_Card"/> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Chest_Next_Card(Reader _Reader, Device _Client, int _ID) : base(_Reader, _Client, _ID) {
            // Chest_Next_Card.
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