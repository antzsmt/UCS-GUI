﻿using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    #region Usings

    using Packets;

    using UCS.Extensions.List;

    #endregion Usings

    internal class Device_Already_Bound : Message
    {
        public const ushort PacketID = 24262;

        /// <summary>
        /// Initialize a new instance of the <see cref="Device_Already_Bound"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Device_Already_Bound(Device _Device) : base(_Device)
        {
            this.ID     = PacketID;
            Debug.Write("ID: " + this.ID);
        }

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddRange("FF-FF-FF-FF-01-00-00-00-02-00-00-37-EE-00-00-00-28-68-77-38-72-37-32-78-66-6B-6D-6E-67-65-72-39-6A-72-77-6E-74-77-6B-77-68-61-33-6B-73-61-73-32-79-77-72-62-63-61-66-77-6A-02-AE-DF-01-00-00-00-00-00-00-00-06-42-65-72-6B-61-6E-02-36-08-A2-22-00-00-00-00-00-00-00-00-07-0F-05-00-89-F9-01-05-01-8A-2C-05-02-9F-04-05-03-00-05-04-00-05-05-8A-2C-05-0C-A3-0E-05-0D-00-05-0E-00-05-0F-8F-06-05-10-09-05-11-12-05-12-0E-05-13-0A-05-16-90-06-00-15-3C-00-0F-3C-01-BD-4B-3C-02-BD-4B-3C-03-BD-4B-3C-04-07-3C-05-07-3C-06-07-3C-07-33-3C-08-33-3C-09-33-3C-0A-01-3C-0B-2E-3C-0C-2E-3C-0D-2E-3C-0E-01-3C-0F-01-3C-10-01-3C-11-1A-3C-12-07-3C-13-07-3C-14-07-0E-3C-00-01-3C-01-01-3C-02-01-3C-03-01-3C-04-01-3C-05-01-3C-06-01-3C-07-01-3C-08-01-3C-09-01-3C-0A-01-3C-0E-01-3C-11-01-3C-12-01-07-05-06-A2-22-05-07-92-02-05-08-33-05-09-84-F3-DF-19-05-0A-BD-4B-05-14-07-05-15-AB-01-30-1A-00-00-1A-01-00-1A-02-00-1A-03-00-1A-04-00-1A-05-00-1A-06-00-1A-07-00-1A-08-01-1A-09-00-1A-0A-00-1A-0B-00-1A-0C-00-1A-0D-00-1A-0E-02-1A-0F-00-1A-10-00-1A-11-02-1A-12-00-1A-13-00-1A-14-00-1A-15-00-1A-16-01-1A-18-01-1A-1B-00-1A-1F-00-1A-20-01-1B-00-00-1B-01-00-1B-02-00-1B-03-00-1B-04-01-1B-05-00-1B-06-00-1B-07-00-1B-08-00-1B-09-00-1B-0A-00-1C-00-00-1C-01-00-1C-02-00-1C-03-00-1C-04-00-1C-05-00-1C-06-00-1C-07-00-1C-08-00-1C-09-00-00-09-09-82-1B-09-80-93-02-09-07-81-91-17-00-00-00-08-44-41-52-4B-53-49-44-45-10-B1-02-02-98-0D-A5-01-97-06-AC-05-07-A1-01-02-00-00".Replace("-", string.Empty).HexaToBytes());
        }
    }
}