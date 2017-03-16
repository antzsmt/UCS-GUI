namespace UCS.Packets.Commands.Client
{
    #region Usings

    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    class Claim_Achievement : Command
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="Claim_Achievement" />
        ///     class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Claim_Achievement(Reader _Reader, Device _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Claim_Achievement.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadInt16();
        }
    }
}