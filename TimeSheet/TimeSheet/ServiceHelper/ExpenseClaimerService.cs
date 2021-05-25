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
    public class ExpenseClaimerService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];

        public static async Task<List<CorporateCardExpenseClaimModel>> GetExpenseClaims()
        {
            var ReturnResult = new List<CorporateCardExpenseClaimModel>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ExpenseClaims/ExpenseClaims")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<CorporateCardExpenseClaimModel>>();                  
                }                
            }
            return ReturnResult;
        }

        public static async Task<List<ExportableExpenseRequests>> ExportExpenseClaims()
        {
            var ReturnResult = new List<ExportableExpenseRequests>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ExpenseClaims/ExportExpenseClaims")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ExportableExpenseRequests>>();
                }
            }
            return ReturnResult;
        }


        public static async Task<List<CorporateCardExpenseClaimItemsModel>> GetExpenseClaimItemsById(int ClaimId)
        {
            var ReturnResult = new List<CorporateCardExpenseClaimItemsModel>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaims/{ClaimId}/GetExpenseClaimItemsById")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<CorporateCardExpenseClaimItemsModel>>();
                }
            }
            return ReturnResult;
        }

        public static async Task<CorporateCardExpenseClaimItemsModel> GetExpenseClaimItem(int ClaimItemId)
        {
            var ReturnResult = new CorporateCardExpenseClaimItemsModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaims/{ClaimItemId}/GetExpenseClaimItem")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<CorporateCardExpenseClaimItemsModel>();
                }
            }
            return ReturnResult;
        }

        public static void UpdateExpenseClaimItem(CorporateCardExpenseClaimItemsModel UpdateClaimItemModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ExpenseClaims/UpdateExpenseClaimItem"), UpdateClaimItemModel).Result;
               
                if (response.IsSuccessStatusCode)
                {
                  //var  ReturnResult =  response.Content.ReadAsAsync<bool>();
                    HttpContext.Current.Session["ResultMessage"] = "Expense ClaimItem Updated Successfully!";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Expense ClaimItem Updated Failed!";
                }
            }
        }

        public static void RegisterExpenseClaim(CorporateCardExpenseClaimModel RegisterClaimRequest)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ExpenseClaims/RegisterExpenseClaim"), RegisterClaimRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    //var  ReturnResult =  response.Content.ReadAsAsync<bool>();
                    HttpContext.Current.Session["ResultMessage"] = "Expense ClaimItem Updated Successfully!";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Expense ClaimItem Updated Failed!";
                }
            }
        }

        public static void ApproveExpenseClaimItem(int ClaimitemId, string ClaimitemStatus)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaims/{ClaimitemId}/{ClaimitemStatus}/ApproveExpenseClaimItem")).Result;

                if (response.IsSuccessStatusCode)
                {
                    //var  ReturnResult =  response.Content.ReadAsAsync<bool>();
                    HttpContext.Current.Session["ResultMessage"] = "Expense ClaimItem Updated Successfully!";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Expense ClaimItem Updated Failed!";
                }
            }
        }
    }
}