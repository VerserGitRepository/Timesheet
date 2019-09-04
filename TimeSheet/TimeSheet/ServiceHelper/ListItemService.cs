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
    public class ListItemService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];

        public static async Task<List<ListItemViewModel>> OpportunityActivities(int OpportunityId)
        {
            List<ListItemViewModel> result = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/{0}/OpportunityActivities", OpportunityId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<ListItemViewModel>>();

                    foreach (var p in projects)
                    {
                        result.Add(new ListItemViewModel() { Id = p.Id, Value = p.Value});
                    }
                }
            }
            return result;
        }
        public static async Task<List<ListItemViewModel>> Warehouses()
        {
            List<ListItemViewModel> WarehousesList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/WarehouseList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Warehouses = await response.Content.ReadAsAsync<List<WarehousesViewModel>>();

                    foreach (var w in Warehouses)
                    {
                        WarehousesList.Add(new ListItemViewModel() { Id = w.Id, Value = w.WarehouseName });
                    }
                }
            }
            return WarehousesList;
        }

        
        public static async Task<List<ListItemViewModel>> Resources()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/ResourcesList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ResourcesViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        projectsList.Add(new ListItemViewModel() { Id = a.Id, Value = a.ResourceName });
                    }
                }
            }
            return projectsList;
        }
        public static async Task<List<ListItemViewModel>> EmploymentTypeList()
        {
            List<ListItemViewModel> EmploymentList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Resource/ResourceEmploymentType")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ListItemViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        EmploymentList.Add(new ListItemViewModel() { Id = a.Id, Value = a.Value });
                    }
                }
            }
            return EmploymentList;
        }

        public static async Task<List<ListItemViewModel>> SalesManagerList()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/SalesManagerList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ListItemViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        projectsList.Add(new ListItemViewModel() { Id = a.Id, Value = a.Value });
                    }
                }
            }
            return projectsList;
        }

        
        public static async Task<List<ListItemViewModel>> StatusList()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/StatusList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ListItemViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        projectsList.Add(new ListItemViewModel() { Id = a.Id, Value = a.Value });
                    }
                }
            }
            return projectsList;
        }
        public static async Task<List<ListItemViewModel>> ProjectManagerList()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/ProjectManagerList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ListItemViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        projectsList.Add(new ListItemViewModel() { Id = a.Id, Value = a.Value });
                    }
                }
            }
            return projectsList;
        }

        public static async Task<List<ListItemViewModel>> ServiceActivitiesList()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/ServiceActivitiesList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ListItemViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        projectsList.Add(new ListItemViewModel() { Id = a.Id, Value = a.Value });
                    }
                }
            }
            return projectsList;
        }

    }
}