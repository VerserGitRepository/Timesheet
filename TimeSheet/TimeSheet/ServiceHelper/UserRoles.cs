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
            string UserName = HttpContext.Current.Session["Username"] != null ? HttpContext.Current.Session["Username"].ToString() : null;
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            var _roles = LoginService.UserRoleList(UserName).Result;
            return _roles.Where(r => r.Value == "Administrator" || r.Value == "Accounts").FirstOrDefault() != null ? true : false;
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
            return _roles.Where(r => r.Value == "HRAdmin").FirstOrDefault() != null ? true : false;
        }
        public static bool UserCanAddOrUpdateProjection()
        {          
            return PMEditCompleteBookings();
        }
    }
}