namespace UCS.Packets.Commands.Client
{
    #region Usings

    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    class Change_Deck : Command
    {
        public int Tick;

        public int Deck;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Change_Deck" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Change_Deck(Reader _Reader, Device _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Change_Deck.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();

            this.Deck = this.Reader.Read() + 1;
        }
    }
}