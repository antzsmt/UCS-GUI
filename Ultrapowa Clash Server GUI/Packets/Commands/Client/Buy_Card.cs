﻿namespace UCS.Packets.Commands.Client
{
    #region Usings

    using System;

    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    class Buy_Card : Command
    {
        public int CardID = 0;

        public int Type = 0;

        public int Count = 0;

        public int Tick;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Buy_Card" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Buy_Card(Reader _Reader, Device _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Buy_Card.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            // AE-03-AE-03-00-01
            // 00-00-01-F4-1A-00-00
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();

            Console.WriteLine("Buy_Card : " + BitConverter.ToString(this.Reader.ReadAllBytes()));
        }
    }
}