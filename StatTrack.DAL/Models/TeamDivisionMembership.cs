using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StatTrack.DAL.Models.Enums;

namespace StatTrack.DAL.Models
{

	public class TeamDivisionMembership
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeamDivisionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeamRoleId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public TeamDivisionMembershipStatus Status { get; set; }

        public DateTime? LockedSince { get; set; }

		public int? LockedByUserId { get; set; }

		[ForeignKey("LockedByUserId")]
		public User LockedByUser { get; set; }

		[ForeignKey("TeamRoleId")]
		public virtual TeamDivisionRole TeamDivisionRole { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		[ForeignKey("TeamDivisionId")]
		public virtual TeamDivision TeamDivision { get; set; }
    }
}
