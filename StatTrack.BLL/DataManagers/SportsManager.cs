using StatTrack.BLL.DataManagers.Settings;
using StatTrack.BLL.Repositories;
using StatTrack.BLL.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace StatTrack.BLL.DataManagers
{
	public class SportsManager : StggManagerBase
	{
		public SportsManager(IRepositories repositories, IAppSettings appSettings, AppUserVm currentUser)
			: base(repositories, appSettings, currentUser)
		{
		}

		/// <summary>
		/// Get all sports from the database
		/// </summary>
		public IEnumerable<SportDetailVm> GetAll()
		{
			return Repositories.Sport.All().Select(x => new SportDetailVm(x));
		}

		/// <summary>
		/// Get a sport by id.
		/// </summary>
		/// <param name="sportsId">Id of the sport type.</param>
		public SportDetailVm GetOneById(int sportsId)
		{
			var sport = Repositories.Sport.GetOne(x => x.Id == sportsId);
			return new SportDetailVm(sport);
		}

		/// <summary>
		/// Create a new sport.
		/// </summary>
		/// <param name="sportVm">View model that contains data from the user form.</param>
		public SportDetailVm Create(SportEditorVm sportEditorVm)
		{
			var sport = Factories.SportFactory.BuildOneSport_BySportsVm(sportEditorVm);
			Repositories.Sport.Create(sport);
			Repositories.SaveChanges();

			var result = new SportDetailVm(sport);
			return result;
		}

		/// <summary>
		/// Updates a sport.
		/// </summary>
		/// <param name="sportVm">View model that contains data from the user form.</param>
		public SportDetailVm Update(SportEditorVm sportEditorVm)
		{
			var sport = Factories.SportFactory.BuildOneSport_BySportsVm(sportEditorVm);
			Repositories.Sport.Update(sport);
			Repositories.SaveChanges();

			var result = new SportDetailVm(sport);
			return result;
		}

		/// <summary>
		/// Deletes a sport from the database.
		/// </summary>
		/// <param name="id">Id of the sport type.</param>
		public void Delete(int id)
		{
			var sport = Repositories.Sport.GetOne(x => x.Id == id);
			Repositories.Sport.Delete(sport);
			Repositories.SaveChanges();
		}
	}
}