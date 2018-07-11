using StatTrack.BLL.DataManagers;
using StatTrack.BLL.ViewModels;
using StatTrack.DAL.Models.Enums;
using StatTrack.WEB.Plumbing.Extension;
using StatTrack.WEB.Plumbing.Security;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StatTrack.WEB.Controllers
{
	[AllowAnonymous]
	public class AccountController : StggControllerBase
	{

		#region Constants

		private const string _TOKEN_KEY = "token";
		private const string _USERNAME_KEY = "username";

		#endregion

		#region Registration

		/// <summary>
		/// Register view.
		/// </summary>
		public ActionResult Register()
		{
			return View();
		}

		/// <summary>
		/// Register a new user.
		/// </summary>
		/// <param name="appUserRegisterVm">Registration vew model.</param>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(AppUserRegisterVm appUserRegisterVm)
		{
			var result = View();

			// If registerVm is null, then return the empty registration form
			if (appUserRegisterVm == null) return result;
			if (appUserRegisterVm.AcceptTermsOfService == false)
			{
				ModelState.AddModelSummaryError("You need to read and accept the Terms of Service.");
			}

			// If model is valid, continue with the registration
			if (ModelState.IsValid)
			{
				// Register the user through the service.
				var stggResult = await Managers.UserAccountManager.RegisterAsync(appUserRegisterVm);

				if (stggResult.Status == StggResultStatus.Succeeded)
				{
					StggSecurityContext.SetCurrentUser(stggResult.Value);
					return RedirectToAction("EmailConfirmTokenSent");
				}
				else
				{
					ModelState.AddModelSummaryError(stggResult.Errors);
				}
			}

			// Model is invalid, bring the user back to the page.
			return View(appUserRegisterVm);
		}

		#endregion

		#region Login and logout

		/// <summary>
		/// Login view.
		/// </summary>
		public ActionResult Login()
		{
			return View();
		}

		/// <summary>
		/// This action handles user registration.
		/// </summary>
		/// <param name="appUserLoginVm"></param>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(AppUserLoginVm appUserLoginVm)
		{
			// Initialize the view.
			var result = View();

			// If AppUserLoginVm is null return the form.
			if (appUserLoginVm == null) return result;

			// If model is valid, continue with the authentication
			if (ModelState.IsValid)
			{
				// Validate username and password.
				var stggResult = await Managers.UserAccountManager.LoginAsync(appUserLoginVm);
				var appUserVm = stggResult.Value;

				switch (appUserVm.SignInStatus)
				{
					case SignInStatus.Success:
						// Set the current application user.
						StggSecurityContext.SetCurrentUser(appUserVm, appUserLoginVm.RememberMe);
						// User passed the login challenge, redirect to the homepage.
						return RedirectToAction("Index", "Home");
					case SignInStatus.RequiresVerification:
						// User passed the login challenge but needs to verify email.
						return RedirectToAction("EmailConfirmTokenSent");
					case SignInStatus.LockedOut:
						// User is currently locked out.
						return RedirectToAction("LockedOut");
					default:
						// Invalid username or password
						ModelState.AddModelSummaryError("Username or password is invalid.");
						break;
				}
			}

			result = View(appUserLoginVm);

			return result;
		}

		/// <summary>
		/// End user session.
		/// </summary>
		public ActionResult Logout()
		{
			// Abandon session then display the page.
			StggSecurityContext.Logout();

			return RedirectToAction("Login");
		}

		/// <summary>
		/// Display a message saying that the user has been locked out.
		/// </summary>
		public ActionResult LockedOut()
		{
			return View();
		}

		#endregion

		#region Email Confirmation

		/// <summary>
		/// Confirms the email of the user.
		/// </summary>
		public async Task<ActionResult> EmailConfirm()
		{
			var userName = GetRouteData<string>(_USERNAME_KEY);
			var token = GetRouteData<string>(_TOKEN_KEY);

			// Confirm the email...
			var stggResult = await Managers.UserAccountManager.ValidateEmailConfirmationTokenAsync(userName, token);

			return RedirectToAction(
				stggResult.Status == StggResultStatus.Succeeded
					? "EmailConfirmSuccess"
					: "EmailConfirmFailed");
		}

		/// <summary>
		/// Display a page where the user can request email confirmation.
		/// </summary>
		public ActionResult EmailConfirmTokenSent()
		{
			return View();
		}

		/// <summary>
		/// Page to request email confirmation.
		/// </summary>
		public ActionResult EmailConfirmRequest()
		{
			return View();
		}

		/// <summary>
		/// Process request for email confirmation and send another one to the user.
		/// </summary>
		/// <param name="emailConfReqVm">View model that contains the email address of the requestor.</param>
		[HttpPost]
		public async Task<ActionResult> EmailConfirmRequest(EmailConfirmRequestVm emailConfReqVm)
		{
			var result = View(emailConfReqVm);

			if (ModelState.IsValid)
			{
				var stggResult = await Managers.UserAccountManager.RequestEmailConfirmationTokenAsync(emailConfReqVm);
				if (stggResult.Status == StggResultStatus.Succeeded)
				{
					return RedirectToAction("EmailConfirmTokenSent");
				}
				else
				{
					return RedirectToAction("Status", "Error", new { @id = 500 });
				}
			}

			return result;
		}

		/// <summary>
		/// Displays email confirmed page.
		/// </summary>
		public ActionResult EmailConfirmSuccess()
		{
			return View();
		}

		/// <summary>
		/// Displays email confirmed page.
		/// </summary>
		public ActionResult EmailConfirmFailed()
		{
			return View();
		}

		#endregion

		#region Password Reset

		/// <summary>
		/// Returns the page where they can enter their email address to request for another token
		/// </summary>
		public ActionResult PasswordResetRequest()
		{
			return View();
		}

		/// <summary>
		/// User enters his/her email and submitted the form.
		/// </summary>
		/// <param name="passLostVm"></param>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> PasswordResetRequest(PasswordLostVm passLostVm)
		{
			var result = View(passLostVm);

			if (ModelState.IsValid)
			{
				// Create and send the new token.
				var stggResult = await Managers.UserAccountManager.RequestPasswordResetTokenAsync(passLostVm);

				return stggResult.Status == StggResultStatus.Succeeded
					? RedirectToAction("PasswordResetTokenSent")
					: RedirectToAction("Status", "Error", new { @id = 500 });
			}

			return result;
		}

		/// <summary>
		/// Password reset token sent.
		/// </summary>
		public ActionResult PasswordResetTokenSent()
		{
			return View();
		}

		/// <summary>
		/// Returns the page where the user can change his or her password.
		/// </summary>
		public async Task<ActionResult> PasswordReset()
		{
			var userName = GetRouteData<string>(_USERNAME_KEY);
			var token = GetRouteData<string>(_TOKEN_KEY);

			// Validate the information.
			var stggResult = await Managers.UserAccountManager.ValidateResetPasswordTokenAsync(userName, token);

			if (stggResult.Status == StggResultStatus.Succeeded)
			{
				var passResetVm = stggResult.Value;
				if (passResetVm != null && passResetVm.IsValidResetPassToken)
				{
					return View(passResetVm);
				}
			}

			return RedirectToAction("PasswordResetFailed");
		}

		/// <summary>
		/// Change the password.
		/// </summary>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> PasswordReset(PasswordResetVm passResetVm)
		{
			if (ModelState.IsValid)
			{
				// Change the account password.
				var stggResult = await Managers.UserAccountManager.ResetPasswordAsync(passResetVm);

				if (stggResult.Status == StggResultStatus.Succeeded)
				{
					return RedirectToAction("PasswordResetSuccess");
				}
			}

			return RedirectToAction("PasswordResetFailed");
		}

		/// <summary>
		/// Manage current users login password.
		/// </summary>
		[StggAuthorize]
		public PartialViewResult PasswordManage()
		{
			return PartialView("_PasswordManage");
		}

		/// <summary>
		/// Update current users login password.
		/// </summary>
		[HttpPost]
		[StggAuthorize]
		[ValidateAntiForgeryToken]
		public PartialViewResult PasswordManage(PasswordManageVm passwordManageVm)
		{
			if (ModelState.IsValid)
			{
				var stggResult = Managers.UserAccountManager.UpdatePassword(passwordManageVm);
				ModelState.AddModelSummaryError(stggResult.Errors);
			}

			return PartialView("_PasswordManage", passwordManageVm);
		}

		public ActionResult PasswordResetSuccess()
		{
			return View();
		}

		public ActionResult PasswordResetFailed()
		{
			return View();
		}

		#endregion

		#region Profile

		/// <summary>
		/// Displays the information about the user.
		/// </summary>
		/// <param name="userName">Optional user name parameter. If empty, the current user profile is displayed.</param>
		public async Task<ActionResult> Display(string userName = "")
		{
			if (string.IsNullOrEmpty(userName) && StggSecurityContext.CurrentUser.Identity.IsAuthenticated)
			{
				userName = StggSecurityContext.CurrentUser.Identity.Name;

				var stggResult = await Managers.UserAccountManager.FindByUserNameAsync(userName);

				if (stggResult.Status == StggResultStatus.Succeeded)
				{
					return View(stggResult.Value);
				}
			}

			return RedirectToAction("Status", "Error", new { @id = 500 });
		}

		#endregion

	}
}