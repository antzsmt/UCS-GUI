#region Usings

using UCS.Core.Network;
using UCS.Logic;

#endregion

namespace UCS.Packets.Commands.Client
{
    #region Usings

    using Packets;
    using Packets;

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic.Slots.Items;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Search_Battle : Command
    {
        /// <summary>
        /// A Temporary <see cref="Player"/> variable.
        /// </summary>
        public ClientAvatar Player = null;

        public int Tick = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Search_Battle" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Search_Battle(Reader _Reader, Device _Client, int _ID) : base(_Reader, _Client, _ID)
        {
            // Search_Battle.
        }

        /// <summary>
        /// <see cref="Decode"/> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();

            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            Debug.Write("Tick: " + this.Tick);
        }

        /// <summary>
        /// <see cref="Process"/> this instance.
        /// </summary>
        public override void Process()
        {
            if (ResourcesManager.Battles.Waiting.Count == 0)
            {
                ResourcesManager.Battles.Enqueue(this.Client.GetLevel());
                new Matchmaking_Info(this.Client).Send();
            } else
            {
                Level _Enemy = ResourcesManager.Battles.Dequeue();
                Battle Battle = new Battle(_Enemy, this.Client.GetLevel());

                ResourcesManager.Battles.Add(Battle);

                Battle.Player1.GetPlayerAvatar().SetBattleID(Battle.BattleID);
                Battle.Player2.GetPlayerAvatar().SetBattleID(Battle.BattleID);

                new Sector_PC(Battle.Player1.GetClient()) { Battle = Battle }.Send();

                new Sector_PC(Battle.Player2.GetClient()) { Battle = Battle }.Send();
            }
        }
    }
}