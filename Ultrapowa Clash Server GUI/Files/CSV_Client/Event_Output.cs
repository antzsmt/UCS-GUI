namespace UCS.Files.CSV_Client
{
    using UCS.Files.CSV_Reader;
    using UCS.GameFiles;

    internal class Event_Output : Data
    {
        public Event_Output(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public int Id
        {
            get; set;
        }

        public int Channels
        {
            get; set;
        }

        public int DurationMillis
        {
            get; set;
        }
    }
}
