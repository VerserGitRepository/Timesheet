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

        public static async Task<ReturnModel> RateResource(ResourceRatingModel theModel)
        {
            ReturnModel ReturnResult = new ReturnModel();
            //wtonsoft.Json.
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Resource/RateResource"), theModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                    HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                }
            }
            return ReturnResult;
        }
        public static async Task<int> FetchOLA(int serviceActivityId,int opportunityId,int totalMins)
        {
            int result = 0;
            //wtonsoft.Json.
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/{0}/{1}/{2}/ActivityOLA", serviceActivityId, opportunityId, totalMins)).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<int>();
                    //HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                }
                else
                {
                   // HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                }
            }
            return result;
        }


    }

    }





