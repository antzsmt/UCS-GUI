namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Shop : Data
    {
        public Shop(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string Category
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string Rarity
        {
            get; set;
        }

        public bool Disabled
        {
            get; set;
        }

        public string Resource
        {
            get; set;
        }

        public int Cost
        {
            get; set;
        }

        public int Count
        {
            get; set;
        }

        public int CycleDuration
        {
            get; set;
        }

        public int CycleDeadzoneStart
        {
            get; set;
        }

        public int CycleDeadzoneEnd
        {
            get; set;
        }

        public bool TopSection
        {
            get; set;
        }

        public bool SpecialOffer
        {
            get; set;
        }

        public int DurationSecs
        {
            get; set;
        }

        public string Chest
        {
            get; set;
        }

        public int TrophyLimit
        {
            get; set;
        }

        public string IAP
        {
            get; set;
        }

        public string StarterPack_Item0_Type
        {
            get; set;
        }

        public string StarterPack_Item0_ID
        {
            get; set;
        }

        public int StarterPack_Item0_Param1
        {
            get; set;
        }

        public string StarterPack_Item1_Type
        {
            get; set;
        }

        public string StarterPack_Item1_ID
        {
            get; set;
        }

        public int StarterPack_Item1_Param1
        {
            get; set;
        }

        public string StarterPack_Item2_Type
        {
            get; set;
        }

        public string StarterPack_Item2_ID
        {
            get; set;
        }

        public int StarterPack_Item2_Param1
        {
            get; set;
        }

        public int ValueMultiplier
        {
            get; set;
        }
    }
}