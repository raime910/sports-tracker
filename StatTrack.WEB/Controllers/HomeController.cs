using System.Web.Mvc;

namespace StatTrack.WEB.Controllers
{
	public class HomeController : StggControllerBase
	{
		public ActionResult Index()
		{
			return View();
		}
    }
}