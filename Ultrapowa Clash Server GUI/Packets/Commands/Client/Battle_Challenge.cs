using UCS.Logic;

namespace UCS.Packets.Commands.Client {
    #region Usings

    using Extensions.Binary;
    using Extensions.List;
    using Packets;

    #endregion

    internal class Battle_Challenge : Command {
        public int Tick = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Battle_Challenge"/> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Battle_Challenge(Reader _Reader, Device _Client, int _ID) : base(_Reader, _Client, _ID) {
            // Battle_Challenge.
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