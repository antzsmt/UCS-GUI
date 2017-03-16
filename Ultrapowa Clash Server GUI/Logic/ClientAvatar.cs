namespace UCS.Logic
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core;
    using Core.Settings;

    using Enums;

    using Extensions.List;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Packets;

    using Slots;
    using Slots.Items;

    using Resource = Slots.Items.Resource;

    #endregion

    internal class ClientAvatar : Avatar
    {
        private int m_Arena;

        private bool m_Banned;

        private DateTime m_BanTime;

        private int m_BattleID;

        private byte m_Changes;

        private bool m_Clan;

        private long m_ClanID;

        private DateTime m_Created;

        private int m_CurrentGems;

        private int m_Donations;

        private int m_Experience;

        private int m_FreeGems;

        private long m_Id = 0;

        private bool m_IsAndroid;

        private byte m_IsAvatarNameSet;

        private int m_League;

        private int m_Legendary_Trophies;

        private int m_Level;

        private int m_Loses;

        private bool m_Muted;

        private DateTime m_MuteTime;

        private string m_Name;

        private string m_Pass;

        private TimeSpan m_PlayTime;

        private int m_Rank;

        private string m_Region;

        private int m_Report;

        private int m_Score;

        private int m_Status;

        private string m_Token;

        private int m_Trophies;

        private byte m_Tutorial;

        private DateTime m_Update;

        private int m_Wins;

        public ClientAvatar() : base()
        {
            this.Achievements = new Achievements();
            this.Resources = new Resources();
            this.Boutique = new Boutique();
            this.Chests = new Chests();
            this.Deck = new Deck();

            // this.AllianceUnits = new List<DataSlot>();
            // this.NpcStars = new List<DataSlot>();
            // this.NpcLootedGold = new List<DataSlot>();
            // this.NpcLootedElixir = new List<DataSlot>();
            // this.Arena = new List<DataSlot>();
            // m_LeagueId = 9;
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="ClientAvatar" /> class.
        /// </summary>
        /// <param name="Id">The player identifier.</param>
        public ClientAvatar(long Id) : this()
        {
            //this.Client = _Device;
            this.m_Id = Id;
            this.LastUpdate = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            this.Login = Id.ToString()
                         + ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            this.m_IsAvatarNameSet = 0x00;
            this.m_Level = 1;
            this.m_ClanID = 0;
            this.m_Experience = 0;
            this.m_CurrentGems = Convert.ToInt32(Settings.StartingGems);
            this.m_Trophies = Convert.ToInt32(Settings.StartingTrophies);
            this.TutorialStepsCount = 0x00; // 0x0A 10
            this.m_Name = "NoNameYet";
            this.m_Rank = (int)Rank.Player;
            this.m_Status = (int)Status.ACTIVE;
            this.m_Arena = (int)Arena.TRAINING_CAMP;
            this.Resources.Set(Enums.Resource.GEMS, Settings.StartingGems);
            this.Resources.Set(Enums.Resource.GOLD, Settings.StartingGold);

            this.Resources.Set(Enums.Resource.MAX_TROPHIES, Settings.StartingTrophies);
            this.Resources.Set(Enums.Resource.CARD_COUNT, this.Deck.Count);

            // this.Resources.Set(Enums.Resource.MAX_TROPHIES, this.Trophies);
            // this.Resources.Set(Enums.Resource.CARD_COUNT, this.Deck.Count);
            this.Boutique.Add(new Shop(1, 0, 26, 39, 0, DateTime.Today.AddDays(1)));
            this.Boutique.Add(new Shop(4, 0, 26, 0, 0, DateTime.Today.AddDays(1)));
            this.Boutique.Add(new Shop(1, 0, 26, 1, 0, DateTime.Today.AddDays(1)));
            this.Boutique.Add(new Shop(1, 0, 26, 2, 0, DateTime.Today.AddDays(1)));
            this.Boutique.Add(new Shop(4, 1, 26, 3, 0, DateTime.Today.AddDays(1)));
            this.Boutique.Add(new Shop(1, 2, 26, 4, 0, DateTime.Today.AddDays(1)));
            this.Boutique.Add(new Shop(3, 0, 0, 0, 0, DateTime.Today.AddDays(1)));
        }

        public static Status Estatus { get; set; }

        public static Rank Ranks { get; set; }

        public Achievements Achievements { get; set; }

        public Boutique Boutique { get; set; }

        public Chests Chests { get; set; }

        public Deck Deck { get; set; }

        public int LastUpdate { get; set; }

        public string Login { get; set; }

        public Resources Resources { get; set; }

        public uint TutorialStepsCount { get; set; }

        public byte[] Data_Part1()
        {
            int TimeStamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            List<byte> _Packet = new List<byte>();

            _Packet.AddLong(this.m_Id);

            _Packet.Add(16);
            _Packet.Add(0);
            _Packet.AddVInt(1698340);
            _Packet.AddVInt(1727920);
            _Packet.AddVInt(TimeStamp);
            _Packet.Add(0);

            _Packet.Add(1);
            {
                _Packet.AddVInt(8);

                foreach (Card _Card in this.Deck.GetRange(0, 8))
                {
                    _Packet.AddVInt(_Card.GlobalID);
                }
            }

            _Packet.Add(255);
            _Packet.AddRange(this.Deck.ToBytes());

            _Packet.AddVInt(this.Deck.Count - 8);
            foreach (Card _Card in this.Deck.Skip(8))
            {
                _Packet.AddVInt(_Card.Type);
                _Packet.AddVInt(_Card.ID);
                _Packet.AddVInt(_Card.Level);
                _Packet.AddVInt(0);
                _Packet.AddVInt(_Card.Count);
                _Packet.AddVInt(0);
                _Packet.AddVInt(0);
                _Packet.AddVInt(_Card.New);
            }

            _Packet.Add(0);
            _Packet.Add(4);

            _Packet.AddRange(this.Chests.Encode());

            _Packet.AddVInt(287600);
            _Packet.AddVInt(288000);

            _Packet.AddVInt(TimeStamp);

            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(127);

            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);

            _Packet.AddVInt(5); // Crown
            _Packet.AddVInt(0); // 0 = Unlocked    1 = locked
            _Packet.AddVInt(360 * 20); // Time from unlock crown chest

            _Packet.AddRange("A4F4D201".HexaToBytes());
            _Packet.AddVInt(TimeStamp);
            _Packet.AddVInt(0);

            _Packet.AddRange("A4F4D201".HexaToBytes());
            _Packet.AddVInt(TimeStamp);
            _Packet.AddVInt(0);

            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(127);

            _Packet.AddVInt(1); // 0 = Tuto Upgrade Spell
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(2); // 0, 1 = Animation Page Card (Tuto)

            _Packet.AddVInt(this.m_Level);
            _Packet.Add(0x36);
            _Packet.AddVInt((int)this.m_Arena);

            _Packet.AddVInt(736968123); // Shop ID
            _Packet.AddVInt((int)DateTime.UtcNow.DayOfWeek + 1);
            _Packet.AddVInt((int)this.m_Update.DayOfWeek + 1);

            int _Time = (int)(DateTime.UtcNow.AddDays(1) - DateTime.UtcNow).TotalSeconds;
            _Packet.AddVInt(20 * _Time);
            _Packet.AddVInt(20 * _Time);

            _Packet.AddVInt(TimeStamp);

            _Packet.AddRange(this.Boutique.EncodeCard());
            _Packet.AddRange(this.Boutique.EncodeOffer());
            _Packet.AddRange(new byte[]
            {
                0x00, 0x00, 0x7F,
                0x00, 0x00, 0x7F,
                0x00, 0x00, 0x7F
            });

            _Packet.AddInt(0);
            _Packet.AddInt(0);
            _Packet.AddInt(9);
            _Packet.AddInt(0);

            _Packet.AddRange("F801".HexaToBytes()); // Prefixe from Deck

            _Packet.AddRange(new byte[]
            {
                0x1A, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1A, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1A, 0x0D, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1C, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1C, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1A, 0x03, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00
            });

            _Packet.AddVInt(TimeStamp);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddInt(1);
            _Packet.AddVInt(TimeStamp);

            return _Packet.ToArray();
        }

        /// <summary>
        /// Encode the second part.
        /// </summary>
        /// <returns>THe encoded second part.</returns>
        public byte[] Data_Part2()
        {
            List<byte> _Packet = new List<byte>();

            _Packet.AddVInt(this.m_Id);
            _Packet.AddVInt(this.m_Id);
            _Packet.AddVInt(this.m_Id);

            _Packet.AddString(this.m_Name);
            _Packet.AddVInt(this.m_Changes);
            _Packet.AddVInt(0x36); // Arena Data
            _Packet.AddVInt((int)this.m_Arena);
            _Packet.AddVInt(this.m_Trophies);

            _Packet.AddInt(0);

            _Packet.Add(0);
            _Packet.AddVInt(0); // Rank
            _Packet.AddVInt(this.m_Trophies);
            _Packet.AddVInt(this.m_Legendary_Trophies); // Legendary Trophies

            _Packet.AddVInt(this.Resources.Count);
            _Packet.AddVInt(this.Resources.Count);

            foreach (Resource _Resource in this.Resources.OrderBy(r => r.Data))
            {
                _Packet.AddVInt(_Resource.Type);
                _Packet.AddVInt(_Resource.Data);
                _Packet.AddVInt(_Resource.Value);
            }

            _Packet.Add(0);

            _Packet.AddVInt(this.Achievements.Count);
            foreach (Achievement _Achievement in this.Achievements)
            {
                _Packet.AddVInt(_Achievement.Type);
                _Packet.AddVInt(_Achievement.Data);
                _Packet.AddVInt(_Achievement.Value);
            }

            _Packet.AddVInt(0); // Completed Achievements
            _Packet.AddVInt(0); // Unknown Count

            _Packet.Add(0);

            _Packet.Add(0);

            _Packet.AddVInt(this.Resources[0].Value);
            _Packet.AddVInt(this.Resources[0].Value);
            _Packet.AddVInt(this.m_Donations);
            _Packet.AddVInt(this.m_Level);

            _Packet.Add(0);

            if (this.m_Clan)
            {
                // 8 = Set name popup + clan
                // 9 = Name already set + clan
                // < 8 =  Set name popup

                _Packet.Add(9);

                _Packet.AddVInt(this.m_ClanID);
                _Packet.AddString("ClashRoyaleSpain");
                _Packet.AddVInt(0x10);
                _Packet.AddVInt(16);
            }
            else
            {
                _Packet.Add(0);
            }

            _Packet.Add(this.m_IsAvatarNameSet);

            _Packet.Add(0);
            _Packet.Add(0);
            _Packet.Add(0);
            _Packet.Add(0);
            _Packet.Add(0);

            _Packet.AddVInt(this.m_Tutorial);

            _Packet.Add(0);
            _Packet.Add(0);

            return _Packet.ToArray();
        }


        public int GetArena()
        {
            return this.m_Arena;
        }

        public int GetAvatarLevel()
        {
            return this.m_Level;
        }

        public string GetAvatarName()
        {
            return this.m_Name;
        }

        public int GetBattleID()
        {
            return this.m_BattleID;
        }

        public bool GetClan()
        {
            return this.m_Clan;
        }

        public long GetClanID()
        {
            return this.m_ClanID;
        }

        public DateTime GetCreated()
        {
            return this.m_Created;
        }

        public int GetDiamonds()
        {
            return this.m_CurrentGems;
        }

        public long GetId()
        {
            return this.m_Id;
        }

        public bool GetIsAndroid()
        {
            return this.m_IsAndroid;
        }

        public int GetLeagueId()
        {
            return this.m_League;
        }

        public string GetPass()
        {
            return this.m_Pass;
        }

        public Rank GetRank()
        {
            return Ranks;
        }

        public string GetRegion()
        {
            return this.m_Region;
        }

        public int GetSecondsFromLastUpdate()
        {
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - this.LastUpdate;
        }

        public Status GetStatus()
        {
            return Estatus;
        }

        public string GetToken()
        {
            return this.m_Token;
        }

        public int GetTrophies()
        {
            return this.m_Trophies;
        }

        public DateTime GetUpdate()
        {
            return this.m_Update;
        }

        public bool HasEnoughDiamonds(int diamondCount)
        {
            return this.m_CurrentGems >= diamondCount;
        }

        public void LoadFromJSON(string jsonString)
        {
            JObject _Json = JObject.Parse(jsonString);

            this.m_Id = _Json["player_id"].ToObject<long>();
            this.m_Id = _Json["home_id"].ToObject<long>();
            this.m_Id = _Json["backup_id"].ToObject<long>();
            this.m_ClanID = _Json["clan_id"].ToObject<long>();

            this.m_Token = _Json["token"].ToObject<string>();
            this.m_Pass = _Json["password"].ToObject<string>();
            this.m_Name = _Json["name"].ToObject<string>();
            this.m_Region = _Json["region"].ToObject<string>();

            this.m_Level = _Json["level"].ToObject<int>();
            this.m_Experience = _Json["experience"].ToObject<int>();
            this.m_Trophies = _Json["trophies"].ToObject<int>();
            this.m_CurrentGems = _Json["Gems"].ToObject<int>();
            this.m_Wins = _Json["wins"].ToObject<int>();
            this.m_Loses = _Json["loses"].ToObject<int>();
            this.m_Report = _Json["report"].ToObject<int>();
            this.m_Donations = _Json["donations"].ToObject<int>();
            this.m_Legendary_Trophies = _Json["legendary_trophies"].ToObject<int>();

            this.m_Arena = _Json["arena"].ToObject<int>();
            this.m_Rank = _Json["rank"].ToObject<int>();

            this.m_Tutorial = _Json["tutorial"].ToObject<byte>();
            this.m_Changes = _Json["changes"].ToObject<byte>();
            this.m_IsAvatarNameSet = _Json["name_set"].ToObject<byte>();

            this.m_IsAndroid = _Json["android"].ToObject<bool>();
            this.m_Clan = _Json["clan"].ToObject<bool>();
            this.m_Banned = _Json["banned"].ToObject<bool>();
            this.m_Muted = _Json["muted"].ToObject<bool>();

            this.m_Update = _Json["update"].ToObject<DateTime>();
            this.m_Created = _Json["created"].ToObject<DateTime>();
            this.m_BanTime = _Json["ban_time"].ToObject<DateTime>();
            this.m_MuteTime = _Json["mute_time"].ToObject<DateTime>();
        }

        /// <summary>
        /// Serialize this instance.
        /// </summary>
        /// <returns>
        /// The player data in JSON.
        /// </returns>
        public string SaveToJSON()
        {
            JObject _JSON = new JObject();

            _JSON.Add("player_id", this.m_Id);
            _JSON.Add("home_id", this.m_Id);
            _JSON.Add("backup_id", this.m_Id);
            _JSON.Add("clan_id", this.m_ClanID);

            _JSON.Add("token", this.m_Token);
            _JSON.Add("password", this.m_Pass);
            _JSON.Add("name", this.m_Name);
            _JSON.Add("region", this.m_Region);

            _JSON.Add("level", this.m_Level);
            _JSON.Add("experience", this.m_Experience);
            _JSON.Add("trophies", this.m_Trophies);
            _JSON.Add("Gems", this.m_CurrentGems);
            _JSON.Add("wins", this.m_Wins);
            _JSON.Add("loses", this.m_Loses);
            _JSON.Add("report", this.m_Report);
            _JSON.Add("donations", this.m_Donations);
            _JSON.Add("legendary_trophies", this.m_Legendary_Trophies);

            _JSON.Add("arena", (int)this.m_Arena);
            _JSON.Add("rank", (int)this.m_Rank);

            _JSON.Add("tutorial", this.m_Tutorial);
            _JSON.Add("changes", this.m_Changes);
            _JSON.Add("name_set", this.m_IsAvatarNameSet);

            _JSON.Add("android", this.m_IsAndroid);
            _JSON.Add("clan", this.m_Clan);
            _JSON.Add("banned", this.m_Banned);
            _JSON.Add("muted", this.m_Muted);

            _JSON.Add("update", this.m_Update);
            _JSON.Add("created", this.m_Created);
            _JSON.Add("ban_time", this.m_BanTime);
            _JSON.Add("mute_time", this.m_MuteTime);

            return JsonConvert.SerializeObject(_JSON);
        }

        public void SetBattleID(int battleID)
        {
            this.m_BattleID = battleID;
        }

        // public bool HasEnoughResources(Resources rd, int buildCost)
        // {
        // return GetResourceCount(rd) >= buildCost;
        // }
        public void SetClanId(long id)
        {
            this.m_ClanID = id;
        }

        public void SetDiamonds(int count)
        {
            this.m_CurrentGems = count;
        }

        public void SetIsAndroid(bool isAndroid)
        {
            this.m_IsAndroid = isAndroid;
        }

        public void SetLeagueId(int id)
        {
            this.m_League = id;
        }

        public void SetName(string name)
        {
            this.m_Name = name;
            this.m_IsAvatarNameSet = 0x01;
            this.TutorialStepsCount = 0x0D;
        }

        public void SetRank(int rank)
        {
            //Ranks = rank;
            this.m_Rank = rank;
        }

        public void SetRegion(string region)
        {
            this.m_Region = region;
        }

        public void SetStatus(int status)
        {
            //Estatus = status;
            this.m_Status = status;
        }

        public void SetToken(string token)
        {
            this.m_Token = token;
        }

        public void SetUpdate(DateTime update)
        {
            this.m_Update = update;
        }

        public void SetCreated(DateTime created)
        {
            this.m_Created = created;
        }

        public void UseDiamonds(int diamondCount)
        {
            this.m_BattleID -= diamondCount;
        }
    }
}