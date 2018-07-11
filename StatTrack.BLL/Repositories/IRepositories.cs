using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using StatTrack.DAL.Models;

namespace StatTrack.BLL.Repositories
{
	public interface IRepositories
	{
		#region Properties

		StggRepository<Sport> Sport { get; }

		StggRepository<SportType> SportType { get; }

		StggRepository<UserProfile> UserProfile { get; }

		StggRepository<Team> Team { get; }

		StggRepository<TeamDivision> TeamDivision { get; }

		StggRepository<TeamDivisionMembership> TeamDivisionMembership { get; }

		StggRepository<TeamDivisionRole> TeamDivisionRole { get; }

		IUserStore<User, int> UserStore { get; }

		IRoleStore<Role, int> RoleStore { get; }


		#endregion

		#region Methods

		int SaveChanges();

		Task<int> SaveChangesAsync();

		#endregion
	}
}