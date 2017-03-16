#region Usings

using UCS.Logic;

#endregion

namespace UCS.Packets.Messages.Server
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using Packets;

    using UCS.Core;
    using UCS.Core.Network;
    using UCS.Extensions.List;
    using UCS.Logic.Slots.Items;

    #endregion

    internal class Battle_Command_Data : Message
    {
        public const ushort PacketID = 21902;

        public Battle Battle = null;

        public long Sender = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Battle_Command_Data" />
        /// class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Battle_Command_Data(Device _Device) : base(_Device)
        {
            this.ID = PacketID;
        }

        /// <summary>
        /// <see cref="Encode"/> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddVInt(this.Battle.Tick);
            this.Writer.AddVInt(this.Battle.Checksum); // D4-A5-CA-94-0C
            this.Writer.Add(this.Battle.Commands.Count > 0);

            if (this.Battle.Commands.Count > 0)
            {
                this.Writer.AddRange(this.Battle.Commands.Dequeue().Handle().Writer);
            }
        }
    }
}