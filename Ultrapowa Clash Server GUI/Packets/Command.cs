namespace UCS.Packets
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using Extensions.Binary;

    using Logic;
    using Logic.Enums;

    #endregion

    internal class Command : IDisposable
    {
        public Command()
        {
            this.ID = 0;

            this.Client = null;
            this.Reader = null;
        }

        public Command(Device _Client)
        {
            this.ID = 0;

            this.Client = _Client;
            this.Reader = null;

            this.Writer = new List<byte>();
        }

        public Command(Reader _Reader, Device _Client, int _ID)
        {
            this.ID = _ID;

            this.Client = _Client;
            this.Reader = _Reader;
        }

        public Device Client { get; set; }

        public Direction Direction { get; set; }

        public int ID { get; set; }

        public Reader Reader { get; set; }

        public List<byte> Writer { get; set; }

        public virtual void Decode()
        {
            // Decode.
        }

        public void Dispose()
        {
            if (this.Reader != null)
            {
                this.Reader.Dispose();
                this.Reader = null;
            }
        }

        public virtual void Encode()
        {
            // Encode.
        }

        public virtual void Process()
        {
            // Process.
        }
    }
}