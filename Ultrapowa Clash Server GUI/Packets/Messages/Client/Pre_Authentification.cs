namespace UCS.Packets.Messages.Client
{
    #region Usings

    using System;

    using UCS.Core;
    using UCS.Core.Network;
    using UCS.Core.Settings;
    using UCS.Extensions.Binary;
    using UCS.Logic;
    using UCS.Logic.Enums;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Pre_Authentification : Message
    {
        public const ushort PacketID = 10100;

        private int AppStore;

        private int Device;

        private string Hash = string.Empty;

        private int KeyVersion;

        private int Protocol;

        private new int[] Version = new int[3];

        /// <summary>
        ///     Initialize a new instance of the <see cref="Pre_Authentification" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Pre_Authentification(Device _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            this.Client.State = State.SESSION;
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Protocol = this.Reader.ReadInt32(); // Protocol 1
            this.KeyVersion = this.Reader.ReadInt32(); // Key Version 7
            this.Version[0] = this.Reader.ReadInt32(); // Major Version 2
            this.Version[1] = this.Reader.ReadInt32(); // Build Version 0
            this.Version[2] = this.Reader.ReadInt32(); // Minor Version 2000

            this.Hash = this.Reader.ReadString(); // Content Hash
            this.Device = this.Reader.ReadInt32(); // Device Type 2
            this.AppStore = this.Reader.ReadInt32(); // App Store 2

            Debug.Write("Protocol: " + this.Protocol);
            Debug.Write("KeyVersion: " + this.KeyVersion);
            Debug.Write("MajorVersion: " + this.Version[0]);
            Debug.Write("Build: " + this.Version[1]);
            Debug.Write("MinorVersion: " + this.Version[2]);
            Debug.Write("Hash: " + this.Hash);
            Debug.Write("Device: " + this.Device);
            Debug.Write("AppStore: " + this.AppStore);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        /// <param name="level">
        /// </param>
        /// <exception cref="OverflowException">
        ///     <paramref name="value" /> es menor que <see cref="F:System.TimeSpan.MinValue" /> o mayor que
        ///     <see cref="F:System.TimeSpan.MaxValue" />.O bienEl valor de <paramref name="value" /> es
        ///     <see cref="F:System.Double.PositiveInfinity" />.O bienEl valor de <paramref name="value" /> es
        ///     <see cref="F:System.Double.NegativeInfinity" />.
        /// </exception>
        public override void Process(Level level)
        {
            var p = new Authentification_Failed(this.Client);
            if (this.Version[0] != 2 && this.Version[2] != 2000)
            {
                this.Client.State = 0;
                p.SetErrorCode(CodesFail.NEW_VERSION);
                if (this.Version[0] > 2)
                {
                    p.SetReason("Esta versión de cliente es superior a la aceptada por el servidor.");
                }
                else if (this.Version[2] < 2000)
                {
                    p.SetReason("Esta versión de cliente es menor a la aceptada por el servidor.");
                }
                else
                {
                    p.SetReason("Este cliente no es aceptado por el servidor.");
                }

                p.Send();
                return;
            }

            if (Settings.Maintenance)
            {
                this.Client.State = State.DISCONNECTED;
                p.SetErrorCode(CodesFail.MAINTENANCE);
                p.SetReason("Duración del mantenimiento: " + TimeSpan.FromMinutes(Settings.MaintenanceDuration));
                p.RemainingTime(Settings.MaintenanceDuration);
                p.Send();
                return;
            }

            if (this.Hash != Constants.Sha)
            {
                p.SetErrorCode(CodesFail.NEW_VERSION);
                p.SetReason("Este cliente no es aceptado por el servidor.");
                p.Send();
            }
            else
            {
                this.Client.State = State.SESSION;
                new Pre_Authentification_OK(this.Client).Send();
            }
        }
    }
}