using StatTrack.BLL.ViewModels;
using StatTrack.WEB.Plumbing.Extension;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StatTrack.WEB.Controllers
{
	public class TeamController : StggControllerBase
	{

		#region Constants



		#endregion

		#region Private methods

		/// <summary>
		/// Get the items for the sport select list.
		/// </summary>
		public SelectList GetSportSelectList()
		{
			return Managers.SportsManager.GetAll().ToSelectList(x => x.Id, x => x.Name, x => x.CategoryName);
		}

		#endregion

		#region Registration

		/// <summary>
		/// GET : Register a new team.
		/// </summary>
		public ActionResult Register()
		{
			ViewBag.SportsList = GetSportSelectList();
			return View();
		}

		/// <summary>
		/// POST : Register new team.
		/// </summary>
		/// <param name="teamRegisterVm">Team editor that contains team information.</param>
		[HttpPost]
		public async Task<ActionResult> Register(TeamEditorVm teamRegisterVm)
		{
			if (ModelState.IsValid)
			{
				var teamDetailVm = await Managers.TeamManager.CreateAsync(teamRegisterVm);
				return RedirectToAction("Details", new { id = teamDetailVm.Id });
			}

			ViewBag.SportsList = GetSportSelectList();
			return View(teamRegisterVm);
		}

		#endregion

		#region Display

		public async Task<ActionResult> Details(int id)
		{
			var teamDetailVm = await Managers.TeamManager.GetOneByIdAsync(id);
			return View(teamDetailVm);
		}

		#endregion

	}
}