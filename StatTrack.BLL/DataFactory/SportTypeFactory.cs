using StatTrack.BLL.ViewModels;
using StatTrack.DAL.Models;

namespace StatTrack.BLL.DataFactory
{
	internal class SportTypeFactory
	{

		/// <summary>
		/// Build one data model from a sport type view model.
		/// </summary>
		/// <param name="sportTypeVm">Sport type view model instance.</param>
		internal SportType BuildOneDataModelFromVm(SportTypeVm sportTypeVm)
		{
			var result = new SportType
			{
				Code = sportTypeVm.Code,
				Name = sportTypeVm.Name,
				Description = sportTypeVm.Description
			};

			return result;
		}

		/// <summary>
		/// Build one view model from data model.
		/// </summary>
		/// <param name="dataModel">Sport type data model instance.</param>
		internal SportTypeDetailVm BuildOneVmFromDataModel(SportType dataModel)
		{
			var result = new SportTypeDetailVm
			{
				Code = dataModel.Code,
				Name = dataModel.Name,
				Description = dataModel.Description,
				LockedByUserId = dataModel.LockedByUserId,
				LockedByUser = dataModel.LockedByUser.UserName,
				LockedSince = dataModel.LockedSince
			};

			return result;
		}

	}
}
