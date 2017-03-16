namespace UCS.Packets.Commands.Server
{
    #region Usings

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Name_Change_Callback : Command
    {
        public const int CommandID = 201;

        public string Name = string.Empty;

        public string Previous = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Name_Change_Callback" />
        ///     class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The Packet ID.</param>
        public Name_Change_Callback(Reader _Reader, Device _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Name_Change_Callback.
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="Name_Change_Callback" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        public Name_Change_Callback(Device _Client)
            : base(_Client)
        {
            this.ID = CommandID;
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Name = this.Reader.ReadString();
            this.Previous = this.Name;
            Debug.Write("Name: " + this.Name);
            Debug.Write("Previous: " + this.Previous);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            Level pl = this.Client.GetLevel();
            this.Writer.AddString(pl.GetPlayerAvatar().GetAvatarName());
            this.Writer.AddInt(1);
        }
    }
}