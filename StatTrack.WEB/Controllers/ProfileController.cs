using StatTrack.BLL.DataManagers;
using StatTrack.BLL.ViewModels;
using StatTrack.WEB.Plumbing.Extension;
using StatTrack.WEB.Plumbing.Security;
using System.Web.Mvc;

namespace StatTrack.WEB.Controllers
{
	public class ProfileController : StggControllerBase
    {

		#region Ctor

		public ActionResult Editor()
		{
			return View();
		}

		#endregion

		#region Account information
		
		public ActionResult EmailManage()
		{
			var result = Managers.ProfileManager.GetAccountInfo(StggSecurityContext.CurrentUser.Id);
			
			return result.Status == StggResultStatus.Succeeded
				? PartialView("_EmailManage", result.Value)
				: PartialView("_EmailManage");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EmailManage(EmailManageVm emailManageVm)
		{
			if (ModelState.IsValid)
			{
				var result = Managers.UserAccountManager.UpdateEmailAddress(emailManageVm);

				if (result.Status == StggResultStatus.Failed)
				{
					ModelState.AddModelSummaryError(result);
				}
            }
            else
            {
                Response.StatusCode = 400;
            }

            return PartialView("_EmailManage", emailManageVm);
		}

		#endregion

		#region Personal information
		
		public ActionResult PersonalInfoManage()
		{
			var result = Managers.ProfileManager.GetPersonalInfo(StggSecurityContext.CurrentUser.Id);
			
			return result.Status == StggResultStatus.Succeeded 
				? PartialView("_PersonalInfoManage", result.Value) 
				: PartialView("_PersonalInfoManage");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult PersonalInfoManage(PersonalInfoManageVm personalInfoEditorVm)
		{
            if (ModelState.IsValid)
            {
                Managers.ProfileManager.UpdatePersonalInfo(personalInfoEditorVm);
            }
            else
            {
                Response.StatusCode = 400;
            }

			return PartialView("_PersonalInfoManage", personalInfoEditorVm);
		} 

		#endregion

	}
}