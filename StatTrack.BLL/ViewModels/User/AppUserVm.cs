using System;
using System.Collections.Generic;
using System.Security.Principal;
using StatTrack.DAL.Models;
using StatTrack.DAL.Models.Enums;

namespace StatTrack.BLL.ViewModels
{
	public class AppUserVm : IPrincipal
	{

		#region Members

		private const string _DEFAULT_USERNAME = "Guest";

		// Read only
		private readonly string _username;
		private readonly bool _isAuthenticated;

		// Read and write
		private AppUserIdentityVm _appUserIdentityVm;
		private List<string> _roles;

		#endregion

		#region Ctor

		public AppUserVm(string username, bool isAuthenticated = false)
		{
			_username = username;
			_isAuthenticated = isAuthenticated;
			SignInStatus = isAuthenticated
				? SignInStatus.Success
				: SignInStatus.Failed;
		}

		public AppUserVm()
		{
			_username = _DEFAULT_USERNAME;
			_isAuthenticated = false;
		}

		internal AppUserVm(User user, IEnumerable<string> roleNames, bool isAuthenticated = false)
		{
			_username = user.UserName;
			_isAuthenticated = isAuthenticated;

			// Default user information
			Id = user.Id;
			Email = user.Email;
			EmailConfirmed = user.EmailConfirmed;

			// User profile information
			FirstName = user.UserProfile.FirstName;
			LastName = user.UserProfile.LastName;
			Bio = user.UserProfile.Bio;
			SubscribeNewsletter = user.UserProfile.SubscribeNewsletter;

			// Tokens
			PasswordResetToken = user.PasswordResetToken;
			EmailConfirmationToken = user.EmailConfirmationToken;

			if (user.EmailConfirmed && isAuthenticated)
			{
				SignInStatus = SignInStatus.Success;
			}
			else if (!user.EmailConfirmed && isAuthenticated)
			{
				SignInStatus = SignInStatus.RequiresVerification;
			}
			else
			{
				SignInStatus = SignInStatus.Failed;
			}

			Roles.AddRange(roleNames);
		}

		#endregion

		#region Properties

		public int Id { get; private set; }

		public string Email { get; private set; }

		public bool EmailConfirmed { get; internal set; }

		public string EmailConfirmationToken { get; internal set; }

		public string PasswordResetToken { get; private set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string FullName => $"{FirstName} {LastName}";
		
		public string Bio { get; set; }
		
		public bool SubscribeNewsletter { get; set; }

		public SignInStatus SignInStatus { get; internal set; }

		public List<string> Roles => _roles ?? (_roles = new List<string>());

		public IIdentity Identity => _appUserIdentityVm ?? (_appUserIdentityVm = new AppUserIdentityVm(_username, _isAuthenticated));

		public bool IsInRole(string role)
		{
			return Roles.Contains(role);
		}

		#endregion

		#region Additional user profile information

		public string AvatarUrl { get; set; }

		public DateTime DateOfBirth { get; set; }

		#endregion

	}

}
