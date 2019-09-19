using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class FinanaceController : Controller
    {
        // GET: Finanace
        public ActionResult Index()
        {

            if (Session["Username"] != null && Session["Accounts"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
          
        }
        public ActionResult WeeklyReport()
        {

            if (Session["Username"] != null && Session["Accounts"] != null)
            {

             var model=   TimeSheetAPIHelperService.TimeSheetApprovedList().Result;
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult MonthlyReport()
        {

            if (Session["Username"] != null && Session["Accounts"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult OverTimeReport()
        {

            if (Session["Username"] != null && Session["Accounts"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
    }
}