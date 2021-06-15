using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
                HttpResponseMessage response = client.GetAsync(string.Format("Resource/ResourceList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ResourcesViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        projectsList.Add(new ListItemViewModel() { Id = a.Id, Value = a.value });
                    }
                }
            }
            return projectsList;
        }
        public static async Task<List<ListItemViewModel>> ProjectManagers()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/ProjectManagerList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ResourcesViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        projectsList.Add(new ListItemViewModel() { Id = a.Id, Value = a.value });
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

        public static async Task<List<ListItemViewModel>> HRBookingResources()
        {
            var _HRresourceList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/HRBookingResources")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var HRBookingResources = await response.Content.ReadAsAsync<List<ListItemViewModel>>();
                    foreach (var a in HRBookingResources)
                    {
                        _HRresourceList.Add(new ListItemViewModel() { Id = a.Id, Value = a.Value });
                    }
                }
            }
            return _HRresourceList;
        }

        public static async Task<List<OpportunityListItem>> ManageOpportunityModelList()
        {
            List<OpportunityListItem> opportunityList = new List<OpportunityListItem>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/ManageOpportunityModelList")).Result;
                if (response.IsSuccessStatusCode)
                {
                     opportunityList = await response.Content.ReadAsAsync<List<OpportunityListItem>>();
                }
            }
            return opportunityList;
        }

        public static TimeSheetViewModel DropDownListFactory()
        {
            var DropDownListItems = new TimeSheetViewModel();
            DropDownListItems.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");
            DropDownListItems.WarehouseNameList = new SelectList(Warehouses().Result, "ID", "Value");
            DropDownListItems.CandidateNameList = new SelectList(Resources().Result, "ID", "Value");
            DropDownListItems.EmploymentList = new SelectList(EmploymentTypeList().Result, "ID", "Value");
            DropDownListItems.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");        

            return DropDownListItems;
        }

        public static async Task<List<ListItems>> GetSuppliers()
        {
            var Suppliers = new List<ListItems>();         
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(TimeSheetAPIURl);
                    HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaimListItems/GetInvoiceSupplierList")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                    Suppliers = await response.Content.ReadAsAsync<List<ListItems>>();
                    }
                }           
            return Suppliers;
        }
        public static async Task<List<ListItems>> GetSuppliersGLCodes()
        {
            var Suppliers = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaimListItems/GetSuppliersGLCodes")).Result;
                if (response.IsSuccessStatusCode)
                {
                    Suppliers = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return Suppliers;
        }

        public static async Task<List<ListItems>> GetCardHolders()
        {
            var Suppliers = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"ExpenseClaimListItems/GetCardHolders")).Result;
                if (response.IsSuccessStatusCode)
                {
                    Suppliers = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return Suppliers;
        }

        public static async Task<List<ListItems>> GetProjects()
        {
            var Suppliers = new List<ListItems>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"inventorycontrol/projects/listitems")).Result;
                if (response.IsSuccessStatusCode)
                {
                    Suppliers = await response.Content.ReadAsAsync<List<ListItems>>();
                }
            }
            return Suppliers;
        }
    }
}
//https://versergateway.com.au/TimesheetCostModelServicesDev/ListItems/HRBookingResources