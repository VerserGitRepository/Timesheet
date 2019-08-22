using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class AdminController : Controller
    {


        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View(ManageListServices.ManageLists());
            }
        }

        [HttpPost]
        public ActionResult Update(AdminModel ModelLists)
        {
            //Use a break point here to watch values
            //Code... (save for example)
            return View();

        }

    }

}
