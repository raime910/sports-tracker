using Microsoft.AspNet.Identity;
using StatTrack.BLL.DataFactory;
using StatTrack.BLL.DataManagers.Settings;
using StatTrack.BLL.Repositories;
using StatTrack.BLL.ViewModels;
using StatTrack.DAL.Models;
using StatTrack.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatTrack.BLL.DataManagers.Security
{
	public class UserAccountManager : StggManagerBase
	{
		public UserAccountManager(IRepositories repositories, IAppSettings appSettings, AppUserVm currentUser)
			: base(repositories, appSettings, currentUser)
		{
		}

		#region Register

		/// <summary>
		///     Create a new user account.
		/// </summary>
		/// <param name="appUserRegisterVm">Registration view model.</param>
		public async Task<StggResult<AppUserVm>> RegisterAsync(AppUserRegisterVm appUserRegisterVm)
		{
			// Make sure that the email isn't being used by an existing user.
			var stggResult = new StggResult<AppUserVm>();
			var userByEmail = await AppUserManager.FindByEmailAsync(appUserRegisterVm.Email);

			if (userByEmail != null)
			{
				stggResult.AddError("The specified email address already exist within the system. Please enter a different email address.");
				return stggResult;
			}

			// Create data models from the register view model.
			var userProfile = SecurityFactory.BuildOneUserProfile_ByAppUserRegisterVm(
				appUserRegisterVm,
				AppSettings.DefaultUserAvatar);

			var user = SecurityFactory.BuildOneUser_ByAppUserRegisterVm(
				appUserRegisterVm,
				userProfile);

			// Save changes to the database.
			var createResult = await AppUserManager.CreateAsync(user, appUserRegisterVm.Password);

			if (createResult.Succeeded == false)
			{
				stggResult.AddError("Failed to create user account.");
				return stggResult;
			}

			// Add user to the User Role
			var addRoleResult = await AppUserManager.AddToRoleAsync(user.Id, Role.NAME_USER);
			if (addRoleResult.Succeeded == false)
			{
				stggResult.AddError("Failed to create user account.");
				return stggResult;
			}

			// Get the application user instance.
			var stggResUser = (await FindByIdAsync(user.Id)).Value;
			stggResUser.EmailConfirmationToken = GenerateEmailConfirmationToken(stggResUser.Id);

			// Set the stgg output
			stggResult.SetValue(stggResUser);

			// Return the application user.
			return stggResult;
		}

		#endregion

		#region Login

		/// <summary>
		///     Authenticate a user.
		/// </summary>
		/// <param name="appUserLoginVm">Login view model.</param>
		public async Task<StggResult<SignInStatus, AppUserVm>> LoginAsync(AppUserLoginVm appUserLoginVm)
		{
			var stggResult = new StggResult<SignInStatus, AppUserVm>();
			var appUserVm = new AppUserVm();
			var user = await AppUserManager.FindByNameAsync(appUserLoginVm.UserName);

			if (user != null)
			{
				var isLockedOut = await AppUserManager.IsLockedOutAsync(user.Id);

				if (AppUserManager.SupportsUserLockout && isLockedOut)
				{
					// User is locked.
					appUserVm.SignInStatus = SignInStatus.LockedOut;
				}
				else
				{
					// Authenticate user.
					if (AppUserManager.CheckPassword(user, appUserLoginVm.Password))
					{
						// Now we need to check and reset the Access Failed Counter for this user.
						var getAccessFailedExceeded = AppUserManager.GetAccessFailedCount(user.Id) > 0;

						if (AppUserManager.SupportsUserLockout && getAccessFailedExceeded)
						{
							// Reset the failed count.
							var resetAccessFailResult = await AppUserManager.ResetAccessFailedCountAsync(user.Id);
							if (resetAccessFailResult.Succeeded == false)
							{
								appUserVm.SignInStatus = SignInStatus.Failed;
								stggResult.AddError("Failed to login.");
							}
						}

						var emailIsConfirmed = AppUserManager.IsEmailConfirmed(user.Id);

						if (!emailIsConfirmed)
						{
							appUserVm.SignInStatus = SignInStatus.RequiresVerification;
							stggResult.AddError("Email requires verification.");
						}

						var userRoleIds = user.Roles.Select(e => e.RoleId);
						var roleNames = FindRolesByIds(userRoleIds);

						appUserVm = new AppUserVm(user, roleNames, true);
					}
					else
					{
						var lockedoutEnabledForUser = await AppUserManager.GetLockoutEnabledAsync(user.Id);

						// Password was not valid.
						if (AppUserManager.SupportsUserLockout && lockedoutEnabledForUser)
						{
							// Increment access failed count.
							var accessFailedResult = await AppUserManager.AccessFailedAsync(user.Id);
							if (accessFailedResult.Succeeded == false)
							{
								appUserVm.SignInStatus = SignInStatus.LockedOut;
								stggResult.AddError("User is locked out.");
							}
							else
							{							
								appUserVm.SignInStatus = SignInStatus.Failed;
							}
						}
						else
						{
							appUserVm.SignInStatus = SignInStatus.Failed;
						}
					}
				}
			}
			else
			{
				// We could not find this user from the database.
				appUserVm.SignInStatus = SignInStatus.Failed;
			}

			stggResult.SetValue(appUserVm);
			stggResult.SetStatus(appUserVm.SignInStatus);

			return stggResult;
		}

		#endregion

		#region Email Confirmation

		private string GenerateEmailConfirmationToken(int userId)
		{
			// Create new confirmation token for this user.
			var token = AppUserManager.GenerateEmailConfirmationToken(userId);
			return token;
		}

		/// <summary>
		///     Confirm email by user id. Returns true if the transaction was successful otherwise - false.
		/// </summary>
		/// <param name="userName">User name of the person confirming his/her email.</param>
		/// <param name="token">Token that was included in the email link.</param>
		public async Task<StggResult> ValidateEmailConfirmationTokenAsync(string userName, string token)
		{
			// Find the user using the manager.
			var stggResult = new StggResult();
			var user = await AppUserManager.FindByNameAsync(userName);

			if (user == null || user.EmailConfirmed)
			{
				/* Could not find user OR the the email has already been confirmed. */
				stggResult.SetValue(false);
				return stggResult;
			}

			var confirmEmailResult = await AppUserManager.ConfirmEmailAsync(user.Id, token);
			stggResult.SetValue(confirmEmailResult.Succeeded);

			return stggResult;
		}

		/// <summary>
		///     Send a new email confirmation to the user.
		/// </summary>
		/// <param name="emailConfReqVm">View model that contains the email information where we need to send the confirmation to.</param>
		public async Task<StggResult> RequestEmailConfirmationTokenAsync(EmailConfirmRequestVm emailConfReqVm)
		{
			// Gather information about the user and generate the email confirmation token.
			var stggResult = new StggResult();
			var user = await AppUserManager.FindByEmailAsync(emailConfReqVm.Email);

			// Throw exception if the user was not found.
			if (user == null)
			{
				stggResult.AddError("Invalid email confirmation parameter.");
				return stggResult;
			}

			var token = await AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);
			stggResult.SetValue(!string.IsNullOrEmpty(token));

			return stggResult;
		}

		/// <summary>
		/// Update the email address of the user and send the token.
		/// </summary>
		/// <param name="emailManageVm">Email manager view model instance.</param>
		public StggResult UpdateEmailAddress(EmailManageVm emailManageVm)
		{
			var stggResult = new StggResult();

			if (emailManageVm.UserName != CurrentUser.Identity.Name)
			{
				stggResult.AddError("Bad request.");
				return stggResult;
			}

			// Get the user record from the database.
			var user = AppUserManager.FindById(CurrentUser.Id);

			// Update the email address of the user and save it.
			user.Email = emailManageVm.Email;
			user.EmailConfirmed = false;

			AppUserManager.Update(user);

			// Generate a new email confirmation token for this user.
			GenerateEmailConfirmationToken(user.Id);

			return stggResult;
		}

		#endregion

		#region Forgot password

		/// <summary>
		///     Request a reset password token. Send it to the user's email.
		/// </summary>
		/// <param name="passLostVm">Forgot password view model.</param>
		public async Task<StggResult> RequestPasswordResetTokenAsync(PasswordLostVm passLostVm)
		{
			var stggResult = new StggResult();

			// Let's get more info about this user with the view model data.
			var user = passLostVm.ValidateByUserName
				? await AppUserManager.FindByNameAsync(passLostVm.UserName)
				: await AppUserManager.FindByEmailAsync(passLostVm.Email);

			if (user != null)
			{
				// Create new confirmation token for this user.
				var token = await AppUserManager.GeneratePasswordResetTokenAsync(user.Id);

				if (string.IsNullOrEmpty(token))
				{
					stggResult.AddError("The password reset token generation failed.");
				}
			}

			return stggResult;
		}

		/// <summary>
		///     Validates the token and reset (change) user account password.
		///     Returns true if the password has been successfuly changed otherwise, false.
		/// </summary>
		/// <param name="passResetVm">Password reset view model that contains all the information needed to change the password.</param>
		public async Task<StggResult> ResetPasswordAsync(PasswordResetVm passResetVm)
		{
			var stggResult = new StggResult();

			if (passResetVm.Password != passResetVm.ConfirmPassword)
			{
				stggResult.AddError("Passwords do not match.");
				return stggResult;
			}

			// Let's find the user we need more information about this user.
			var user = await AppUserManager.FindByEmailAsync(passResetVm.Email);

			// Make sure the user is not null and password token is not null;
			if (string.IsNullOrEmpty(user?.PasswordResetToken))
			{
				stggResult.AddError("User not found.");
				return stggResult;
			}

			// Try resetting the password...
			var resetPassResult = await AppUserManager.ResetPasswordAsync(user.Id, passResetVm.Token, passResetVm.Password);
			stggResult.SetValue(resetPassResult.Succeeded);

			// Reset the token to null
			user.PasswordResetToken = null;
			AppUserManager.Update(user);

			return stggResult;
		}

		/// <summary>
		///     Returns true if the token is valid otherwise; false.
		/// </summary>
		/// <param name="userName">Username of the person who's password is being reset.</param>
		/// <param name="token">Token that came from the email that was sent out.</param>
		public async Task<StggResult<PasswordResetVm>> ValidateResetPasswordTokenAsync(string userName, string token)
		{
			// Find the user using the manager.
			var stggResult = new StggResult<PasswordResetVm>();
			var user = await AppUserManager.FindByNameAsync(userName);

			// Return value

			if (!string.IsNullOrEmpty(user?.PasswordResetToken))
			{
				var passResetVm = new PasswordResetVm
				{
					FirstName = user.UserProfile.FirstName,
					LastName = user.UserProfile.LastName,
					Email = user.Email,
					Token = token,
					IsValidResetPassToken = user.PasswordResetToken == token
				};

				stggResult.SetValue(passResetVm);
			}
			else
			{
				stggResult.AddError("User not found.");
			}

			return stggResult;
		}

		/// <summary>
		///     Update the current users password.
		/// </summary>
		/// <param name="passwordManageVm">Password manage instance.</param>
		/// <returns></returns>
		public StggResult UpdatePassword(PasswordManageVm passwordManageVm)
		{
			var stggResult = new StggResult();
			var user = AppUserManager.FindById(CurrentUser.Id);

			if (user == null)
			{
				stggResult.AddError("User not found.");
				return stggResult;
			}

			if (string.Equals(passwordManageVm.OldPassword, passwordManageVm.NewPassword, StringComparison.CurrentCultureIgnoreCase))
			{
				stggResult.AddError("You cannot reuse the same password.");
				return stggResult;
			}

			var identityResult = AppUserManager.ChangePassword(user.Id, passwordManageVm.OldPassword, passwordManageVm.NewPassword);

			if (!identityResult.Succeeded)
			{
				stggResult.AddError("Failed to change password.");
			}

			return stggResult;
		}

		#endregion

		#region Query

		/// <summary>
		///     Find a user by Id.
		/// </summary>
		/// <param name="userId">User account Id.</param>
		public async Task<StggResult<AppUserVm>> FindByIdAsync(int userId)
		{
			// Find the user account 
			var stggResult = new StggResult<AppUserVm>();
			var user = await AppUserManager.FindByIdAsync(userId);

			if (user == null)
			{
				stggResult.AddError("User not found.");
				return stggResult;
			}

			// Convert the user account into an application user view model
			var userRoleIds = user.Roles.Select(e => e.RoleId);
			var roleNames = FindRolesByIds(userRoleIds);

			stggResult.SetValue(new AppUserVm(user, roleNames));

			return stggResult;
		}

		/// <summary>
		///     Find a user by UserName.
		/// </summary>
		/// <param name="userName">UserName of the account.</param>
		public async Task<StggResult<AppUserVm>> FindByUserNameAsync(string userName)
		{
			// Find the user account 
			var stggResult = new StggResult<AppUserVm>();
			var user = await AppUserManager.FindByNameAsync(userName);

			if (user == null)
			{
				stggResult.AddError("User not found.");
				return stggResult;
			}

			// Convert the user account into an application user view model
			var userRoleIds = user.Roles.Select(e => e.RoleId);
			var roleNames = FindRolesByIds(userRoleIds);
			var appUserVm = new AppUserVm(user, roleNames);

			return new StggResult<AppUserVm>(appUserVm);
		}

		/// <summary>
		///     Find roles by role ids.
		/// </summary>
		/// <param name="userRoleIds">Collection of role ids.</param>
		private IEnumerable<string> FindRolesByIds(IEnumerable<int> userRoleIds)
		{
			var userRoleNames = from role in AppRoleManager.Roles
								where userRoleIds.Contains(role.Id)
								select role.Name;

			return userRoleNames.ToList();
		}

		#endregion
	}
}