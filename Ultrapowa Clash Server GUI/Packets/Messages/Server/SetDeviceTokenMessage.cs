using System;
using System.Collections.Generic;
using UCS.Core;
using UCS.Extensions.List;
using UCS.Logic;


namespace UCS.Packets.Messages.Server
{
    using Packets;
    internal class SetDeviceTokenMessage : Message
	{
        public const ushort PacketID = 20113;

        /// <summary>
        /// Initialize a new instance of the <see cref="SetDeviceTokenMessage"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public SetDeviceTokenMessage(Device _Device) : base(_Device)
        {
            this.ID = PacketID;
            Debug.Write("ID: " + this.ID);
        }

        /// <summary>
        /// Encode this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddString("12345678910112548950");
		}
	}
}
