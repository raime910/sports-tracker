using System.ComponentModel.DataAnnotations;

namespace StatTrack.BLL.ViewModels
{
	/// <summary>
	/// This model is used to wrap login information entered by the person who's logging in.
	/// </summary>
	public class AppUserLoginVm
	{
		/// <summary>
		/// Username of the person loggin into the system.
		/// </summary>
		[Required]
		[Display(Name = "Username")]
		public string UserName { get; set; }

		/// <summary>
		/// Password to the account.
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		/// <summary>
		/// True if the user wishes to stay logged in even after the browser has been closed.
		/// </summary>
		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}
