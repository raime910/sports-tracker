using StatTrack.BLL.ViewModels;
using StatTrack.DAL.Models;
using StatTrack.DAL.Models.Enums;

namespace StatTrack.BLL.DataFactory
{
	internal class SportFactory
	{
		/// <summary>
		/// Build one from view dataModel.
		/// </summary>
		/// <param name="sportEditorVm">Sports editor view dataModel instance to build from.</param>
		internal Sport BuildOneSport_BySportsVm(SportEditorVm sportEditorVm)
		{
			var result = new Sport
			{
				Code = sportEditorVm.Code,
				Name = sportEditorVm.Name,
				Description = sportEditorVm.Description,
				Status = (SportStatus)sportEditorVm.StatusId
			};

			return result;
		}
	}
}
