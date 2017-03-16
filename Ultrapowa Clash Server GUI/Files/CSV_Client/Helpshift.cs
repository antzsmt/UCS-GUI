namespace UCS.Files.CSV_Client
{
    using UCS.Files.CSV_Reader;
    using UCS.GameFiles;

    internal class Helpshift : Data
    {
        public Helpshift(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string HelpshiftId
        {
            get; set;
        }
    }
}
