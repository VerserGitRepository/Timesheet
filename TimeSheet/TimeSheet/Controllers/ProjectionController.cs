using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                    var itemToChange = listA.FirstOrDefault(d => d.OpportunityNumber == int.Parse(x.OpportunityNumber));
                    if (itemToChange != null)
                    {
                        itemToChange.Activity = x.Activity;
                        itemToChange.ActivityId = x.ActivityId;
                        itemToChange.Comments = x.Comments;
                        itemToChange.OpportinutyId = x.OpportunityID;
                        itemToChange.OpportunityNumber = int.Parse(x.OpportunityNumber);
                        itemToChange.DateInvoiced = x.Created;
                        itemToChange.DateInvoiced = x.DateInvoiced;
                        itemToChange.ProjectManager = x.ProjectManager;
                        itemToChange.wareHouseName = x.WarehouseName;
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
                    var ProjectionEntryModelRecord = new ProjectionEntryModel()
                    {
                        ProjectID = data.ProjectID,
                        OpportunityID = data.OpportunityID,
                        WarehouseId = data.WarehouseID,
                        Quantity = data.Quantity,
                        Created = data.Created,
                        DateInvoiced = data.DateInvoiced,
                        ActivityId = data.ActivityId,
                        ServiceActivityId = data.ServiceActivityId,
                        OpportunityNumber = int.Parse(data.OpportunityNumber)

                    };

                    var returnstatus = ProjectionHelperService.ProjectionEntryAdd(ProjectionEntryModelRecord);
                }
            }
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Projection", new { });

            try
            {
                // perform some action
            }
            catch (Exception)
            {
                return Json(new { Url = redirectUrl, status = "Error" });
            }

            return Json(new { Url = redirectUrl, status = "OK" });
        }
    }

  
}
