﻿#region Usings



#endregion

namespace UCS.Packets.Commands.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Move_Card : Command
    {
        public int ID;

        public int Position;

        public int Tick;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Move_Card" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Move_Card(Reader _Reader, Device _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Move_Card.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();

            this.ID = this.Reader.Read();
            this.Position = this.Reader.Read();

            Debug.Write("Position: " + this.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process()
        {
            this.Client.GetLevel().GetPlayerAvatar().Deck.Invert(this.ID, this.Position);
        }
    }
}