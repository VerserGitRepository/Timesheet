using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;
using Newtonsoft.Json;

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
              //  model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                model.CandidateTimeSheetList = TimeSheetAPIHelperService.CandidateTimelines().Result;
                Session["CalenderModel"] = model;
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
              //  model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
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
        public JsonResult SearchResources(string ProjectID, string warehouseId, string opportunityId, List<int> ResourceIDs)
        {
            TimeSheetViewModel newModel = new TimeSheetViewModel();
           
                TimeSheetViewModel model = (TimeSheetViewModel)Session["CalenderModel"] ;
                newModel.Projectlist = model.Projectlist;
                newModel.WarehouseNameList = model.WarehouseNameList;
                newModel.CandidateNameList = model.CandidateNameList;
                List<TimeSheetViewModel> whereList = model.CandidateTimeSheetList;

                if (ResourceIDs != null && ResourceIDs.Count > 0)
                    whereList = model.CandidateTimeSheetList.Where(item => ResourceIDs.Contains(Convert.ToInt32(item.ResourceID))).ToList();
                if (!string.IsNullOrEmpty(ProjectID))
                    whereList = whereList.Where(item => item.ProjectID == Convert.ToInt32(ProjectID)).ToList();
                if (!string.IsNullOrEmpty(warehouseId))
                    whereList = whereList.Where(item => item.WarehouseID == Convert.ToInt32(warehouseId)).ToList();
                if (!string.IsNullOrEmpty(opportunityId) && opportunityId != "0")
                    whereList = whereList.Where(item => item.OpportunityID == Convert.ToInt32(opportunityId)).ToList();
                newModel.CandidateTimeSheetList = whereList;
               
            List<ResourceListModel> reslt = (from k in newModel.CandidateTimeSheetList select new ResourceListModel { BookingId = k.Id, ProjectManager = k.ProjectManager, id = Convert.ToString(k.ResourceID), title = Convert.ToString(k.CandidateName), eventColor = k.Colour, ResourceName = k.CandidateName, ActivityDescription = k.Activity, ProjectName = k.ProjectName, StartTime = Convert.ToDateTime(k.StartTime), EndTime = Convert.ToDateTime(k.EndTime) }).Distinct().ToList();

            newModel.jsonResources = JsonConvert.SerializeObject(reslt);
            List<ResourceEventsModel> resourceEvents = (from k in newModel.CandidateTimeSheetList select new ResourceEventsModel { BookingId = k.Id, projectmanager = k.ProjectManager, resourceId = Convert.ToString(k.ResourceID), title = k.ProjectName+"-"+k.ProjectManager+"-"+k.Activity, start = Convert.ToDateTime(k.StartTime).ToString("s"), end = Convert.ToDateTime(k.EndTime).ToString("s") }).Distinct().ToList();
            newModel.jsonEvents = JsonConvert.SerializeObject(resourceEvents);


            return new JsonResult { Data = newModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = int.MaxValue };
        }
        public JsonResult SearchVehicleTimeLine(string ProjectID, string warehouseId, string opportunityId, List<int> ResourceIDs)
        {
            TimeSheetViewModel newModel = new TimeSheetViewModel();

            TimeSheetViewModel model = (TimeSheetViewModel)Session["CalenderModel"];
            newModel.Projectlist = model.Projectlist;
            newModel.WarehouseNameList = model.WarehouseNameList;
            newModel.CandidateNameList = model.CandidateNameList;
            List<TimeSheetViewModel> whereList = model.CandidateTimeSheetList;

            if (ResourceIDs != null && ResourceIDs.Count > 0)
                whereList = model.CandidateTimeSheetList.Where(item => ResourceIDs.Contains(Convert.ToInt32(item.ResourceID))).ToList();
            if (!string.IsNullOrEmpty(ProjectID))
                whereList = whereList.Where(item => item.ProjectID == Convert.ToInt32(ProjectID)).ToList();
            if (!string.IsNullOrEmpty(warehouseId))
                whereList = whereList.Where(item => item.WarehouseID == Convert.ToInt32(warehouseId)).ToList();
            if (!string.IsNullOrEmpty(opportunityId) && opportunityId != "0")
                whereList = whereList.Where(item => item.OpportunityID == Convert.ToInt32(opportunityId)).ToList();
            newModel.CandidateTimeSheetList = whereList;

            var reslt = whereList.GroupBy(x => x.Vechicles).Select(grp => new VehicleModel { id = grp.Key, title = grp.Key }).ToList();

            newModel.jsonResources = Newtonsoft.Json.JsonConvert.SerializeObject(reslt);
            var resourceEvents = (from k in whereList
                                  select new VehicleBookedModel
                                  {
                                      resourceId = k.Vechicles,
                                      project = k.ProjectName,

                                      title = k.CandidateName + "-" +
k.WarehouseName + "-" + k.ProjectName + "-" + k.ProjectManager + "-" + "-" + k.Activity,
                                      start = Convert.ToDateTime(k.StartTime.Value).ToString("s"),
                                      end = Convert.ToDateTime(k.EndTime).ToString("s")
                                  }).ToList();
            newModel.jsonEvents = JsonConvert.SerializeObject(resourceEvents);


            return new JsonResult { Data = newModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult SearchPM(string ProjectID, string warehouseId, string opportunityId, List<int> ResourceIDs)
        {
            ProjectManagerTimesheet newModel = new ProjectManagerTimesheet();

            ProjectManagerTimesheet model = (ProjectManagerTimesheet)Session["CalenderModel"];
            newModel.Projectlist = model.Projectlist;
            newModel.WarehouseNameList = model.WarehouseNameList;
            newModel.PMTimeSheetList = model.PMTimeSheetList;
            List<ProjectManagerTimesheet> whereList = model.PMTimeSheetList;

            if (ResourceIDs != null && ResourceIDs.Count > 0)
                whereList = model.PMTimeSheetList.Where(item => ResourceIDs.Contains(Convert.ToInt32(item.ResourceID))).ToList();
            if (!string.IsNullOrEmpty(ProjectID))
                whereList = whereList.Where(item => item.ProjectID == Convert.ToInt32(ProjectID)).ToList();
            if (!string.IsNullOrEmpty(warehouseId))
                whereList = whereList.Where(item => item.WarehouseID == Convert.ToInt32(warehouseId)).ToList();
            if (!string.IsNullOrEmpty(opportunityId) && opportunityId != "0")
                whereList = whereList.Where(item => item.OpportunityID == Convert.ToInt32(opportunityId)).ToList();
            newModel.PMTimeSheetList = whereList;

            List<ResourceListModel> reslt = (from k in newModel.PMTimeSheetList select new ResourceListModel { ProjectManager = k.CandidateName, id = Convert.ToString(k.ResourceID), title = Convert.ToString(k.CandidateName), eventColor = k.Colour, ResourceName = k.CandidateName, ActivityDescription = k.Activity, ProjectName = k.ProjectName, StartTime = Convert.ToDateTime(k.StartTime), EndTime = Convert.ToDateTime(k.EndTime) }).Distinct().ToList();

            newModel.jsonResources = JsonConvert.SerializeObject(reslt);
            List<ResourceEventsModel> resourceEvents = (from k in newModel.PMTimeSheetList select new ResourceEventsModel { projectmanager = k.CandidateName, resourceId = Convert.ToString(k.ResourceID), title = k.ProjectName + "-" + k.CandidateName + "-" + k.Activity, start = Convert.ToDateTime(k.StartTime).ToString("s"), end = Convert.ToDateTime(k.EndTime).ToString("s") }).Distinct().ToList();
            newModel.jsonEvents = JsonConvert.SerializeObject(resourceEvents);


            return new JsonResult { Data = newModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
            TempData["opportunity"] = OpportunityId;
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