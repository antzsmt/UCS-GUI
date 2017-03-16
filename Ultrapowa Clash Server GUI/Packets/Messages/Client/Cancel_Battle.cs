namespace UCS.Packets.Messages.Client
{
    #region Usings

    using Core;
    using Core.Network;

    using Extensions.Binary;

    using Logic;

    using Packets;

    using Server;

    #endregion

    internal class Cancel_Battle : Message
    {
        public const ushort PacketID = 14107;

        /// <summary>
        /// Initialize a new instance of the <see cref="Cancel_Battle" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Cancel_Battle(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header)
        {
            // Cancel_Battle.
        }

        /// <summary>
        /// <see cref="Process"/> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            if (ResourcesManager.Battles.Waiting.Contains(this.Client.GetLevel()))
            {
                ResourcesManager.Battles.Waiting.Remove(this.Client.GetLevel());
                new Cancel_Battle_OK(this.Client).Send();
            }
        }
    }
}