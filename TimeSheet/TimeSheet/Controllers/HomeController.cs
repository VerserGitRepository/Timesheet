using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    
    public class HomeController : Controller
    {
        [OutputCache(CacheProfile = "OneHour", VaryByHeader ="X-Requested-With", Location =OutputCacheLocation.Server)]       
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TimeSheetViewModel model = new TimeSheetViewModel();                            
                model.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");  
                model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                model.EmploymentList = new SelectList(ListItemService.EmploymentTypeList().Result, "ID", "Value");
                model.CandidateTimeSheetList = TimeSheetAPIHelperService.TimeSheetList().Result;
                model.HasUserPermissionsToEdit = UserRoles.UserCanEditTimesheet();
                Session["HasUserPermissionsToEdit"] = UserRoles.UserCanEditTimesheet();
                Session["HomeIndex"] = model;
                return View(model);
            }                 
        }
        [HttpPost]
        public ActionResult Index(TimeSheetViewModel SearchModel)
        {           
            if (UserRoles.UserCanEditTimesheet() != true)
            {
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                TimeSheetViewModel model = new TimeSheetViewModel();
                model.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");
                model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
                var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                });
                int opportunityId = listitem.FirstOrDefault().Id;
                model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                model.EmploymentList = new SelectList(ListItemService.EmploymentTypeList().Result, "ID", "Value");
                model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                var Search= new SearchViewModel
                {
                    ProjectID= SearchModel.ProjectID,
                    WarehouseNameId = SearchModel.WarehouseID,
                    CandidateNameId = SearchModel.ResourceID,
                    OpportunityNumberID  = SearchModel.OpportunityID,
                    EmploymentTypeID = SearchModel.EmploymentTypeID
                };
                model.CandidateTimeSheetList = SearchFilterService.SearchTimeSheetRecord(Search).Result;               
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult ExportTimesSheetToExcel()
        {
            List<TimeSheetViewModel> TimeSheetmodel = new List<TimeSheetViewModel>();
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {               
                TimeSheetmodel = TimeSheetAPIHelperService.TimeSheetList().Result;
                GridView gv = new GridView();
                gv.DataSource = TimeSheetmodel;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=TimeScheduleActivitiesList.xls");
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
        [HttpGet]
        public JsonResult ProjectOpportunities(int projectId)
        {
            List<ListItemViewModel> load = new List<ListItemViewModel>();
            load = TimeSheetAPIHelperService.ProjectOpportunities(projectId).Result.Select(x => new ListItemViewModel()
            {
                Id = x.Id,
                Value = x.Value
            }).ToList();
            return Json(load, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult TimeSheetCandidateDetails(string CandidateName)
        {
            TimeSheetViewModel model = new TimeSheetViewModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetList().Result;
            model.CandidateTimeSheetList = AlltimesheetRecords.Where(c => c.CandidateName == CandidateName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("TimeSheetCandidateDetails", model);
        }

        [HttpGet]
        public ActionResult ProjectDetails(string ProjectName)
        {
            TimeSheetViewModel model = new TimeSheetViewModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetList().Result;
            model.CandidateTimeSheetList= AlltimesheetRecords.Where(c => c.ProjectName == ProjectName).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("ProjectDetails", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (UserRoles.UserCanEditTimesheet()!=true)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var ModelReturnData = TimeSheetAPIHelperService.TimeSheetSearchById(id).Result;
                ModelReturnData.Id = id;
                ModelReturnData.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
                return PartialView("Edit", ModelReturnData);
            }
        }
        [HttpPost]
        public ActionResult Edit(UpdateTimeSheetModel EditModel)
        {
            if (UserRoles.UserCanEditTimesheet() != true)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (EditModel== null)
                {
                    Session["ErrorMessage"] = "Please Update With Valid Details!";
                }
                if ( EditModel.Day != null && EditModel.StartTime != null && EditModel.EndTime != null)
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
                var ReturnValue = RegisterTimesheetService.EditTimesheetModel(EditModel);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult UpdateCandidate(UpdateTimeSheetModel CandidateEdit)
        {
            if (UserRoles.UserCanEditTimesheet() != true)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {               
                if (CandidateEdit == null)
                {
                    Session["ErrorMessage"] = "Please Update With Valid Details!";
                }
                if (CandidateEdit != null)
                {
                    CandidateEdit.FullName= Session["FullName"].ToString();
                    var ReturnValue = RegisterTimesheetService.EditTimesheetModel(CandidateEdit);
                }                
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SearchIndex(string ProjectID, string warehouseId, string opportunityId, List<int> ResourceIDs)
        {
            TimeSheetViewModel newModel = new TimeSheetViewModel();
            if (Session["HomeIndex"] != null)
            {
                TimeSheetViewModel model = (TimeSheetViewModel)Session["HomeIndex"];
                newModel.Projectlist = model.Projectlist;
                newModel.WarehouseNameList = model.WarehouseNameList;
                newModel.CandidateNameList = model.CandidateNameList;
                List<TimeSheetViewModel> whereList = model.CandidateTimeSheetList;
                
                if (ResourceIDs!= null && ResourceIDs.Count > 0)
                    whereList = model.CandidateTimeSheetList.Where(item => ResourceIDs.Contains(Convert.ToInt32(item.ResourceID))).ToList();
                if(!string.IsNullOrEmpty(ProjectID))
                    whereList = whereList.Where(item => item.ProjectID == Convert.ToInt32(ProjectID)).ToList();
                if (!string.IsNullOrEmpty(warehouseId))
                    whereList = whereList.Where(item => item.WarehouseID == Convert.ToInt32(warehouseId)).ToList();
                if (!string.IsNullOrEmpty(opportunityId) && opportunityId !="0")
                    whereList = whereList.Where(item => item.OpportunityID == Convert.ToInt32(opportunityId)).ToList();
                newModel.CandidateTimeSheetList = whereList;
                Session["HomeSearchIndex"] = newModel;
            }
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("ViewIndex", "Home", new { });
            try
            {
                // perform some action
            }
            catch (Exception)
            {
                return Json(new { Url = redirectUrl, status = "Error" });
            }

            return Json(new { Url = redirectUrl, status = "OK" });
            //ModelState.Clear();
        }
        public ActionResult ViewIndex()
        {
            if (Session["HomeSearchIndex"] != null)
            {
                return View("Index",(TimeSheetViewModel)Session["HomeSearchIndex"]);
            }
            return View();
        }
        public ActionResult Rating()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {                
                return PartialView("RatingDetails");
            }
        }
    }
}