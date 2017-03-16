namespace UCS.Logic.Slots
{
	#region Usings

	using System.Collections.Generic;
	using System.Configuration;
	using Core;
	using Core.Database;
	using Core.Settings;
	using Enums;

	// using Core.Database.Models;

	#endregion

	internal class Players : Dictionary<long, Level>
	{
		/// <summary>
		/// The seed is the latest player id stored in the database, plus one.
		/// </summary>
		public long Seed = 0;

		public new void Add(Level _Player)
		{
			if (this.ContainsKey(_Player.GetPlayerAvatar().GetId()))
			{
				this[_Player.GetPlayerAvatar().GetId()] = _Player;
			}
			else
			{
				this.Add(_Player.GetPlayerAvatar().GetId(), _Player);
			}
		}

		/// <summary>
		/// <see cref="Remove" /> the specified player from the list.
		/// </summary>
		/// <param name="_Player">The player.</param>
		public new void Remove(Level _Player)
		{
			if (this.ContainsKey(_Player.GetPlayerAvatar().GetId()))
			{
				this.Remove(_Player.GetPlayerAvatar().GetId());
			}
		}

		/// <summary>
		/// <see cref="Remove" /> the specified player from the list.
		/// </summary>
		/// <param name="_PlayerID">The player identifier.</param>
		public new void Remove(long _PlayerID)
		{
			if (this.ContainsKey(_PlayerID))
			{
				base.Remove(_PlayerID);
			}
		}
	}
}