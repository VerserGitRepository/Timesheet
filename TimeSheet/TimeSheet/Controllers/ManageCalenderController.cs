using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class ManageCalenderController : Controller
    {
        [OutputCache(CacheProfile = "HalfHour", VaryByHeader = "X-Requested-With", Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
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
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Index(TimeSheetViewModel SearchModel)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TimeSheetViewModel model = new TimeSheetViewModel();
                model.Projectlist = new SelectList(TimeSheetAPIHelperService.JMSProjects().Result, "ID", "Value");
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
                var Search = new SearchViewModel
                {
                    ProjectID = SearchModel.ProjectID,
                    WarehouseNameId = SearchModel.WarehouseID,
                    CandidateNameId = SearchModel.ResourceID,
                    OpportunityNumberID = SearchModel.OpportunityID
                };

                model.CandidateTimeSheetList = SearchFilterService.SearchTimeSheetRecord(Search).Result;
                return View(model);
            }
        }
        [HttpGet]
        public JsonResult GetEvents()
        {
            List<TimeSheetViewModel> model = new List<TimeSheetViewModel>();
            model = TimeSheetAPIHelperService.TimeSheetList().Result;
            model = model.ToList();
            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult SearchEvents(string ProjectID, string warehouseId, string opportunityId, string candidateId)
        {
            List<TimeSheetViewModel> model = new List<TimeSheetViewModel>();
            SearchViewModel searchModel = new SearchViewModel()
            {
                CandidateNameId = String.IsNullOrEmpty(candidateId) ? (int?)null : Convert.ToInt32(candidateId),
                WarehouseNameId = String.IsNullOrEmpty(warehouseId) ? (int?)null : Convert.ToInt32(warehouseId),
                OpportunityNumberID = String.IsNullOrEmpty(opportunityId) ? (int?)null : Convert.ToInt32(opportunityId),
                ProjectID = String.IsNullOrEmpty(ProjectID) ? (int?)null : Convert.ToInt32(ProjectID)

            };
            model = SearchFilterService.SearchTimeSheetRecord(searchModel).Result;
            model = model.ToList();
            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult ProjectOpportunities(int projectId)
        {
            List<ListItemViewModel> load = new List<ListItemViewModel>();
                load = TimeSheetAPIHelperService.ProjectOpportunities(projectId).Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                }).ToList();           
            return Json(load, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ProjectActivities(int OpportunityId)
        {
            List<ListItemViewModel> load = new List<ListItemViewModel>();
            load = TimeSheetAPIHelperService.ProjectActivities(OpportunityId).Result.Select(x => new ListItemViewModel()
            {
                Id = x.Id,
                Value = x.Value
            }).ToList();
            return Json(load, JsonRequestBehavior.AllowGet);
        }
		
			 public JsonResult JobNo(int OpportunityId)
        {
            List<ListItemViewModel> Jobs = new List<ListItemViewModel>();
            Jobs = TimeSheetAPIHelperService.JobNo(OpportunityId).Result.Select(x => new ListItemViewModel()
            {
                Id = x.Id,
                Value = x.Value
            }).ToList();
            return Json(Jobs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PostResourceData(TimeSheetViewModel theModel)
        {
            for (int i = 0; i < theModel.ResourceIDs.Count; i++)
            {
                TimeSheetRegisterModel regModel = new TimeSheetRegisterModel();

                 string dateString = String.Format("{0:dd/MM/yyyy}", theModel.Day);
                string StartTimeString = String.Format("{0:HH:mm}", theModel.StartTime);
                string EndTimeString = String.Format("{0:HH:mm}", theModel.EndTime);

                string dtSt = dateString + " " + StartTimeString;
                string dtEn = dateString + " " + EndTimeString;

                var StartdateTime = Convert.ToDateTime(dtSt);
                var EnddateTime = Convert.ToDateTime(dtEn);

                regModel.CandidateNameId = theModel.ResourceIDs[i];
                regModel.Colour = theModel.Colour;
                regModel.Day = theModel.Day;
                regModel.EndTime = EnddateTime;
                regModel.Id = theModel.Id;
                regModel.OLATarget = theModel.OLATarget;
                regModel.OpportunityNumberID = theModel.OpportunityID;
                regModel.ProjectID = theModel.ProjectID;
                regModel.ServiceActivityId = theModel.ServiceActivityID;
                regModel.StartTime= StartdateTime;
                regModel.StatusID = theModel.StatusID;
                regModel.WarehouseNameId = theModel.WarehouseID;
                regModel.JobID = theModel.JobID;
                var test = RegisterTimesheetService.RegisterTimesheetModel(regModel);
            }
            return RedirectToAction("Index", "ManageCalender");


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
            return PartialView("Register", model);
        }

    }
}