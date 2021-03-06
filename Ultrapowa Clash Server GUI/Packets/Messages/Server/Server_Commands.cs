﻿namespace UCS.Packets.Messages.Server
{
    #region Usings

    using UCS.Core;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Server_Commands : Message
    {
        public const ushort PacketID = 24111;

        public Command _Command = null;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Server_Commands" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Server_Commands(Device _Device)
            : base(_Device)
        {
            this.ID = PacketID;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddVInt(this._Command.ID);
            this.Writer.AddRange(this._Command.Writer);
            Debug.Write("_Command.Writer: " + this._Command.Writer);
        }
    }
}