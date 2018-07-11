using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{
	public class UserProfile
    {
		[Key, ForeignKey("User")]
		public int UserId { get; set; }

		public User User { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

		[Column(TypeName = "date")]
		public DateTime? LastUpdateDate { get; set; }

		[DefaultValue(false)]
		public bool SubscribeNewsletter { get; set; }

		public int? CountryId { get; set; }

		public string Bio { get; set; }

		public string AvatarUrl { get; set; }

		public virtual ICollection<TeamDivisionMembership> TeamDivisionMemberships { get; set; }
    }
}
