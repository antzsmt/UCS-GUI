namespace UCS.Logic.Slots
{
    using System.Collections.Generic;

    internal class Clans : Dictionary<long, Clan>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Clans"/> class.
        /// </summary>
        public Clans() : base()
        {
            // Clans.
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Clans"/> class.
        /// </summary>
        /// <param name="_Max">The maximum of clans in the list.</param>
        public Clans(int _Max) : base(_Max)
        {
            // Clans.
        }

        /// <summary>
        /// Add the specified clan to the list.
        /// </summary>
        /// <param name="_Clan">The clan.</param>
        public new void Add(Clan _Clan)
        {
            if (this.ContainsKey(_Clan.ClanID))
            {
                this[_Clan.ClanID] = _Clan;
            }
            else
            {
                this.Add(_Clan.ClanID, _Clan);
            }
        }

        /// <summary>
        /// Remove the specified clan from the list.
        /// </summary>
        /// <param name="_Clan">The clan.</param>
        public new void Remove(Clan _Clan)
        {
            if (this.ContainsKey(_Clan.ClanID))
            {
                this.Remove(_Clan.ClanID);
            }
        }
    }
}
