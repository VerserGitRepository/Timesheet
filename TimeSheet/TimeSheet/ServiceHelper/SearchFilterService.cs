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
    public class SearchFilterService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];
        public static async Task<List<TimeSheetViewModel>> SearchTimeSheetRecord(SearchViewModel SearchModel)
        {
            List<TimeSheetViewModel> CostModelLists = new List<TimeSheetViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("TimeSheet/TimeSheetSearch"), SearchModel).Result;            
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<TimeSheetViewModel>>();
                    foreach (var a in projectactivities)
                    {
                        CostModelLists.Add(a);
                    }
                }
            }
            return CostModelLists;
        }
    }
}