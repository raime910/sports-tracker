using System.Data.Entity;

namespace StatTrack.DAL
{
	public interface IStatTrackEntities
	{
		DbSet<LeaguePhase> LeaguePhases { get; set; }
		DbSet<League> Leagues { get; set; }
		DbSet<LeagueSet> LeagueSets { get; set; }
		DbSet<LeagueType> LeagueTypes { get; set; }
		DbSet<PlayerProfile> PlayerProfiles { get; set; }
		DbSet<Sport> Sports { get; set; }
		DbSet<SportType> SportTypes { get; set; }
		DbSet<TeamRole> TeamRoles { get; set; }
		DbSet<Team> Teams { get; set; }
		DbSet<TeamMembership> TeamMemberships { get; set; }
	}
}
