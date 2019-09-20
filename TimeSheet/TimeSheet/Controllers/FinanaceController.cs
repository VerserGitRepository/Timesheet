using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class FinanaceController : Controller
    {
        // GET: Finanace
        public ActionResult Index()
        {

            if (Session["Username"] != null && Session["Accounts"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
          
        }
        public ActionResult WeeklyReport()
        {

            if (Session["Username"] != null && Session["Accounts"] != null)
            {

                CompletedTimesheetModel model = new CompletedTimesheetModel();
                model.CompletedTimeSheetList = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
                //  var result = model.CompletedTimeSheetList.GroupBy(x => new { (x.CandidateName, x.ProjectName)});
                DateTime dtFrom = DateTime.Parse("10:00 AM");
                DateTime dtTo = DateTime.Parse("12:00 PM");
                model.AggregaredTimesheetModel = new List<AggregatedCompletedTimesheetModel>();
                DateTime reference = DateTime.Now;
                System.Globalization.Calendar calendar = CultureInfo.CurrentCulture.Calendar;

                IEnumerable<int> daysInMonth = Enumerable.Range(1, calendar.GetDaysInMonth(reference.Year, reference.Month));

                List<Tuple<DateTime, DateTime>> weeks = daysInMonth.Select(day => new DateTime(reference.Year, reference.Month, day))
                    .GroupBy(d => calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday))
                    .Select(g => new Tuple<DateTime, DateTime>(g.First(), g.Last()))
                    .ToList();
                
                weeks.ForEach(x => Console.WriteLine("{0:MM/dd/yyyy} - {1:MM/dd/yyyy}", x.Item1, x.Item2));

                var group = from completedTimeSheet in model.CompletedTimeSheetList group completedTimeSheet by new { completedTimeSheet.CandidateName, completedTimeSheet.ProjectManager } into g select g;
                int weekofMonth = 0;
                //DateTime date = new DateTime();
                foreach (var subgroup in group)
                {
                    double hours = 0;
                    AggregatedCompletedTimesheetModel agrModel = new AggregatedCompletedTimesheetModel();

                    foreach (var t in subgroup)
                    {
                       // date = new DateTime(t.Day.Value.Year, t.Day.Value.Month, t.Day.Value.Day);
                        agrModel.CandidateName = t.CandidateName;
                        agrModel.ProjectManager = t.ProjectManager;
                        agrModel.EmploymentTypeID = t.EmploymentTypeID;
                        hours += t.EndTime.Value.Subtract(t.StartTime.Value).TotalMinutes / 60;
                        DateTime date = t.Day.Value.Date;
                        DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
                        DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
                        if (firstMonthMonday > date)
                        {
                            firstMonthDay = firstMonthDay.AddMonths(-1);
                            firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
                        }
                        weekofMonth = (date - firstMonthMonday).Days / 7 + 1;
                        agrModel.PayCycle = "Pay Cycle for week -" + weekofMonth;


                    }
                    
                    agrModel.Hours = hours;
                    model.AggregaredTimesheetModel.Add(agrModel);
                }
                //return View(model);

               // var model=   TimeSheetAPIHelperService.TimeSheetApprovedList().Result;
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult MonthlyReport()
        {

            if (Session["Username"] != null && Session["Accounts"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult OverTimeReport()
        {

            if (Session["Username"] != null && Session["Accounts"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        [HttpGet]
        public ActionResult ApprovedResourceDetails(string CandidateName)
        {
            CompletedTimesheetModel model = new CompletedTimesheetModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
            model.CompletedTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("ApprovedResourceDetails", model);
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
                var TimeSheetmodel = TimeSheetAPIHelperService.TimeSheetApprovedList().Result;
                foreach (var item in TimeSheetmodel)
                {

                    TimeSheetExportData.Add(new CompletedtimesheetExportModel
                    {
                        ProjectName = item.ProjectName,
                        CandidateName = item.CandidateName,
                        OpportunityNumber = item.OpportunityNumber,
                        Activity = item.Activity,
                        WarehouseName = item.WarehouseName,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime,
                        JobNo = item.JobNo,
                        OLATarget = item.OLATarget,
                        ActualQuantity = item.ActualQuantity,
                        Day = item.Day.Value.Date,
                        Status = item.Status,
                        TimeSheetComments = item.TimeSheetComments,
                        Hours = item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes / 60
                });
                }
                GridView gv = new GridView();
                gv.DataSource = TimeSheetExportData;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ApprovedTimeSchedule.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("WeeklyReport", "Finanace");
        }

    }
}