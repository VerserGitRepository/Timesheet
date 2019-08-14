using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;

namespace TimeSheet.Controllers
{
    public class CostModelListItemsController : Controller
    {
        // GET: CostModelListItems
        public ActionResult Index()
        {
            List<CostModelViewModel> Model = new List<CostModelViewModel>();
            Model = ServiceHelper.TimeSheetAPIHelperService.CostModelLists().Result;
            return View(Model);
        }

    }
}