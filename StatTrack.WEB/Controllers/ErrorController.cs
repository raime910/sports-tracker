using System.Web.Mvc;

namespace StatTrack.WEB.Controllers
{
	[AllowAnonymous]
	public class ErrorController : StggControllerBase
	{
		public ActionResult Status()
		{
			var statusId = GetRouteData<int>("statusId");
			ViewBag.StatusId = statusId;

			return View();
		}
	}
}