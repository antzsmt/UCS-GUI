#region Usings



#endregion

namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Core.Network;
    using UCS.Extensions.Binary;
    using UCS.Logic;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Go_Home : Message
    {
        public const ushort PacketID = 14101;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Go_Home" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Go_Home(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Go_Home.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            var test = this.Reader.ReadBoolean();
            Debug.Write("ReadBoolean: " + test);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            new Own_Home_Data(this.Client).Send();
        }
    }
}