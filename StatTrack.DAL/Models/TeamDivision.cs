using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{
	public class TeamDivision
	{
		public int Id { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }

		public int SportId { get; set; }

		public int TeamId { get; set; }

		[ForeignKey("SportId")]
		public Sport Sport { get; set; }

		[ForeignKey("TeamId")]
		public Team Team { get; set; }

		public virtual ICollection<TeamDivisionMembership> TeamDivisionMemberships { get; set; }
	}
}
