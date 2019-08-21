using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TimeSheet.Models;

namespace TimeSheet.ServiceHelper
{
    public class ResourceHelperService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];

        public static async Task<List<ResourceAvailable>> ResourceListItems()
        {
            List<ResourceAvailable> ReturnResult = new List<ResourceAvailable>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Resource/AvailableResources")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ResourceAvailable>>();

                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Resource Entries";
                }
            }
            return ReturnResult;
        }
       
        }

    }





