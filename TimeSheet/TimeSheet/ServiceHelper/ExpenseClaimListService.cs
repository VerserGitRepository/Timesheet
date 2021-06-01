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
    public class ExpenseClaimListService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];

        public static async Task<List<CorporateCardHolderViewModel>> GetExpenseClaims()
        {
            var ReturnResult = new List<CorporateCardHolderViewModel>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ExpenseClaimListItems/GetCardHolderList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<CorporateCardHolderViewModel>>();
                }
            }
            return ReturnResult;
        }
        // GetCardHolderList
    }
}