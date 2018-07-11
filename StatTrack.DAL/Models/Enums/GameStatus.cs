namespace StatTrack.DAL.Models.Enums
{
	/// <summary>
	/// Status of the team.
	/// </summary>
	public enum GameStatus
	{
		/// <summary>
		/// Game is pending from approval.
		/// </summary>
		Pending = 0,
		/// <summary>
		/// Game is active.
		/// </summary>
		Active = 1,
		/// <summary>
		/// Game is inactive.
		/// </summary>
		Inactive = 2
	}
}
