namespace UCS.Logic
{
    #region Usings

    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #endregion

    internal class Level
    {
        public GameObjectManager GameObjectManager; // a1 + 44

        private byte m_vAccountPrivileges;

        private byte m_vAccountStatus;

        private Device m_vClient;

        private ClientAvatar m_vClientAvatar;

        private DateTime m_vTime; // a1 + 40

        public Level()
        {
            this.GameObjectManager = new GameObjectManager(this);
            this.m_vClientAvatar = new ClientAvatar();
            this.m_vAccountPrivileges = 0;
            this.m_vAccountStatus = 0;
        }

        public Level(long id)
        {
            this.GameObjectManager = new GameObjectManager(this);
            this.m_vClientAvatar = new ClientAvatar(id);
            this.m_vTime = DateTime.UtcNow;
            this.m_vAccountPrivileges = 0;
            this.m_vAccountStatus = 0;
        }

        public byte GetAccountPrivileges()
        {
            return this.m_vAccountPrivileges;
        }

        public byte GetAccountStatus()
        {
            return this.m_vAccountStatus;
        }

        public Device GetClient()
        {
            return this.m_vClient;
        }

        public ClientAvatar GetHomeOwnerAvatar()
        {
            return this.m_vClientAvatar;
        }

        public ClientAvatar GetPlayerAvatar()
        {
            return this.m_vClientAvatar;
        }

        public DateTime GetTime()
        {
            return this.m_vTime;
        }

        public void LoadFromJSON(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);
            this.GameObjectManager.Load(jsonObject);
        }

        public string SaveToJSON()
        {
            return JsonConvert.SerializeObject(this.GameObjectManager.Save());
        }

        public void SetAccountPrivileges(byte privileges)
        {
            this.m_vAccountPrivileges = privileges;
        }

        public void SetAccountStatus(byte status)
        {
            this.m_vAccountStatus = status;
        }

        public void SetClient(Device client)
        {
            this.m_vClient = client;
        }

        public void SetHome(string jsonHome)
        {
            GameObjectManager.Load(JObject.Parse(jsonHome));
        }

        public void SetTime(DateTime t)
        {
            this.m_vTime = t;
        }

        public void Tick()
        {
            this.SetTime(DateTime.UtcNow);
            this.GameObjectManager.Tick();
        }
    }
}