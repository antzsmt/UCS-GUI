namespace UCS.Logic.Slots
{
    using System.Collections.Generic;

    internal class Tournaments : Dictionary<long, Tournament>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Tournaments"/> class.
        /// </summary>
        public Tournaments() : base()
        {
            // Tournaments.
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Tournaments"/> class.
        /// </summary>
        /// <param name="_Max">The maximum of tournaments in the list.</param>
        public Tournaments(int _Max) : base(_Max)
        {
            // Tournaments.
        }

        /// <summary>
        /// Add the specified tournament to the list.
        /// </summary>
        /// <param name="_Tournament">The tournament.</param>
        public new void Add(Tournament _Tournament)
        {
            if (this.ContainsKey(_Tournament.TournamentID))
            {
                this[_Tournament.TournamentID] = _Tournament;
            }
            else
            {
                this.Add(_Tournament.TournamentID, _Tournament);
            }
        }

        /// <summary>
        /// Remove the specified tournament from the list.
        /// </summary>
        /// <param name="_Tournament">The tournament.</param>
        public new void Remove(Tournament _Tournament)
        {
            if (this.ContainsKey(_Tournament.TournamentID))
            {
                this.Remove(_Tournament.TournamentID);
            }
        }
    }
}
