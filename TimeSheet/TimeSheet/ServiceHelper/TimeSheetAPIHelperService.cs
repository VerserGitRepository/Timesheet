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
    public class TimeSheetAPIHelperService
    {
       private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];

        public static async Task<List<ListItemViewModel>> CostModelProject()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projects/CostModelProjects")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<ProjectModel>>();

                    foreach (var p in projects)
                    {
                        projectsList.Add(new ListItemViewModel() { Id = p.Id, Value = p.ProjectName});
                    }                   
                }
            }
            return projectsList;
        }
        public static async Task<List<ProjectDetailsModel>> CostModelProjectDetails()
        {
            List<ProjectDetailsModel> projectsList = new List<ProjectDetailsModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projects/CostModelProjects")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<ProjectDetailsModel>>();

                    foreach (var p in projects)
                    {
                        projectsList.Add(new ProjectDetailsModel() { Id = p.Id, Project = p.Project, OpportunityNumber = p.OpportunityNumber,ProjectManager=p.ProjectManager
                        ,SiteAddress=p.SiteAddress,CustomerContactName=p.CustomerContactName,VerserBranch=p.VerserBranch});
                    }
                }
            }
            return projectsList;
        }
        public static async Task<List<ListItemViewModel>> JMSProjects()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projects/JMSProjectsList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projects = await response.Content.ReadAsAsync<List<JMSProjectsViewModel>>();

                    foreach (var p in projects)
                    {
                        projectsList.Add(new ListItemViewModel() { Id = p.Id, Value = p.Summary });
                    }
                }
            }
            return projectsList;
        }

        public static async Task<List<ListItemViewModel>> ProjectOpportunities(int projectID)
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/{0}/OpportunityList", projectID)).Result;
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

	  public static async Task<List<ListItemViewModel>> JobNo(int OpportunityID)
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/{0}/JobListByOpportunity", OpportunityID)).Result;
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
        public static async Task<List<ListItemViewModel>> ProjectActivities(int OpportunityID)
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ServiceCategories/{0}/ServiceCategoriesList", OpportunityID)).Result;
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
        public static async Task<List<ListItemViewModel>> WarehouseResources(int WarehouseID)
        {
            List<ListItemViewModel> ResourceList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/{0}/ResourcesByWarehouse", WarehouseID)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ListItemViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        ResourceList.Add(new ListItemViewModel() { Id = a.Id, Value = a.Value });
                    }
                }
            }
            return ResourceList;
        }


        public static async Task<List<ListItemViewModel>> ResourceFilter(int WarehouseID, int employmentType)
        {
            List<ListItemViewModel> ResourceList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("resource/ResourceFilter/{0}/{1}", WarehouseID, employmentType)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ListItemViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        ResourceList.Add(new ListItemViewModel() { Id = a.Id, Value = a.Value });
                    }
                }
            }
            return ResourceList;
        }


        public static async Task<List<ListItemViewModel>> Activities()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ServiceCategories/ActivitiesList")).Result;
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
        public static async Task<List<ListItemViewModel>> Warehouses()
        {
            List<ListItemViewModel> WarehousesList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projects/warehouseslist")).Result;
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

        public static async Task<List<ListItemViewModel>> EmploymentType()
        {
            List<ListItemViewModel> EmploymentList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Resource/ResourceEmploymentType")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Warehouses = await response.Content.ReadAsAsync<List<EmploymentTypeViewModel>>();

                    foreach (var w in Warehouses)
                    {
                        EmploymentList.Add(new ListItemViewModel() { Id = w.Id, Value = w.Value });
                    }
                }
            }
            return EmploymentList;
        }

        public static async Task<List<ListItemViewModel>> Resources()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Projects/ResourcesList")).Result;
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
        public static async Task<List<CostModelViewModel>> CostModelLists()
        {
            List<CostModelViewModel> CostModelLists = new List<CostModelViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("CostModels/CostModelsLists")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<CostModelViewModel>>();

                    foreach (var a in projectactivities)
                    {
                        CostModelLists.Add(a);
                    }
                }
            }
            return CostModelLists;
        }
        public static async Task<List<TimeSheetViewModel>> TimeSheetList()
        {
            List<TimeSheetViewModel> CostModelLists = new List<TimeSheetViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Timesheet/TimeSheetList")).Result;
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


        public static async Task<List<CompletedTimesheetModel>> TimeSheetCompletedList()
        {
            List<CompletedTimesheetModel> CostModelLists = new List<CompletedTimesheetModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/TimeSheetCompletedList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<CompletedTimesheetModel>>();

                    foreach (var a in projectactivities)
                    {
                        CostModelLists.Add(a);
                    }
                }
            }
            return CostModelLists;
        }
        public static async Task<List<CompletedTimesheetModel>> TimeSheetApprovedList()
        {
            List<CompletedTimesheetModel> CostModelLists = new List<CompletedTimesheetModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/TimeSheetApprovedList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<CompletedTimesheetModel>>();

                    foreach (var a in projectactivities)
                    {
                        CostModelLists.Add(a);
                    }
                }
            }
            return CostModelLists;
        }

        public static async Task<TimeSheetViewModel> TimeSheetSearchById(int id)
        {
           var CostModelLists = new TimeSheetViewModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Timesheet/{0}/TimeSheetSearchById",id)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<TimeSheetViewModel>();
                        CostModelLists = projectactivities;
                    //foreach (var a in projectactivities)
                    //{
                    //    CostModelLists.Add(a);
                    //}
                }
            }
            return CostModelLists;
        }

        public static async Task<List<ProjectDetailsModel>> ServiceCostList(int OpportunityID)
        {
            List<ProjectDetailsModel> CostModelLists = new List<ProjectDetailsModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/{0}/ServiceCostList",OpportunityID)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ProjectDetailsModel>>();
                    foreach (var a in projectactivities)
                    {
                        CostModelLists.Add(a);
                    }
                }
            }
            return CostModelLists;
        }
        public static async Task<ReturnModel> TimeSheetApproval(int activityId)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/TimeSheetApproval/{0}", activityId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult  = await response.Content.ReadAsAsync<ReturnModel>();
                    HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                }
                else
                {
                    ReturnResult.Message = "There is an issue with the TimeSheetApproval. Details " + response.ReasonPhrase;
                    HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                }
            }
            return ReturnResult;
        }
    }
}