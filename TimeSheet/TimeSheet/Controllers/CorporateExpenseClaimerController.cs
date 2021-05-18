using System;
using System.Collections.Generic;
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
    public class CorporateExpenseClaimerController : Controller
    {
        
        public ActionResult Index()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public ActionResult ExpenseClaims()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            var ClaimData= ExpenseClaimerService.GetExpenseClaims().Result;
            return View(ClaimData);
        }
       
        public ActionResult ExpenseClaimItems()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ExpenseClaimItems(int ClaimId)
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            var ClaimItemsData = ExpenseClaimerService.GetExpenseClaimItemsById(ClaimId).Result;
            return View(ClaimItemsData);
        }
        [HttpPost]
        public ActionResult UpdateExpenseClaimItem(int ClaimItemId)
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            var ClaimItemsData = ExpenseClaimerService.GetExpenseClaimItem(ClaimItemId).Result;
            return View(ClaimItemsData);           
        }
        [HttpPost]
        public ActionResult UpdateExpenseClaimItemEntity(CorporateCardExpenseClaimItemsModel ClaimItemsUpdateModel)
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            ClaimItemsUpdateModel.ExpenseTotal = (ClaimItemsUpdateModel.AccommodationCost + ClaimItemsUpdateModel.TravelCost + ClaimItemsUpdateModel.ToolsCost + ClaimItemsUpdateModel.MealsCost + ClaimItemsUpdateModel.OtherCost);
            ExpenseClaimerService.UpdateExpenseClaimItem(ClaimItemsUpdateModel);
            return RedirectToAction("ExpenseClaims", "CorporateExpenseClaimer");
        }

        [HttpPost]
        public ActionResult CreateExpenseClaimItemEntity(CorporateCardExpenseClaimModel ClaimItemsUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ExpenseClaims", "CorporateExpenseClaimer");
            }
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            //foreach(var items in ClaimItemsUpdateModel.CorporateCardExpenseClaimItems)
            //{  
            //    decimal? _itemtotal= (items.AccommodationCost + items.TravelCost + items.ToolsCost + items.MealsCost + items.OtherCost);

            //    ClaimItemsUpdateModel.AccommodationTotal += items.AccommodationCost;
            //    ClaimItemsUpdateModel.TravelTotal += items.TravelCost;
            //    ClaimItemsUpdateModel.ToolsTotal += items.ToolsCost;
            //    ClaimItemsUpdateModel.MealsTotal += items.MealsCost;
            //    ClaimItemsUpdateModel.OtherTotal += items.OtherCost;
            //    ClaimItemsUpdateModel.ExpenseTotal += Convert.ToDecimal( _itemtotal);
            //}

            ExpenseClaimerService.RegisterExpenseClaim(ClaimItemsUpdateModel);
            return RedirectToAction("ExpenseClaims", "CorporateExpenseClaimer");
        }

        [HttpPost]
        public ActionResult ExportExpenseClaims()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            //var ExportableData = new List<ExportableExpenseRequests>();

           // var ClaimData = 

            //foreach (var item in ClaimData)
            //{
            //    foreach (var claimitem in item.CorporateCardExpenseClaimItems)
            //    {
            //        var _ExportableData = new ExportableExpenseRequests
            //        {
            //            ExpenseDateOfClaim = item.ExpenseDateOfClaim,
            //            EmployeeName = item.EmployeeName,
            //            ApprovedBy = item.ApprovedBy,
            //            ExpenseClaimType = item.ExpenseClaimType,
            //            CreditCardNo = item.CreditCardNo,
            //            SharePointFileLocation = item.SharePointFileLocation,
            //            IsEmailApproved = item.IsEmailApproved,
            //            Supplier = claimitem.Supplier,
            //            ItemDescription = claimitem.ItemDescription,
            //            Project = claimitem.Project,
            //            Branch = claimitem.Branch,
            //            PONumber = claimitem.PONumber,
            //            TravelCost = claimitem.TravelCost,
            //            MealsCost = claimitem.MealsCost,
            //            AccommodationCost = claimitem.AccommodationCost,
            //            ToolsCost = claimitem.ToolsCost,
            //            OtherCost = claimitem.OtherCost,
            //            ClaimDate = claimitem.ClaimDate                       
            //        };
            //        ExportableData.Add(_ExportableData);
            //    }                
            //}

            GridView gv = new GridView();
            gv.DataSource = ExpenseClaimerService.ExportExpenseClaims().Result; ;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=CurrentTimeSheetBookings.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("ExpenseClaims", " CorporateExpenseClaimer");
        }
    }
}