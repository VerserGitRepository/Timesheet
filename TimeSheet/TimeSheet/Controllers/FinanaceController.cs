using System;
using System.Collections.Generic;
using System.Configuration;
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

            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            DayOfWeek firstDay = defaultCultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = DateTime.Now.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }
            //firstDayInWeek

            if (Session["Username"] != null && Session["Accounts"] != null)
            {

                CompletedTimesheetModel model = new CompletedTimesheetModel();
                List<CompletedTimesheetModel> permCompleted = TimeSheetAPIHelperService.TimeSheetCompletedList().Result.Where(item => item.EmployeementType != "Casual" && item.Status == "Completed").ToList();
                List<CompletedTimesheetModel> casualApproved = TimeSheetAPIHelperService.TimeSheetApprovedList().Result.Where(item => item.EmployeementType == "Casual" && item.Status == "Approved").ToList();

                model.CompletedTimeSheetList = permCompleted.Concat(casualApproved).Distinct().ToList();

                
                //  var result = model.CompletedTimeSheetList.GroupBy(x => new { (x.CandidateName, x.ProjectName)});
              
                model.AggregaredTimesheetModel = new List<AggregatedCompletedTimesheetModel>();
                

                var group = from completedTimeSheet in model.CompletedTimeSheetList group completedTimeSheet by new { completedTimeSheet.CandidateName, completedTimeSheet.ProjectManager } into g select g;
               
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
                        agrModel.EmployeementType = t.EmployeementType;
                        hours += t.EndTime.Value.Subtract(t.StartTime.Value).TotalMinutes / 60;
                        double pc = (DateTime.Now.Date.Subtract(t.Day.Value).Days / 5)+1;
                        
                        agrModel.PayCycle = pc.ToString();
                        agrModel.ADPEmployeeID = Convert.ToInt32(t.AdpEmployeeID);
                        agrModel.PayFrequency = t.PayFrequency;
                        agrModel.ResourceId = Convert.ToInt32(t.ResourceID);

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

                CompletedTimesheetModel model = new CompletedTimesheetModel();
                //List<CompletedTimesheetModel> permCompleted = TimeSheetAPIHelperService.TimeSheetCompletedList().Result.Where(item => item.EmployeementType != "Casual" && item.Status == "Completed").ToList();
                //List<CompletedTimesheetModel> casualApproved = TimeSheetAPIHelperService.TimeSheetApprovedList().Result.Where(item => item.EmployeementType == "Casual" && item.Status == "Approved").ToList();

                model.CompletedTimeSheetList = TimeSheetAPIHelperService.PaidTimeSheetList().Result;


                //  var result = model.CompletedTimeSheetList.GroupBy(x => new { (x.CandidateName, x.ProjectName)});

                model.AggregaredTimesheetModel = new List<AggregatedCompletedTimesheetModel>();


                var group = from completedTimeSheet in model.CompletedTimeSheetList group completedTimeSheet by new { completedTimeSheet.CandidateName, completedTimeSheet.ProjectManager } into g select g;

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
                        agrModel.EmployeementType = t.EmployeementType;
                        hours += t.EndTime.Value.Subtract(t.StartTime.Value).TotalMinutes / 60;
                        double pc = (DateTime.Now.Date.Subtract(t.Day.Value).Days / 5) + 1;

                        agrModel.PayCycle = pc.ToString();
                        agrModel.ADPEmployeeID = Convert.ToInt32(t.AdpEmployeeID);
                        agrModel.PayFrequency = t.PayFrequency;


                    }

                    agrModel.Hours = hours;
                    model.AggregaredTimesheetModel.Add(agrModel);
                }
                model.CompletedTimeSheetList = TimeSheetAPIHelperService.PaidTimeSheetList().Result;
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult OverTimeReport()
        {

            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            DayOfWeek firstDay = defaultCultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = DateTime.Now.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }
            //firstDayInWeek

            if (Session["Username"] != null && Session["Accounts"] != null)
            {
                string otHours = (string)ConfigurationManager.AppSettings["OTHours"];
                List<string> otvalues = new List<string>();
                otvalues = otHours.Split(';').ToList();
                string OTWeekEndSatDay = (string)ConfigurationManager.AppSettings["OTWeekEndSatDay"];
                string OTWeekEndSatDayException = (string)ConfigurationManager.AppSettings["OTWeekEndSatDayException"];
                string OTWeekEndSunDay = (string)ConfigurationManager.AppSettings["OTWeekEndSunDay"];
                string OTHoursVal = string.Empty;

                CompletedTimesheetModel model = new CompletedTimesheetModel();
                List<CompletedTimesheetModel> permCompleted = TimeSheetAPIHelperService.TimeSheetCompletedList().Result.Where(item => item.EmployeementType != "Casual" && item.Status == "Completed").ToList();
                List<CompletedTimesheetModel> casualApproved = TimeSheetAPIHelperService.TimeSheetApprovedList().Result.Where(item => item.EmployeementType == "Casual" && item.Status == "Approved").ToList();

                model.CompletedTimeSheetList = permCompleted.Concat(casualApproved).Distinct().ToList();
                //  var result = model.CompletedTimeSheetList.GroupBy(x => new { (x.CandidateName, x.ProjectName)});

                model.AggregaredTimesheetModel = new List<AggregatedCompletedTimesheetModel>();
               

                model.CompletedTimeSheetList = model.CompletedTimeSheetList.Where(item => item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes / 60 > double.Parse(otvalues[0].Replace("GT", ""))).ToList();

                var group = from completedTimeSheet in model.CompletedTimeSheetList group completedTimeSheet by new { completedTimeSheet.CandidateName, completedTimeSheet.ProjectManager } into g select g;
                
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
                        agrModel.EmployeementType = t.EmployeementType;
                        //if (t.EndTime.Value.Subtract(t.StartTime.Value).TotalMinutes / 60 > double.Parse(otvalues[0].Replace("GT", "")))
                        //{
                        hours += t.EndTime.Value.Subtract(t.StartTime.Value).TotalMinutes / 60 - double.Parse(otvalues[0].Replace("GT", ""));
                        //}
                        double pc = (DateTime.Now.Date.Subtract(t.Day.Value).Days / 5) + 1;
                        agrModel.PayCycle =pc.ToString();
                        agrModel.ADPEmployeeID = Convert.ToInt32(t.AdpEmployeeID);
                        agrModel.PayFrequency = t.PayFrequency;


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

        [HttpGet]
        public ActionResult ApprovedResourceDetails(string CandidateName)
        {
            CompletedTimesheetModel model = new CompletedTimesheetModel();
            List<CompletedTimesheetModel> permCompleted = TimeSheetAPIHelperService.TimeSheetCompletedList().Result.Where(item => item.EmployeementType != "Casual" && item.Status == "Completed").ToList();
            List<CompletedTimesheetModel> casualApproved = TimeSheetAPIHelperService.TimeSheetApprovedList().Result.Where(item => item.EmployeementType == "Casual" && item.Status == "Approved").ToList();

            var AlltimesheetRecords = permCompleted.Concat(casualApproved).Distinct().ToList();
            model.CompletedTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("ApprovedResourceDetails", model);
        }


        [HttpGet]
        public ActionResult PaidResourceDetails(string CandidateName)
        {
            CompletedTimesheetModel model = new CompletedTimesheetModel();


            var AlltimesheetRecords = TimeSheetAPIHelperService.PaidTimeSheetList().Result;
            model.CompletedTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("PaidResourceDetails", model);
        }

        [HttpPost]
        public ActionResult ExportCompletedScheduls()
        {
            var TimeSheetExportData = new List<CompletedtimesheetExcelExportModel>();
            string otHours = (string)ConfigurationManager.AppSettings["OTHours"];
            List<string> otvalues = new List<string>();
            otvalues = otHours.Split(';').ToList();
            string OTWeekEndSatDay = (string)ConfigurationManager.AppSettings["OTWeekEndSatDay"];
            string OTWeekEndSatDayException = (string)ConfigurationManager.AppSettings["OTWeekEndSatDayException"];
            string OTWeekEndSunDay = (string)ConfigurationManager.AppSettings["OTWeekEndSunDay"];
            string OTHoursVal = string.Empty;
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                #region Old Code
                CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
                DayOfWeek firstDay = defaultCultureInfo.DateTimeFormat.FirstDayOfWeek;
                DateTime firstDayInWeek = DateTime.Now.Date;

                while (firstDayInWeek.DayOfWeek != firstDay)
                {
                    firstDayInWeek = firstDayInWeek.AddDays(-1);
                }
                DateTime lastDayInWeek = firstDayInWeek.AddDays(6);

                List<CompletedTimesheetModel> permCompleted = TimeSheetAPIHelperService.TimeSheetCompletedList().Result.Where(item => item.EmployeementType != "Casual" && item.Status == "Completed").ToList();
                List<CompletedTimesheetModel> casualApproved = TimeSheetAPIHelperService.TimeSheetApprovedList().Result.Where(item => item.EmployeementType == "Casual" && item.Status == "Approved").ToList();

               // model.CompletedTimeSheetList = permCompleted.Concat(casualApproved).Distinct().ToList();

                var TimeSheetmodel = permCompleted.Concat(casualApproved).Distinct().ToList();//.Where(item => item.Day.Value.Date >= firstDayInWeek.Date && item.Day.Value.Date <= lastDayInWeek.Date);
               
                //TimeSheet
                foreach (var item in TimeSheetmodel)
                {
                    int otEnd = 0;
                    int otStart = 0;
                    if (((item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes - item.BreakHours) / 60) >= double.Parse(otvalues[0].Replace("GT", "")))//&& item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes / 60) >= int.Parse(otvalues[0].Replace("GT", "")))
                    {

                        OTHoursVal = Convert.ToString(((item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes - item.BreakHours) / 60 - double.Parse(otvalues[0].Replace("GT", ""))));
                    }
                    else
                    {
                        OTHoursVal = "0";
                    }
                    if (Convert.ToInt32(item.EndTime.Value.ToString("HH")) > 18)
                    {
                        otEnd = Convert.ToInt32(item.EndTime.Value.ToString("HH")) - 18;
                    }
                    if (Convert.ToInt32(item.StartTime.Value.ToString("HH")) < 6)
                    {
                        otStart = 6 - Convert.ToInt32(item.StartTime.Value.ToString("HH"));
                    }
                    TimeSheetExportData.Add(new CompletedtimesheetExcelExportModel
                    {

                        ADPEmployeeId = item.AdpEmployeeID.ToString(),
                        ProjectName = item.ProjectName,
                        ProjectManager = item.ProjectManager,
                        CandidateName = item.CandidateName,
                        OpportunityNumber = item.OpportunityNumber,
                        Activity = item.Activity,
                        WarehouseName = item.WarehouseName,
                        //Monday
                        StartTime = item.StartTime.Value.ToString("HH:mm"),
                        EndTime = item.EndTime.Value.ToString("HH:mm"),
                        JobNo = item.JobNo,
                        OLATarget = item.OLATarget,
                        ActualQuantity = item.ActualQuantity,
                        Day = item.Day.Value.Date.ToShortDateString(),
                        Status = item.Status,
                        TimeSheetComments = item.TimeSheetComments,
                        TotalHours = item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes / 60,
                        BreakHours = item.BreakHours,
                        WorkedHours = (item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes - item.BreakHours) / 60,
                        OutsideWorkHours = otStart + otEnd,                        
                        OTHours = OTHoursVal,
                        PayFrequency = item.PayFrequency,
                        PayCycle = Convert.ToString((DateTime.Now.Date.Subtract(item.Day.Value).Days / 5) + 1)


                    });
                }

                GridView gv = new GridView();
                gv.DataSource = TimeSheetExportData;

                gv.DataBind();
                foreach (GridViewRow row in gv.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            row.Cells[i].Attributes.Add("class", "text");
                        }
                    }
                }
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
                #endregion

                //CompletedTimesheetModel model = new CompletedTimesheetModel();
                //model.CompletedTimeSheetList = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;



                //DateTime today = DateTime.Today;
                //int currentDayOfWeek = (int)today.DayOfWeek;
                //DateTime sunday = today.AddDays(-currentDayOfWeek);
                //DateTime monday = sunday.AddDays(1);
                //// If we started on Sunday, we should actually have gone *back*
                //// 6 days instead of forward 1...
                //if (currentDayOfWeek == 0)
                //{
                //    monday = monday.AddDays(-7);
                //}
                //var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();

                //var group = from completedTimeSheet in model.CompletedTimeSheetList.OrderBy(x => x.CandidateName) select completedTimeSheet;
                //string path = Server.MapPath(@"\Templates");
                //string outputPath = "";
                //ExcelUtils.openExcel(group, path);



                ////string path = @"~/Templates/TimeSchedulerCopyToModify.xls";
                //System.IO.FileInfo file = new System.IO.FileInfo(path+ "\\ApprovedList.xls");
                //if (file.Exists)
                //{
                //    Response.Clear();
                //    Response.AddHeader("Content-Disposition", "attachment; filename=ApprovedTimeSchedule.xls");


                //    Response.AddHeader("Content-Length", file.Length.ToString());
                //    Response.ContentType = "application/ms-excel";
                //    Response.WriteFile(file.FullName);
                //    Response.Flush();
                //    Response.End();
                //    file.Delete();

                //}
                //System.IO.FileInfo Secfile = new System.IO.FileInfo(outputPath);
                //if (Secfile.Exists)
                //{
                //    Secfile.Delete();
                //}
            }
            return RedirectToAction("WeeklyReport", "Finanace");
        }

        [HttpPost]
        public ActionResult ExportCompletedSchedule()
        {
            var TimeSheetExportData = new List<CompletedtimesheetExportModel>();

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var TimeSheetmodel = TimeSheetAPIHelperService.PaidTimeSheetList().Result;
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
                        TimeSheetComments = item.TimeSheetComments
                    });
                }
                GridView gv = new GridView();
                gv.DataSource = TimeSheetExportData;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=PaidTimeSchedule.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("MonthlyReport", "Finanace");
        }
        [HttpPost]
        public ActionResult ApprovePaymentBulk(string CandidateName)
        {
            CompletedTimesheetModel model = new CompletedTimesheetModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
            model.CompletedTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            List<int> activityIds = model.CompletedTimeSheetList.Select(i => i.Id).ToList();

            foreach (var activityId in activityIds)
            {
                var _a = TimeSheetAPIHelperService.TimeSheetApproval(activityId).Result;
            }
            return View(model);
        }
    }
}