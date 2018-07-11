using StatTrack.BLL.Repositories;
using StatTrack.BLL.ViewModels;
using StatTrack.DAL.Models;
using StatTrack.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatTrack.BLL.DataManagers.Settings;

namespace StatTrack.BLL.DataManagers
{
	public class TeamManager : StggManagerBase
	{

		#region Ctor

		public TeamManager(IRepositories repositories, IAppSettings appSettings, AppUserVm currentUser)
			: base(repositories, appSettings, currentUser)
		{
		}

		#endregion

		#region Get team details

		/// <summary>
		/// Get one team by id.
		/// </summary>
		/// <param name="teamId">Team Id.</param>
		public async Task<TeamDetailVm> GetOneByIdAsync(int teamId)
		{
			var team = await Repositories.Team.GetOneAsync(x => x.Id == teamId, x => x.TeamDivisions);
			var teamOwner = team.TeamDivisions
								.SelectMany(x => x.TeamDivisionMemberships)
								.First(x => string.Equals(x.TeamDivisionRole.Name, TeamDivisionRole.NAME_OWNER, StringComparison.CurrentCultureIgnoreCase))
								.User.UserProfile;

			var teamOwnerName = teamOwner.FirstName + " " + teamOwner.LastName;

			var teamDetailVm = new TeamDetailVm
			{
				Name = team.Name,
				Code = team.Code,
				Description = team.Description,
				MemberCount = team.TeamDivisions.SelectMany(x => x.TeamDivisionMemberships).Count(),
				OwnerName = teamOwnerName,
				StatusText = team.Status.ToString()
			};

			return teamDetailVm;
		}

		#endregion

		#region Create new team

		/// <summary>
		/// Create a new team.
		/// </summary>
		/// <param name="teamEditorVm">Team editor instance that contains information about the team that needs to be created.</param>
		public async Task<TeamDetailVm> CreateAsync(TeamEditorVm teamEditorVm)
		{
			// Create the team object and save it into the database
			var team = new Team
			{
				Name = teamEditorVm.Name,
				Code = teamEditorVm.Code,
				Description = teamEditorVm.Description,
				Status = TeamStatus.Pending,
				DateCreated = DateTime.Now,
				OwnerId = CurrentUser.Id
			};

			Repositories.Team.Create(team);

			var teamDivision = new TeamDivision
			{
				Team = team,
				Name = team.Name,
				SportId = teamEditorVm.SportId
			};

			// Set team owner
			var ownerRoleId = await Repositories.TeamDivisionRole.GetOneAsync(x => x.Name == TeamDivisionRole.NAME_OWNER);
			var ownerTeamMembership = new TeamDivisionMembership
			{
				TeamDivision = teamDivision,
				UserId = CurrentUser.Id,
				TeamRoleId = ownerRoleId.Id
			};

			teamDivision.TeamDivisionMemberships = new List<TeamDivisionMembership>
			{
				ownerTeamMembership
			};

			Repositories.TeamDivision.Create(teamDivision);

			// Save changes
			Repositories.SaveChanges();

			// Upload the logo and the banner
			var logoTask = await SetTeamLogoAsync(team.Id, teamEditorVm);
			var bannerTask = await SetTeamBannerAsync(team.Id, teamEditorVm);

			team.LogoImageUrl = logoTask.Value;
			team.BannerImageUrl = bannerTask.Value;

			// Save changes to the team model (logo/banner)
			Repositories.SaveChanges();

			return new TeamDetailVm(team);
		}

		/// <summary>
		/// Save the team logo.
		/// </summary>
		/// <param name="teamId">Team Id.</param>
		/// <param name="teamEditorVm">Editor instance.</param>
		public async Task<StggResult<string>> SetTeamLogoAsync(int teamId, TeamEditorVm teamEditorVm)
		{
			var logoPath = await Managers.ContentManager.SaveFileAsync(GetTeamContentFolder(teamId), "logo.jpg", teamEditorVm.LogoFile.InputStream);
			return new StggResult<string>(logoPath);
		}

		/// <summary>
		/// Save the team banner.
		/// </summary>
		/// <param name="teamId">Team Id.</param>
		/// <param name="teamEditorVm">Editor instance.</param>
		public async Task<StggResult<string>> SetTeamBannerAsync(int teamId, TeamEditorVm teamEditorVm)
		{
			var bannerPath = await Managers.ContentManager.SaveFileAsync(GetTeamContentFolder(teamId), "banner.jpg", teamEditorVm.BannerFile.InputStream);
			return new StggResult<string>(bannerPath);
		}
		
		private static string GetTeamContentFolder(int teamId)
		{
			return $@"/team/{teamId}/";
		}

		#endregion

	}
}
