using System.Web.Mvc;
using System.Web.Routing;

namespace StatTrack.WEB
{
	public static class RouteConfig
	{
		#region Catch all route

		internal const string CATCH_ALL_ROUTE_DATA_KEY = "DefaultRouteDataKey";
		internal const string CATCH_ALL_ROUTE_NAME = "CatchAllRoute";
		private const string _CATCH_ALL_ROUTE_TEMPLATE = "{controller}/{action}/{*" + CATCH_ALL_ROUTE_DATA_KEY + "}";

		#endregion
		
		#region Default route

		internal const string DEFAULT_ROUTE_NAME = "DefaultRoute";
		private const string _DEFAULT_ROUTE_TEMPLATE = "{controller}/{action}/{id}";

		#endregion

		#region Ignore route

		private const string _ROUTE_IGNORE = "{resource}.axd/{*pathInfo}"; 

		#endregion

		public static void RegisterRoutes()
		{
			RouteTable.Routes.IgnoreRoute(_ROUTE_IGNORE);

			RouteTable.Routes.MapRoute(DEFAULT_ROUTE_NAME, _DEFAULT_ROUTE_TEMPLATE, new
			{
				controller = "Home",
				action = "Index",
				id = ""
			});

			RouteTable.Routes.MapRoute(CATCH_ALL_ROUTE_NAME, _CATCH_ALL_ROUTE_TEMPLATE, new
			{
				controller = "Home",
				action = "Index"
			});
		}
    }
}
