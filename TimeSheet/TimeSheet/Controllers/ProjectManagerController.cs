using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class ProjectManagerController : Controller
    {
        
        // GET: ProjectManager
        public ActionResult Register()
        {
            var modelregister = new PMRegisterViewModel()
            {
                Day = DateTime.Now,
                StartTime= DateTime.Now,
                EndTime = DateTime.Now,
                ServiceActivityID = 5,
                WarehouseID=1,
                ProjectID = 7,
                OpportunityID=5,
                Colour = "red",
                TimeSheetComments = "test",
                ResourceID=251
            };


            var a = RegisterTimesheetService.RegisterPMBooking(modelregister);
            return View();
        }
    }
}