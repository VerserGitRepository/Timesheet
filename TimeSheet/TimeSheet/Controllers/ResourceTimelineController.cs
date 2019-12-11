using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;


namespace TimeSheet.Controllers
{
    public class ResourceTimelineController : Controller
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
                model.CandidateTimeSheetList = TimeSheetAPIHelperService.CandidateTimelines().Result;
                Session["CalenderModel"] = model;
                var jsonlist = Newtonsoft.Json.JsonConvert.SerializeObject(model.WarehouseNameList);
                //var jsonobj= JsonResult { Data = model.WarehouseNameList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                List<ResourceListModel> reslt = (from k in model.CandidateTimeSheetList  select new  ResourceListModel { ProjectManager = k.ProjectManager, id = Convert.ToInt32(k.ResourceID), title = Convert.ToString(k.CandidateName),eventColor=k.Colour, ResourceName = k.CandidateName, WarehouseName = k.WarehouseName, ActivityDescription = k.Activity,ProjectName=k.ProjectName,StartTime=Convert.ToDateTime(k.StartTime),EndTime=Convert.ToDateTime(k.EndTime) } ).Distinct().ToList();

                model.jsonResources = Newtonsoft.Json.JsonConvert.SerializeObject(reslt);
                List<ResourceEventsModel> resourceEvents = (from k in model.CandidateTimeSheetList  select new ResourceEventsModel { projectmanager = k.ProjectManager, resourceId = Convert.ToString(k.ResourceID), title = k.ProjectName + "-" + k.ProjectManager + "-" + k.Activity + "-" + k.WarehouseName, start=Convert.ToDateTime(k.StartTime.Value).ToString("s"),end=Convert.ToDateTime(k.EndTime).ToString("s"), activitydescription = k.Activity }).Distinct().ToList();
                model.jsonEvents = Newtonsoft.Json.JsonConvert.SerializeObject(resourceEvents);
               
                return View(model);
            }
        }


        public ActionResult VehicleTimeline()
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
                model.CandidateTimeSheetList = TimeSheetAPIHelperService.VehicleTimeSheetList().Result;
                Session["CalenderModel"] = model;
                var jsonlist = Newtonsoft.Json.JsonConvert.SerializeObject(model.WarehouseNameList);
                //var jsonobj= JsonResult { Data = model.WarehouseNameList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                List<ResourceListModel> reslt = (from k in model.CandidateTimeSheetList.Where(m=>m.Vechicles != null && m.Vechicles != "No Vehicle" && m.Vechicles != "Brisbane Verser Van" &&  m.Vechicles != "Sydney Verser Van") select new ResourceListModel { Vechicles = k.Vechicles, ProjectManager = k.ProjectManager,
                    id = Convert.ToInt32(k.ResourceID), title = Convert.ToString(k.Vechicles), eventColor = k.Colour, ResourceName = k.CandidateName, ActivityDescription = k.Activity,
                    WarehouseName = k.WarehouseName,
                    ProjectName = k.ProjectName, StartTime = Convert.ToDateTime(k.StartTime), EndTime = Convert.ToDateTime(k.EndTime) }).Distinct().ToList();

                model.jsonResources = Newtonsoft.Json.JsonConvert.SerializeObject(reslt);
                List<ResourceEventsModel> resourceEvents = (from k in model.CandidateTimeSheetList select new ResourceEventsModel { Vechicles=k.Vechicles, projectmanager = k.ProjectManager,
                    WarehouseName = k.WarehouseName,
                    resourceId = Convert.ToString(k.ResourceID), title = k.CandidateName + "-" +
                k.WarehouseName +"-" + k.ProjectName + "-" + k.ProjectManager + "-"  +"-" + k.Activity  , start = Convert.ToDateTime(k.StartTime.Value).ToString("s"), end = Convert.ToDateTime(k.EndTime).ToString("s") }).Distinct().ToList();
                model.jsonEvents = Newtonsoft.Json.JsonConvert.SerializeObject(resourceEvents);

                return View(model);
            }
        }
        [HttpPost]
        public ActionResult PostResourceTimelineData(TimeSheetViewModel theModel)
        {

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (UserRoles.UserCanRegisterTimesheet() != true)
            {
                Session["ErrorMessage"] = "Resource Book Pemission IS Restricted !";
                return RedirectToAction("Index", "Home");
            }
            if (theModel == null)
            {
                return RedirectToAction("Register", "Timesheet");
            }
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
                regModel.ResourceID = theModel.ResourceIDs[i];
                regModel.Colour = theModel.Colour;
                regModel.Day = theModel.Day;
                regModel.EndTime = EnddateTime;
                regModel.Id = theModel.Id;
                regModel.OLATarget = theModel.OLATarget;
                regModel.OpportunityNumberID = theModel.OpportunityID;
                regModel.OpportunityID = theModel.OpportunityID;
                regModel.ProjectID = theModel.ProjectID;
                regModel.ServiceActivityId = theModel.ServiceActivityID;
                regModel.StartTime = StartdateTime;
                regModel.StatusID = theModel.StatusID;
                regModel.WarehouseNameId = theModel.WarehouseID;
                regModel.JobID = theModel.JobID;
                regModel.FullName = Session["FullName"].ToString();
                regModel.TimeSheetComments = theModel.TimeSheetComments;
                regModel.Vechicles = theModel.Vechicles;
                var test = RegisterTimesheetService.RegisterTimesheetModel(regModel);

            }
            return Json(new { newUrl = Url.Action("Index", "ResourceTimeline") });

            // return View("~ManageCalender/Index");
        }

        public ActionResult PMTimeline()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ProjectManagerTimesheet model = new ProjectManagerTimesheet();
                model.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");
                model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
                var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                });
                int opportunityId = listitem.FirstOrDefault().Id;
               // model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                model.PMTimeSheetList = TimeSheetAPIHelperService.PMTimeSheetList().Result;
                Session["CalenderModel"] = model;
                var jsonlist = Newtonsoft.Json.JsonConvert.SerializeObject(model.WarehouseNameList);
                //var jsonobj= JsonResult { Data = model.WarehouseNameList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                List<ResourceListModel> reslt = (from k in model.PMTimeSheetList select new ResourceListModel {  id = Convert.ToInt32(k.ResourceID), title = Convert.ToString(k.CandidateName), eventColor = k.Colour, ResourceName = k.CandidateName, WarehouseName = k.WarehouseName, ActivityDescription = k.Activity, ProjectName = k.ProjectName, StartTime = Convert.ToDateTime(k.StartTime), EndTime = Convert.ToDateTime(k.EndTime) }).Distinct().ToList();

                model.jsonResources = Newtonsoft.Json.JsonConvert.SerializeObject(reslt);
                List<ResourceEventsModel> resourceEvents = (from k in model.PMTimeSheetList select new ResourceEventsModel {  resourceId = Convert.ToString(k.ResourceID), title = k.ProjectName + "-" + k.CandidateName + "-" + k.Activity + "-" + k.WarehouseName, start = Convert.ToDateTime(k.StartTime).ToString("s"), end = Convert.ToDateTime(k.EndTime).ToString("s") }).Distinct().ToList();
                model.jsonEvents = Newtonsoft.Json.JsonConvert.SerializeObject(resourceEvents);

                return View(model);
            }
        }
        [HttpGet]
        public ActionResult FetchOLA(int serviceActivityId, int opportunityId, int totalMins)
        {
            var result = ResourceHelperService.FetchOLA(serviceActivityId, opportunityId, totalMins);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}