using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatTrack.WEB.Plumbing.Security
{
	public class StggAuthorize : AuthorizeAttribute
	{
		private bool _skipAuthorization;

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			// We don't check for authorization if 
			// 1. The action has AllowAnonymous attribute
			// 2. The controller has AllowAnonymous attribute
			// 3. If the action is a child action
			_skipAuthorization =
				filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any() ||
				filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any() ||
				filterContext.IsChildAction;

			base.OnAuthorization(filterContext);
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return _skipAuthorization || StggSecurityContext.CurrentUser.Identity.IsAuthenticated;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			var urlHelper = new UrlHelper(filterContext.RequestContext);
			var redirectUrl = urlHelper.RouteUrl(RouteConfig.CATCH_ALL_ROUTE_NAME, new
			{
				controller = "Account",
				action = "Login"
			});

			if (redirectUrl != null) filterContext.HttpContext.Response.Redirect(redirectUrl, true);
		}
	}
}