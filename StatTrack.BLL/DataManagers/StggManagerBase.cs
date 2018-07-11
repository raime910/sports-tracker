using StatTrack.BLL.DataFactory;
using StatTrack.BLL.DataManagers.Security;
using StatTrack.BLL.DataManagers.Settings;
using StatTrack.BLL.Repositories;
using StatTrack.BLL.ViewModels;

namespace StatTrack.BLL.DataManagers
{
	public abstract class StggManagerBase
	{
		/// <summary>
		/// Create new instance of the manager base.
		/// </summary>
		/// <param name="repositories">Repository instance.</param>
		/// <param name="appSettings">Application settings</param>
		/// <param name="currentUser">Current user instance.</param>
		protected StggManagerBase(IRepositories repositories, IAppSettings appSettings, AppUserVm currentUser)
		{
			Repositories = repositories;
			CurrentUser = currentUser;
			AppSettings = appSettings;

			Factories = new Factories();
		}

		private Managers _managers;

		#region Properties

		protected Managers Managers => _managers ?? (_managers = new Managers(AppSettings, CurrentUser));

		/// <summary>
		/// StggRepositories used for database transactions.
		/// </summary>
		protected IRepositories Repositories { get; }

		/// <summary>
		/// Application settings.
		/// </summary>
		protected IAppSettings AppSettings { get; }

		/// <summary>
		/// Applications current user.
		/// </summary>
		protected AppUserVm CurrentUser { get; set; }

		/// <summary>
		/// Factories used for building and transforming data models and view models.
		/// </summary>
		internal Factories Factories { get; }

		#endregion

		#region User and Role managers

		// private members
		// -------------------------
		private AppUserManager _appUserManager;
		private AppRoleManager _appRoleManager;

		/// <summary>
		/// Application user manager that is used to access user information from the database.
		/// </summary>
		protected AppUserManager AppUserManager
			=> _appUserManager ?? (_appUserManager = new AppUserManager(Repositories.UserStore, AppSettings));

		/// <summary>
		/// Application role manager that is used to access user role information from the database.
		/// </summary>
		protected AppRoleManager AppRoleManager
			=> _appRoleManager ?? (_appRoleManager = new AppRoleManager(Repositories.RoleStore));

		#endregion
	}
}