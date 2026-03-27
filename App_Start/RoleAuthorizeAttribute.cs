using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demomvc.App_Start
{
	public class RoleAuthorizeAttribute: AuthorizeAttribute
	{
		public string RolesRequired { get; set; }
		protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
		{
			var role = httpContext.Session["VaiTro"]?.ToString();
            if (string.IsNullOrEmpty(role))
                return false;
            return RolesRequired.Split(',').Any(r => r.Trim() == role);
		}
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(
                    new { controller = "DangNhap", action = "Index" }
                )
            );
        }
    }
}