namespace UCS.Packets.Messages.Client
{
    #region Usings

    using System;

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Device_Link_Code : Message
    {
        public const ushort PacketID = 16000;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Device_Link_Code" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Device_Link_Code(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Device_Link_Code.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            Debug.Write("Device_Link_Code : " + BitConverter.ToString(this.Reader.ReadAllBytes()));
        }
    }
}