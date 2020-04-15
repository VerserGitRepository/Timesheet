using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet.Models;
using TimeSheet.Models.DTOs;
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
        [HttpPost]
        public ActionResult ExportApprovedBookings()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var TimeSheetmodel = AutoMapper.Mapper.Map<List<ExportApprovedTimesheetDto>>( TimeSheetAPIHelperService.TimeSheetApprovedListt().Result);

                GridView gv = new GridView();
                gv.DataSource = TimeSheetmodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ApprovedBookings.xls");
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


        [HttpPost]
        public ActionResult ExportCompleteBookingSchedule()
        {
            var TimeSheetExportData = new List<CompletedtimesheetExcelExportModel>();
            var TimeSheetmodel = TimeSheetAPIHelperService.AllBookingEntries().Result;
            string otHours = (string)ConfigurationManager.AppSettings["OTHours"];
            List<string> otvalues = new List<string>();
            otvalues = otHours.Split(';').ToList();
            string OTWeekEndSatDay = (string)ConfigurationManager.AppSettings["OTWeekEndSatDay"];
            string OTWeekEndSatDayException = (string)ConfigurationManager.AppSettings["OTWeekEndSatDayException"];
            string OTWeekEndSunDay = (string)ConfigurationManager.AppSettings["OTWeekEndSunDay"];
            string OTHoursVal = string.Empty;                                                                             //TimeSheet
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
                    if (Convert.ToInt32(item.EndTime.Value.ToString("HH")) >= 23 && Convert.ToInt32(item.EndTime.Value.ToString("mm")) >= 30)
                    {
                        otEnd = otEnd + 1;
                    }
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
                    BookedBy = item.BookedBy,
                    ApprovedBy = item.ApprovedBy,
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
                    TotalHours = Math.Round((item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes / 60), 1, MidpointRounding.ToEven),
                    BreakHours = item.BreakHours,
                    WorkedHours = Math.Round(((item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes - item.BreakHours) / 60), 1, MidpointRounding.ToEven),
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
            Response.AddHeader("content-disposition", "attachment; filename=ApprovedBookings.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("AllBookingEntries", "Home");
        }        
    }
}