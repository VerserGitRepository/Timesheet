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
                return View(LoadDropDownsServices.ProjectionDropdowns());
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
                        ServiceActivityId= ProjectionEntryModel.ServiceActivityId,
                        ProjectManagerID = ProjectionEntryModel.ProjectManagerID,
                       

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
    }

  
}
