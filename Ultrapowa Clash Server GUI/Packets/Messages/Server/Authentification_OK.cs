namespace UCS.Packets.Messages.Server
{
    #region Usings

    using System;
    using System.Linq;

    using UCS.Extensions.List;
    using UCS.Library.Blake2b;
    using UCS.Library.Sodium;
    using UCS.Logic;
    using UCS.Logic.Enums;

    #endregion

    internal class Authentification_OK : Message
    {
        public const ushort PacketID = 20104;

        private string FacebookAppID = string.Empty;

        private string FacebookID = string.Empty;

        private string GamecenterID = string.Empty;

        private string m_vAccountCreatedDate;

        private long m_vAccountId;

        private int m_vContentVersion;

        private string m_vCountryCode;

        private int m_vDaysSinceStartedPlaying;

        private string m_vFacebookId;

        private string m_vGamecenterId;

        private string m_vPassToken;

        private int m_vPlayTimeSeconds;

        private int m_vServerBuild;

        private string m_vServerEnvironment;

        private int m_vServerMajorVersion;

        private string m_vServerTime;

        private int m_vSessionCount;

        private int m_vStartupCooldownSeconds;

        private string Startup = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Authentification_OK" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Authentification_OK(Device _Device)
            : base(_Device)
        {
            this.ID = PacketID;
            this.Client.State = State.LOGGED;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            Level pl = this.Client.GetLevel();
            TimeSpan TimeStamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1));
            this.Writer.AddLong(pl.GetPlayerAvatar().GetId());
            this.Writer.AddLong(pl.GetPlayerAvatar().GetId());
            this.Writer.AddString(pl.GetPlayerAvatar().GetToken());

            this.Writer.AddString(null); // FacebookId
            this.Writer.AddString(null); // GamecenterId

            this.Writer.AddVInt((int)CVersion.Major); // ServerMajorVersion
            this.Writer.AddVInt((int)CVersion.Minor + 1); // ServerBuild
            this.Writer.AddVInt((int)CVersion.Minor + 1);
            this.Writer.AddVInt((int)CVersion.Major); // ContentVersion

            this.Writer.AddString("prod"); // ServerEnvironment

            this.Writer.AddVInt(2); // SessionCount 2
            this.Writer.AddVInt(this.m_vDaysSinceStartedPlaying); // PlayTimeSeconds 270

            // FacebookAppID String
            // StartupCooldownSeconds String
            // AccountCreatedDate String
            // 0 int32
            // GoogleID String
            // null String
            // CountryCode String
            // someid2 String
            this.Writer.AddVInt((int)TimeStamp.TotalSeconds);
            this.Writer.AddString(TimeStamp.TotalMilliseconds.ToString("#"));
            this.Writer.AddString(
                pl.GetPlayerAvatar().GetCreated().Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString("#"));
            this.Writer.AddString(
                pl.GetPlayerAvatar().GetCreated().Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString("#"));

            this.Writer.AddVInt(0);
            this.Writer.AddString(null);
        }

        /// <summary>
        ///     <see cref="Encrypt" /> this instance.
        /// </summary>
        public override void Encrypt()
        {
            Blake2BHasher _Hasher = new Blake2BHasher();

            _Hasher.Update(this.Client.SNonce);
            _Hasher.Update(this.Client.PublicKey);
            _Hasher.Update(Keys.Sodium.PublicKey);

            byte[] _Nonce = _Hasher.Finish();

            this.Data = this.Client.RNonce.Concat(this.Client.PublicKey).Concat(this.Writer).ToArray();
            this.Data = Sodium.Encrypt(this.Data, _Nonce, Keys.Sodium.PrivateKey, this.Client.PublicKey);

            this.Length = this.Data.Length;
        }

        public void SetAccountCreatedDate(string date)
        {
            this.m_vAccountCreatedDate = date;
        }

        public void SetAccountId(long id)
        {
            this.m_vAccountId = id;
        }

        public void SetContentVersion(int version)
        {
            this.m_vContentVersion = version;
        }

        public void SetCountryCode(string code)
        {
            this.m_vCountryCode = code;
        }

        public void SetDaysSinceStartedPlaying(int days)
        {
            this.m_vDaysSinceStartedPlaying = days;
        }

        public void SetFacebookId(string id)
        {
            this.m_vFacebookId = id;
        }

        public void SetGamecenterId(string id)
        {
            this.m_vGamecenterId = id;
        }

        public void SetPassToken(string token)
        {
            this.m_vPassToken = token;
        }

        public void SetPlayTimeSeconds(int seconds)
        {
            this.m_vPlayTimeSeconds = seconds;
        }

        public void SetServerBuild(int build)
        {
            this.m_vServerBuild = build;
        }

        public void SetServerEnvironment(string env)
        {
            this.m_vServerEnvironment = env;
        }

        public void SetServerMajorVersion(int version)
        {
            this.m_vServerMajorVersion = version;
        }

        public void SetServerTime(string time)
        {
            this.m_vServerTime = time;
        }

        public void SetSessionCount(int count)
        {
            this.m_vSessionCount = count;
        }

        public void SetStartupCooldownSeconds(int seconds)
        {
            this.m_vStartupCooldownSeconds = seconds;
        }
    }
}