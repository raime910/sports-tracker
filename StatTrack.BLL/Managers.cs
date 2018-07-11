using StatTrack.BLL.DataManagers;
using StatTrack.BLL.DataManagers.Security;
using StatTrack.BLL.DataManagers.Settings;
using StatTrack.BLL.Repositories;
using StatTrack.BLL.ViewModels;
using StatTrack.DAL;

namespace StatTrack.BLL
{

	/// <summary>
	/// Contains all application managers. All database transactions and queries within the application goes through this class.
	/// </summary>
	public class Managers
	{
		/// <summary>
		/// Create a new instance of the stat track application manager.
		/// </summary>
		public Managers(IAppSettings appSettings, AppUserVm currentUser = null)
		{
			_repositories = new StggRepositories(new StatTrackEntitiesFactory().Create());
			_appSettings = appSettings;
			_currentUser = currentUser;
		}

		private readonly IAppSettings _appSettings;
		private readonly StggRepositories _repositories;
		private readonly AppUserVm _currentUser;

		private ContentManager _contentManager;
		private SportsManager _sportsManager;
		private SportsTypeManager _sportsTypeManager;
		private UserAccountManager _userAccountManager;
		private ProfileManager _profileManager;
		private TeamManager _teamManager;

		/// <summary>
		/// Content manager.
		/// </summary>
		public ContentManager ContentManager => _contentManager ?? (_contentManager = new ContentManager(_repositories, _appSettings, _currentUser));

		/// <summary>
		/// Sports manager.
		/// </summary>
		public SportsManager SportsManager => _sportsManager ?? (_sportsManager = new SportsManager(_repositories, _appSettings, _currentUser));

		/// <summary>
		/// Sports type manager.
		/// </summary>
		public SportsTypeManager SportsTypeManager => _sportsTypeManager ?? (_sportsTypeManager = new SportsTypeManager(_repositories, _appSettings, _currentUser));

		/// <summary>
		/// Security manager.
		/// </summary>
		public UserAccountManager UserAccountManager => _userAccountManager ?? (_userAccountManager = new UserAccountManager(_repositories, _appSettings, _currentUser));

		/// <summary>
		/// Profile manager.
		/// </summary>
		public ProfileManager ProfileManager => _profileManager ?? (_profileManager = new ProfileManager(_repositories, _appSettings, _currentUser));

		/// <summary>
		/// Team manager.
		/// </summary>
		public TeamManager TeamManager => _teamManager ?? (_teamManager = new TeamManager(_repositories, _appSettings, _currentUser));
	}
}
