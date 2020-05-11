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
                model.ProjectList = new SelectList(listadd.Select(x => x.ProjectName).Distinct());
                model.OpportunityNumberList = new SelectList(listadd.Select(x => x.OpportunityNumber).Distinct());
                model.ProjectmanagerList = new SelectList(listadd.Select(x => x.ProjectManager).Distinct());
                model.projectionOpportunityModel = listadd;  // listA;
                Session["projectionModel"] = model;
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
        public ActionResult IndexJSON()
        {
            if (UserRoles.UserCanApproveTimesheet() == true)
            {
                ProjectionModel model = new ProjectionModel();

                model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");

                var listadd = new List<ProjectionOppurtunityModel>();
                listadd = ProjectionHelperService.MergedProjectionServices().Result;
                model.ProjectList = new SelectList(listadd.Select(x => x.ProjectName).Distinct());
                model.OpportunityNumberList = new SelectList(listadd.Select(x => x.OpportunityNumber).Distinct());
                model.ProjectmanagerList = new SelectList(listadd.Select(x => x.ProjectManager).Distinct());
                model.projectionOpportunityModel = listadd;  // listA;
                Session["projectionModel"] = model;
                return Json(new { data = model.projectionOpportunityModel }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetProjections(int opportunityId)
        {
            ProjectionModel model = new ProjectionModel();

            model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");

            var listadd = new List<ProjectionOppurtunityModel>();
            listadd = ProjectionHelperService.MergedProjectionServices().Result;
            model.ProjectList = new SelectList(listadd.Select(x => x.ProjectName).Distinct());
            model.OpportunityNumberList = new SelectList(listadd.Select(x => x.OpportunityNumber).Distinct());
            model.ProjectmanagerList = new SelectList(listadd.Select(x => x.ProjectManager).Distinct());
            model.projectionOpportunityModel = listadd.Where(i=> i.OpportunityNumber == opportunityId).ToList();  // listA;
            return PartialView("ProjectionsFilter", model);
        }
        [HttpGet]
        public ActionResult GetProjectionsForPM(string projectManager)
        {
            ProjectionModel model = new ProjectionModel();

            model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");

            var listadd = new List<ProjectionOppurtunityModel>();
            listadd = ProjectionHelperService.MergedProjectionServices().Result;
            //model.ProjectList = new SelectList(listadd.Select(x => x.ProjectName).Distinct());
            //model.OpportunityNumberList = new SelectList(listadd.Select(x => x.OpportunityNumber).Distinct());
            //model.ProjectmanagerList = new SelectList(listadd.Select(x => x.ProjectManager).Distinct());
            model.projectionOpportunityModel = listadd.Where(i => i.ProjectManager == projectManager).ToList();  // listA;
            return PartialView("ProjectionsFilter", model);
        }
        [HttpGet]
        public ActionResult GetProjectionsJSONData(int opportunityId)
        {
            ProjectionModel model = new ProjectionModel();

            model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");

            var listadd = new List<ProjectionOppurtunityModel>();
            listadd = ProjectionHelperService.MergedProjectionServices().Result;
            model.ProjectList = new SelectList(listadd.Select(x => x.ProjectName).Distinct());
            model.OpportunityNumberList = new SelectList(listadd.Select(x => x.OpportunityNumber).Distinct());
            model.ProjectmanagerList = new SelectList(listadd.Select(x => x.ProjectManager).Distinct());
            model.projectionOpportunityModel = listadd.Where(i => i.OpportunityNumber == opportunityId).ToList();  // listA;
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchIndex(string projectName, int oppNumber,string pmName)
        {
            if (UserRoles.UserCanApproveTimesheet() == true)
            {
                if (Session["projectionModel"] != null)
                {
                    ProjectionModel model = new ProjectionModel();

                    model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");

                    var listadd = new List<ProjectionOppurtunityModel>();
                    listadd = ProjectionHelperService.MergedProjectionServices().Result;
                    model.ProjectList = new SelectList(listadd.Select(x => x.ProjectName).Distinct());
                    model.OpportunityNumberList = new SelectList(listadd.Select(x => x.OpportunityNumber).Distinct());
                    model.ProjectmanagerList = new SelectList(listadd.Select(x => x.ProjectManager).Distinct());
                    model.projectionOpportunityModel = listadd;  // listA;
                    listadd = model.projectionOpportunityModel.Where(x => x.ProjectName == projectName && x.OpportunityNumber == Convert.ToInt32(oppNumber) && x.ProjectManager == pmName).ToList();
                    model.projectionOpportunityModel = listadd;  // listA;
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
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
        }
        [HttpPost]
        public ActionResult Index(ProjectionModel ProjectionEntryModel)
        {
            ProjectionModel model = new ProjectionModel();
            if (UserRoles.UserCanRegisterPMTimesheet())
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
        public ActionResult AddOrUpdateProjectionModel(ProjectionModel data)
        {
            ProjectionModel model = new ProjectionModel();
            if (UserRoles.UserCanRegisterPMTimesheet())
            {
                if (data != null)
                {
                    if (data.WarehouseID != 0)
                    {
                        var ProjectionEntryModelRecord = new ProjectionEntryModel()
                        {
                            ProjectID = data.ProjectID,
                            ProjectionQuantity = data.ProjectionQuantity,
                            CostModelQuantity = data.CostModelQuantity,
                            Id = data.Id,
                            ProjectionId = data.ProjectionId,
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
                        if (data.IsAdd)
                        {
                            var returnstatus = ProjectionHelperService.ProjectionEntryAdd(ProjectionEntryModelRecord);
                        }
                        else
                        {
                            var returnstatus = ProjectionHelperService.ProjectionEntryUpdate(ProjectionEntryModelRecord);
                        }
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
            if (UserRoles.UserCanApproveTimesheet())
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
                    var agrModel = new AggregatedCompletedTimesheetModel();
                    foreach (var t in subgroup)
                    {
                        agrModel.CandidateName = t.CandidateName;
                        agrModel.ProjectManager = t.ProjectManager;
                        agrModel.EmployeementType = t.EmployeementType;
                        hours += t.EndTime.Value.Subtract(t.StartTime.Value).TotalMinutes/60;
                        agrModel.ResourceId = Convert.ToInt32(t.ResourceID);
                        agrModel.ProjectID = Convert.ToInt32(t.ProjectID);
                        agrModel.projectManagerID = Convert.ToInt32(t.projectManagerID);
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
            var _a = TimeSheetAPIHelperService.ApproveIndividualTimesheet(_timesheetid).Result;
            return PartialView("ResourceDetails", model);
        }
        [HttpGet]
        public ActionResult ApprovePaymentBulk(int resourceId,int PMId,int projectId)
        {
            if (UserRoles.UserCanApproveTimesheet() != true)
            {
                return RedirectToAction("Index", "Home");
            }            
            var _a = TimeSheetAPIHelperService.ApproveBulkTimesheet(resourceId,PMId,projectId).Result;            
            return PartialView("WeeklyReport");
        }
        [HttpGet]
        public ActionResult Updatepaid(int resourceId, int PMId, int projectId)
        {
            if (UserRoles.UserCanApproveTimesheet() != true)
            {
                return RedirectToAction("Index", "Home");
            }
            var _a = TimeSheetAPIHelperService.UpdatePaid(resourceId, PMId, projectId).Result;
            return PartialView("WeeklyReport");
        }
        [HttpPost]
        public ActionResult ExportProjections()
        {
            List<ProjectionOppurtunityModel> list = ProjectionHelperService.MergedProjectionServices().Result;

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

                List<ProjectionOppurtunityExportModel> theModel = new List<ProjectionOppurtunityExportModel>();
                foreach (ProjectionOppurtunityModel mdl in list)
                {
                    ProjectionOppurtunityExportModel modelexport = new ProjectionOppurtunityExportModel();
                    modelexport.Activity = mdl.Activity;
                    modelexport.ActualQuantity = Convert.ToInt32(mdl.ActualQuantity);                 
                    modelexport.Comments = mdl.Comments;
                    modelexport.DateAllocated = mdl.DateAllocated.HasValue ? mdl.DateAllocated.Value.Date.ToString("dd/MM/yyyy") : "";
                    modelexport.DateInvoiced = mdl.DateInvoiced.HasValue ? mdl.DateInvoiced.Value.Date.ToString("dd/MM/yyyy") : "";                 
                    modelexport.ProjectionRevenue = Convert.ToDecimal(mdl.ProjectionQuantity * mdl.priceperUnit);
                    modelexport.CostModelRevenue = Convert.ToDecimal(mdl.CostModelQuantity * mdl.priceperUnit);
                    modelexport.Opportunity = mdl.OpportunityNumber;
                    modelexport.priceperUnit = mdl.priceperUnit;
                    modelexport.CostModelQuantity = mdl.CostModelQuantity;
                    modelexport.ProjectionQuantity = mdl.ProjectionQuantity;
                    modelexport.ProjectManager = mdl.ProjectManager;
                    modelexport.CostperUnit = mdl.CostperUnit;
                    modelexport.LabourCostHoursPerUnit = mdl.LabourCostHoursPerUnit;
                    modelexport.PMCostHoursPerUnit = mdl.PMCostHoursPerUnit;
                    modelexport.SalesManager = mdl.SalesManager;
                    modelexport.Project = mdl.ProjectName;
                    modelexport.Facility = mdl.wareHouseName;
                    modelexport.Gap = (modelexport.CostModelRevenue - modelexport.ProjectionRevenue);
                    theModel.Add(modelexport);
                }
                GridView gv = new GridView();
                gv.DataSource = theModel;
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

        [HttpGet]
        public JsonResult ProjectOpportunities(string projectName)
        {
            List<int> load = new List<int>();
            if (Session["projectionModel"] != null)
            {
                ProjectionModel model = new ProjectionModel();

                model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");

                var listadd = new List<ProjectionOppurtunityModel>();
                listadd = ProjectionHelperService.MergedProjectionServices().Result;
              
                model.projectionOpportunityModel = listadd;  // listA;
                load = model.projectionOpportunityModel.Where(x => x.ProjectName == projectName).Select(x =>  Convert.ToInt32(x.OpportunityNumber)).Distinct().ToList();
                // load = listadd.Select(x => new ListItemViewModel() { Id = Convert.ToInt32(x.OpportunityNumber) }).ToList();
                // model.projectionOpportunityModel = listadd;  // listA;
                return Json(load, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(load, JsonRequestBehavior.AllowGet);
            }

           // return Json(load, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult PMList(int OpportunityId)
        {
            List<string> load = new List<string>();
            if (Session["projectionModel"] != null)
            {
                ProjectionModel model = new ProjectionModel();

                model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");

                var listadd = new List<ProjectionOppurtunityModel>();
                listadd = ProjectionHelperService.MergedProjectionServices().Result;
               
                model.projectionOpportunityModel = listadd;  // listA;
                load = model.projectionOpportunityModel.Where(x => x.OpportunityNumber == OpportunityId).Select(x =>  x.ProjectManager ).Distinct().ToList();
                // load = listadd.Select(x => new ListItemViewModel() { Id = Convert.ToInt32(x.OpportunityNumber) }).ToList();
                // model.projectionOpportunityModel = listadd;  // listA;
                return Json(load, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(load, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
