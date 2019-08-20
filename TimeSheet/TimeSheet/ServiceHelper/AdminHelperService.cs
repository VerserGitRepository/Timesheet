﻿using System;
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
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Projection Entries";
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
                    HttpContext.Current.Session["ErrorMessage"] = "Unable To Load Projection Entries";
                }
            }
            return ReturnResult;
        }


    }

    }