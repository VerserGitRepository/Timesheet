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
                return View(ManageListServices.ManageLists());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }          

        }
        [HttpPost]
        public ActionResult UpdateResourceState(int resourceID, bool isActive)
        {
            if (Session["Username"] != null && Session["Administrator"] != null)
            {
                var _r = AdminHelperService.UpdateResourceState(resourceID, isActive);
                return View(ManageListServices.ManageLists());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        [HttpPost]
        public ActionResult UpdateProjectState(int ProjectID, bool isActive)
        {
            if (Session["Username"] != null && Session["Administrator"] != null)
            {
                var _r = AdminHelperService.UpdateOpportunity(ProjectID, isActive);
                return View(ManageListServices.ManageLists());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

    }

}
