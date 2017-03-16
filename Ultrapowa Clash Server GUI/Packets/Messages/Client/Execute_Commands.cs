namespace UCS.Packets.Messages.Client
{
    #region Usings

    using System;

    using UCS.Core;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Logic;

    #endregion

    internal class Execute_Commands : Message
    {
        public const ushort PacketID = 14102;

        public uint Checksum;

        public byte[] Commands;

        public uint Count;

        public uint Tick;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Execute_Commands" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Execute_Commands(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            // Execute_Commands.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = (uint)this.Reader.ReadVInt();
            this.Checksum = (uint)this.Reader.ReadVInt();
            this.Count = (uint)this.Reader.ReadVInt();

            this.Commands = this.Reader.ReadBytes(this.Length - 6);
            Debug.Write("Tick: " + this.Tick);
            Debug.Write("Checksum: " + this.Checksum);
            Debug.Write("Count: " + this.Count);
            Debug.Write("Commands Length: " + this.Commands.Length);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            using (Reader _Reader = new Reader(this.Commands))
            {
                for (int _Index = 0; _Index < this.Count; _Index++)
                {
                    int _ID = _Reader.ReadVInt();
                    Debug.Write("Command _ID: " + _ID);
                    if (Command_Factory.Commands.ContainsKey(_ID))
                    {
                        Command _Command =
                            Activator.CreateInstance(Command_Factory.Commands[_ID], _Reader, this.Client, _ID) as
                                Command;

                        if (_Command != null)
                        {
                            _Command.Decode();
                            _Command.Process();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Debug.Write("Command " + _ID + " has not been handled.");

                        Console.ResetColor();
                        break;
                    }
                }
            }
        }
    }
}