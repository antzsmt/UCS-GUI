﻿using UCS.Core;
using UCS.Logic;

namespace UCS.Packets.Messages.Server
{
    #region Usings

    using Extensions.List;
    using Packets;

    #endregion Usings

    internal class Device_Link_Error : Message
    {
        public const ushort PacketID    = 26008;

        public bool Allow_Back          = false;
        public int Error_Code           = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Device_Link_Error"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Device_Link_Error(Device _Device) : base(_Device)
        {
            this.ID     = PacketID;
            Debug.Write("ID: " + this.ID);
        }

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.Add(this.Allow_Back);
            this.Writer.AddInt(0);
        }
    }
}