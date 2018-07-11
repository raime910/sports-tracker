namespace StatTrack.DAL.Models.Enums
{
	/// <summary>
	/// Status of the team that is registering on a league.
	/// </summary>
	public enum LeagueTeamStatus
	{
		/// <summary>
		/// Waiting for email or other form of confirmation.
		/// </summary>
		Pending = 0,
		/// <summary>
		/// Team registration for the league was accepted by the organizer.
		/// </summary>
		Accepted = 1,
		/// <summary>
		/// Team registration for the league was declined by the organizer.
		/// </summary>
		Declined = 2,
		/// <summary>
		/// User was banned from the league.
		/// </summary>
		Banned = 3
	}
}
