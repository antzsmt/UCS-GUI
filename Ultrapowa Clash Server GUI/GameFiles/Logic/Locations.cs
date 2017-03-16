namespace UCS.GameFiles
{
   
    

    internal class Locations : Data
    {
        public Locations(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public bool NpcOnly
        {
            get; set;
        }

        public bool PvpOnly
        {
            get; set;
        }

        public int ShadowR
        {
            get; set;
        }

        public int ShadowG
        {
            get; set;
        }

        public int ShadowB
        {
            get; set;
        }

        public int ShadowA
        {
            get; set;
        }

        public int ShadowOffsetX
        {
            get; set;
        }

        public int ShadowOffsetY
        {
            get; set;
        }

        public string Music
        {
            get; set;
        }

        public int MusicStartTime
        {
            get; set;
        }

        public string Sound
        {
            get; set;
        }

        public int SoundStartTime
        {
            get; set;
        }

        public bool SoundPlayOvertime
        {
            get; set;
        }

        public string ExtraTimeMusic
        {
            get; set;
        }

        public int MatchLength
        {
            get; set;
        }

        public string WinCondition
        {
            get; set;
        }

        public int OvertimeSeconds
        {
            get; set;
        }

        public bool NoStartScreen
        {
            get; set;
        }

        public bool NoEndScreen
        {
            get; set;
        }

        public bool HideTopUI
        {
            get; set;
        }

        public bool HideManaBar
        {
            get; set;
        }

        public int EndScreenDelay
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string AmbientSound
        {
            get; set;
        }

        public string OverlaySC
        {
            get; set;
        }

        public string OverlayExportName
        {
            get; set;
        }

        public bool CrowdEffects
        {
            get; set;
        }

        public string CloudFileName
        {
            get; set;
        }

        public string CloudExportName
        {
            get; set;
        }

        public int CloudMinScale
        {
            get; set;
        }

        public int CloudMaxScale
        {
            get; set;
        }

        public int CloudMinSpeed
        {
            get; set;
        }

        public int CloudMaxSpeed
        {
            get; set;
        }

        public int CloudMinAlpha
        {
            get; set;
        }

        public int CloudMaxAlpha
        {
            get; set;
        }

        public int CloudCount
        {
            get; set;
        }
    }
}
