using StatTrack.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CommonLib.Extensions;

namespace StatTrack.BLL.ViewModels
{
	public class PersonalInfoManageVm : IValidatableObject
	{
		public PersonalInfoManageVm()
		{
		}

		internal PersonalInfoManageVm(UserProfile userProfile)
		{
			FirstName = userProfile.FirstName;
			LastName = userProfile.LastName;
			Bio = userProfile.Bio;
			BirthDate = userProfile.BirthDate?.ToStdDateString();
			AvatarUrl = userProfile.AvatarUrl;
			SubscribeNewsletter = userProfile.SubscribeNewsletter;
		}

		public int? Id { get; set; }

		[Required]
		[StringLength(25, ErrorMessage = "The {0} must be at least (2) characters long.", MinimumLength = 2)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(25, ErrorMessage = "The {0} must be at least (2) characters long.", MinimumLength = 2)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[MaxLength(2500, ErrorMessage = "Personal bio cannot go over 25000 characters.")]
		[Display(Name = "Bio")]
		public string Bio { get; set; }

		public string AvatarUrl { get; set; }

		[Display(Name = "Subscribe to monthly newsletter")]
		public bool SubscribeNewsletter { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Birth Date")]
		public string BirthDate { get; set; }

		/// <summary>
		/// Validates this instance.
		/// </summary>
		/// <param name="validationContext">Validation context.</param>
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			DateTime birthdate;
			DateTime.TryParse(BirthDate, out birthdate);

			if (birthdate > DateTime.MinValue && birthdate.GetAge() < 18)
			{
				yield return new ValidationResult("You have to be 18 or older.");
			}
		}
	}
}
