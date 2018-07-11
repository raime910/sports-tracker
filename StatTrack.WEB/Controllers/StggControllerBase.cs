using StatTrack.BLL;
using StatTrack.BLL.DataManagers.Settings;
using StatTrack.WEB.Plumbing.Config;
using StatTrack.WEB.Plumbing.Security;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StatTrack.WEB.Controllers
{
    // [RequireHttps]
    [StggAuthorize]
	public class StggControllerBase : Controller
	{

		#region Private members

		private AppSettings _appSettings;
		private Managers _managers;

		#endregion

		#region Action filters

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			// Parse the route data coming from the request url.
			ParseUrlRouteData();
		}

		#endregion

		#region Properties

        private string GetBaseUrl()
        {
            return Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        }

		private IAppSettings AppSettings => _appSettings ?? (_appSettings = new AppSettings(Server.MapPath("~"), GetBaseUrl()));

		protected Managers Managers => _managers ?? (_managers = new Managers(AppSettings, StggSecurityContext.CurrentUser));

		#endregion

		#region Route data

		private Dictionary<string, string> _urlRouteData;

		/// <summary>
		/// Dictionary of route data that came from the url.
		/// </summary>
		private Dictionary<string, string> UrlRouteData => _urlRouteData ?? (_urlRouteData = new Dictionary<string, string>());

		/// <summary>
		/// Splits the route data into a dictionary so the controller can use it.
		/// </summary>
		private void ParseUrlRouteData()
		{
			if (RouteData.Values[RouteConfig.CATCH_ALL_ROUTE_DATA_KEY] == null) return;

			// We get all the values then we split them into a array then move it into our dictionary.
			var routeData = RouteData.Values[RouteConfig.CATCH_ALL_ROUTE_DATA_KEY].ToString();
			var routeDataArr = routeData.Split('/');
			var inPairs = (routeDataArr.Length % 2) == 0;

			if (inPairs)
			{
				for (var i = 0; i < routeDataArr.Length - 1; i += 2)
				{
					UrlRouteData.Add(routeDataArr[i].ToLower(), routeDataArr[i + 1]);
				}
			}
			else
			{
				throw new Exception("Route Data has to be in pairs.");
			}
		}

		/// <summary>
		/// Get data from the route collection.
		/// </summary>
		/// <typeparam name="T">Desired return type.</typeparam>
		/// <param name="key">Key of the route data item.</param>
		/// <param name="defaultVal">Optional default return value in case the route key does not exist in the route data collection.</param>
		/// <returns>Returns a route data value.</returns>
		protected T GetRouteData<T>(string key = "id", T defaultVal = default(T))
		{
			var result = defaultVal;
			var lowerKey = key.ToLower();

			if (UrlRouteData.ContainsKey(lowerKey))
			{
				// Found the value in the dictionary, let's return that value.
				result = (T)Convert.ChangeType(UrlRouteData[lowerKey], typeof(T));
			}

			return result;
		}

		#endregion

	}
}