namespace StatTrack.DAL.Models.Enums
{
	/// <summary>
	/// Possible results from a sign in attempt,
	/// </summary>
	public enum SignInStatus
	{
		/// <summary>
		/// Sign in failed.
		/// </summary>
		Failed = 0,
		/// <summary>
		/// Sign in was successful.
		/// </summary>
		Success = 1,
		/// <summary>
		/// User is locked out.
		/// </summary>
		LockedOut = 2,
		/// <summary>
		/// Sign in requires addition verification (i.e. two factor).
		/// </summary>
		RequiresVerification = 3,
	}
}
