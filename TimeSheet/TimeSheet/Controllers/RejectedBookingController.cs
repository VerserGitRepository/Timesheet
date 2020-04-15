using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet.Models;
using System.Linq;
using System;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class RejectedBookingController : Controller
    {        
        public ActionResult RejectedBooking()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                CompletedTimesheetModel model = new CompletedTimesheetModel();
                model.CompletedTimeSheetList = TimeSheetAPIHelperService.RejectedTimeSheets().Result;
                model.HasUserPermissionsToEdit = UserRoles.UserCanEditTimesheet();
                model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
                return View("RejectedBooking",model);
            }
        }

        [HttpPost]
        public ActionResult ExportRejectedBookings()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var model = TimeSheetAPIHelperService.RejectedTimeSheets().Result;
                GridView gv = new GridView();
                gv.DataSource = model;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=RejectedBookings.xls");
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
        [HttpGet]
        public ActionResult CandidateDetails(string CandidateName)
        {
            CompletedTimesheetModel model = new CompletedTimesheetModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.RejectedTimeSheets().Result;
            model.CompletedTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("CandidateDetails", model);
        }
        [HttpGet]
        public ActionResult ProjectDetail(string ProjectName)
        {
            CompletedTimesheetModel model = new CompletedTimesheetModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.RejectedTimeSheets().Result;
            model.CompletedTimeSheetList = AlltimesheetRecords.Where(c => c.ProjectName == ProjectName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("ProjectDetail", model);
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
                if (CandidateEdit == null)
                {
                    Session["ErrorMessage"] = "Please Update With Valid Details!";
                }
                if (CandidateEdit != null)
                {
                    string dateString = String.Format("{0:dd/MM/yyyy}", CandidateEdit.Day);
                    string StartTimeString = String.Format("{0:HH:mm}", CandidateEdit.StartTime);
                    string EndTimeString = String.Format("{0:HH:mm}", CandidateEdit.EndTime);
                    string dtSt = dateString + " " + StartTimeString;
                    string dtEn = dateString + " " + EndTimeString;
                    var StartdateTime = Convert.ToDateTime(dtSt);
                    var EnddateTime = Convert.ToDateTime(dtEn);
                    CandidateEdit.StartTime = StartdateTime;
                    CandidateEdit.EndTime = EnddateTime;
                    var ReturnValue = RegisterTimesheetService.EditTimesheetModel(CandidateEdit);                    
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}