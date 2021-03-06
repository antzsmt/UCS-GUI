﻿namespace UCS.Packets.Messages.Server
{
    #region Usings

    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Clan_Local_Ranking : Message
    {
        public const ushort PacketID = 24402;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Clan_Local_Ranking" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Clan_Local_Ranking(Device _Device)
            : base(_Device)
        {
            this.ID = PacketID;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddInt(0);
        }
    }
}