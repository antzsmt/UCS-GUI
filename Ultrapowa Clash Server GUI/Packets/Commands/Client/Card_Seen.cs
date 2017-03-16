namespace UCS.Packets.Commands.Client
{
    #region Usings

    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Card_Seen : Command
    {
        public int CardID;

        public int Tick;

        public int Type;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Card_Seen" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Card_Seen(Reader _Reader, Device _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Card_Seen.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();

            this.Reader.ReadVInt();
            this.Type = this.Reader.ReadVInt();
            this.CardID = this.Reader.ReadVInt();
        }

        /// <summary>
        ///     Processe this instance.
        /// </summary>
        /// <param name="_level"></param>
        public override void Process()
        {
            int _Index =
                this.Client.GetLevel()
                    .GetPlayerAvatar()
                    .Deck.FindIndex(_Card => _Card.Type == this.Type && _Card.ID == this.CardID);
            this.Client.GetLevel().GetPlayerAvatar().Deck[_Index].New = 0;
        }
    }
}