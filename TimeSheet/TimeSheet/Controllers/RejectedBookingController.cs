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
        // GET: CompletedTask
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                CompletedTimesheetModel model = new CompletedTimesheetModel();
                model.CompletedTimeSheetList = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
                model.HasUserPermissionsToEdit = UserRoles.UserCanEditTimesheet();
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult ExportCompletedScheduls()
        {
            var TimeSheetExportData = new List<CompletedtimesheetExportModel>();

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
              var  TimeSheetmodel = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
                foreach (var item in TimeSheetmodel)
                {
                    int otEnd = 0;
                    int otStart = 0;
                    if (Convert.ToInt32(item.EndTime.Value.ToString("HH")) > 18)
                    {
                        otEnd = Convert.ToInt32(item.EndTime.Value.ToString("HH")) - 18;
                    }
                    if (Convert.ToInt32(item.StartTime.Value.ToString("HH")) < 6)
                    {
                        otStart = 6 - Convert.ToInt32(item.StartTime.Value.ToString("HH"));
                    }
                    TimeSheetExportData.Add(new CompletedtimesheetExportModel
                    {
                        ProjectName = item.ProjectName,
                        CandidateName = item.CandidateName,
                        OpportunityNumber = item.OpportunityNumber,
                        Activity = item.Activity,
                        WarehouseName = item.WarehouseName,
                        StartTime =  item.StartTime,
                        EndTime = item.EndTime,
                        JobNo = item.JobNo,
                        OLATarget = item.OLATarget,
                        ActualQuantity = item.ActualQuantity,
                        Day = item.Day.Value.Date,
                        Status = item.Status,
                        TimeSheetComments = item.TimeSheetComments,
                        TotalHours = item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes / 60,
                        BreakHours = item.BreakHours,
                        WorkedHours = (item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes - item.BreakHours) / 60,
                        OutsideWorkHours = otStart + otEnd,
                        ProjectManager = item.ProjectManager,
                        BookedBy = item.BookedBy,
                        ApprovedBy = item.ApprovedBy,
                    });
                }
                GridView gv = new GridView();          
                gv.DataSource = TimeSheetExportData;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=CompletedTimeSchedule.xls");
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
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
            model.CompletedTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("CandidateDetails", model);
        }

        [HttpGet]
        public ActionResult ProjectDetail(string ProjectName)
        {
            CompletedTimesheetModel model = new CompletedTimesheetModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
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
                //   int EditId = Convert.ToInt32( TempData["EditID"].ToString());
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