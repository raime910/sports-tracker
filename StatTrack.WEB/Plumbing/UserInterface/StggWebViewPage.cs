using System.Web;
using System.Web.Mvc;
using StatTrack.BLL.DataManagers.Settings;
using StatTrack.WEB.Plumbing.Config;

namespace StatTrack.WEB.Plumbing
{
	public class StggWebViewPage : WebViewPage
	{
		protected const string HTTP_POST = "POST";
		protected const string HTTP_GET = "GET";

		public override void Execute()
		{

        }

        private string GetBaseUrl()
        {
            return Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        }

        private IAppSettings _appSettings;

		protected IAppSettings AppSettings => _appSettings ?? (_appSettings = new AppSettings(HttpContext.Current.Server.MapPath("~"), GetBaseUrl()));
	}

	public class StggWebViewPage<TModel> : WebViewPage<TModel> where TModel : class
	{
		protected const string HTTP_POST = "POST";
		protected const string HTTP_GET = "GET";

		public override void Execute()
		{

        }

        private string GetBaseUrl()
        {
            return Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        }

        private IAppSettings _appSettings;

		protected IAppSettings AppSettings => _appSettings ?? (_appSettings = new AppSettings(HttpContext.Current.Server.MapPath("~"), GetBaseUrl()));
	}
}