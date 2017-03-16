namespace UCS.Logic.Slots
{
    using System.Collections.Generic;

    using UCS.Logic.Slots.Items;

    internal class Members : Dictionary<long, Member>
    {
        private long m_vAvatarId;

        /// <summary>
        /// Initialize a new instance of the <see cref="Members"/> class.
        /// </summary>
        public Members() : base()
        {
            // Members.
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Members"/> class.
        /// </summary>
        /// <param name="_Max">The maximum of players in the list.</param>
        public Members(int _Max) : base(_Max)
        {
            // Members.
        }

        /// <summary>
        /// Add the specified member to the clan.
        /// </summary>
        /// <param name="_Member">The member.</param>
        public new void Add(Member _Member)
        {
            if (this.ContainsKey(_Member.PlayerID))
            {
                this[_Member.PlayerID] = _Member;
            }
            else
            {
                this.Add(_Member.PlayerID, _Member);
            }
        }

        /// <summary>
        /// Add the specified player to the clan.
        /// </summary>
        /// <param name="_Player">The player.</param>
        public void Add(ClientAvatar _Player)
        {
            Member _Member = new Member(_Player);

            if (this.ContainsKey(_Member.PlayerID))
            {
                this[_Member.PlayerID] = _Member;
            }
            else
            {
                this.Add(_Member.PlayerID, _Member);
            }
        }

        /// <summary>
        /// Remove the specified player from the clan.
        /// </summary>
        /// <param name="_Player">The player.</param>
        public new void Remove(ClientAvatar _Player)
        {
            if (this.ContainsKey(_Player.GetId()))
            {
                this.Remove(_Player.GetId());
            }
        }
        

        /// <summary>
        /// Remove the specified member from the clan.
        /// </summary>
        /// <param name="_Member">The member.</param>
        public new void Remove(Member _Member)
        {
            if (this.ContainsKey(_Member.PlayerID))
            {
                this.Remove(_Member.PlayerID);
            }
        }

        public long GetAvatarId()
        {
            return this.m_vAvatarId;
        }
    }
}
