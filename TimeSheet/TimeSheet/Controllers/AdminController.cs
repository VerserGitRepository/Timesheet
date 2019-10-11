using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Username"] != null && Session["Administrator"] != null)
            {               
                return View(ManageListServices.ManageLists());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult Update(AdminModel ModelLists)
        {
            //Use a break point here to watch values
            //Code... (save for example)
            return View();
        }
        [HttpPost]
        public ActionResult UpdateOpportunities(int opportunityID, bool isActive)
        {
            if (Session["Username"] != null && Session["Administrator"] != null)
            {
                var _r= AdminHelperService.UpdateOpportunity(opportunityID, isActive);
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Admin");
                return Json(new { Url = redirectUrl });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }        

        }
        [HttpPost]
        public ActionResult UpdateResourceState(string resourceName, bool isActive)
        {
            if (Session["Username"] != null && Session["Administrator"] != null)
            {
                var _r = AdminHelperService.UpdateResourceState(resourceName, isActive);
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Admin");
                return Json(new { Url = redirectUrl });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult UpdateProjectState(string Project, bool isActive)
        {
            if (Session["Username"] != null && Session["Administrator"] != null)
            {
                var _r = AdminHelperService.UpdateProjectState(Project, isActive);
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Admin");
                return Json(new { Url = redirectUrl });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


        [HttpPost]
        public ActionResult UpdateActivityState(int ServiceActivityID, bool isActive)

        {
            if (Session["Username"] != null && Session["Administrator"] != null)
            {
                var _r = AdminHelperService.UpdateActivity(ServiceActivityID, isActive);
        var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Admin");
                return Json(new { Url = redirectUrl
    });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }        

        }
    }

}
