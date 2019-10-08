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

        public ActionResult Index()
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
                model.ProjectManagerNameList = new SelectList(ListItemService.ProjectManagers().Result, "ID", "Value");
                model.EmploymentList = new SelectList(ListItemService.EmploymentTypeList().Result, "ID", "Value");
                model.PMTimeSheetList = TimeSheetAPIHelperService.PMTimeSheetList().Result;
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult TimesheetPMDetails(string CandidateName)
        {
            ProjectManagerTimesheet model = new ProjectManagerTimesheet();
            var AlltimesheetRecords = TimeSheetAPIHelperService.PMTimeSheetList().Result;
            model.PMTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("TimesheetPMDetails", model);
        }


        [HttpGet]
        public ActionResult ProjectDetails(string ProjectName)
        {
            ProjectManagerTimesheet model = new ProjectManagerTimesheet();
            var AlltimesheetRecords = TimeSheetAPIHelperService.PMTimeSheetList().Result;
            model.PMTimeSheetList = AlltimesheetRecords.Where(c => c.ProjectName == ProjectName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("ProjectDetails", model);
        }
        [HttpPost]
        public ActionResult Edit(UpdateProjectManagerModel EditModel)
        {
            if (UserRoles.UserCanEditTimesheet() != true)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (EditModel == null)
                {
                    Session["ErrorMessage"] = "Please Update With Valid Details!";
                }
                if (EditModel.Day != null && EditModel.StartTime != null && EditModel.EndTime != null)
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
                    EditModel.FullName = Session["FullName"].ToString();
                }
                var ReturnValue = RegisterTimesheetService.EditPMModel(EditModel);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult UpdateCandidate(UpdateProjectManagerModel CandidateEdit)
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
                    var ReturnValue = RegisterTimesheetService.EditPMModel(CandidateEdit);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}