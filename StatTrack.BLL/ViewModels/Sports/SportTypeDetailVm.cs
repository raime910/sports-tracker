using System;

namespace StatTrack.BLL.ViewModels
{
	public class SportTypeDetailVm
	{
		public int Id { get; set; }

		public string Code { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string LockedByUser { get; set; }

		public int? LockedByUserId { get; set; }

		public DateTime? LockedSince { get; set; }
	}
}
