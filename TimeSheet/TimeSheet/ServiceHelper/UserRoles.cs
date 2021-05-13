using System.Linq;
using System.Web;

namespace TimeSheet.ServiceHelper
{
    public class UserRoles
    {

        public static bool UserEditCompleteBookings()
        {
            string UserName = HttpContext.Current.Session["Username"] != null ? HttpContext.Current.Session["Username"].ToString() : null;
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            var _roles = LoginService.UserRoleList(UserName).Result;
            return _roles.Where(r => r.Value == "Administrator"
            || r.Value == "Accounts" || r.Value == "WarehouseManager" || r.Value == "ProjectmanagerAdmin").FirstOrDefault() != null ? true : false;
        }

        public static bool UserCanConfirmBookings()
        {
            string UserName = HttpContext.Current.Session["Username"] != null ? HttpContext.Current.Session["Username"].ToString() : null;
            string FullName = HttpContext.Current.Session["FullName"] != null ? HttpContext.Current.Session["FullName"].ToString() : null;
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            var _roles = LoginService.UserRoleList(UserName).Result;
            return _roles.Where(r => r.Value == "Administrator"|| r.Value == "Accounts").FirstOrDefault() != null ? true : false;
        }

        public static bool PMEditCompleteBookings()
        {
            string UserName = HttpContext.Current.Session["Username"] != null ? HttpContext.Current.Session["Username"].ToString() : null;
            if (string.IsNullOrEmpty(UserName))         
              return false;          
            var _roles = LoginService.UserRoleList(UserName).Result;
            return _roles.Where(r => r.Value == "Administrator" || r.Value == "Accounts" ||  r.Value == "ProjectmanagerAdmin")
            .FirstOrDefault() != null ? true : false;
        }

        public static bool UserCanEditTimesheet()
        {
            return UserEditCompleteBookings();
        }
        public static bool UserCanRateTimesheet()
        {          
            return UserEditCompleteBookings();
        }
        public static bool SiteAdmin()
        {
            bool returnvalue = false;
            string UserName = HttpContext.Current.Session["Username"] != null ? HttpContext.Current.Session["Username"].ToString() : null;
            if (string.IsNullOrEmpty(UserName))
            {
                return returnvalue;
            }
            var _roles = LoginService.UserRoleList(UserName).Result.Where(r=>r.Value== "Administrator" || r.Value == "Accounts" || r.Value == "Verser Admin").ToList();
            if (_roles.Count()>0)
            {
                returnvalue = true;
            }          
            return returnvalue;
        }
        public static bool UserCanEditCompletedTimesheet()
        {
            return UserEditCompleteBookings();
        }

        public static bool UserCanRegisterPMTimesheet()
        {           
            return PMEditCompleteBookings();
        }
        public static bool UserCanRegisterTimesheet()
        {           
            return UserEditCompleteBookings();
        }
        public static bool UserCanViewResourceDetails()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                return true;     
            }
            return false;
        }
        public static bool UserCanApproveTimesheet()
        {           
            return UserEditCompleteBookings();
        }
        public static bool HRUser()
        {
            string UserName = HttpContext.Current.Session["Username"] != null ? HttpContext.Current.Session["Username"].ToString() : null;
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            var _roles = LoginService.UserRoleList(UserName).Result;
            return _roles.Where(r => r.Value == "HRAdmin" || r.Value == "Administrator" || r.Value == "Accounts").FirstOrDefault() != null ? true : false;
        }
        public static bool UserCanAddOrUpdateProjection()
        {          
            return PMEditCompleteBookings();
        }

        public static bool IsLoginActive()
        {
            if (HttpContext.Current.Session["Username"] != null || HttpContext.Current.Session["FullName"] != null)
            {
                return true;
            }           
            return false;
        }
    }
}