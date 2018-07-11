using System;

namespace StatTrack.MDL.Infos
{
	public class SportTypeDetailVm
	{
		public int Id { get; set; }

		public string Code { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string LockedByUser { get; set; }

		public DateTime? LockedSince { get; set; }
	}
}
