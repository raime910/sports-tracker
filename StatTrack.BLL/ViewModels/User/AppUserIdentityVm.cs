using System.Security.Principal;

namespace StatTrack.BLL.ViewModels
{

	public class AppUserIdentityVm : IIdentity
	{
		internal AppUserIdentityVm(string name, bool isAuthenticated = false)
		{
			Name = name;
			AuthenticationType = "ASP.NET Identity Core 2.0.0.0";
			IsAuthenticated = isAuthenticated;
		}

		public string AuthenticationType { get; }

		public bool IsAuthenticated { get; }

		public string Name { get; }
	}
}
