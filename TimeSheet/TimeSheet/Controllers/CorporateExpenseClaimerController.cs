using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
            foreach(var items in ClaimItemsUpdateModel.CorporateCardExpenseClaimItems)
            {  
                decimal? _itemtotal= (items.AccommodationCost + items.TravelCost + items.ToolsCost + items.MealsCost + items.OtherCost);

                ClaimItemsUpdateModel.AccommodationTotal += items.AccommodationCost;
                ClaimItemsUpdateModel.TravelTotal += items.TravelCost;
                ClaimItemsUpdateModel.ToolsTotal += items.ToolsCost;
                ClaimItemsUpdateModel.MealsTotal += items.MealsCost;
                ClaimItemsUpdateModel.OtherTotal += items.OtherCost;
                ClaimItemsUpdateModel.ExpenseTotal += Convert.ToDecimal( _itemtotal);
            }
           
            //ExpenseClaimerService.UpdateExpenseClaimItem(ClaimItemsUpdateModel);

            ExpenseClaimerService.RegisterExpenseClaim(ClaimItemsUpdateModel);
            return RedirectToAction("ExpenseClaims", "CorporateExpenseClaimer");
        }
    }
}