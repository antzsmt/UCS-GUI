namespace UCS.Packets.Commands.Client {
    using System;

    using Core;

    using Extensions.Binary;
    using Extensions.List;

    using Logic;

    using Packets;

    class Friendly_Battle : Command {
        /// <summary>
        /// Initialize a new instance of the <see cref="Friendly_Battle"/> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Friendly_Battle(Reader _Reader, Device _Client, int _ID) : base(_Reader, _Client, _ID) {
            // Friendly_Battle.
        }

        /// <summary>
        /// Decode this instance.
        /// </summary>
        public override void Decode() {
            Debug.Write("Friendly_Battle : " + this.Reader.ReadString());
            Debug.Write("Friendly_Battle : " + BitConverter.ToString(this.Reader.ReadAllBytes()));
        }
    }
}