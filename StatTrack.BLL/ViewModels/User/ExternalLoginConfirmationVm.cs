using System.ComponentModel.DataAnnotations;

namespace StatTrack.BLL.ViewModels
{
	/// <summary>
	/// Used for external login confirmation.
	/// </summary>
	public class ExternalLoginConfirmationVm
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email address")]
		public string Email { get; set; }
	}
}
