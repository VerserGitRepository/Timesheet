using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using TimeSheet.Models;

namespace TimeSheet.ServiceHelper
{
    public class ImportInvoiceDataService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];

        public static void AddImportedInvoiceLines(List<ImportInvoiceDataModel> InvoiceDataRequest)
        {

            var _json = Newtonsoft.Json.JsonConvert.SerializeObject(InvoiceDataRequest);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ExpenseClaims/AddImportedInvoiceLines"), InvoiceDataRequest).Result;

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