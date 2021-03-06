﻿namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core.Network;
    using UCS.Extensions.Binary;
    using UCS.Logic;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Cancel_Friendly_Battle : Message
    {
        public const ushort PacketID = 14423;

        /// <summary>
        ///     Initialize a new instance of the
        ///     <see cref="Cancel_Friendly_Battle" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Cancel_Friendly_Battle(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Cancel_Friendly_Battle.
        }

        /// <summary>
        ///     <see cref="Cancel_Friendly_Battle.Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Cancel_Battle_OK(this.Client).Send();
        }
    }
}