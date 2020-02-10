using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class HRController : Controller
    {

        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = new HRTimeSheetList();
                model.HRTimeSheets = TimeSheetAPIHelperService.HRTimeSheetList().Result;                
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult TimesheetHRDetails(string CandidateName)
        {
            var model = new HRTimeSheetList();
            var AlltimesheetRecords = TimeSheetAPIHelperService.HRTimeSheetList().Result;
            model.HRTimeSheets = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("TimesheetHRDetails", model);
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
        public ActionResult UpdateHRTimesheet(UpdateHRModel EditModel)
        {
            ReturnModel ReturnValue = new ReturnModel();
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
               var ReturnModel = RegisterTimesheetService.EditHRModel(EditModel);
               return new JsonResult { Data = ReturnModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
           
        }
        [HttpPost]
        public ActionResult UpdateCandidate(UpdateHRModel CandidateEdit)
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
                    if (CandidateEdit.Day != null && CandidateEdit.StartTime != null && CandidateEdit.EndTime != null)
                    {
                        string dateString = String.Format("{0:dd/MM/yyyy}", CandidateEdit.Day.Value.Date);
                        string StartTimeString = String.Format("{0:HH:mm}", CandidateEdit.StartTime.Value);
                        string EndTimeString = String.Format("{0:HH:mm}", CandidateEdit.EndTime.Value);
                        string dtSt = dateString + " " + StartTimeString;
                        string dtEn = dateString + " " + EndTimeString;
                        var StartdateTime = Convert.ToDateTime(dtSt);
                        var EnddateTime = Convert.ToDateTime(dtEn);
                        CandidateEdit.StartTime = StartdateTime;
                        CandidateEdit.EndTime = EnddateTime;
                        CandidateEdit.FullName = Session["FullName"].ToString();
                    }

                    var ReturnValue = RegisterTimesheetService.EditHRModel(CandidateEdit);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult ExportPMBookingSchedule()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = new HRTimeSheetList();
                model.HRTimeSheets = TimeSheetAPIHelperService.HRTimeSheetList().Result;
                GridView gv = new GridView();
                gv.DataSource = model.HRTimeSheets;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=PMScheduleExport.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("Index", "CompletedBooking");
        }
    }
}