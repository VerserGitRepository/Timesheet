using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class ResourceController : Controller
    {    
            // GET: Resource
    public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
              ResourceAvailable model = new ResourceAvailable();

                model.ResourceAvailableList = ResourceHelperService.ResourceListItems().Result;
               model.ResourceAvailableList = model.ResourceAvailableList.OrderBy(w => w.Warehouse).OrderBy(x=>x.ResourceName).ToList();
                //model.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");

               

                return View(model);
            }
        }
        //var ResourceAvailable=  ResourceHelperService.ResourceListItems();
        ////  //ResourceAvailable List<ResourceAvailable>

        public ActionResult _ResourceBooked(string ResourceName)
        {
            TimeSheetViewModel model = new TimeSheetViewModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetList().Result;
            model.CandidateTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == ResourceName).ToList();
            return PartialView("_ResourceBooked", model);
        }

        public ActionResult Register()
        {
            TimeSheetViewModel model = new TimeSheetViewModel();
            model.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");
            model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
            var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
            {
                Id = x.Id,
                Value = x.Value
            });
            int opportunityId = listitem.FirstOrDefault().Id;
            model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
            model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
            model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
            model.CandidateTimeSheetList = TimeSheetAPIHelperService.TimeSheetList().Result;
            return PartialView("Register",model);
        }

    }
}