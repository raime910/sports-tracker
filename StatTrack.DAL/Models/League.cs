using StatTrack.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{

	public class League
    {
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

        public int SportId { get; set; }

        public DateTime? LockedSince { get; set; }

		public int? LockedByUserId { get; set; }

		[ForeignKey("LockedByUserId")]
		public User LockedByUser { get; set; }

		public LeagueStatus Status { get; set; }

		[ForeignKey("LeagueTypeId")]
		public virtual LeagueType LeagueType { get; set; }

		[ForeignKey("SportId")]
		public virtual Sport Sport { get; set; }
		
        public virtual ICollection<LeagueSet> LeagueSets { get; set; }
    }
}
