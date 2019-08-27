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
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ProjectionModel model = new ProjectionModel();
                model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                
                List<ProjectionModel> listB = ProjectionHelperService.ProjectionListItems().Result;
                List<ProjectionOppurtunityModel> listA = ProjectionHelperService.ProjectionOppurtunityServiceListItems().Result;

                
                foreach (var x in listB)
                {
                    var itemToChange = listA.Find(d => d.OpportunityNumber == int.Parse(x.OpportunityNumber) && d.OpportinutyId == x.OpportunityID && d.ServiceActivityId == x.ServiceActivityId);
                    if (itemToChange != null)
                    {
                        itemToChange.Activity = x.Activity;
                        itemToChange.ActivityId = x.ActivityId;
                        itemToChange.Comments = x.Comments;
                        itemToChange.OpportinutyId = x.OpportunityID;
                        itemToChange.OpportunityNumber = int.Parse(x.OpportunityNumber);
                        itemToChange.DateInvoiced = x.DateInvoiced;
                        itemToChange.DateAllocated = x.DateAllocated;
                        itemToChange.Created = x.Created;
                        itemToChange.ProjectManager = x.ProjectManager;
                        itemToChange.wareHouseName = x.WarehouseName;
                        itemToChange.ActualQuantity = x.Quantity;
                       // itemToChange = null;
                    }
                        
                }
                model.projectionOpportunityModel = listA;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Index(ProjectionModel ProjectionEntryModel)
        {
            ProjectionModel model = new ProjectionModel();

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (ProjectionEntryModel != null)
                {
                    var ProjectionEntryModelRecord = new ProjectionEntryModel()
                    { 
                        ProjectID = ProjectionEntryModel.ProjectID,
                        OpportunityID= ProjectionEntryModel.OpportunityID,                       
                        WarehouseId = ProjectionEntryModel.WarehouseID,
                        Quantity= ProjectionEntryModel.Quantity,
                        Created= ProjectionEntryModel.Created,
                        DateInvoiced = ProjectionEntryModel.DateInvoiced,
                        ActivityId = ProjectionEntryModel.ActivityId,
                        ServiceActivityId= ProjectionEntryModel.ServiceActivityId                    

                    };

                 var returnstatus = ProjectionHelperService.ProjectionEntryAdd(ProjectionEntryModelRecord);
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

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (projectionUpdate == null)
                {
                    Session["ErrorMessage"] = "Please Update With Valid Details!";
                }
                if (projectionUpdate != null)
                {
                     var projectionupdate = new ProjectionUpdate()
                    {
                         Id= projectionUpdate.Id,
                         Quantity = projectionUpdate.Quantity,
                         DateInvoiced = projectionUpdate.DateInvoiced.ToString(),
                         Comments = projectionUpdate.Comments
                     };
                    var IsReturnValid = ProjectionHelperService.EditProjectionModel(projectionupdate);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult UpdateProjectionModel(ProjectionModel data)
        {
            ProjectionModel model = new ProjectionModel();

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
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
                            Comments = data.Comments
                           
                        };
                        var returnstatus = ProjectionHelperService.ProjectionEntryAdd(ProjectionEntryModelRecord);

                    }
                    else
                    {
                        Session["ErrorMessage"] = "Please Select Faciity to proceed.";
                    }

                   
                }
            }
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Projection", new { });            
            return Json(new { Url = redirectUrl, status = "OK" });
        }
    }

  
}
