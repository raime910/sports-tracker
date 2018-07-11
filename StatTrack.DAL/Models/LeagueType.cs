using StatTrack.DAL.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StatTrack.DAL.Models
{

	public class LeagueType
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

        public LeagueTypeStatus Status { get; set; }
		
        public virtual ICollection<LeaguePhase> LeaguePhases { get; set; }
		
        public virtual ICollection<League> Leagues { get; set; }
    }
}
