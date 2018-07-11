using StatTrack.BLL.ViewModels;
using StatTrack.DAL.Models;
using StatTrack.DAL.Models.Enums;
using System;

namespace StatTrack.BLL.DataFactory
{
	internal static class SecurityFactory
	{
		/// <summary>
		/// Build a User object isntance used for registring a new user.
		/// </summary>
		/// <param name="appUserRegisterVm">User registration view model that contains the user name and the password.</param>
		/// <param name="userProfile">User profile of this user.</param>
		internal static User BuildOneUser_ByAppUserRegisterVm(AppUserRegisterVm appUserRegisterVm, UserProfile userProfile)
		{
			return new User
			{
				UserName = appUserRegisterVm.UserName,
				Email = appUserRegisterVm.Email,
				AccountStatus = AccountStatus.Pending,
				RegisterDate = DateTime.UtcNow,
				UserProfile = userProfile
			};
		}

		/// <summary>
		/// Creates a very basic profile instance for a registering user.
		/// </summary>
		/// <param name="appUserRegisterVm">User registration view model that contains the first name and the last name of the user.</param>
		/// <param name="defaultAvatar">Default user avatar.</param>
		internal static UserProfile BuildOneUserProfile_ByAppUserRegisterVm(AppUserRegisterVm appUserRegisterVm, string defaultAvatar = "")
		{
			var userProfile = new UserProfile
			{
				FirstName = appUserRegisterVm.FirstName,
				LastName = appUserRegisterVm.LastName,
				SubscribeNewsletter = appUserRegisterVm.SubscribeNewsletter
			};

			return userProfile;
		}
	}
}
