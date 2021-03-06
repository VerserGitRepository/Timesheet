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
                var model = new TimeSheetViewModel();

                model = ListItemService.DropDownListFactory();

                var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                });
                int opportunityId = listitem.FirstOrDefault().Id;
                model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                model.CandidateTimeSheetList = TimeSheetAPIHelperService.CandidateTimelines().Result;
                Session["CalenderModel"] = model;

                var jsonlist = Newtonsoft.Json.JsonConvert.SerializeObject(model.WarehouseNameList);
                List<ResourceListModel> reslt = (from k in model.CandidateTimeSheetList
                                                 select new ResourceListModel
                                                 {
                                                     BookingId = k.Id,
                                                     ProjectManager = k.ProjectManager,
                                                     id = Convert.ToString(k.ResourceID),
                                                     title = Convert.ToString(k.CandidateName),
                                                     eventColor = k.Colour,
                                                     ResourceName = k.CandidateName,
                                                     WarehouseName = k.WarehouseName,
                                                     ActivityDescription = k.Activity,
                                                     ProjectName = k.ProjectName,
                                                     StartTime = Convert.ToDateTime(k.StartTime),
                                                     EndTime = Convert.ToDateTime(k.EndTime),
                                                     JobId = Convert.ToInt32(k.JobID)
                                                 }).Distinct().ToList();

                model.jsonResources = Newtonsoft.Json.JsonConvert.SerializeObject(reslt);
                List<ResourceEventsModel> resourceEvents = (from k in model.CandidateTimeSheetList
                                                            select new ResourceEventsModel
                                                            {
                                                                BookingId = k.Id,
                                                                projectmanager = k.ProjectManager,
                                                                resourceId = Convert.ToString(k.ResourceID),
                                                                title = k.ProjectName + "-" + k.ProjectManager + "-" + k.Activity + "-" + k.WarehouseName,
                                                                start = Convert.ToDateTime(k.StartTime.Value).ToString("s"),
                                                                end = Convert.ToDateTime(k.EndTime).ToString("s"),
                                                                activitydescription = k.Activity,
                                                                JobId = Convert.ToInt32(k.JobID)
                                                            }).Distinct().ToList();

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
                var model = new TimeSheetViewModel();

                model = ListItemService.DropDownListFactory();

                var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                });
                int opportunityId = listitem.FirstOrDefault().Id;


                model.CandidateTimeSheetList = TimeSheetAPIHelperService.VehicleTimeSheetList().Result;
                Session["CalenderModel"] = model;
                var jsonlist = Newtonsoft.Json.JsonConvert.SerializeObject(model.WarehouseNameList);
                var reslt = model.CandidateTimeSheetList.GroupBy(x => x.Vechicles).Select(grp => new VehicleModel { id = grp.Key, title = grp.Key }).ToList();
                model.jsonResources = Newtonsoft.Json.JsonConvert.SerializeObject(reslt);
                var resourceEvents = (from k in model.CandidateTimeSheetList
                                      select new VehicleBookedModel
                                      {
                                          resourceId = k.Vechicles,
                                          project = k.ProjectName,
                                          title = k.CandidateName + "-" +
                                          k.WarehouseName + "-" + k.ProjectName + "-" + k.ProjectManager + "-" + "-" + k.Activity,
                                          start = Convert.ToDateTime(k.StartTime.Value).ToString("s"),
                                          end = Convert.ToDateTime(k.EndTime).ToString("s"),
                                          jobid = Convert.ToInt32(k.JobID)
                                      }).ToList();
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
                model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");

                var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                });
                int opportunityId = listitem.FirstOrDefault().Id;               
                model.PMTimeSheetList = TimeSheetAPIHelperService.PMTimeSheetList().Result;
                Session["CalenderModel"] = model;
                var jsonlist = Newtonsoft.Json.JsonConvert.SerializeObject(model.WarehouseNameList);
              
                List<ResourceListModel> reslt = (from k in model.PMTimeSheetList
                                                 select new ResourceListModel
                                                 {
                                                     id = Convert.ToString(k.ResourceID),
                                                     title = Convert.ToString(k.CandidateName),
                                                     eventColor = k.Colour,
                                                     ResourceName = k.CandidateName,
                                                     WarehouseName = k.WarehouseName,
                                                     ActivityDescription = k.Activity,
                                                     ProjectName = k.ProjectName,
                                                     StartTime = Convert.ToDateTime(k.StartTime),
                                                     EndTime = Convert.ToDateTime(k.EndTime)
                                                 }).Distinct().ToList();

                model.jsonResources = Newtonsoft.Json.JsonConvert.SerializeObject(reslt);
                List<ResourceEventsModel> resourceEvents = (from k in model.PMTimeSheetList
                                                            select new ResourceEventsModel
                                                            {
                                                                resourceId = Convert.ToString(k.ResourceID),
                                                                title = k.ProjectName + "-" + k.CandidateName + "-" + k.Activity + "-" + k.WarehouseName,
                                                                start = Convert.ToDateTime(k.StartTime).ToString("s"),
                                                                end = Convert.ToDateTime(k.EndTime).ToString("s")
                                                            }).Distinct().ToList();

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
        [HttpGet]
        public ActionResult UpdateTime(int bookingId, string resourceName, string projectManager, string start, string end, string activityDescription)
        {
            TimeSheetViewModel vm = new TimeSheetViewModel();
            vm.CandidateName = resourceName;
            vm.ProjectManager = projectManager;
            vm.StartTime = Convert.ToDateTime(start);
            vm.EndTime = Convert.ToDateTime(end);
            vm.Activity = activityDescription;
            vm.Id = bookingId;
            return PartialView("UpdateResourceTimeline", vm);
        }
        [HttpPost]
        public ActionResult UpdateCandidate(UpdateTimeSheetModel CandidateEdit)
        {
            if (UserRoles.UserCanEditTimesheet() == true && CandidateEdit != null && CandidateEdit.Id > 0)
            {
                string dateString = String.Format("{0:dd/MM/yyyy}", CandidateEdit.Day);
                string StartTimeString = String.Format("{0:HH:mm}", CandidateEdit.StartTime);
                string EndTimeString = String.Format("{0:HH:mm}", CandidateEdit.EndTime);
                string dtSt = dateString + " " + StartTimeString;
                string dtEn = dateString + " " + EndTimeString;
                var StartdateTime = Convert.ToDateTime(dtSt);
                var EnddateTime = Convert.ToDateTime(dtEn);
                CandidateEdit.StartTime = StartdateTime;
                CandidateEdit.Day = Convert.ToDateTime(CandidateEdit.Day.Value.ToString("dd/MM/yyyy"));
                CandidateEdit.EndTime = EnddateTime;
                CandidateEdit.FullName = Session["FullName"].ToString();
                var ReturnValue = RegisterTimesheetService.EditTimesheetModel(CandidateEdit);
            }
            else
            {
                Session["ErrorMessage"] = "Update Details Not Valid!";
            }
            return Json(new { newUrl = Url.Action("Index", "ResourceTimeline") });
        }
    }
}