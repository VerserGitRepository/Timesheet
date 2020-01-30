using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using TimeSheet.Controllers;

namespace TimeSheet.ServiceHelper
{
    public class UserRoles
    {
        public static bool UserCanEditTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["Accounts"] != null ||  HttpContext.Current.Session["WarehouseManager"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool UserCanRateTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["Accounts"] != null ||  HttpContext.Current.Session["WarehouseManager"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool UserCanEditCompletedTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["WarehouseManager"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool UserCanRegisterPMTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if ( HttpContext.Current.Session["Accounts"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }

            }
            return false;
        }

        public static bool UserCanRegisterTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                return true;
               
            }
            return false;
        }
        public static bool UserCanViewResourceDetails()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["WarehouseManager"]   != null || HttpContext.Current.Session["Accounts"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }

            }
            return false;
        }
        public static bool UserCanApproveTimesheet()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["WarehouseManager"] != null || HttpContext.Current.Session["Accounts"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }

            }
            return false;
        }

        public static bool HRUser()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["HR"] != null || HttpContext.Current.Session["Accounts"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }

            }
            return false;
        }

        public static bool UserCanAddOrUpdateProjection()
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                if (HttpContext.Current.Session["Accounts"] != null || HttpContext.Current.Session["ProjectManager"] != null || HttpContext.Current.Session["Administrator"] != null)
                {
                    return true;
                }

            }
            return false;
        }
    }
}