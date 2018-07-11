using System.ComponentModel.DataAnnotations;

namespace StatTrack.BLL.ViewModels
{
	public class SportEditorVm : StggViewModelBase
	{
		public int? Id { get; set; }

		[Required]
		[MaxLength(10)]
		public string Code { get; set; }

		[Required]
		[MaxLength(50)]
		[MinLength(5)]
		public string Name { get; set; }

		[Required]
		[MaxLength(5000)]
		public string Description { get; set; }

		[Required]
		public int StatusId { get; set; }
	}
}
