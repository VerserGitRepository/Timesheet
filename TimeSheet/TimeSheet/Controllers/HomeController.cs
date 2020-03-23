using System;
using System.Collections.Generic;
using System.Configuration;
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
            Session["Accounts"] = "";
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
        public ActionResult StatusList(string status)
        {
            TimeSheetViewModel model = new TimeSheetViewModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetList().Result;
            model.CandidateTimeSheetList = AlltimesheetRecords.Where(c => c.Status == status).ToList();
            model.StatusList = new SelectList(ListItemService.StatusList().Result, "ID", "Value");
            return PartialView("ProjectDetails", model);
        }
        [HttpGet]
        public ActionResult ProjectManagerDetails(string ProjectManagerName)
        {
            TimeSheetViewModel model = new TimeSheetViewModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.TimeSheetList().Result;
            model.CandidateTimeSheetList = AlltimesheetRecords.Where(c => c.ProjectManager == ProjectManagerName).ToList();
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
            if (UserRoles.UserCanEditTimesheet() == true && CandidateEdit != null && CandidateEdit.Id > 0)
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
                    CandidateEdit.FullName= Session["FullName"].ToString();
                    var ReturnValue = RegisterTimesheetService.EditTimesheetModel(CandidateEdit);                                  
            }
            else
            {
                Session["ErrorMessage"] = "Update Details Not Valid!";
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
        public ActionResult AllBookingEntries()
        {
            Session["Accounts"] = "";
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
               CompletedTimesheetModel model = new CompletedTimesheetModel();
               model.CompletedTimeSheetList =  TimeSheetAPIHelperService.AllBookingEntries().Result;              
                
                Session["HasUserPermissionsToEdit"] = UserRoles.UserCanEditTimesheet();
                Session["HomeIndex"] = model;
                return View("AllBookingEntries",model);
            }
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
            Response.AddHeader("content-disposition", "attachment; filename=CompleteTimeEntrySchedule.xls");
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
        [HttpGet]
        public ActionResult ReturnSlider(string StartTime, string EndTime)
        {
            if (UserRoles.UserCanEditTimesheet() != true)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                BreakTimeModel model = new BreakTimeModel();
                model.BreakStartTime = Convert.ToDateTime(StartTime).ToString("hh:mm tt");
                model.BreakEndTime = Convert.ToDateTime(EndTime).ToString("hh:mm tt");
                return PartialView("_BreakTimeSlider",model);
            }
        }
    }
}