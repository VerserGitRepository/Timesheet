using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class ProjectManagerController : Controller
    {

        // GET: ProjectManager

        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                ProjectManagerTimesheet model = new ProjectManagerTimesheet();
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
                model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                model.ProjectManagerNameList = new SelectList(ListItemService.ProjectManagers().Result, "ID", "Value");
                model.EmploymentList = new SelectList(ListItemService.EmploymentTypeList().Result, "ID", "Value");
                model.PMTimeSheetList = TimeSheetAPIHelperService.PMTimeSheetList().Result;
                return View(model);
            }

        }


        //[HttpPost]
        //public ActionResult Register(ProjectManagerTimesheet RegisterModel)
        //{
        //    int? a = RegisterModel.PMRegisterViewModel.ProjectID;


        //    if (Session["Username"] == null)
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }
        //    else
        //    {
        //        if (RegisterModel.PMRegisterViewModel.ProjectID != null && RegisterModel.PMRegisterViewModel.WarehouseID != null && RegisterModel.PMRegisterViewModel.OpportunityID != null
        //            && RegisterModel.PMRegisterViewModel.ResourceID != null
        //            && RegisterModel.PMRegisterViewModel.Day != null)
        //        {

        //            string dateString = String.Format("{0:dd/MM/yyyy}", RegisterModel.PMRegisterViewModel.Day);
        //            string StartTimeString = String.Format("{0:HH:mm}", RegisterModel.PMRegisterViewModel.StartTime.Value);
        //            string EndTimeString = String.Format("{0:HH:mm}", RegisterModel.PMRegisterViewModel.EndTime);

        //            string dtSt = dateString + " " + StartTimeString;
        //            string dtEn = dateString + " " + EndTimeString;

        //            var StartdateTime = Convert.ToDateTime(dtSt);
        //            var EnddateTime = Convert.ToDateTime(dtEn);
        //            RegisterModel.PMRegisterViewModel.StartTime = StartdateTime;
        //            RegisterModel.PMRegisterViewModel.EndTime = EnddateTime;

        //            var MapRegisterData = MapTimesheetRegisterModel(RegisterModel);
        //            var test = RegisterTimesheetService.RegisterPMBooking(MapRegisterData);
        //        }
        //        return RedirectToAction("Index", "ManageCalender");


        //    }
        //}
        //public PMRegisterViewModel MapTimesheetRegisterModel(ProjectManagerTimesheet timesheetviewmodel)
        //{

        //    var MapRegisterData = new PMRegisterViewModel()
        //    {
        //        ProjectID = timesheetviewmodel.PMRegisterViewModel.ProjectID,
        //        WarehouseID = timesheetviewmodel.PMRegisterViewModel.WarehouseID,
        //        OpportunityID = timesheetviewmodel.PMRegisterViewModel.OpportunityID,
        //        ResourceID = timesheetviewmodel.PMRegisterViewModel.ResourceID,
        //        ServiceActivityID = timesheetviewmodel.ServiceActivityID,

        //        Colour = timesheetviewmodel.PMRegisterViewModel.Colour,
        //        StartTime = timesheetviewmodel.PMRegisterViewModel.StartTime,
        //        EndTime = timesheetviewmodel.PMRegisterViewModel.EndTime,
        //        Day = timesheetviewmodel.PMRegisterViewModel.Day,
        //        TimeSheetComments = timesheetviewmodel.PMRegisterViewModel.TimeSheetComments,

        //    };
        //    return MapRegisterData;
        //}
        //public ActionResult Register()
        //{
        //    var modelregister = new PMRegisterViewModel()
        //    {
        //        Day = DateTime.Now,
        //        StartTime= DateTime.Now,
        //        EndTime = DateTime.Now,
        //        ServiceActivityID = 5,
        //        WarehouseID=1,
        //        ProjectID = 7,
        //        OpportunityID=5,
        //        Colour = "red",
        //        TimeSheetComments = "test",
        //        ResourceID=251
        //    };


        //    var a = RegisterTimesheetService.RegisterPMBooking(modelregister);
        //    return View();
        //}
    }
}