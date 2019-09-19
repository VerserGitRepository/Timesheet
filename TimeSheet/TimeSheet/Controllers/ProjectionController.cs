using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;
namespace TimeSheet.Controllers
{
    public class ProjectionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Username"] != null && Session["ProjectManager"] != null || Session["Administrator"] != null)
            {
                ProjectionModel model = new ProjectionModel();
                model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "Id", "Value");              

                List<ProjectionModel> listB = ProjectionHelperService.ProjectionListItems().Result;
                List<ProjectionOppurtunityModel> listA = ProjectionHelperService.ProjectionOppurtunityServiceListItems().Result;                
                foreach (var x in listB)
                {
                    var itemToChange = listA.Find(d => d.OpportunityNumber == int.Parse(x.OpportunityNumber) && d.OpportinutyId == x.OpportunityID && d.ServiceActivityId == x.ServiceActivityId && d.IsUpdated != true);
                    if (itemToChange == null)
                    {
                        itemToChange = new ProjectionOppurtunityModel();
                        itemToChange.Activity = x.Activity;
                        itemToChange.ActivityId = x.ActivityId == 0?x.ServiceRevenueId:x.ActivityId;
                        itemToChange.ServiceActivityId = int.Parse(Convert.ToString(x.ServiceActivityId));
                        itemToChange.Comments = x.Comments;
                        itemToChange.OpportinutyId = x.OpportunityID;
                        itemToChange.OpportunityNumber = int.Parse(x.OpportunityNumber);
                        itemToChange.DateInvoiced = x.DateInvoiced;
                        itemToChange.DateAllocated = x.DateAllocated;
                        itemToChange.Created = x.Created;
                        itemToChange.ProjectManager = x.ProjectManager;
                        itemToChange.wareHouseName = x.WarehouseName;
                        itemToChange.wareHouseId = x.WarehouseID;
                        itemToChange.ActualQuantity = x.Quantity;
                        itemToChange.ProjectName = x.ProjectName;
                        itemToChange.Id = x.Id;
                        itemToChange.IsUpdated = true;
                        listA.Add(itemToChange);
                    }
                    else
                    {
                        itemToChange.Activity = x.Activity;
                        if (x.ActivityId == 0)
                        {
                            if (itemToChange.ActivityId == 0)
                            {
                                itemToChange.ActivityId = x.ActivityId;
                            }
                        }
                        itemToChange.ServiceActivityId = int.Parse(Convert.ToString(x.ServiceActivityId));
                        itemToChange.Comments = x.Comments;
                        itemToChange.OpportinutyId = x.OpportunityID;
                        itemToChange.OpportunityNumber = int.Parse(x.OpportunityNumber);
                        itemToChange.DateInvoiced = x.DateInvoiced;
                        itemToChange.DateAllocated = x.DateAllocated;
                        itemToChange.Created = x.Created;
                        itemToChange.ProjectManager = x.ProjectManager;
                        itemToChange.wareHouseName = x.WarehouseName;
                        itemToChange.ActualQuantity = x.Quantity;
                        itemToChange.wareHouseId = x.WarehouseID;
                        itemToChange.Id = x.Id;
                        itemToChange.IsUpdated = true;                      
                    }                        
                }
                model.projectionOpportunityModel = listA;
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
            else if(Session["Username"] != null)
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
            if (Session["Username"] != null && Session["ProjectManager"] != null || Session["Administrator"] != null)
            {
                CompletedTimesheetModel model = new CompletedTimesheetModel();
                model.CompletedTimeSheetList = TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
                //  var result = model.CompletedTimeSheetList.GroupBy(x => new { (x.CandidateName, x.ProjectName)});
                DateTime dtFrom = DateTime.Parse("10:00 AM");
                DateTime dtTo = DateTime.Parse("12:00 PM");
                model.AggregaredTimesheetModel = new List<AggregatedCompletedTimesheetModel>();
                model.HasUserPermissionsToEdit = UserRoles.UserCanEditTimesheet();
                //int timeDiff = dtFrom.Value.Subtract(dtTo.Value).Hours;
                var group = from completedTimeSheet in model.CompletedTimeSheetList group completedTimeSheet by new {completedTimeSheet.CandidateName, completedTimeSheet.ProjectManager } into g select g  ;
                // var trwer = model.CompletedTimeSheetList.GroupBy(item => item.CandidateName).Select(g => new {Sum = g.Sum(x => x.EndTime.Value.Subtract(x.StartTime.Value).Hours),}) });
               
               
                foreach (var subgroup in group)
                {
                    int hours = 0;
                    AggregatedCompletedTimesheetModel agrModel = new AggregatedCompletedTimesheetModel();
                    
                    foreach (var t in subgroup)
                    {
                        agrModel.CandidateName = t.CandidateName;
                        agrModel.ProjectManager = t.ProjectManager;
                        agrModel.EmploymentTypeID = t.EmploymentTypeID;
                        hours += t.EndTime.Value.Subtract(t.StartTime.Value).Hours;
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
            model.CompletedTimeSheetList= TimeSheetAPIHelperService.TimeSheetCompletedList().Result;
            int _timesheetid = Convert.ToInt32(activityId);
            var _a = TimeSheetAPIHelperService.TimeSheetApproval(_timesheetid).Result;
            return PartialView("ResourceDetails", model);
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
             var _a =   TimeSheetAPIHelperService.TimeSheetApproval(activityId).Result;
            }
            return PartialView("ResourceDetails", model);
        }
    }  
}
