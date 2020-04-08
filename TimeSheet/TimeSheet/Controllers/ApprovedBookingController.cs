using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class ApprovedBookingController : Controller
    {
        // GET: ApprovedBooking
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ApprovedTimesheetModel model = new ApprovedTimesheetModel();
                model.ApprovedTimeSheetListt = TimeSheetAPIHelperService.TimeSheetApprovedListt().Result;
               
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult CandidateDetails(string CandidateName)
        {
            ApprovedTimesheetModel model = new ApprovedTimesheetModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetApprovedListt().Result;
            model.ApprovedTimeSheetListt = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("CandidateDetails", model);
        }

        [HttpGet]
        public ActionResult ProjectDetail(string ProjectName)
        {
            ApprovedTimesheetModel model = new ApprovedTimesheetModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetApprovedListt().Result;
            model.ApprovedTimeSheetListt = AlltimesheetRecords.Where(c => c.ProjectName == ProjectName).ToList();
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