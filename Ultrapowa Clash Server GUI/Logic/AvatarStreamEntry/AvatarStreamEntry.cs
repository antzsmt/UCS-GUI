namespace UCS.Logic
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using UCS.Extensions.List;
    using UCS.Helpers;

    #endregion

    class AvatarStreamEntry
    {
        private int m_vId;

        private long m_vSenderId;

        private string m_vSenderName;

        private int m_vSenderLevel;

        private int m_vSenderLeagueId;

        private DateTime m_vCreationTime;

        private byte m_vIsNew;

        //private byte m_vIsRemoved;

        public AvatarStreamEntry()
        {
            this.m_vCreationTime = DateTime.UtcNow;
        }

        public int GetAgeSeconds()
        {
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                   - (int)this.m_vCreationTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public int GetId()
        {
            return this.m_vId;
        }

        public long GetSenderAvatarId()
        {
            return this.m_vSenderId;
        }

        public int GetSenderLevel()
        {
            return this.m_vSenderLevel;
        }

        public string GetSenderName()
        {
            return this.m_vSenderName;
        }

        public virtual int GetStreamEntryType()
        {
            return -1;
        }

        public virtual byte[] Encode()
        {
            List<Byte> data = new List<Byte>();

            data.AddInt32(this.GetStreamEntryType()); //alliancemailstreamentry
            data.AddInt32(0);
            data.AddInt32(this.m_vId);
            data.AddInt64(this.m_vSenderId);
            data.AddString(this.m_vSenderName);
            data.AddInt32(this.m_vSenderLevel);
            data.AddInt32(this.m_vSenderLeagueId);
            data.Add(this.m_vIsNew);

            return data.ToArray();
        }

        public byte IsNew()
        {
            return this.m_vIsNew;
        }

        public void SetAvatar(ClientAvatar avatar)
        {
            this.m_vSenderId = avatar.GetId();
            this.m_vSenderName = avatar.GetAvatarName();
            this.m_vSenderLevel = avatar.GetAvatarLevel();
            this.m_vSenderLeagueId = avatar.GetLeagueId();
        }

        public void SetId(int id)
        {
            this.m_vId = id;
        }

        public void SetIsNew(byte isNew)
        {
            this.m_vIsNew = isNew;
        }

        public void SetSenderLeagueId(int id)
        {
            this.m_vSenderLeagueId = id;
        }

        /*public void SetRemoved(byte removed)
        {
            m_vIsRemoved = removed;
        }*/

        public void SetSenderAvatarId(long id)
        {
            this.m_vSenderId = id;
        }

        public void SetSenderLevel(int level)
        {
            this.m_vSenderLevel = level;
        }

        public void SetSenderName(string name)
        {
            this.m_vSenderName = name;
        }
    }
}