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

        public static async Task UpdateOpportunity(int opportunityID, bool isActive)
        {           
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Administration/{0}/{1}/ManageOpportunities", opportunityID, isActive)).Result;                
                if (response.IsSuccessStatusCode)
                {
                  var  ReturnResult = await response.Content.ReadAsAsync<bool>();
                    HttpContext.Current.Session["ResultMessage"] = "Opportunity Updated Successfully";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Opportunity Update UN-Successfully!";
                }

            }
        }

        public static async Task UpdateActivity(int ServiceActivityID, bool isActive)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Administration/{0}/{1}/ManageServiceActivity", ServiceActivityID, isActive)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ReturnResult = await response.Content.ReadAsAsync<bool>();
                    HttpContext.Current.Session["ResultMessage"] = "Activity Updated Successfully";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Activity Update UN-Successfully!";
                }

            }
        }
        public static async Task UpdateResourceState(string resourceName, bool isActive)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Administration/{0}/{1}/ManageResources", resourceName, isActive)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ReturnResult = await response.Content.ReadAsAsync<bool>();
                    HttpContext.Current.Session["ResultMessage"] = "Opportunity Updated Successfully";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Opportunity Update UN-Successfully!";
                }

            }
        }
        public static async Task UpdateProjectState(string Project, bool isActive)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Administration/{0}/{1}/ManageProjects", Project, isActive)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ReturnResult = await response.Content.ReadAsAsync<bool>();
                    HttpContext.Current.Session["ResultMessage"] = "Opportunity Updated Successfully";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Opportunity Update UN-Successfully!";
                }

            }
        }
        public static async Task UpdateOpportunityModel(OpportunityListUpdateModel OpportunityUpdateModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = 
                client.PostAsJsonAsync(string.Format("Administration/UpdateOpportunities"), OpportunityUpdateModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ReturnResult = await response.Content.ReadAsAsync<bool>();
                    HttpContext.Current.Session["ResultMessage"] = "Opportunity Updated Successfully";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Opportunity Update UN-Successfully!";
                }

            }
        }
    }
}