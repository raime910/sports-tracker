using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatTrack.DAL.Models
{
	public class Country
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(2)]
		[Index(IsUnique = true)]
		public string Code { get; set; }
		
		[MaxLength(50)]
		public string FlagThumb { get; set; }
	}
}
