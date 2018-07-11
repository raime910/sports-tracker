using Microsoft.AspNet.Identity;
using StatTrack.BLL.DataManagers.Settings;
using StatTrack.DAL;
using System;
using System.Threading.Tasks;
using StatTrack.DAL.Models;
using StatTrack.DAL.Models.Enums;

namespace StatTrack.BLL.DataManagers.Security
{
	/// <summary>
	/// Provides token generation and confrimation for Email Confirmation and Password Reset.
	/// </summary>
	internal class UserTokenProvider : IUserTokenProvider<User, int>
	{

		private const string _PURPOSE_PASSWORD_RESET = "ResetPassword";
		private const string _PURPOSE_EMAIL_CONFIRMATION = "Confirmation";

		private static IAppSettings _appSettings;

		#region Ctor

		public UserTokenProvider(IAppSettings appSettings)
		{
			_appSettings = appSettings;
		}

		#endregion

		#region IUserTokenProvider

		/// <summary>
		/// Generate a token for a user with a specific purpose.
		/// </summary>
		/// <param name="purpose">Purpose of this validation.</param>
		/// <param name="manager">User manager instance that will be used to update user information.</param>
		/// <param name="user">User that is being processed.</param>
		public Task<string> GenerateAsync(string purpose, UserManager<User, int> manager, User user)
		{
			// Generate token
			var resetToken = Guid.NewGuid().ToString();

			switch (purpose)
			{
				case _PURPOSE_EMAIL_CONFIRMATION:
					// Set the Email confirmation token property
					user.EmailConfirmationToken = resetToken;
					manager.Update(user);

					// Send out the email.
					NotifyEmailConfirmation(manager, user, _appSettings.EmailConfirmTokenUrlTemplate);
					break;
				case _PURPOSE_PASSWORD_RESET:
					// Set the Password reset token property
					user.PasswordResetToken = resetToken;
					manager.Update(user);

					// Send out the email.
					NotifyResetPassword(manager, user, _appSettings.ResetPasswordTokenUrlTemplate);
					break;
			}

			return Task.FromResult(resetToken);
		}

		/// <summary>
		/// Returns true if provider can be used for this user, i.e. could require a
		/// user to have an email
		/// </summary>
		/// <param name="manager">User manager being used by the application.</param>
		/// <param name="user">User whos being processed.</param>
		public Task<bool> IsValidProviderForUserAsync(UserManager<User, int> manager, User user)
		{
			if (manager == null)
			{
				throw new Exception("User manager is null.");
			}

			return Task.FromResult(manager.SupportsUserPassword);
		}

		/// <summary>
		/// Notifies the user that a token has been generated, for example an email or
		/// sms could be sent, or this can be a no-op
		/// </summary>
		/// <param name="token"></param>
		/// <param name="manager"></param>
		/// <param name="user"></param>
		public Task NotifyAsync(string token, UserManager<User, int> manager, User user)
		{
			return Task.FromResult(0);
		}

		/// <summary>
		/// Validate the token that was provided by the requestor.
		/// </summary>
		/// <param name="purpose">Purpose of this validation.</param>
		/// <param name="token">Token from the user.</param>
		/// <param name="manager">User manager instance that will be used to update user information.</param>
		/// <param name="user">User that is being processed.</param>
		public Task<bool> ValidateAsync(string purpose, string token, UserManager<User, int> manager, User user)
		{
			Task<bool> result = null;
			switch (purpose)
			{
				case _PURPOSE_EMAIL_CONFIRMATION:
					result = Task.FromResult(user.EmailConfirmationToken == token);

					if (result.Result)
					{
						// Set the account status of the user and save it to the database.
						user.AccountStatus = AccountStatus.Active;
						manager.Update(user);
					}
					break;
				case _PURPOSE_PASSWORD_RESET:
					result = Task.FromResult(user.PasswordResetToken == token);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(purpose));
			}
			return result;
		}

		#endregion

		#region Static methods

		/// <summary>
		/// Notify the user about the newly generated email confirmation token.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="user"></param>
		/// <param name="emailConfUrl">Email confirmation url that will be sent out to the user.</param>
		private static void NotifyEmailConfirmation(UserManager<User, int> manager, User user, string emailConfUrl)
		{
			// Create the contents of the email.
			var subject = _appSettings.AppName + " Email confirmation";

			// Build the body of the email.
			var verificationUrl = string.Format(emailConfUrl, user.UserName, user.EmailConfirmationToken);
			var body = $"Please click <a href='{verificationUrl}'>here</a> to verify your email.";

			// Send the email.
			manager.SendEmail(user.Id, subject, body);
		}

		/// <summary>
		/// Notify the user about the newly generated password reset token.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="user"></param>
		/// <param name="resetPassUrl">Reset password url that will be sent out the user.</param>
		private static void NotifyResetPassword(UserManager<User, int> manager, User user, string resetPassUrl)
		{
			// Create the contents of the email.
			var subject = _appSettings.AppName + " Reset Password";

			// Build the body of the email.
			var verificationUrl = string.Format(resetPassUrl, user.UserName, user.PasswordResetToken);
			var body = $"Please click <a href='{verificationUrl}' >here</a> reset your password.";

			// Send the email.
			manager.SendEmail(user.Id, subject, body);
		}

		#endregion

	}
}
