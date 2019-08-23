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
    public class AdminHelperService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];

        public static async Task<List<OpportunityListModel>> OpportunityListItems()
        {
            List<OpportunityListModel> ReturnResult = new List<OpportunityListModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/OpportunityListItems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<OpportunityListModel>>();

                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Opportunity Entries";
                }
            }
            return ReturnResult;
        }

        public static async Task<List<ProjectListModel>> ProjectListItems()
        {
            List<ProjectListModel> ReturnResult = new List<ProjectListModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projects/CostModelProjects")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ProjectListModel>>();

                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Project Entries";
                }
            }
            return ReturnResult;
        }


        public static async Task<List<ResourceListModel>> ResourceListItems()
        {
            List<ResourceListModel> ReturnResult = new List<ResourceListModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Resource/ManageResourceList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ResourceListModel>>();

                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Resource Entries";
                }
            }
            return ReturnResult;
        }

        public static async Task<List<ActivityListModel>> ActivityListItem()
        {
            List<ActivityListModel> ReturnResult = new List<ActivityListModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/ActivityList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ActivityListModel>>();

                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Resource Entries";
                }
            }
            return ReturnResult;
        }

        public static async Task UpdateOpportunityModel(OpportunityListModel UpdateModel)
        {
            var ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Administration/{0}/{1}/ManageOpportunities", UpdateModel.Id,UpdateModel.IsActive), UpdateModel).Result;
                //HttpResponseMessage response1 = client.GetAsync(string.Format("ListItems/{0}/OpportunityActivities", OpportunityId)).Result;
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

        }
    }

}

