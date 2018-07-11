using StatTrack.BLL.DataManagers.Settings;
using StatTrack.BLL.Repositories;
using StatTrack.BLL.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace StatTrack.BLL.DataManagers
{
	public class SportsTypeManager : StggManagerBase
	{
		public SportsTypeManager(IRepositories repositories, IAppSettings appSettings, AppUserVm currentUser)
			: base(repositories, appSettings, currentUser)
		{
			
		}

		/// <summary>
		/// Get all sport types from the database
		/// </summary>
		public IEnumerable<SportTypeDetailVm> GetAll()
		{
			return Repositories.SportType.All().Select(Factories.SportTypeFactory.BuildOneVmFromDataModel);
		}

		/// <summary>
		/// Get a sport type by id.
		/// </summary>
		/// <param name="id">Id of the sport type.</param>
		public SportTypeDetailVm GetOneById(int id)
		{
			var model = Repositories.SportType.GetOne(x => x.Id == id);
			return Factories.SportTypeFactory.BuildOneVmFromDataModel(model);
		}

		/// <summary>
		/// Create a new sport type.
		/// </summary>
		/// <param name="sportTypeVm">View model that contains data from the user form.</param>
		public SportTypeDetailVm Create(SportTypeVm sportTypeVm)
		{
			var model = Factories.SportTypeFactory.BuildOneDataModelFromVm(sportTypeVm);
			Repositories.SportType.Create(model);
			Repositories.SaveChanges();

			var result = Factories.SportTypeFactory.BuildOneVmFromDataModel(model);
			return result;
		}

		/// <summary>
		/// Updates a sport type.
		/// </summary>
		/// <param name="sportTypeVm">View model that contains data from the user form.</param>
		public SportTypeDetailVm Update(SportTypeVm sportTypeVm)
		{
			var model = Factories.SportTypeFactory.BuildOneDataModelFromVm(sportTypeVm);
			Repositories.SportType.Update(model);
			Repositories.SaveChanges();

			var result = Factories.SportTypeFactory.BuildOneVmFromDataModel(model);
			return result;
		}

		/// <summary>
		/// Deletes a sport type from the database.
		/// </summary>
		/// <param name="id">Id of the sport type.</param>
		public void Delete(int id)
		{
			var model = Repositories.SportType.GetOne(x => x.Id == id);
			Repositories.SportType.Delete(model);
			Repositories.SaveChanges();
		}
	}
}