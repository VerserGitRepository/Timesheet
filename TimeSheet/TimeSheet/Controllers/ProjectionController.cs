using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;
namespace TimeSheet.Controllers
{
    public class ProjectionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (UserRoles.UserCanApproveTimesheet() == true)
            {
                ProjectionModel model = new ProjectionModel();

                model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");

                var listadd =new List<ProjectionOppurtunityModel>();
                listadd= ProjectionHelperService.MergedProjectionServices().Result;
                model.projectionOpportunityModel = listadd;  // listA;
                return View(model);
            }
            else if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult Index(ProjectionModel ProjectionEntryModel)
        {
            ProjectionModel model = new ProjectionModel();
            if (Session["Username"] != null && Session["ProjectManager"] != null || Session["Administrator"] != null)
            {
                if (ProjectionEntryModel != null)
                {
                    var ProjectionEntryModelRecord = new ProjectionEntryModel()
                    {
                        ProjectID = ProjectionEntryModel.ProjectID,
                        OpportunityID = ProjectionEntryModel.OpportunityID,
                        WarehouseId = ProjectionEntryModel.WarehouseID,
                        Quantity = ProjectionEntryModel.Quantity,
                        Created = ProjectionEntryModel.Created,
                        DateInvoiced = ProjectionEntryModel.DateInvoiced,
                        ActivityId = ProjectionEntryModel.ActivityId,
                        ServiceActivityId = ProjectionEntryModel.ServiceActivityId
                    };
                    var returnstatus = ProjectionHelperService.ProjectionEntryAdd(ProjectionEntryModelRecord);
                }
            }
            else if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View(LoadDropDownsServices.ProjectionDropdowns());
        }
        public ActionResult InlineEditing()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProjection(ProjectionModel projectionUpdate)
        {
            if (Session["Username"] != null && Session["ProjectManager"] != null || Session["Administrator"] != null)
            {
                if (projectionUpdate == null)
                {
                    Session["ErrorMessage"] = "Please Update With Valid Details!";
                }
                if (projectionUpdate != null)
                {
                    var projectionupdate = new ProjectionUpdate()
                    {
                        Id = projectionUpdate.Id,
                        Quantity = projectionUpdate.Quantity,
                        DateInvoiced = projectionUpdate.DateInvoiced.ToString(),
                        Comments = projectionUpdate.Comments
                    };
                    var IsReturnValid = ProjectionHelperService.EditProjectionModel(projectionupdate);
                }
            }
            else if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AddOrUpdateProjectionModel(ProjectionModel data)
        {
            ProjectionModel model = new ProjectionModel();
            if (Session["Username"] != null && Session["ProjectManager"] != null || Session["Administrator"] != null)
            {
                if (data != null)
                {
                    if (data.WarehouseID != 0)
                    {
                        var ProjectionEntryModelRecord = new ProjectionEntryModel()
                        {
                            ProjectID = data.ProjectID,
                            OpportunityID = data.OpportunityID,
                            WarehouseId = data.WarehouseID,
                            Quantity = data.Quantity,
                            DateAllocated = data.DateAllocated,
                            DateInvoiced = data.DateInvoiced,
                            ActivityId = data.ActivityId,
                            ServiceActivityId = data.ServiceActivityId,
                            Comments = data.Comments,
                            IsAdd = data.IsAdd
                        };
                        var returnstatus = ProjectionHelperService.ProjectionEntryAdd(ProjectionEntryModelRecord);
                    }
                    else
                    {
                        Session["ErrorMessage"] = "Please Select Faciity to proceed.";
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Projection", new { });
            return Json(new { Url = redirectUrl, status = "OK" });
        }

        public ActionResult ApproveProjectTimesheet()
        {
            if (UserRoles.UserCanApproveTimesheet() == true)
            {
                CompletedTimesheetModel model = new CompletedTimesheetModel();
                model.CompletedTimeSheetList = TimeSheetAPIHelperService.TimeSheetCompletedList().Result.Where(c=>c.EmployeementType == "Casual").ToList();

                DateTime dtFrom = DateTime.Parse("10:00 AM");
                DateTime dtTo = DateTime.Parse("12:00 PM");
                model.AggregaredTimesheetModel = new List<AggregatedCompletedTimesheetModel>();
                model.HasUserPermissionsToEdit = UserRoles.UserCanEditTimesheet();
                var group = from completedTimeSheet in model.CompletedTimeSheetList group completedTimeSheet by new { completedTimeSheet.CandidateName, completedTimeSheet.ProjectManager } into g select g;
                foreach (var subgroup in group)
                {
                    double hours = 0;
                    AggregatedCompletedTimesheetModel agrModel = new AggregatedCompletedTimesheetModel();

                    foreach (var t in subgroup)
                    {
                        agrModel.CandidateName = t.CandidateName;
                        agrModel.ProjectManager = t.ProjectManager;
                        agrModel.EmployeementType = t.EmployeementType;
                        hours += t.EndTime.Value.Subtract(t.StartTime.Value).TotalMinutes/60;
                    }
                    agrModel.Hours = hours;
                    model.AggregaredTimesheetModel.Add(agrModel);
                }
                return View(model);
            }
            else if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public ActionResult ResourceDetails(string CandidateName)
        {
            CompletedTimesheetModel model = new CompletedTimesheetModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
            model.CompletedTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("ResourceDetails", model);
        }
        [HttpPost]
        public ActionResult ApprovePaymentIndividual(string activityId)
        {
            var model = new CompletedTimesheetModel();
            model.CompletedTimeSheetList = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
            int _timesheetid = Convert.ToInt32(activityId);
            var _a = TimeSheetAPIHelperService.TimeSheetApproval(_timesheetid).Result;
            return PartialView("ResourceDetails", model);
        }
        [HttpPost]
        public ActionResult ApprovePaymentBulk(string CandidateName)
        {
            if (UserRoles.UserCanApproveTimesheet() != true)
            {
                return RedirectToAction("Index", "Home");
            }
            CompletedTimesheetModel model = new CompletedTimesheetModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
            model.CompletedTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            List<int> activityIds = model.CompletedTimeSheetList.Select(i => i.Id).ToList();

            foreach (var activityId in activityIds)
            {
                var _a = TimeSheetAPIHelperService.TimeSheetApproval(activityId).Result;
            }
            return PartialView("ResourceDetails", model);
        }
        [HttpPost]
        public ActionResult ExportProjections()
        {
            var TimeSheetExportData = new List<CompletedtimesheetExportModel>();

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                //ProjectionModel model = new ProjectionModel();

                //model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");

                //var listadd = new List<ProjectionOppurtunityModel>();
                //listadd = ProjectionHelperService.MergedProjectionServices().Result;
                //model.projectionOpportunityModel = listadd;  // listA;

                
                GridView gv = new GridView();
                gv.DataSource = ProjectionHelperService.MergedProjectionServices().Result;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ProjectionListExport.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("Index", "Projection");
        }
    }
}
