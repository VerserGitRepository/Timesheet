using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {               
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Session["ErrorMessage"] = "Username Or Password Is Empty!";
                return View("Login");
            }
            LoginModel user = new LoginModel { UserName = login.UserName, Password = login.Password };
            Task<LoginModel> userReturn = LoginService.Login(user);
            if (userReturn.Result.IsLoggedIn == true)
            {
                Session["Username"] = login.UserName;
                Session["FullName"] = userReturn.Result.FullName;
                Session["ErrorMessage"] = null;       
                //var _roles = LoginService.UserRoleList(login.UserName).Result;
                //if (_roles.Count() > 0 )
                //{
                //    foreach (var r in _roles)
                //    {
                //        if (r.Value == "Administrator")
                //        {
                //            Session["Administrator"] = true;
                //            Session["HR"] = true;
                //        }
                //        if (r.Value == "Accounts")
                //        {
                //            Session["Accounts"] = true;
                //            Session["HR"] = true;
                //        }
                //        if (r.Value == "ProjectmanagerAdmin")
                //        {
                //            Session["ProjectManager"] = true;
                //        }
                //        if (r.Value == "WarehouseManager")
                //        {
                //            Session["WarehouseManager"] = true;
                //        }
                //        if (r.Value == "HRAdmin")
                //        {
                //            Session["HR"] = true;
                //        }
                //    }                  
                //}
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["FullUserName"] = null;
                Session["ErrorMessage"] = "Invalid UserName Or Password";             
                return View("Login");
            }
        }
        public ActionResult Logout(LoginModel login)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }       
    }
}