using StatTrack.DAL.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{
	public class LeagueSet
	{
		public int Id { get; set; }

        public int LeagueId { get; set; }

        public int PhaseId { get; set; }

        public LeagueSetStatus Status { get; set; }

		[ForeignKey("PhaseId")]
		public virtual LeaguePhase LeaguePhase { get; set; }

		[ForeignKey("LeagueId")]
		public virtual League League { get; set; }
    }
}
