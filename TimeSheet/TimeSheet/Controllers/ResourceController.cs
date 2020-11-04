using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    
    public class ResourceController : Controller
    {
        private static readonly string RecruitmentProjectName = ConfigurationManager.AppSettings["RecruitmentProjectName"];         
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (UserRoles.UserCanRegisterTimesheet() == true)
                {
                    ResourceAvailable model = new ResourceAvailable();
                    model.ResourceAvailableList = ResourceHelperService.ResourceListItems().Result;
                    model.ResourceAvailableList = model.ResourceAvailableList.OrderBy(w => w.Warehouse).OrderBy(x => x.ResourceName).ToList();
                    model.WarehouseNameList = new SelectList(TimeSheetAPIHelperService.Warehouses().Result, "ID", "Value");
                    return View(model);
                }
                Session["ErrorMessage"] = "Resource Specifics Pemission Is Restricted!";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult _ResourceBooked(string ResourceName)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TimeSheetViewModel model = new TimeSheetViewModel();
                var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetList().Result;
                model.CandidateTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == ResourceName).ToList();
                return PartialView("_ResourceBooked", model);
            }
        }
        public ActionResult Register()
        {
           if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (UserRoles.UserCanRegisterTimesheet() == true)
                {
                    TimeSheetViewModel model = new TimeSheetViewModel();
                    List<ListItemViewModel> salesForceOpp = new List<ListItemViewModel>();                
                    var projectList = TimeSheetAPIHelperService.CostModelProject().Result;
                    model.Projectlist = new SelectList(projectList.Where(i => i.Value != RecruitmentProjectName), "ID", "Value");
                    model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
                    var salesForceProjectList = TimeSheetAPIHelperService.SalesForceEntities(out salesForceOpp);
                    var newProjectList = projectList.Where(item => item.Value == RecruitmentProjectName);            
                    int verserProjectId = newProjectList.FirstOrDefault().Id;
                    newProjectList.FirstOrDefault().Id = salesForceProjectList.Count();
                    salesForceProjectList.Add(newProjectList.FirstOrDefault());
                    model.SalesForceProjectlist = new SelectList(salesForceProjectList, "ID", "Value");
                    List<ListItemViewModel> load = new List<ListItemViewModel>();
                    load = TimeSheetAPIHelperService.ProjectOpportunities(verserProjectId).Result.Select(x => new ListItemViewModel()
                    {
                        Value = x.Value,                       
                        OpportunityNumber = x.Value
                    }).ToList();
                    salesForceOpp.Add(load.FirstOrDefault());

                    model.SalesForceOpportunityNumberList = new SelectList(salesForceOpp, "Value", "OpportunityNumber");

                    var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                    {
                        Id = x.Id,
                        Value = x.Value
                    });
                    int opportunityId = listitem.FirstOrDefault().Id;
                    model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                    model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                    model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                    model.HRBookingResourceList = new SelectList(ListItemService.HRBookingResources().Result, "ID", "Value");
                    model.ProjectManagerNameList = new SelectList(ListItemService.ProjectManagers().Result, "ID", "Value");
                    model.EmploymentList = new SelectList(ListItemService.EmploymentTypeList().Result, "ID", "Value");
                    model.CandidateTimeSheetList = TimeSheetAPIHelperService.TimeSheetList().Result;
                    model.HRActivityList = new SelectList(TimeSheetAPIHelperService.HRActivities().Result, "ID", "Value");
                    return View(model);
                }
                Session["ErrorMessage"] = "Resource Book Pemission IS Restricted !";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ResourceRegister(string day)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (UserRoles.UserCanRegisterTimesheet() == true)
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
                    model.ProjectManagerNameList = new SelectList(ListItemService.ProjectManagers().Result, "ID", "Value");
                    model.EmploymentList = new SelectList(ListItemService.EmploymentTypeList().Result, "ID", "Value");
                    model.CandidateTimeSheetList = TimeSheetAPIHelperService.TimeSheetList().Result;
                    model.Day = Convert.ToDateTime(day.Split('T')[0]);
                    return PartialView("ResourceRegister", model);
                }
                Session["ErrorMessage"] = "Resource Book Pemission IS Restricted !";
                return RedirectToAction("ResourceRegister", "Resource");
            }
        }
        [HttpPost]
        public ActionResult PostResourceData(TimeSheetViewModel theModel)
        {
            bool isPMBooking = false;
            theModel.BookingTravelTime = theModel.BookingTravelTime.Replace("Source Start ", "").Replace("Dest Reach ", ",").Replace("Dest Start ", "-").Replace("Source Reach ", ",");
          
            theModel.BookingTravelTime = theModel.BookingTravelTime.Replace("AM", "00 AM").Replace("PM", "00 PM");
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
                regModel.BookingTravelTime = theModel.BookingTravelTime;
                regModel.BookingEstimatedTravelCost = theModel.BookingEstimatedTravelCost;
                regModel.BookingEstimatedTravelHrs = theModel.BookingEstimatedTravelHrs;
                if (regModel.JobID == null || regModel.JobID == 0)
                {
                    isPMBooking = true;
                    string addlActvty = "";
                    if (theModel.AdditionalActivities != null)
                    {
                        addlActvty = theModel.AdditionalActivities.Replace(";undefined", "");
                    }

                    TimeSheetRegisterPMModel regPMModel = new TimeSheetRegisterPMModel
                    {
                        Day = regModel.Day,
                        StartTime = regModel.StartTime,
                        EndTime = regModel.EndTime,
                        ServiceActivityID = regModel.ServiceActivityId,
                        ResourceID = theModel.ResourceIDs[i],
                        ProjectID = regModel.ProjectID,
                        OpportunityID = regModel.OpportunityNumberID,
                        Colour = regModel.Colour,
                        TimeSheetComments = regModel.TimeSheetComments,
                        AdditionalActivities = addlActvty
                    };

                    var a = RegisterTimesheetService.RegisterPMBooking(regPMModel);

                }
                else
                {
                    isPMBooking = false;
                    var test = RegisterTimesheetService.RegisterTimesheetModel(regModel);
                }

            }
            if (isPMBooking)
            {
                return Json(new { newUrl = Url.Action("Register", "Resource") });
            }
            else
            {
                return Json(new { newUrl = Url.Action("Register", "Resource") });
            }
        }

        [HttpPost]
        public ActionResult PostHRData(HRRegisterViewModel theModel)
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
            TimeSheetRegisterModel regModel = new TimeSheetRegisterModel();
            string dateString = String.Format("{0:dd/MM/yyyy}", theModel.Day);
            string StartTimeString = String.Format("{0:HH:mm}", theModel.StartTime);
            string EndTimeString = String.Format("{0:HH:mm}", theModel.EndTime);
            string dtSt = dateString + " " + StartTimeString;
            string dtEn = dateString + " " + EndTimeString;
            var StartdateTime = Convert.ToDateTime(dtSt);
            var EnddateTime = Convert.ToDateTime(dtEn);
            regModel.Colour = theModel.Colour;
            regModel.Day = theModel.Day;
            regModel.EndTime = EnddateTime;
            regModel.OpportunityNumberID = theModel.OpportunityID;
            regModel.OpportunityID = theModel.OpportunityID;
            regModel.ProjectID = theModel.ProjectID;
            regModel.ServiceActivityId = theModel.ServiceActivityID;
            regModel.StartTime = StartdateTime;
            regModel.WarehouseNameId = theModel.WarehouseID;
            regModel.FullName = Session["FullName"].ToString();
            regModel.TimeSheetComments = theModel.TimeSheetComments;

            return Json(new { newUrl = Url.Action("Register", "Resource") });            
        }
        [HttpPost]
        public ActionResult PostRecruitmentTimesheet(TimeSheetViewModel theModel)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (UserRoles.HRUser() != true)
            {
                Session["ErrorMessage"] = "Resource Book Pemission IS Restricted !";
                return Json(new { newUrl = Url.Action("Register", "Resource") });
            }
            if (theModel == null)
            {
                return RedirectToAction("Register", "Timesheet");
            }
            for (int i = 0; i < theModel.ResourceIDs.Count; i++)
            {

                string addlActvty = "";
                if (theModel.AdditionalActivities != null)
                {
                    addlActvty = theModel.AdditionalActivities.Replace(";undefined", "");
                }
                HRRegisterViewModel regHRModel = new HRRegisterViewModel
                {
                    Day = Convert.ToDateTime(theModel.Day),
                    StartTime = Convert.ToDateTime(theModel.StartTime),
                    EndTime = Convert.ToDateTime(theModel.EndTime),
                    ServiceActivityID = Convert.ToInt32(theModel.ServiceActivityID),
                    ResourceID = Convert.ToInt32(theModel.ResourceIDs[i]),
                    ProjectID = Convert.ToInt32(theModel.ProjectID),
                    OpportunityID = Convert.ToInt32(theModel.OpportunityID),
                    Colour = theModel.Colour,
                    TimeSheetComments = theModel.TimeSheetComments,
                    AdditionalActivities = addlActvty,
                    OpportunityName = theModel.ProjectName
                };
                var a = RegisterTimesheetService.RegisterHRBooking(regHRModel);
            }
            return Json(new { newUrl = Url.Action("Register", "Resource") });
        }

        [HttpPost]
        public ActionResult RateResource(ResourceRatingModel theModel)
        {
            var result = ResourceHelperService.RateResource(theModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PMRegister()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (UserRoles.UserCanRegisterTimesheet() == true)
                {
                    TimeSheetViewModel model = new TimeSheetViewModel();
                    model.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");
                    model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
                    var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                    {
                        Id = x.Id,
                        Value = x.Value
                    });
                    //   int opportunityId = listitem.FirstOrDefault().Id;
                    //  model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                    model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                    model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                    model.ProjectManagerNameList = new SelectList(ListItemService.ProjectManagers().Result, "ID", "Value");
                    model.EmploymentList = new SelectList(ListItemService.EmploymentTypeList().Result, "ID", "Value");
                    model.CandidateTimeSheetList = TimeSheetAPIHelperService.TimeSheetList().Result;
                    return View(model);
                }
                Session["ErrorMessage"] = "Resource Book Pemission IS Restricted !";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult FetchOLA(int serviceActivityId, int opportunityId, int totalMins)
        {
            var result = ResourceHelperService.FetchOLA(serviceActivityId, opportunityId, totalMins);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FetchTravelOLA(int serviceActivityId, int opportunityId, int totalMins)
        {
            var result = ResourceHelperService.FetchTravelOLA(serviceActivityId, opportunityId, totalMins);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}