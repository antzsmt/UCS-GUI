namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Search_Clans : Message
    {
        public const ushort PacketID = 14324;

        public string Name = string.Empty;

        public int Minimum_Players;

        public int Maxium_Players;

        public int Required_Score;

        public int Origin;

        public bool Open_Only;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Search_Clans" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Search_Clans(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Search_Clans.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            // Decode.

            this.Name = this.Reader.ReadString();
            this.Origin = this.Reader.ReadVInt();

            if (this.Origin > 0)
            {
                this.Origin = this.Reader.ReadVInt();
            }

            this.Minimum_Players = this.Reader.ReadInt32();
            this.Maxium_Players = this.Reader.ReadInt32();
            this.Required_Score = this.Reader.ReadInt32();
            this.Open_Only = this.Reader.ReadBoolean();

            var test = this.Reader.ReadInt32();
            var test2 = this.Reader.ReadInt32();
            Debug.Write("Name: " + this.Name);
            Debug.Write("Origin: " + this.Origin);
            Debug.Write("Minimum_Players: " + this.Minimum_Players);
            Debug.Write("Maxium_Players: " + this.Maxium_Players);
            Debug.Write("Required_Score: " + this.Required_Score);
            Debug.Write("Open_Only: " + this.Open_Only);
            Debug.Write("test: " + test);
            Debug.Write("Name: " + test2);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            // Regions _Region = CSV.Tables.Get(Gamefile.Regions).GetDataWithID(this.Origin) as Regions;
            Debug.Write("Implementar");
        }
    }
}