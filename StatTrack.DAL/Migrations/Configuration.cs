using StatTrack.DAL.Models;
using StatTrack.DAL.Models.Enums;

namespace StatTrack.DAL.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<StatTrackEntities>
	{
		public Configuration()
		{
#if DEBUG
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
#else
			AutomaticMigrationsEnabled = false;
			AutomaticMigrationDataLossAllowed = false;
#endif
		}

		protected override void Seed(StatTrackEntities context)
		{

			#region Role

			var owner = new Role
			{
				Name = Role.NAME_SITE_OWNER,
				AccessLevel = Role.ACCLEVEL_SITE_OWNER
			};

			var admin = new Role
			{
				Name = Role.NAME_ADMIN,
				AccessLevel = Role.ACCLEVEL_ADMIN
			};

			var mod = new Role
			{
				Name = Role.NAME_MOD,
				AccessLevel = Role.ACCLEVEL_MOD
			};

			var user = new Role
			{
				Name = Role.NAME_USER,
				AccessLevel = Role.ACCLEVEL_USER
			};

			var suspended = new Role
			{
				Name = Role.NAME_SUSPEND,
				AccessLevel = Role.ACCLEVEL_SUSPEND
			};

			var banned = new Role
			{
				Name = Role.NAME_BANNED,
				AccessLevel = Role.ACCLEVEL_BANNED
			};

			context.Roles.AddOrUpdate(e => e.Name,
				owner,
				admin,
				mod,
				user,
				suspended,
				banned);

			#endregion

			#region Sports

			var sportType = new SportType
			{
				Name = "Traditional Sports",
				Description = "Traditional sports.",
				Code = "TRADSPORT"
			};

			context.SportTypes.AddOrUpdate(e => e.Code,
				sportType);

			var basketball = new Sport
			{
				Name = "Basketball",
				Code = "BBALL",
				Status = SportStatus.Active,
				SportType = sportType,
				SportTypeId = sportType.Id,
				Description =
					"Basketball is a sport that is played by two teams of five players on a rectangular court. " +
					"The objective is to shoot a ball through a hoop 18 inches (46 cm) in diameter and mounted " +
					"at a height of 10 feet (3.048 m) to backboards at each end of the court."
			};

			var football = new Sport
			{
				Name = "Football",
				Code = "FBALL",
				Status = SportStatus.Active,
				SportType = sportType,
				SportTypeId = sportType.Id,
				Description =
					"Football is a family of team sports that involve, to varying degrees, kicking a ball with the " +
					"foot to score a goal. Unqualified, the word football is understood to refer to whichever form " +
					"of football is the most popular in the regional context in which the word appears. Sports commonly " +
					"called 'football' in certain places include: association football (known as soccer in some countries); " +
					"gridiron football (specifically American football or Canadian football); Australian rules football; " +
					"rugby football (either rugby league or rugby union); and Gaelic football. These different " +
					"variations of football are known as football codes."
			};

			context.Sports.AddOrUpdate(x => x.Name, 
				basketball,
				football);

			#endregion

			#region Team Division Role

			var teamRoleOwner = new TeamDivisionRole
			{
				Name = TeamDivisionRole.NAME_OWNER,
				AccessLevel = TeamDivisionRole.ACCLEVEL_OWNER,
				Description = "The owner of the team.",
				Status = TeamDivisionRoleStatus.Active,
				SportId = basketball.Id,
				Sport = basketball
			};

			var teamRoleModerator = new TeamDivisionRole
			{
				Name = TeamDivisionRole.NAME_MOD,
				AccessLevel = TeamDivisionRole.ACCLEVEL_MOD,
				Description = "The team web moderator.",
				Status = TeamDivisionRoleStatus.Active,
				SportId = basketball.Id,
				Sport = basketball
			};

			var teamRolePlayer = new TeamDivisionRole
			{
				Name = TeamDivisionRole.NAME_PLAYER,
				AccessLevel = TeamDivisionRole.ACCLEVEL_PLAYER,
				Description = "A player within a team.",
				Status = TeamDivisionRoleStatus.Active,
				SportId = basketball.Id,
				Sport = basketball
			};

			var teamRoleSuspended = new TeamDivisionRole
			{
				Name = TeamDivisionRole.NAME_SUSPEND,
				AccessLevel = TeamDivisionRole.ACCLEVEL_SUSPEND,
				Description = "A suspended team member.",
				Status = TeamDivisionRoleStatus.Active,
				SportId = basketball.Id,
				Sport = basketball
			};

			var teamRoleBanned = new TeamDivisionRole
			{
				Name = TeamDivisionRole.NAME_BANNED,
				AccessLevel = TeamDivisionRole.ACCLEVEL_BANNED,
				Description = "A user who's been banned from the team.",
				Status = TeamDivisionRoleStatus.Active,
				SportId = basketball.Id,
				Sport = basketball
			};

			context.TeamDivisionRoles.AddOrUpdate(e => e.Name,
				teamRoleOwner,
				teamRoleModerator,
				teamRolePlayer,
				teamRoleSuspended,
				teamRoleBanned);

			#endregion

		}
	}
}
