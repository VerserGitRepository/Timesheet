using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class CompletedBookingController : Controller
    {
        // GET: CompletedTask
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                CompletedTimesheetModel model = new CompletedTimesheetModel();
                model.CompletedTimeSheetList = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
                return View(model);
            }
        }
    }
}