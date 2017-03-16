#region Usings

using UCS.Core;
using UCS.Logic;

#endregion

namespace UCS.Packets.Commands.Client
{
    #region Usings

    using Packets;

    using UCS.Core.Network;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Packets.Commands.Server;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Free_Chest : Command
    {
        public int Chest_ID = 0;

        public int Tick = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Free_Chest" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Free_Chest(Reader _Reader, Device _Client, int _ID) : base(_Reader, _Client, _ID)
        {
            // Free_Chest.
        }

        /// <summary>
        /// <see cref="Decode"/> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();
            Debug.Write("Tick: " + this.Tick);
        }

        /// <summary>
        /// Processe this instance.
        /// </summary>
        public override void Process()
        {
            new Server_Commands(this.Client) { _Command = new Buy_Chest_Callback(this.Client) { ChestID = 7 }.Handle() }
                .Send();
        }
    }
}