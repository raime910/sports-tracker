using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StatTrack.DAL.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace StatTrack.BLL.Repositories
{
	public class StggRepositories : IRepositories
	{
		#region Ctor

		/// <summary>
		/// Create a new instance of repository.
		/// </summary>
		/// <param name="dbContext">Db context instance.</param>
		public StggRepositories(DbContext dbContext)
		{
			_dbContext = dbContext;
		} 

		#endregion

		#region Members

		private readonly DbContext _dbContext;

		private StggRepository<Sport> _sportRepo;
		private StggRepository<SportType> _sportTypeRepo;
		private StggRepository<UserProfile> _userProfileRepo;
		private StggRepository<Team> _teamRepo;
		private StggRepository<TeamDivision> _teamDivisionRepo;
		private StggRepository<TeamDivisionMembership> _teamDivisionMembershipRepo;
		private StggRepository<TeamDivisionRole> _teamDivisionRoleRepo;

		private UserStore<User, Role, int, UserLogin, UserRole, UserClaim> _userRepo;
		private RoleStore<Role, int, UserRole> _roleRepo;

		#endregion

		#region Properties

		public StggRepository<Sport> Sport => _sportRepo ?? (_sportRepo = new StggRepository<Sport>(_dbContext));

		public StggRepository<SportType> SportType => _sportTypeRepo ?? (_sportTypeRepo = new StggRepository<SportType>(_dbContext));

		public StggRepository<UserProfile> UserProfile => _userProfileRepo ?? (_userProfileRepo = new StggRepository<UserProfile>(_dbContext));

		public StggRepository<Team> Team => _teamRepo ?? (_teamRepo = new StggRepository<Team>(_dbContext));

		public StggRepository<TeamDivision> TeamDivision => _teamDivisionRepo ?? (_teamDivisionRepo = new StggRepository<TeamDivision>(_dbContext));

		public StggRepository<TeamDivisionMembership> TeamDivisionMembership => _teamDivisionMembershipRepo ?? (_teamDivisionMembershipRepo = new StggRepository<TeamDivisionMembership>(_dbContext));

		public StggRepository<TeamDivisionRole> TeamDivisionRole => _teamDivisionRoleRepo ?? (_teamDivisionRoleRepo = new StggRepository<TeamDivisionRole>(_dbContext));

		public IUserStore<User, int> UserStore => _userRepo ?? (_userRepo = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(_dbContext));

		public IRoleStore<Role, int> RoleStore => _roleRepo ?? (_roleRepo = new RoleStore<Role, int, UserRole>(_dbContext));

		#endregion

		#region Methods

		public int SaveChanges()
		{
			return _dbContext.SaveChanges();
		}

		public Task<int> SaveChangesAsync()
		{
			return _dbContext.SaveChangesAsync();
		}

		#endregion
	}
}
