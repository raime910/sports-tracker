using Microsoft.AspNet.Identity.EntityFramework;

namespace StatTrack.DAL.Models
{
	/// <summary>
	/// A type of user role.
	/// </summary>
	public class Role : IdentityRole<int, UserRole>
	{
		public const string NAME_SITE_OWNER = "Site Owner";
		public const string NAME_ADMIN = "Administrator";
		public const string NAME_MOD = "Moderator";
		public const string NAME_USER = "User";
		public const string NAME_SUSPEND = "Suspended";
		public const string NAME_BANNED = "Banned";

		public const int ACCLEVEL_SITE_OWNER = 100;
		public const int ACCLEVEL_ADMIN = 90;
		public const int ACCLEVEL_MOD = 80;
		public const int ACCLEVEL_USER = 30;
		public const int ACCLEVEL_SUSPEND = 20;
		public const int ACCLEVEL_BANNED = 10;

		public int AccessLevel { get; set; }
	}
}
