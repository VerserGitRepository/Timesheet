using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class CorporateExpenseClaimerController : Controller
    {
        // GET: CorporateExpenseClaimer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpenseClaims()
        {
            var ClaimData= ExpenseClaimerService.GetExpenseClaims().Result;
            return View(ClaimData);
        }
    }
}