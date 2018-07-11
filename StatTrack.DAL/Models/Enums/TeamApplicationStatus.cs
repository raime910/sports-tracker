namespace StatTrack.DAL.Models.Enums
{
	/// <summary>
	/// Status of the person who applied for a player slot.
	/// </summary>
	public enum TeamApplicationStatus
	{
		/// <summary>
		/// Waiting for email or other form of confirmation.
		/// </summary>
		Pending = 0,
		/// <summary>
		/// Team membership application was accepted.
		/// </summary>
		Accepted = 1,
		/// <summary>
		/// Team membership application is rejected.
		/// </summary>
		Rejected = 2,
		/// <summary>
		/// Team membership is suspended.
		/// </summary>
		Suspended = 3
	}

}
