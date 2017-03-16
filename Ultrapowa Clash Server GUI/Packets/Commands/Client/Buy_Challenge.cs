using UCS.Logic;

namespace UCS.Packets.Commands.Client {
    #region Usings

    using Packets;

    using UCS.Extensions.Binary;
    using UCS.Extensions.List;

    #endregion

    internal class Buy_Challenge : Command {
        public int Tick = 0;
        public int Type = 0;
        public int Mode = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Buy_Challenge"/> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Buy_Challenge(Reader _Reader, Device _Client, int _ID) : base(_Reader, _Client, _ID) {
            // Buy_Challenge.
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();

            this.Type = this.Reader.ReadVInt();
            this.Mode = this.Reader.ReadVInt();
        }
    }
}