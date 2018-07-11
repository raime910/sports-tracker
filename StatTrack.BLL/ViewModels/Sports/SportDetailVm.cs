using StatTrack.DAL.Models;
using System;

namespace StatTrack.BLL.ViewModels
{
	public class SportDetailVm
	{

		public SportDetailVm()
		{
			
		}

		public SportDetailVm(Sport sport)
		{
			Id = sport.Id;
			Code = sport.Code;
			Name = sport.Name;
			CategoryName = sport.SportType.Name;
			Description = sport.Description;
			StatusId = (int) sport.Status;
			LockedByUserId = sport.LockedByUserId;
			LockedByUser = sport.LockedByUser?.UserName;
			LockedSince = sport.LockedSince;
		}
		
		public int Id { get; set; }

		public string Code { get; set; }
		
		public string Name { get; set; }

		public string CategoryName { get; set; }

		public string Description { get; set; }
		
		public int StatusId { get; set; }

		public string LockedByUser { get; set; }

		public DateTime? LockedSince { get; set; }

		public int? LockedByUserId { get; set; }
	}
}
