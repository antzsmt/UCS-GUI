using UCS.GameFiles;

namespace UCS.Files.CSV_Client
{
    using UCS.Files.CSV_Reader;

    internal class Texts_Patch : Data
    {
        public Texts_Patch(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string TID
        {
            get; set;
        }

        public string EN
        {
            get; set;
        }

        public string FR
        {
            get; set;
        }

        public string DE
        {
            get; set;
        }

        public string ES
        {
            get; set;
        }

        public string IT
        {
            get; set;
        }

        public string NL
        {
            get; set;
        }

        public string NO
        {
            get; set;
        }

        public string TR
        {
            get; set;
        }

        public string JP
        {
            get; set;
        }

        public string KR
        {
            get; set;
        }

        public string RU
        {
            get; set;
        }

        public string AR
        {
            get; set;
        }

        public string PT
        {
            get; set;
        }

        public string CN
        {
            get; set;
        }

        public string CNT
        {
            get; set;
        }
    }
}
