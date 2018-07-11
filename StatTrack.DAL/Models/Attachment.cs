using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{
	public class Attachment
	{
		public int Id { get; set; }

		[MaxLength(100)]
		public string FileName { get; set; }

		public int FileSizeKb { get; set; }

		public byte[] Content { get; set; }

		public int UploadedById { get; set; }

		[ForeignKey("UploadedById")]
		public virtual User UploadedBy { get; set; }
	}
}
