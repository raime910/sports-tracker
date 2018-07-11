using StatTrack.BLL.DataManagers.Settings;
using StatTrack.BLL.Repositories;
using StatTrack.BLL.ViewModels;
using System;

namespace StatTrack.BLL.DataManagers.Security
{
	public class ProfileManager : StggManagerBase
	{
		public ProfileManager(IRepositories repositories, IAppSettings appSettings, AppUserVm currentUser)
			: base(repositories, appSettings, currentUser)
		{
		}

		public StggResult<EmailManageVm> GetAccountInfo(int id)
		{
			// Get data from the database and build the result.
			var userProfile = Repositories.UserProfile.GetOne(x => x.UserId == id, x => x.User);
			var accountInfoEditorVm = new EmailManageVm(userProfile);
			var stggResult = new StggResult<EmailManageVm>();

			// quick check on returned value.
			if (userProfile == null)
			{
				stggResult.SetStatus(StggResultStatus.Failed);
			}
			else
			{
				// profile was found - return this profile.
				stggResult.SetStatus(StggResultStatus.Succeeded);
				stggResult.SetValue(accountInfoEditorVm);
			}

			// return value
			return stggResult;
		}

		/// <summary>
		/// Get personal information from the database.
		/// </summary>
		/// <param name="id">User id of the user.</param>
		public StggResult<PersonalInfoManageVm> GetPersonalInfo(int id)
		{
			// Get data from the database and build the result.
			var userProfile = Repositories.UserProfile.GetOne(x => x.UserId == id);
			var personalInfoEditorVm = new PersonalInfoManageVm(userProfile);
			var stggResult = new StggResult<PersonalInfoManageVm>();

			// quick check on returned value.
			if (userProfile == null)
			{
				stggResult.SetStatus(StggResultStatus.Failed);
			}
			else
			{
				// profile was found - return this profile.
				stggResult.SetStatus(StggResultStatus.Succeeded);
				stggResult.SetValue(personalInfoEditorVm);
			}

			// return value
			return stggResult;
		}

		/// <summary>
		/// Update user profile.
		/// </summary>
		/// <param name="personalInfoEditorVm">Personal information view model instance.</param>
		public StggResult UpdatePersonalInfo(PersonalInfoManageVm personalInfoEditorVm)
		{
			var userProfile = Repositories.UserProfile.GetOne(x => x.UserId == CurrentUser.Id);

			userProfile.FirstName = personalInfoEditorVm.FirstName;
			userProfile.LastName = personalInfoEditorVm.LastName;

			DateTime birthdate;
			DateTime.TryParse(personalInfoEditorVm.BirthDate, out birthdate);
			userProfile.BirthDate = birthdate;

			userProfile.Bio = personalInfoEditorVm.Bio;
			userProfile.SubscribeNewsletter = personalInfoEditorVm.SubscribeNewsletter;

			Repositories.SaveChanges();

			return new StggResult(true);
		}
	}
}
