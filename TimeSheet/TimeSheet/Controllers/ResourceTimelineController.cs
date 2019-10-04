﻿using System;
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
                model.CandidateTimeSheetList = TimeSheetAPIHelperService.TimeSheetList().Result;
                Session["CalenderModel"] = model;
                var jsonlist = Newtonsoft.Json.JsonConvert.SerializeObject(model.WarehouseNameList);
                //var jsonobj= JsonResult { Data = model.WarehouseNameList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                List<ResourceListModel> reslt = (from k in model.CandidateTimeSheetList  select new  ResourceListModel { ProjectManager = k.ProjectManager, id = Convert.ToInt32(k.ResourceID), title = Convert.ToString(k.CandidateName),eventColor=k.Colour, ResourceName = k.CandidateName, WarehouseName = k.WarehouseName, ActivityDescription = k.Activity,ProjectName=k.ProjectName,StartTime=Convert.ToDateTime(k.StartTime),EndTime=Convert.ToDateTime(k.EndTime) } ).Distinct().ToList();

                model.jsonResources = Newtonsoft.Json.JsonConvert.SerializeObject(reslt);
                List<ResourceEventsModel> resourceEvents = (from k in model.CandidateTimeSheetList  select new ResourceEventsModel { projectmanager = k.ProjectManager, resourceId = Convert.ToString(k.ResourceID), title = k.ProjectName + "-" + k.ProjectManager + "-" + k.Activity + "-" + k.WarehouseName, start=Convert.ToDateTime(k.StartTime.Value).ToString("s"),end=Convert.ToDateTime(k.EndTime).ToString("s")  }).Distinct().ToList();
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
                model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                model.CandidateTimeSheetList = TimeSheetAPIHelperService.VehicleTimeSheetList().Result;
                Session["CalenderModel"] = model;
                var jsonlist = Newtonsoft.Json.JsonConvert.SerializeObject(model.WarehouseNameList);
                //var jsonobj= JsonResult { Data = model.WarehouseNameList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                List<ResourceListModel> reslt = (from k in model.CandidateTimeSheetList select new ResourceListModel { Vechicles = k.Vechicles, ProjectManager = k.ProjectManager,
                    id = Convert.ToInt32(k.ResourceID), title = Convert.ToString(k.CandidateName), eventColor = k.Colour, ResourceName = k.CandidateName, ActivityDescription = k.Activity,
                    WarehouseName = k.WarehouseName,
                    ProjectName = k.ProjectName, StartTime = Convert.ToDateTime(k.StartTime), EndTime = Convert.ToDateTime(k.EndTime) }).Distinct().ToList();

                model.jsonResources = Newtonsoft.Json.JsonConvert.SerializeObject(reslt);
                List<ResourceEventsModel> resourceEvents = (from k in model.CandidateTimeSheetList select new ResourceEventsModel { Vechicles=k.Vechicles, projectmanager = k.ProjectManager,
                    WarehouseName = k.WarehouseName,
                    resourceId = Convert.ToString(k.ResourceID), title = k.Vechicles + "-"+ k.ProjectName + "-" + k.ProjectManager + "-"  +"-" + k.Activity + "-" +
                    k.WarehouseName, start = Convert.ToDateTime(k.StartTime.Value).ToString("s"), end = Convert.ToDateTime(k.EndTime).ToString("s") }).Distinct().ToList();
                model.jsonEvents = Newtonsoft.Json.JsonConvert.SerializeObject(resourceEvents);

                return View(model);
            }
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
                model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
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
    }
}