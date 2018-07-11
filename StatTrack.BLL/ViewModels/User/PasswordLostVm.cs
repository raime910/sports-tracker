using System.ComponentModel.DataAnnotations;

namespace StatTrack.BLL.ViewModels
{
	public class PasswordLostVm
	{
		[Display(Name = "UserName")]
		public string UserName { get; set; }

		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		internal bool ValidateByUserName => !string.IsNullOrEmpty(UserName);
	}
}
