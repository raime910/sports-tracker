using System;
using System.ComponentModel.DataAnnotations;
using StatTrack.DAL.Models;

namespace StatTrack.BLL.ViewModels
{
	public class TeamDetailVm
	{
		public TeamDetailVm()
		{

		}

		public TeamDetailVm(Team team)
		{
			Id = team.Id;
			Code = team.Code;
			Name = team.Name;
			Description = team.Description;
			StatusText = team.Status.ToString();
			DateCreated = team.DateCreated;

			//OwnerName = "";
			//MemberCount = team.TeamMemberships.Count;
		}

		public int Id { get; set; }
		
		public string Code { get; set; }
		
		public string Name { get; set; }
		
		public string Description { get; set; }
		
		[Display(Name = "Owner Name")]
		public string OwnerName { get; set; }

		[Display(Name = "Status")]
		public string StatusText { get; set; }

		[Display(Name = "Member Count")]
		public int MemberCount { get; set; }

		[Display(Name = "Date Created")]
		public DateTime DateCreated { get; set; }
	}
}
