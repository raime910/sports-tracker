using System.ComponentModel.DataAnnotations;

namespace StatTrack.BLL.ViewModels
{
	/// <summary>
	/// View model used for requesting new token. Email address provided here will be recieve an email containing a new confirmation token.
	/// </summary>
	public class EmailConfirmRequestVm
	{
		/// <summary>
		/// Email address where we send the confirmation to.
		/// </summary>
		[Required]
		[EmailAddress]
		[Display(Name = "Email address")]
		public string Email { get; set; }
	}
}
