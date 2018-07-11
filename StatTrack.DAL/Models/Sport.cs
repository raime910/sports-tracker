using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StatTrack.DAL.Models.Enums;

namespace StatTrack.DAL.Models
{

	public class Sport
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

        public SportStatus Status { get; set; }

        public int SportTypeId { get; set; }

        public DateTime? LockedSince { get; set; }

		public int? LockedByUserId { get; set; }

		[ForeignKey("LockedByUserId")]
		public virtual User LockedByUser { get; set; }

		[ForeignKey("SportTypeId")]
		public virtual SportType SportType { get; set; }

		public virtual ICollection<League> Leagues { get; set; }
		
        public virtual ICollection<TeamDivisionRole> TeamRoles { get; set; }
    }
}
