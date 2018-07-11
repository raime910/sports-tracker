using StatTrack.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{

	public class TeamDivisionRole
	{
		public const string NAME_OWNER = "Team Owner";
		public const string NAME_MOD = "Moderator";
		public const string NAME_PLAYER = "Player";
		public const string NAME_SUSPEND = "Suspended";
		public const string NAME_BANNED = "Banned";

		public const int ACCLEVEL_OWNER = 90;
		public const int ACCLEVEL_MOD = 80;
		public const int ACCLEVEL_PLAYER = 30;
		public const int ACCLEVEL_SUSPEND = 20;
		public const int ACCLEVEL_BANNED = 10;
		
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public int SportId { get; set; }

		public DateTime? LockedSince { get; set; }

		public int? LockedByUserId { get; set; }

		public int AccessLevel { get; set; }

		[ForeignKey("LockedByUserId")]
		public User LockedByUser { get; set; }

		public TeamDivisionRoleStatus Status { get; set; }

		[ForeignKey("SportId")]
		public virtual Sport Sport { get; set; }
		
		public virtual ICollection<TeamDivisionMembership> TeamDivisionMembership { get; set; }
	}
}
