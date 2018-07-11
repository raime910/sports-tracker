using System.ComponentModel.DataAnnotations;
using StatTrack.DAL.Models;

namespace StatTrack.BLL.ViewModels
{
	public class EmailManageVm
	{

		public EmailManageVm()
		{

		}

		public EmailManageVm(UserProfile userProfile)
		{
			Email = userProfile.User.Email;
			UserName = userProfile.User.UserName;
		}

		public int Id { get; set; }

		[Display(Name = "User Name")]
		public  string UserName { get; set; }
		
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[DataType(DataType.EmailAddress)]
		[Display(Name = "Confirm Email")]
		[Compare("Email", ErrorMessage = "The email and email confirmation fields do not match.")]
		public string ConfirmEmail { get; set; }
	}
}
