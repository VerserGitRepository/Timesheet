using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;
namespace TimeSheet.Controllers
{
    public class TimesheetController : Controller
    {      
        public ActionResult Index()
        {
            {
                if (Session["Username"] != null)
                {
                    return View();
                }
                else
                {                  
                    return RedirectToAction("Login", "Home");
                }
            }
        }  
        public ActionResult TimeSheet()
        {            
            TimeSheetViewModel model = new TimeSheetViewModel();
            ListItemViewModel listitems = new ListItemViewModel();
            model.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");
            model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");            
            var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select( x=> new ListItemViewModel()
            {
                Id =x.Id,Value=x.Value
            });
            int opportunityId = listitem.FirstOrDefault().Id;
            
           // model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID","Value");
            return View(model);
        }
        [HttpGet]
        public ActionResult Register() 
         {

            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                TimeSheetViewModel model = new TimeSheetViewModel();
                model.Projectlist = new SelectList(TimeSheetAPIHelperService.JMSProjects().Result, "ID", "Value");
                model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
                var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                });
                int opportunityId = listitem.FirstOrDefault().Id;
                model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                model.WarehouseNameList = new SelectList(TimeSheetAPIHelperService.Warehouses().Result, "ID", "Value");
                model.EmploymentList = new SelectList(ListItemService.EmploymentTypeList().Result, "ID", "Value");
                model.CandidateNameList = new SelectList(TimeSheetAPIHelperService.Resources().Result, "ID", "Value");
                model.CandidateTimeSheetList = TimeSheetAPIHelperService.TimeSheetList().Result;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Register(TimeSheetViewModel RegisterModel)
        {
            int? a = RegisterModel.TimeSheetRegisterModel.ProjectID;
            int? b = RegisterModel.TimeSheetRegisterModel.CandidateNameId;

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (RegisterModel.TimeSheetRegisterModel.ProjectID != null && RegisterModel.TimeSheetRegisterModel.WarehouseNameId != null && RegisterModel.TimeSheetRegisterModel.OpportunityNumberID != null 
                    && RegisterModel.TimeSheetRegisterModel.CandidateNameId != null && RegisterModel.TimeSheetRegisterModel.StartTime != null && RegisterModel.TimeSheetRegisterModel.EndTime != null
                    && RegisterModel.TimeSheetRegisterModel.Day != null)
                {

                    string dateString = String.Format("{0:dd/MM/yyyy}", RegisterModel.TimeSheetRegisterModel.Day.Value.Date);
                    string StartTimeString = String.Format("{0:HH:mm}", RegisterModel.TimeSheetRegisterModel.StartTime.Value);
                    string EndTimeString = String.Format("{0:HH:mm}", RegisterModel.TimeSheetRegisterModel.EndTime.Value);

                    string dtSt = dateString + " " + StartTimeString;
                    string dtEn = dateString + " " + EndTimeString;

                    var StartdateTime = Convert.ToDateTime(dtSt);
                    var EnddateTime = Convert.ToDateTime(dtEn);
                    RegisterModel.TimeSheetRegisterModel.StartTime = StartdateTime;
                    RegisterModel.TimeSheetRegisterModel.EndTime = EnddateTime;

                    var MapRegisterData = MapTimesheetRegisterModel(RegisterModel);
                    var test = RegisterTimesheetService.RegisterTimesheetModel(MapRegisterData);
                }
                return RedirectToAction("Index", "ManageCalender");


            }            
        }
        [HttpPost]
        public ActionResult Edit(TimeSheetViewModel RegisterModel)
        {


            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (RegisterModel.TimeSheetRegisterModel.ProjectID != null && RegisterModel.TimeSheetRegisterModel.WarehouseNameId != null && RegisterModel.TimeSheetRegisterModel.OpportunityNumberID != null
                    && RegisterModel.TimeSheetRegisterModel.CandidateNameId != null
                    && RegisterModel.TimeSheetRegisterModel.Day != null)
                {
                    string dateString = String.Format("{0:dd/MM/yyyy}", RegisterModel.TimeSheetRegisterModel.Day.Value.Date);
                    string StartTimeString = String.Format("{0:HH:mm}", RegisterModel.TimeSheetRegisterModel.StartTime.Value);
                    string EndTimeString = String.Format("{0:HH:mm}", RegisterModel.TimeSheetRegisterModel.EndTime.Value);

                    string dtSt = dateString + " " + StartTimeString;
                    string dtEn = dateString + " " + EndTimeString;

                    var StartdateTime = Convert.ToDateTime(dtSt);
                    var EnddateTime = Convert.ToDateTime(dtEn);
                    RegisterModel.TimeSheetRegisterModel.StartTime = StartdateTime;
                    RegisterModel.TimeSheetRegisterModel.EndTime = EnddateTime;

                    var MapRegisterData = MapTimesheetRegisterModel(RegisterModel);
                    var test = RegisterTimesheetService.RegisterTimesheetModel(MapRegisterData);
                }
                return RedirectToAction("Index", "ManageCalender");
            }
        }
        [HttpGet]
        public JsonResult OpportunityActivities(int OpportunityID)
        {
            List<ListItemViewModel> load = new List<ListItemViewModel>();

            load = ListItemService.OpportunityActivities(OpportunityID).Result.Select(x => new ListItemViewModel()
            {
                Id = x.Id,
                Value = x.Value
            }).ToList();
            return Json(load, JsonRequestBehavior.AllowGet);
        }

        public TimeSheetRegisterModel MapTimesheetRegisterModel(TimeSheetViewModel timesheetviewmodel)
        {
            
            var MapRegisterData = new TimeSheetRegisterModel()
            {
                ProjectID = timesheetviewmodel.TimeSheetRegisterModel.ProjectID,
                WarehouseNameId = timesheetviewmodel.TimeSheetRegisterModel.WarehouseNameId,
                OpportunityNumberID = timesheetviewmodel.TimeSheetRegisterModel.OpportunityNumberID,
                CandidateNameId = timesheetviewmodel.TimeSheetRegisterModel.CandidateNameId,
                ServiceActivityId = timesheetviewmodel.ServiceActivityID,
                OLATarget = timesheetviewmodel.TimeSheetRegisterModel.OLATarget,
                ActualQuantity = timesheetviewmodel.TimeSheetRegisterModel.ActualQuantity,
                Colour = timesheetviewmodel.TimeSheetRegisterModel.Colour,
                StartTime = timesheetviewmodel.TimeSheetRegisterModel.StartTime,
                EndTime = timesheetviewmodel.TimeSheetRegisterModel.EndTime,
                Day = timesheetviewmodel.TimeSheetRegisterModel.Day,
                TimeSheetComments= timesheetviewmodel.TimeSheetRegisterModel.TimeSheetComments,

            };
            return MapRegisterData;
        }
    }
}