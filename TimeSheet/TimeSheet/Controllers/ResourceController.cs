﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class ResourceController : Controller
    {               
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
                Session["ErrorMessage"] = "Resource Book Pemission IS Restricted !";
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
                return View(model);
                }
                Session["ErrorMessage"] = "Resource Book Pemission IS Restricted !";
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult PostResourceData(TimeSheetViewModel theModel)
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
                regModel.Colour = theModel.Colour;
                regModel.Day = theModel.Day;
                regModel.EndTime = EnddateTime;
                regModel.Id = theModel.Id;
                regModel.OLATarget = theModel.OLATarget;
                regModel.OpportunityNumberID = theModel.OpportunityID;
                regModel.ProjectID = theModel.ProjectID;
                regModel.ServiceActivityId = theModel.ServiceActivityID;
                regModel.StartTime = StartdateTime;
                regModel.StatusID = theModel.StatusID;
                regModel.WarehouseNameId = theModel.WarehouseID;
                regModel.JobID = theModel.JobID;
                var test = RegisterTimesheetService.RegisterTimesheetModel(regModel);
            }
           
            return View("~ManageCalender/Index");
        }
        [HttpPost]
        public ActionResult RateResource(ResourceRatingModel theModel)
        {
            var result = ResourceHelperService.RateResource(theModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        
    }
}