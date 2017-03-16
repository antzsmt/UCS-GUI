namespace UCS.Files.CSV_Client.Sorted_Regions
{
    using UCS.Files.CSV_Reader;
    using UCS.GameFiles;

    internal class JP_Regions : Data
    {
        public JP_Regions(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string Dummy
        {
            get; set;
        }
    }
}
