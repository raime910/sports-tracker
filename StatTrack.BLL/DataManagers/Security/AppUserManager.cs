using Microsoft.AspNet.Identity;
using StatTrack.BLL.DataManagers.Settings;
using StatTrack.DAL.Models;
using System;

namespace StatTrack.BLL.DataManagers.Security
{
	/// <summary>
	/// Manage users. Can only be used inside the service layer.
	/// </summary>
	public class AppUserManager : UserManager<User, int>
	{
		#region Ctor

		internal AppUserManager(IUserStore<User, int> userStore, IAppSettings appSettings)
			: base(userStore)
		{
			EmailService = new MessageService();
			UserTokenProvider = new UserTokenProvider(appSettings);
			DefaultAccountLockoutTimeSpan = new TimeSpan(0, 0, appSettings.AccountLockoutTimeSpanMinutes, 0);

			UserLockoutEnabledByDefault = appSettings.UserLockoutEnabledByDefault;
			MaxFailedAccessAttemptsBeforeLockout = appSettings.MaxFailedAccessAttemptsBeforeLockout;
		}

		#endregion
	}
}
