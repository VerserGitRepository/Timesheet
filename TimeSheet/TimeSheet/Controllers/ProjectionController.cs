using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
namespace TimeSheet.Controllers
{
    public class ProjectionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {


            if (Session["Username"] != null && Session["ProjectManager"] != null)
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
                        itemToChange.ActivityId = x.ActivityId;
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
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult Index(ProjectionModel ProjectionEntryModel)
        {
            ProjectionModel model = new ProjectionModel();

            if (Session["Username"] != null && Session["ProjectManager"] !=null)
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
               
            else
            {
                    return RedirectToAction("Login", "Login");
             }
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

            if (Session["Username"] != null && Session["ProjectManager"] != null)
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
            if (Session["Username"] != null && Session["ProjectManager"] != null)
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
    }

  
}
