using StatTrack.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{

	public class Team
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

        public DateTime? LockedSince { get; set; }

		[Required]
		public DateTime DateCreated { get; set; }

		[Required]
		public int OwnerId { get; set; }

		[ForeignKey("OwnerId")]
		public User Owner { get; set; }

		public int? LockedByUserId { get; set; }

		[ForeignKey("LockedByUserId")]
		public User LockedByUser { get; set; }

		public string BannerImageUrl { get; set; }

		public string LogoImageUrl { get; set; }

		public TeamStatus Status { get; set; }

		public ICollection<TeamDivision> TeamDivisions { get; set; }
    }
}
