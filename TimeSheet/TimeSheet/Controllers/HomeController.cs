﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(CacheProfile = "OneHour", VaryByHeader ="X-Requested-With", Location =OutputCacheLocation.Server)]
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
             //   model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");            
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
                var Search= new SearchViewModel
                {
                    ProjectID= SearchModel.ProjectID,
                    WarehouseNameId = SearchModel.WarehouseID,
                    CandidateNameId = SearchModel.ResourceID,
                    OpportunityNumberID  = SearchModel.OpportunityID
                };
                model.CandidateTimeSheetList = SearchFilterService.SearchTimeSheetRecord(Search).Result;               
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {
            List<TimeSheetViewModel> TimeSheetmodel = new List<TimeSheetViewModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {               
                TimeSheetmodel = TimeSheetAPIHelperService.TimeSheetList().Result;
                GridView gv = new GridView();
                gv.DataSource = TimeSheetmodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=TimeScheduleActivitiesList.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("Jobs", "Home");
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
        public ActionResult TimeSheetCandidateDetails(string CandidateName)
        {
            TimeSheetViewModel model = new TimeSheetViewModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetList().Result;
            model.CandidateTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("TimeSheetCandidateDetails", model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
           // TempData["EditID"] = id;
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
               // TimeSheetViewModel model = new TimeSheetViewModel();
                var ModelReturnData = TimeSheetAPIHelperService.TimeSheetSearchById(id).Result;
                ModelReturnData.Id = id;
                ModelReturnData.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
                return PartialView("Edit", ModelReturnData);
            }
        }
        [HttpPost]
        public ActionResult Edit(UpdateTimeSheetModel EditModel)
        {
             if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
             //   int EditId = Convert.ToInt32( TempData["EditID"].ToString());
                if (EditModel== null)
                {
                    Session["ErrorMessage"] = "Please Update With Valid Details!";
                }
                if ( EditModel.Day != null && EditModel.StartTime != null && EditModel.EndTime != null)
                {
                    string dateString = String.Format("{0:dd/MM/yyyy}", EditModel.Day.Value.Date);
                    string StartTimeString = String.Format("{0:HH:mm}", EditModel.StartTime.Value);
                    string EndTimeString = String.Format("{0:HH:mm}", EditModel.EndTime.Value);
                    string dtSt = dateString + " " + StartTimeString;
                    string dtEn = dateString + " " + EndTimeString;
                    var StartdateTime = Convert.ToDateTime(dtSt);
                    var EnddateTime = Convert.ToDateTime(dtEn);
                    EditModel.StartTime = StartdateTime;
                    EditModel.EndTime = EnddateTime;                    
                }  
                var ReturnValue = RegisterTimesheetService.EditTimesheetModel(EditModel);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult UpdateCandidate(UpdateTimeSheetModel CandidateEdit)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                //   int EditId = Convert.ToInt32( TempData["EditID"].ToString());
                if (CandidateEdit == null)
                {
                    Session["ErrorMessage"] = "Please Update With Valid Details!";
                }
                if (CandidateEdit != null)
                {
                    var ReturnValue = RegisterTimesheetService.EditTimesheetModel(CandidateEdit);
                }                
            }
            return RedirectToAction("Index", "Home");
        }
    }
}