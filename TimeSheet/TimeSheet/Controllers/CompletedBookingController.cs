using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class CompletedBookingController : Controller
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

                    TimeSheetExportData.Add(new CompletedtimesheetExportModel
                    {
                        ProjectName = item.ProjectName,
                        CandidateName = item.CandidateName,
                        OpportunityNumber = item.OpportunityNumber,
                        Activity = item.Activity,
                        WarehouseName = item.WarehouseName,
                        StartTime =  item.StartTime,
                        EndTime = item.EndDate,
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
                Response.AddHeader("content-disposition", "attachment; filename=CompletedTimeScheduls.xls");
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