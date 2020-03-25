using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TimeSheet.Models;

namespace TimeSheet.ServiceHelper
{
    public class ProjectionHelperService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];

        public static async Task<ReturnModel> ProjectionEntryAdd(ProjectionEntryModel ProjectionActivityEntryRecord)
        {
            ReturnModel ReturnResult = new ReturnModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Projection/ProjectionActivityEntry"), ProjectionActivityEntryRecord).Result;
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
        public static async Task<ReturnModel> ProjectionEntryUpdate(ProjectionEntryModel ProjectionActivityEntryRecord)
        {
            ReturnModel ReturnResult = new ReturnModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Projection/ProjectionUpdate"), ProjectionActivityEntryRecord).Result;
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
        public static async Task<ReturnModel> AddProjectionEnter(ProjectionModel ProjectionActivityEntryRecord)
        {
            ReturnModel ReturnResult = new ReturnModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Projection/ProjectionActivityEntry"), ProjectionActivityEntryRecord).Result;
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
        public static async Task<List<ProjectionModel>> ProjectionListItems()
        {
            List<ProjectionModel> ReturnResult = new List<ProjectionModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projection/ProjectionListItems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ProjectionModel>>();                   
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Projection Entries";
                }
            }
            return ReturnResult;
        }
        public static async Task<List<ProjectionOppurtunityModel>> MergedProjectionServices()
        {
            List<ProjectionOppurtunityModel> ReturnResult = new List<ProjectionOppurtunityModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projection/MergedProjectionServices")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ProjectionOppurtunityModel>>();
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Projection Entries";
                }
            }
            return ReturnResult;
        }
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
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Projection Entries";
                }
            }
            return ReturnResult;
        }

        public static async Task EditProjectionModel(ProjectionUpdate UpdateModel)
        {
            var ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("Projection/Update"), UpdateModel).Result;
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
        public static async Task<List<ProjectionOppurtunityModel>> ProjectionOppurtunityServiceListItems()
        {
            List<ProjectionOppurtunityModel> ReturnResult = new List<ProjectionOppurtunityModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projection/ProjectionOpportunityServiceList ")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ProjectionOppurtunityModel>>();

                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Projection Entries";
                }
            }
            return ReturnResult;
        }
        public static async Task<List<ProjectionModel>> MergedProjectionList()
        {
            List<ProjectionModel> ReturnResult = new List<ProjectionModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projection/MergedProjectionServices")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ProjectionModel>>();

                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Projection Entries";
                }
            }
            return ReturnResult;
        }

    }
}