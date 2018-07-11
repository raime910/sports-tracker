using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StatTrack.BLL.ViewModels
{
	public class TeamEditorVm : IValidatableObject
	{
		/// <summary>
		/// Team tag.
		/// </summary>
		[Required]
		[StringLength(10)]
		public string Code { get; set; }

		/// <summary>
		/// Id of the sport for this team.
		/// </summary>
		[Required]
		[Display(Name = "Sports")]
		public int SportId { get; set; }

		/// <summary>
		/// A list of user ids that will recieve an invite.
		/// </summary>
		[Display(Name = "Search for members")]
		public List<int> MemberIds { get; set; }

		/// <summary>
		/// Team name.
		/// </summary>
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		/// <summary>
		/// A description for this team.
		/// </summary>
		[Required]
		[StringLength(5000)]
		public string Description { get; set; }

		/// <summary>
		/// Banner url.
		/// </summary>
		[Required]
		[Display(Name = "Banner Image")]
		public HttpPostedFileBase BannerFile { get; set; }

		/// <summary>
		/// Team logo url.
		/// </summary>
		[Required]
		[Display(Name = "Logo Image")]
		public HttpPostedFileBase LogoFile { get; set; }

		/// <summary>
		/// Twitter url.
		/// </summary>
		[Display(Name = "Twitter")]
		public string TwitterUrl { get; set; }

		/// <summary>
		/// Facebook url.
		/// </summary>
		[Display(Name = "Facebook")]
		public string FacebookUrl { get; set; }

		/// <summary>
		/// Steam Group url.
		/// </summary>
		[Display(Name = "Steam Group")]
		public string SteamGroupUrl { get; set; }

		/// <summary>
		/// YouTube url.
		/// </summary>
		[Display(Name = "YouTube")]
		public string YouTubeUrl { get; set; }

		/// <summary>
		/// Twitch Url.
		/// </summary>
		[Display(Name = "Twitch")]
		public string TwitchUrl { get; set; }

		/// <summary>
		/// Website Url.
		/// </summary>
		[Display(Name = "Website")]
		public string WebsiteUrl { get; set; }

		/// <summary>
		/// Id of the creator if this team.
		/// </summary>
		public int CreatorId { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var bannerFileExt = LogoFile.FileName.Substring(LogoFile.FileName.LastIndexOf('.') + 1);
			var logoFileExt = LogoFile.FileName.Substring(LogoFile.FileName.LastIndexOf('.') + 1);

			var imageFileEx = new[] { "png", "jpg", "jpeg" };

			if (!imageFileEx.Contains(bannerFileExt))
			{
				yield return new ValidationResult($"The Banner file format is invalid please upload a file with the following extensions ({ string.Join(", ", imageFileEx) }).");
			}

			if (!logoFileExt.Contains(bannerFileExt))
			{
				yield return new ValidationResult($"The Logo file format is invalid please upload a file with the following extensions ({ string.Join(", ", imageFileEx) }).");
			}
		}
	}
}
