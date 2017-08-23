using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session==null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "LogIn", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
        public enum AlertType
        {
            Danger = 0,
            Success = 1,
            Warning = 2,
            Info = 3
        }
        protected void SetAlert(string message,AlertType alertType = AlertType.Info)
        {
            TempData["AlertMessage"] = message;
            switch (alertType)
            {
                case AlertType.Danger: TempData["AlertType"] = "alert-danger";
                    break;
                case AlertType.Success: TempData["AlertType"] = "alert-success";
                    break;
                case AlertType.Warning: TempData ["AlertType"] = "alert-warning";
                    break;
                default:
                    TempData["AlertType"] = "alert-info";
                    break;
            }
        }
    }
}