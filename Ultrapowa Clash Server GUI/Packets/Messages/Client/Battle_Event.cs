namespace UCS.Packets.Messages.Client
{
    #region Usings

    using UCS.Core;
    using UCS.Core.Network;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;
    using UCS.Packets.Commands.Battles;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Battle_Event : Message
    {
        public const ushort PacketID = 12951;

        public int CommandID;

        public int CommandSum;

        public int CommandTick;

        public int CommandUnk;

        public int CommandUnk2;

        public int CommandValue;

        public int nose;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Battle_Event" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Battle_Event(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Battle_Event.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.CommandID = this.Reader.ReadVInt();
            this.nose = this.Reader.ReadVInt();
            this.CommandSum = this.Reader.ReadVInt();
            this.CommandUnk = this.Reader.ReadVInt();
            this.CommandTick = this.Reader.ReadVInt();
            this.CommandUnk2 = this.Reader.ReadInt16();
            this.CommandValue = this.Reader.ReadVInt();
            Debug.Write("CommandID: " + this.CommandID);
            Debug.Write("nose: " + this.nose);
            Debug.Write("CommandSum: " + this.CommandSum);
            Debug.Write("CommandUnk: " + this.CommandUnk);
            Debug.Write("CommandTick: " + this.CommandTick);
            Debug.Write("CommandUnk2: " + this.CommandUnk2);
            Debug.Write("CommandValue: " + this.CommandValue);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            Level _Enemy = ResourcesManager.Battles.GetEnemy(
                this.Client.GetLevel().GetPlayerAvatar().GetBattleID(),
                this.Client.GetLevel().GetPlayerAvatar().GetId());

            ResourcesManager.Battles[this.Client.GetLevel().GetPlayerAvatar().GetId()].Tick = this.CommandTick;
            ResourcesManager.Battles[this.Client.GetLevel().GetPlayerAvatar().GetId()].Checksum = this.CommandSum;

            if (this.CommandID == 1)
            {
                ResourcesManager.Battles[this.Client.GetLevel().GetPlayerAvatar().GetId()].Commands.Enqueue(
                    new Place_Unit(_Enemy.GetClient()) { Sender = this.Client.GetLevel().GetPlayerAvatar().GetId() });

                /* new Battle_Command_Data(this.Client)
                {
                    Sender      = this.Client.Level.PlayerID,
                    Tick        = this.CommandTick,
                    Checksum    = this.CommandSum
                }.Send();

                new Battle_Command_Data(_Enemy.Client)
                {
                    Sender      = this.Client.Level.PlayerID,
                    Tick        = this.CommandTick,
                    Checksum    = this.CommandSum
                }.Send(); */
            }
            else if (this.CommandID == 3)
            {
                new Battle_Event_Data(_Enemy.GetClient())
                        {
                            CommandID = this.CommandID,
                            CommandValue = this.CommandValue,
                            CommandTick = this.CommandTick,
                            CommandUnk = this.CommandUnk,
                            CommandUnk2 = this.CommandUnk2,
                            CommandSender =
                                this.Client.GetLevel().GetPlayerAvatar().GetId()
                        }
                    .Send();
            }
        }
    }
}