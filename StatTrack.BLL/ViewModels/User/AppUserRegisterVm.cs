using System.ComponentModel.DataAnnotations;

namespace StatTrack.BLL.ViewModels
{
	public class AppUserRegisterVm
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email address")]
		public string Email { get; set; }
		
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Confirm email address")]
		[Compare("Email", ErrorMessage = "The email and email confirmation fields do not match.")]
		public string ConfirmEmail { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least (6) characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and password confirmation fields do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[StringLength(15, ErrorMessage = "The {0} must be at least (6) characters long.", MinimumLength = 6)]
		[Display(Name = "Username")]
		public string UserName { get; set; }

		[Required]
		[StringLength(25, ErrorMessage = "The {0} must be at least (2) characters long.", MinimumLength = 2)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(25, ErrorMessage = "The {0} must be at least (2) characters long.", MinimumLength = 2)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Subscribe to monthly newsletter")]
		public bool SubscribeNewsletter { get; set; }
		
		[Display(Name = "Accept terms of service")]
		public bool AcceptTermsOfService { get; set; }
	}
}
