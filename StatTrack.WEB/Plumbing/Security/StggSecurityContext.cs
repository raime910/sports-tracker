using StatTrack.BLL.ViewModels;
using System;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace StatTrack.WEB.Plumbing.Security
{
	public static class StggSecurityContext
	{

		#region Functions

		/// <summary>
		/// Set current user account.
		/// </summary>
		/// <param name="appUserVm">Application user account object.</param>
		/// <param name="rememberMe">True to remember this person.</param>
		public static void SetCurrentUser(AppUserVm appUserVm, bool rememberMe = false)
		{
			SessionManager.Set(SessionKey.CurrentUser, appUserVm);

			// The user is not authenticated, we need not to add ASP.NET cookie.
			if (!appUserVm.Identity.IsAuthenticated)
			{
				return;
			}

			Thread.CurrentPrincipal = HttpContext.Current.User = appUserVm;

			var roles = string.Join(",", appUserVm.Roles);
			var expireDateTime = DateTime.UtcNow.AddDays(7);

			// Create an authenticated cookie.
			var ticket = new FormsAuthenticationTicket(
				1, /* version number of the ticket */
				appUserVm.Identity.Name,
				DateTime.UtcNow,
				expireDateTime,
				rememberMe,
				roles,
				FormsAuthentication.FormsCookiePath);

			var encryptedTicket = FormsAuthentication.Encrypt(ticket);

			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
			{
				HttpOnly = true,
				Expires = rememberMe
					? expireDateTime
					: DateTime.MinValue
			};

			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		public static void Logout()
		{
			SetCurrentUser(new AppUserVm());
			FormsAuthentication.SignOut();
			SessionManager.Abandon();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Current user account accessor.
		/// </summary>
		public static AppUserVm CurrentUser
		{
			get
			{
				var currentAppUser = SessionManager.Get<AppUserVm>(SessionKey.CurrentUser);
				if (currentAppUser == null)
				{
					// Create an instance of an unsigned user then set it as the current user.

					// IMPORTANT : Calling the SetCurrentUser and passing a non-authenticated user will only 
					// save the AppUserVm into the session but will not create a cookie for ASP.NET membership.
					currentAppUser = new AppUserVm();
					SetCurrentUser(currentAppUser);
				}

				return currentAppUser;
			}
		}

		public static bool CurrentUserIsLoggedIn => CurrentUser.Identity.IsAuthenticated;

		#endregion

	}
}