using Microsoft.AspNet.Identity;
using StatTrack.DAL.Models;

namespace StatTrack.BLL.DataManagers.Security
{
	/// <summary>
	/// Manage application roles. Can only be used inside the service layer.
	/// </summary>
	public class AppRoleManager : RoleManager<Role, int>
	{
		#region Ctor

		internal AppRoleManager(IRoleStore<Role, int> store) : base(store) { }

		#endregion
	}
}
