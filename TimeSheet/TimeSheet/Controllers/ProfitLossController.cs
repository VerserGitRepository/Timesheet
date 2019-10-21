using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class ProfitLossController : Controller
    {
        // GET: ProfitLoss
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ProfitLossModel model = new ProfitLossModel();
                model.ProfitLossList = TimeSheetAPIHelperService.ProfitLoss().Result;
                // model.HasUserPermissionsToEdit = UserRoles.UserCanEditTimesheet();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult CostDetails(string opportunityNumber)
        {
            ProfitLossModel model = new ProfitLossModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.ProfitLoss().Result;
            model.ProfitLossList = AlltimesheetRecords.Where(c => c.opportunityNumber == opportunityNumber).ToList();
           
            return PartialView("CostDetails", model);
        }
    }
}