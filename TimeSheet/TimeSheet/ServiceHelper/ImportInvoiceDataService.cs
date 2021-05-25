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

        public static void CreateInvoiceItem(ImportInvoiceDataModel InvoiceDataRequest)
        {
            var _json = Newtonsoft.Json.JsonConvert.SerializeObject(InvoiceDataRequest);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ExpenseClaims/CreateInvoiceItem"), InvoiceDataRequest).Result;
                if (response.IsSuccessStatusCode)
                {
                    //var  ReturnResult =  response.Content.ReadAsAsync<bool>();
                    HttpContext.Current.Session["ResultMessage"] = "Invoice Created Successfully!";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Invoice Creation Failed!";
                }
            }
        }

        public static async Task<IEnumerable<ImportInvoiceDataModel>> GetInvoiceLineItems()
        {
            var ReturnResult = new List<ImportInvoiceDataModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ExpenseClaims/InvoiceList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ImportInvoiceDataModel>>();
                }
            }
            return ReturnResult;
        }
        public static async Task<IEnumerable<ImportInvoiceDataModel>> GetApprovedInvoiceLineItems()
        {
            var ReturnResult = new List<ImportInvoiceDataModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ExpenseClaims/GetApprovedInvoiceLineItems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ImportInvoiceDataModel>>();
                }
            }
            return ReturnResult;
        }
        public static async Task<IEnumerable<ImportInvoiceDataModel>> ExportAllInvoices()
        {
            var ReturnResult = new List<ImportInvoiceDataModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ExpenseClaims/ExportAllInvoices")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<List<ImportInvoiceDataModel>>();
                }
            }
            return ReturnResult;
        }
        public static async Task ApproveExpenseInvoice(int InvoiceID)
        {          
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaims/{InvoiceID}/ApproveExpenseInvoice")).Result;
                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Current.Session["ResultMessage"] = "Invoice Approval is Successfully!";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Invoice Approval is Failed!";
                }
            }           
        }

        public static async Task UpdateInvoicePaid(int InvoiceID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaims/{InvoiceID}/UpdateInvoicePaid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Current.Session["ResultMessage"] = "Invoice Status Updated to Paid Successfully!";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Invoice Status Updated to Paid is Failed!";
                }
            }
        }

        public static async Task DisputedExpenseInvoice(int InvoiceID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaims/{InvoiceID}/DisputedExpenseInvoice")).Result;
                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Current.Session["ResultMessage"] = "Invoice Approval is Successfully!";
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Invoice Approval is Failed!";
                }
            }
        }

        public static async Task<ImportInvoiceDataModel> GetInvoiceLineItem(int InvoiceID)
        {
            var ReturnResult = new ImportInvoiceDataModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaims/{InvoiceID}/GetInvoiceLineItem")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ImportInvoiceDataModel>();
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Invoice Approval is Failed!";
                }
            }
            return ReturnResult;
        }

        public static void UpdateInvoiceLineItem(ImportInvoiceDataModel UpdateInvoiceDataRequest)
        {
            var _json = Newtonsoft.Json.JsonConvert.SerializeObject(UpdateInvoiceDataRequest);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ExpenseClaims/UpdateInvoiceLineItem"), UpdateInvoiceDataRequest).Result;
                if (response.IsSuccessStatusCode)
                {                   
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