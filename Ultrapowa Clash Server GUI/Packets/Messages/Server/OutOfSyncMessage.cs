namespace UCS.Packets
{
    #region Usings

    using UCS.Helpers;
    using UCS.Logic;

    #endregion

    //Packet 24104
    class OutOfSyncMessage : Message
    {
        public OutOfSyncMessage(Device client)
            : base(client)
        {
            this.SetMessageType(24104);
        }

        public override void Encode()
        {
            this.Writer.AddInt32(0);
            this.Writer.AddInt32(0);
            this.Writer.AddInt32(0);
        }
    }
}