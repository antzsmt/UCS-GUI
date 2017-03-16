namespace UCS.Packets.Messages.Server
{
    #region Usings

    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    // Packet 20161
    class ShutdownStartedMessage : Message
    {
        private int m_vCode;

        public ShutdownStartedMessage(Device client)
            : base(client)
        {
            this.SetMessageType(20161);
        }

        public override void Encode()
        {
            this.Writer.AddInt(this.m_vCode);
        }

        public void SetCode(int code)
        {
            this.m_vCode = code;
        }
    }
}