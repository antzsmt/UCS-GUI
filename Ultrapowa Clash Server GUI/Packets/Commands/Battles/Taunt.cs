using UCS.Logic;

namespace UCS.Packets.Commands.Battles {
    #region Usings

    using Packets;

    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Packets.Commands.Server;

    #endregion Usings

    internal class Taunt : Command {
        public const int CommandID = 1;

        /// <summary>
        /// Initialize a new instance of the <see cref="Taunt"/> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        public Taunt(Device _Client) : base(_Client) {
            this.ID = CommandID;
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {}

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode() {}
    }
}