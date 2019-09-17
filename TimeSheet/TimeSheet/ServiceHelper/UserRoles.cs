using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using TimeSheet.Controllers;

namespace TimeSheet.ServiceHelper
{

    //public class IsUserLoggedIn : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        HttpSessionStateBase session = filterContext.HttpContext.Session;
    //        if (session != null && session["Username"] == null)
    //        {
    //            filterContext.Result = new RedirectToRouteResult(
    //                new RouteValueDictionary {
    //                            { "Controller", "Users" },
    //                            { "Action", "Login" }
    //                            });
    //        }
    //    }
    //}
    //public class Permission
    //{
    //    public bool WarehouseManager { get; set; }
    //    public bool Accounts { get; set; }
    //    public bool Administrator { get; set; }
    //    public bool ProjectManagerAdmin { get; set; }

    //}
    public class UserRoles
    {
        public static bool UserCanEditTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["WarehouseManager"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool UserCanRateTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["WarehouseManager"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool UserCanEditCompletedTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["WarehouseManager"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool UserCanRegisterTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["WarehouseManager"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }
            }
            return false;
        }

        // public ActionResult redirectologin() => RedirectToRouteResult("Login", "Login");
    }
}