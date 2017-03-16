namespace UCS.Packets.Messages.Client
{
    #region Usings

    using System;
    using System.Linq;
    using System.Security.Cryptography;

    using UCS.Core;
    using UCS.Core.Network;
    using UCS.Core.Settings;
    using UCS.Extensions.Binary;
    using UCS.Extensions.List;
    using UCS.Library.Blake2b;
    using UCS.Library.Sodium;
    using UCS.Logic;
    using UCS.Logic.Enums;
    using UCS.Packets.Messages.Server;

    #endregion

    internal class Authentification : Message
    {
        public const ushort PacketID = 10101;

        private string AdvertiseID = string.Empty;

        private bool Advertising = true;

        private string AndroidID = string.Empty;

        private string DeviceModel = string.Empty;

        private string FacebookID = string.Empty;

        private bool isAndroid = true;

        private string MacAddress = string.Empty;

        private string MasterHash = string.Empty;

        private string OpenUDID = string.Empty;

        private string OSVersion = string.Empty;

        private string Region = string.Empty;

        private uint Seed = 0;

        private string SVersion = string.Empty;

        private string Token = string.Empty;

        private string Udid = string.Empty;

        private string[] Unknown = new string[5];

        private long UserID;

        private string VendorID = string.Empty;

        private new int[] Version = new int[3];

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="Authentification" />
        ///         class.
        ///     </para>
        ///     <para>
        ///         This message is sent by the client, for login into the server.
        ///     </para>
        ///     <para>
        ///         The server send a
        ///         <see cref="UCS.Packets.Messages.Server.Authentification_OK" /> if it
        ///         success, otherwise, it send a
        ///         <see cref="UCS.Packets.Messages.Server.Authentification_Failed" /> .
        ///     </para>
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Authentification(Device _Client, Reader Reader, int[] _Header)
            : base(_Client, Reader, _Header)
        {
            this.Client.State = State.LOGIN;
        }

        /// <summary>
        ///     <see cref="Decrypt" /> this instance.
        /// </summary>
        public override void Decrypt()
        {
            byte[] _Payload = this.Data;
            this.Client.PublicKey = _Payload.Take(32).ToArray();

            Blake2BHasher _Blake = new Blake2BHasher();

            _Blake.Update(this.Client.PublicKey);
            _Blake.Update(Keys.Sodium.PublicKey);

            this.Client.RNonce = _Blake.Finish();

            byte[] _Decrypted = Sodium.Decrypt(
                _Payload.Skip(32).ToArray(),
                this.Client.RNonce,
                Keys.Sodium.PrivateKey,
                this.Client.PublicKey);

            this.Client.SNonce = _Decrypted.Skip(24).Take(24).ToArray();
            this.Data = _Decrypted.Skip(48).ToArray();

            this.Reader = new Reader(this.Data);
            this.Length = this.Data.Length;
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.UserID = this.Reader.ReadInt64(); // User ID
            this.Token = this.Reader.ReadString(); // User Token

            this.Version[0] = this.Reader.ReadVInt(); // MajorVersion
            this.Version[1] = this.Reader.ReadVInt(); // Revision Version
            this.Version[2] = this.Reader.ReadVInt(); // MinorVersion

            this.MasterHash = this.Reader.ReadString(); // MasterHash
            this.Udid = this.Reader.ReadString(); // Udid
            this.OpenUDID = this.Reader.ReadString(); // OpenUDID
            this.MacAddress = this.Reader.ReadString(); // Mac Address
            this.DeviceModel = this.Reader.ReadString(); // Device Model
            this.AdvertiseID = this.Reader.ReadString(); // AGUID
            this.OSVersion = this.Reader.ReadString(); // OS Version

            this.isAndroid = this.Reader.ReadBoolean(); // Android != iPhone
            this.Unknown[0] = this.Reader.ReadString(); // Unknown

            this.AndroidID = this.Reader.ReadString(); // ADID
            this.Region = this.Reader.ReadString(); // Region
            this.Reader.ReadInt32(); // Unknown
            this.Reader.ReadInt32(); // Unknown

            this.Unknown[2] = this.Reader.ReadString();
            Debug.Write("this.Unknown[2]: " + this.Unknown[2]);
            this.Reader.ReadInt32(); // Unknown
            this.Reader.ReadInt32(); // Unknown

            this.Reader.ReadInt32(); // Unknown

            this.Reader.ReadInt32(); // Unknown

            this.Reader.ReadInt32(); // Unknown
            this.Reader.ReadInt32(); // Unknown
            this.Reader.ReadInt32(); // Unknown
            this.Reader.ReadInt32();
            this.Reader.ReadChar();

            Debug.Write("UserID: " + this.UserID);
            Debug.Write("Token: " + this.Token);
            Debug.Write("MajorVersion: " + this.Version[0]);
            Debug.Write("Build: " + this.Version[1]);
            Debug.Write("MinorVersion: " + this.Version[2]);
            Debug.Write("MasterHash: " + this.MasterHash);
            Debug.Write("Udid: " + this.Udid);
            Debug.Write("OpenUDID: " + this.OpenUDID);
            Debug.Write("MacAddress: " + this.MacAddress);
            Debug.Write("DeviceModel: " + this.DeviceModel);
            Debug.Write("AdvertiseID: " + this.AdvertiseID);
            Debug.Write("OSVersion: " + this.OSVersion);
            Debug.Write(message: "isAndroid: " + this.isAndroid);
            Debug.Write("Unknown[0] : " + this.Unknown[0]);
            Debug.Write("AndroidID: " + this.AndroidID);
            Debug.Write("Region: " + this.Region);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        /// <param name="level">
        /// </param>
        public override void Process(Level level)
        {
            level = ResourcesManager.GetPlayer(this.UserID);
 
            if (Convert.ToBoolean(Constants.Patching))
            {
                if (this.MasterHash != ObjectManager.FingerPrint.sha)
                {
                    var p = new Authentification_Failed(this.Client);
                    p.SetErrorCode(CodesFail.NEW_VERSION);
                    p.SetResourceFingerprintData(ObjectManager.FingerPrint.SaveToJson());
                    p.SetContentURL(Constants.PatchURL);
                    p.SetUpdateURL("https://ClashRoyaleSpain.es/");
                    p.Send();
                    return;
                }
            }

            if (level == null)
            {
                level = ObjectManager.CreateAvatar(this.UserID);
                byte[] tokenSeed = new byte[20];
                new Random().NextBytes(tokenSeed);
                SHA1 sha = new SHA1CryptoServiceProvider();
                this.Token = BitConverter.ToString(sha.ComputeHash(tokenSeed)).Replace("-", string.Empty);
                Debug.Write("New Token: " + this.Token);
                level.GetPlayerAvatar().SetCreated(DateTime.UtcNow);
                level.GetPlayerAvatar().SetToken(this.Token);
            }
            else
            {
                if (level.GetPlayerAvatar().GetToken() != this.Token)
                {
                    new Authentification_Failed(this.Client).Send();
                }

                if (level.GetAccountStatus() == 99)
                {
                    var p = new Authentification_Failed(this.Client);
                    p.SetErrorCode(CodesFail.BANNED);
                    p.Send();
                    return;
                }
            }

            ResourcesManager.LogPlayerIn(level, this.Client);
            level.Tick();
            
            if (level.GetAccountPrivileges() > 0)
            {
                level.GetPlayerAvatar().SetLeagueId(21);
            }

            if (level.GetAccountPrivileges() > 4)
            {
                level.GetPlayerAvatar().SetLeagueId(22);
            }

            level.GetPlayerAvatar().SetIsAndroid(this.isAndroid);
            level.GetPlayerAvatar().SetRegion(this.Region);
            level.GetPlayerAvatar().SetUpdate(DateTime.UtcNow);

            var loginOk = new Authentification_OK(this.Client);
            loginOk.SetAccountId(level.GetPlayerAvatar().GetId());
            loginOk.SetPassToken(this.Token);
            loginOk.SetServerMajorVersion(this.Version[0]);
            loginOk.SetServerBuild(this.Version[1]);
            loginOk.SetContentVersion(this.Version[2]);
            loginOk.SetServerEnvironment("prod");
            loginOk.SetDaysSinceStartedPlaying(10);
            loginOk.SetServerTime(
                Math.Round(level.GetTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000).ToString());
            loginOk.SetAccountCreatedDate(level.GetPlayerAvatar().GetCreated().ToString());
            loginOk.SetStartupCooldownSeconds(0);
            loginOk.SetCountryCode("ES");
            loginOk.Send();

            if (level.GetPlayerAvatar().GetBattleID() > 0)
            {
                if (ResourcesManager.Battles.ContainsKey(level.GetPlayerAvatar().GetId()))
                {
                    var SectorPC = new Sector_PC(this.Client)
                                       {
                                           Battle = ResourcesManager.Battles[level.GetPlayerAvatar().GetId()]
                                       };
                    SectorPC.Send();

                    this.Client.State = State.IN_BATTLE;
                }
                else
                {
                    level.GetPlayerAvatar().SetBattleID(0);
                }
            }
            else
            {
                new Own_Home_Data(this.Client).Send();
                
                if (level.GetPlayerAvatar().GetClan())
                {
                    new Clan_Data(this.Client).Send();
                }
            }
        }
    }
}