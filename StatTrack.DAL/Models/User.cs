using Microsoft.AspNet.Identity.EntityFramework;
using StatTrack.DAL.Models.Enums;
using System;
using System.Collections.Generic;

namespace StatTrack.DAL.Models
{
	public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
	{
		public DateTime RegisterDate { get; set; }

		public AccountStatus AccountStatus { get; set; }

		public string EmailConfirmationToken { get; set; }

		public string PasswordResetToken { get; set; }

		public virtual UserProfile UserProfile { get; set; }

		public virtual ICollection<Team> OwnedTeams { get; set; }

		public virtual ICollection<TeamDivisionMembership> TeamDivisionMemberships { get; set; }
	}
}
