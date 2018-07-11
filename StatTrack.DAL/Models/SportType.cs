using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{

	public class SportType
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

		public int? LockedByUserId { get; set; }

		[ForeignKey("LockedByUserId")]
		public User LockedByUser { get; set; }
	}
}
