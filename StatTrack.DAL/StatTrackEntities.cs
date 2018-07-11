using Microsoft.AspNet.Identity.EntityFramework;
using StatTrack.DAL.Models;
using System.Data.Entity;

namespace StatTrack.DAL
{

	public class StatTrackEntities : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
	{
		internal StatTrackEntities() : base("name=StatTrackConn") { }

		#region Stat Track GG

		public virtual DbSet<League> Leagues { get; set; }
		public virtual DbSet<LeaguePhase> LeaguePhases { get; set; }
		public virtual DbSet<LeagueSet> LeagueSets { get; set; }
		public virtual DbSet<LeagueType> LeagueTypes { get; set; }
		public virtual DbSet<Sport> Sports { get; set; }
		public virtual DbSet<SportType> SportTypes { get; set; }
		public virtual DbSet<Team> Teams { get; set; }
		public virtual DbSet<TeamDivision> TeamDivisions { get; set; }
		public virtual DbSet<TeamDivisionMembership> TeamDivisionMemberships { get; set; }
		public virtual DbSet<TeamDivisionRole> TeamDivisionRoles { get; set; }

		#endregion

		#region Identity

		public virtual DbSet<UserClaim> UserClaims { get; set; }
		public virtual DbSet<UserLogin> UserLogin { get; set; }
		public virtual DbSet<UserProfile> UserProfile { get; set; }
		public virtual DbSet<UserRole> UserRole { get; set; }

		#endregion

		#region Lookup 

		public virtual DbSet<Country> Countries { get; set; }

		#endregion

		#region Attachments

		public virtual DbSet<Attachment> Attachments { get; set; }

		#endregion

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("AspNetUserAccount");
			modelBuilder.Entity<UserClaim>().ToTable("AspNetUserClaim");
			modelBuilder.Entity<UserLogin>().ToTable("AspNetUserLogin");
			modelBuilder.Entity<Role>().ToTable("AspNetRole");
			modelBuilder.Entity<UserRole>().ToTable("AspNetUserRole");
			modelBuilder.Entity<UserProfile>().ToTable("AspNetUserProfile");

			// disable cascade on delete - Leagues_dbo.LeagueTypes_LeagueTypeId
			modelBuilder.Entity<LeagueType>()
				.HasMany(e => e.Leagues)
				.WithRequired(e => e.LeagueType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TeamDivisionMembership>()
				.HasRequired(x => x.TeamDivisionRole)
				.WithMany(x => x.TeamDivisionMembership)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TeamDivisionMembership>()
				.HasRequired(x => x.User)
				.WithMany(x => x.TeamDivisionMemberships)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Team>()
				.HasRequired(x => x.Owner)
				.WithMany(x => x.OwnedTeams)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<UserRole>()
				.HasKey(x => new { x.UserId, x.RoleId });

			modelBuilder.Entity<UserLogin>()
				.HasKey(x => x.ProviderKey);
		}
	}
}
