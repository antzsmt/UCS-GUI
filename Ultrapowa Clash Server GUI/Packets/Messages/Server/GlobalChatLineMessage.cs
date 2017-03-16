namespace UCS.Packets
{
    #region Usings

    using UCS.Extensions.List;
    using UCS.Helpers;
    using UCS.Logic;

    #endregion

    //Packet 24715
    class GlobalChatLineMessage : Message
    {
        private string m_vMessage;

        private long m_vHomeId;

        private long m_vCurrentHomeId;

        private string m_vPlayerName;

        private int m_vLeagueId;

        public GlobalChatLineMessage(Device client)
            : base(client)
        {
            this.SetMessageType(24715);

            this.m_vMessage = "default";
            this.m_vPlayerName = "default";
            this.m_vHomeId = 1;
            this.m_vCurrentHomeId = 1;
        }

        public override void Encode()
        {
            this.Writer.AddString(this.m_vMessage);
            this.Writer.AddString(this.m_vPlayerName);
            this.Writer.AddInt32(0x05);
            this.Writer.AddInt32(this.m_vLeagueId);
            this.Writer.AddInt64(this.m_vHomeId);
            this.Writer.AddInt64(this.m_vCurrentHomeId);
            this.Writer.AddInt32(0);
        }

        public void SetChatMessage(string message)
        {
            this.m_vMessage = message;
        }

        public void SetPlayerId(long id)
        {
            this.m_vHomeId = id;
            this.m_vCurrentHomeId = id;
        }

        public void SetPlayerName(string name)
        {
            this.m_vPlayerName = name;
        }

        public void SetLeagueId(int leagueId)
        {
            this.m_vLeagueId = leagueId;
        }
    }
}