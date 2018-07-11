namespace StatTrack.DAL.Models.Enums
{
	/// <summary>
	/// Status of the account holder.
	/// </summary>
	public enum AccountStatus
	{
		/// <summary>
		/// Waiting for email or other form of confirmation.
		/// </summary>
		Pending = 0,
		/// <summary>
		/// Account activated.
		/// </summary>
		Active = 1,
		/// <summary>
		/// Account deactivated.
		/// </summary>
		Inactive = 2,
		/// <summary>
		/// Account banned from the website.
		/// </summary>
		Banned = 3
	}
}
