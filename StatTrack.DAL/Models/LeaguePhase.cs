using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{

	public class LeaguePhase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

		[Required]
		[StringLength(10)]
		public string Code { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		public int LeagueTypeId { get; set; }

		public DateTime? LockedSince { get; set; }

		public int? LockedByUserId { get; set; }

		[ForeignKey("LockedByUserId")]
		public User LockedByUser { get; set; }

		[ForeignKey("LeagueTypeId")]
		public virtual LeagueType LeagueType { get; set; }

		public virtual ICollection<LeagueSet> LeagueSets { get; set; }

	}
}
