﻿namespace UCS.Packets.Messages.Server
{
    #region Usings

    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Battle_Result : Message
    {
        public const ushort PacketID = 20225;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Battle_Result" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Battle_Result(Device _Device)
            : base(_Device)
        {
            this.ID = PacketID;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddRange(
                "01-1E-00-1E-00-3F-00-00-03-00-13-09-00-00-04-6F-AC-13-8D-14-0B-AC-13-7A-CD-33-55".HexaToBytes());
            this.Writer.AddVInt(1); // Battle ID ? C0-C5-98-CE-0B
            this.Writer.AddRange(
                "12-9D-A8-E8-06-12-9D-A8-E8-06-12-9D-A8-E8-06-00-00-00-07-32-33-39-33-35-30-35-00-36-01-00-00-00-00-00-00-00-00-00-07-08-05-01-86-06-05-02-07-05-03-00-05-05-86-06-05-0D-00-05-0E-00-05-10-02-05-04-00-00-06-3C-04-01-3C-05-01-3C-06-01-3C-07-0C-3C-08-0C-3C-09-0C-00-02-05-08-0C-05-09-80-EA-E5-18-0C-1A-00-01-1A-01-01-1A-03-01-1A-0C-00-1A-0D-00-1A-0E-01-1A-02-01-1C-01-01-1A-13-00-1A-12-00-1C-00-00-1B-01-00-00-AA-01-AA-01-04-01-00-01-01-00-00-00-00-07-00-00-00-12-96-DB-EB-06-12-96-DB-EB-06-12-96-DB-EB-06-00-00-00-06-42-65-72-6B-61-6E-00-36-01-1E-00-00-00-00-00-00-00-00-07-08-05-01-93-03-05-05-93-03-05-0D-05-05-0E-07-05-03-01-05-10-01-05-02-07-05-04-03-00-06-3C-07-0A-3C-08-0A-3C-09-0A-3C-04-01-3C-05-01-3C-06-01-00-03-05-08-0A-05-07-01-05-06-1E-09-1A-00-06-1A-01-0F-1A-03-0A-1A-0D-09-1C-00-05-1A-0E-06-1C-01-02-1A-0F-03-1A-0C-02-00-A2-01-A2-01-10-01-00-01-00-00-00-00-00-07-00-00-00-0F-02-00-36-01-12-9D-A8-E8-06-12-96-DB-EB-06-00-00-00-00-00-00-00-00-00-00-01-00-A1-3E-01-B9-03-C7-7C-00-B5-01-F4-7E-08-23-01-23-01-23-00-23-00-22-03-22-0E-22-01-22-01-01-01-00-01-01-01-01-01-05-00-05-02-05-04-05-05-05-06-05-10-05-26-05-27-00-0D-A4-E2-01-9C-8E-03-00-00-C0-7C-00-A4-01-00-00-00-00-02-00-00-00-00-00-0D-AC-36-9C-8E-03-00-17-C2-7C-00-A4-01-00-00-00-00-01-00-00-00-00-00-0C-A8-8C-01-B8-2E-00-A4-03-B2-01-02-A4-01-00-00-00-00-A0-3E-00-00-00-00-00-05-04-07-7A-01-02-03-06-00-03-01-05-05-05-00-00-90-EF-0F-00-06-49-96-34-00-00-82-EA-E5-18-81-EA-E5-18-8E-EA-E5-18-80-EA-E5-18-81-FC-D9-1A-83-EA-E5-18-7F-7F-00-0C-A8-8C-01-88-C5-03-00-00-C0-7C-00-A4-01-00-00-00-00-00-00-00-00-00-00-05-04-03-02-7B-02-03-07-06-04-01-01-01-03-00-00-88-A1-0F-00-04-00-00-00-00-83-EA-E5-18-8D-EA-E5-18-8F-EA-E5-18-8E-EA-E5-18-8C-EA-E5-18-81-EA-E5-18-7F-7F-00-0F-AF-BB-01-A3-46-00-DC-7C-CE-7E-02-A4-01-00-00-00-00-02-00-00-00-00-00-0F-A2-D6-01-86-8F-01-00-E5-7D-F5-7C-02-A4-01-00-00-00-00-02-00-00-00-00-01-0F-AD-C3-01-A0-8B-01-00-FE-7D-E4-7C-02-A4-01-00-00-00-00-02-00-00-00-00-01-0F-AE-D8-01-87-7B-00-CC-7D-CB-7D-02-A4-01-00-00-00-00-02-00-00-00-00-00-00-A8-8C-01-B8-2E-00-00-00-00-00-00-00-00-05-04-00-00-A8-8C-01-B8-2E-00-00-00-00-0E-00-00-00-05-04-00-00-AF-BB-01-A3-46-A6-D0-01-96-02-00-00-0D-00-00-00-00-00-00-AC-36-A4-65-00-00-00-00-00-00-00-00-00-00-00-A8-8C-01-B8-2E-00-00-00-00-0A-00-00-00-05-04-00-00-A8-8C-01-B8-2E-00-00-00-00-11-00-00-00-05-04-00-00-A8-8C-01-B8-2E-00-00-00-00-03-00-00-00-05-04-00-00-A8-8C-01-B8-2E-00-00-00-00-02-00-00-00-05-04-02-00-00-00-00-00-00-00-00-00-7F-00-00-00-E5-7C-FC-7D-00-8C-07-00-00-7F-7F-02-00-00-00-00-00-00-00-00-00-7F-00-00-00-D2-7E-DA-7C-00-98-6D-00-00-7F-7F-02-00-00-00-00-00-00-00-00-00-7F-00-00-00-EE-7D-EE-7C-00-8C-2A-00-00-7F-7F-02-00-00-00-00-00-00-00-00-00-7F-00-00-00-CC-7D-CB-7D-00-B4-39-00-00-7F-7F-B8-15-84-0C-00-A9-03-A0-25-84-0E-94-05-89-02-89-02-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-00-00-A4-01-A4-01-FF-01-1A-00-01-1A-01-00-1A-02-00-1C-01-00-1A-12-00-1A-03-00-1A-0E-00-1A-13-00-FF-01-1A-00-01-1A-01-01-1A-0D-01-1C-01-01-1A-0C-00-1A-03-00-1A-0E-00-1A-0F-00-05-04-05-05-00-02-05-00-05-02-00-00-00-00-00-00-00-00-00-0C-00-00-00-8D-FD-BB-85-0A-00-00-36-01-36-01-36-01-36-01"
                    .HexaToBytes());
        }
    }
}