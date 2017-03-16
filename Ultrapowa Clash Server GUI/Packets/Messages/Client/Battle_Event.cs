namespace UCS.Packets.Messages.Client
{
    #region Usings

    using Commands.Battles;

    using Core;
    using Core.Network;

    using Extensions.Binary;
    using Extensions.List;

    using Logic;

    using Packets;

    using Server;

    #endregion

    internal class Battle_Event : Message
    {
        public const ushort PacketID = 12951;

        public int CommandID = 0;

        public int CommandSum = 0;

        public int CommandTick = 0;

        public int CommandUnk = 0;

        public int CommandUnk2 = 0;

        public int CommandValue = 0;

        public int nose = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Battle_Event" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Battle_Event(Device _Client, Reader Reader, int[] _Header) : base(_Client, Reader, _Header)
        {
            // Battle_Event.
        }

        /// <summary>
        /// <see cref="Decode"/> this instance.
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
        /// <see cref="Process"/> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            Level _Enemy = ResourcesManager.Battles.GetEnemy(this.Client.GetLevel().GetPlayerAvatar().GetBattleID(),
                                                                    this.Client.GetLevel().GetPlayerAvatar().GetId());

            ResourcesManager.Battles[this.Client.GetLevel().GetPlayerAvatar().GetId()].Tick = this.CommandTick;
            ResourcesManager.Battles[this.Client.GetLevel().GetPlayerAvatar().GetId()].Checksum = this.CommandSum;

            if (this.CommandID == 1)
            {
                ResourcesManager.Battles[this.Client.GetLevel().GetPlayerAvatar().GetId()].Commands.Enqueue(new Place_Unit(_Enemy.GetClient())
                                                                                          {
                                                                                              Sender = this.Client.GetLevel().GetPlayerAvatar().GetId()
                                                                                          });

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
                    CommandSender = this.Client.GetLevel().GetPlayerAvatar().GetId()
                }.Send();
            }
        }
    }
}