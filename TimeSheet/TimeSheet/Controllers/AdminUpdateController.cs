using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class AdminUpdateController : Controller
    {
        // GET: AdminUpdate
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateOpportunityModel(OpportunityListModel data)
        {          
                if (Session["Username"] != null && Session["Administrator"].ToString() == "true")
                {
                    if (data != null)
                    {                      
                        var returnstatus = AdminHelperService.UpdateOpportunityModel(data);                       
                    }
                }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Admin", new { });
            return Json(new { Url = redirectUrl, status = "OK" });
        }
    }
}