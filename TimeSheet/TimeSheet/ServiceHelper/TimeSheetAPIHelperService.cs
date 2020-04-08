using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TimeSheet.Models;
using TimeSheet.VerserSalesforce;

namespace TimeSheet.ServiceHelper
{
    public class TimeSheetAPIHelperService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];
        private static readonly string salesForceQuery = ConfigurationManager.AppSettings["salesForceQuery"];
        private static readonly string salesForceUser = ConfigurationManager.AppSettings["salesForceUser"];
        private static readonly string salesForcePWD = ConfigurationManager.AppSettings["salesForcePWD"];
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
                        projectsList.Add(new ListItemViewModel() { Id = p.Id, Value = p.ProjectName });
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
                        projectsList.Add(new ProjectDetailsModel()
                        {
                            Id = p.Id,
                            Project = p.Project,
                            OpportunityNumber = p.OpportunityNumber,
                            ProjectManager = p.ProjectManager
                        ,
                            SiteAddress = p.SiteAddress,
                            CustomerContactName = p.CustomerContactName,
                            VerserBranch = p.VerserBranch
                        });
                    }
                }
            }
            return projectsList;
        }

        public static async Task<List<ListItemViewModel>> HRActivities()
        {
            List<ListItemViewModel> projectsList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/HRServiceActivities")).Result;
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

        //public static async Task<List<TimesheetExportViewModel>> TimeSheetExportList()
        //{
        //    var TimesheetExport = new List<TimesheetExportViewModel>();
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(TimeSheetAPIURl);
        //        HttpResponseMessage response = client.GetAsync(string.Format("Timesheet/TimeSheetList")).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var _TimesheetExportViewModel = await response.Content.ReadAsAsync<List<TimesheetExportViewModel>>();

        //            foreach (var a in _TimesheetExportViewModel)
        //            {
        //                TimesheetExport.Add(a);
        //            }
        //        }
        //    }
        //    return TimesheetExport;
        //}

        public static async Task<List<TimeSheetViewModel>> CandidateTimelines()
        {
            List<TimeSheetViewModel> CostModelLists = new List<TimeSheetViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Timesheet/CandidateTimelines")).Result;
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
        public static async Task<List<CompletedTimesheetModel>> PaidTimeSheetList()
        {
            List<CompletedTimesheetModel> CostModelLists = new List<CompletedTimesheetModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("Timesheet/PaidTimeSheetList")).Result;
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
        public static async Task<List<ApprovedTimesheetModel>> TimeSheetApprovedListt()
        {
            List<ApprovedTimesheetModel> CostModelLists = new List<ApprovedTimesheetModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/ApprovedTimelines")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ApprovedTimesheetModel>>();

                    foreach (var a in projectactivities)
                    {
                        CostModelLists.Add(a);
                    }
                }
            }
            return CostModelLists;
        }
        public static async Task<List<TimeSheetViewModel>> VehicleTimeSheetList()
        {
            List<TimeSheetViewModel> CostModelLists = new List<TimeSheetViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/VechiclebookingList")).Result;
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
        public static async Task<List<ProjectManagerTimesheet>> PMTimeSheetList()
        {
            List<ProjectManagerTimesheet> CostModelLists = new List<ProjectManagerTimesheet>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ProjectMangersTimesheet/PMTimeSheetList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ProjectManagerTimesheet>>();

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

        public static async Task<List<CompletedTimesheetModel>> RejectedTimeSheets()
        {
            List<CompletedTimesheetModel> CostModelLists = new List<CompletedTimesheetModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/RejectedTimeSheets")).Result;
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
                HttpResponseMessage response = client.GetAsync(string.Format("Timesheet/{0}/TimeSheetSearchById", id)).Result;
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
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/{0}/ServiceCostList", OpportunityID)).Result;
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
        public static async Task<ReturnModel> ApproveIndividualTimesheet(int? activityId)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/ApproveIndividualTimesheet/{0}", activityId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
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
        public static async Task<ReturnModel> ApproveBulkTimesheet(int resourceId, int PMId, int projectId)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/ApproveBulkTimesheet/{0}/{1}/{2}", resourceId, PMId, projectId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                    HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                }
                else
                {
                    ReturnResult.Message = "There is an issue with the TimeSheet Approval. Details " + response.ReasonPhrase;
                    HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                }
            }
            return ReturnResult;
        }

        public static async Task<ReturnModel> UpdatePaid(int? ResourceId, int PMId, int projectId)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/UpdateTimesheetPaid/{0}/{1}/{2}", ResourceId, PMId, projectId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                    HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                }
                else
                {
                    ReturnResult.Message = "There is an issue with the TimeSheet Approval. Details " + response.ReasonPhrase;
                    HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                }
            }
            return ReturnResult;
        }

        public static async Task<ReturnModel> TimeSheetApproval(int? activityId)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/UpdateTimesheetPaid/{0}", activityId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
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
        public static async Task<List<CompletedTimesheetModel>> AllBookingEntries()
        {
            List<CompletedTimesheetModel> completeList = new List<CompletedTimesheetModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/AllTimesheetEnteries")).Result;
                if (response.IsSuccessStatusCode)
                {
                    completeList = await response.Content.ReadAsAsync<List<CompletedTimesheetModel>>();

                    //foreach (var a in list)
                    //{
                    //    completeList.Add(a);
                    //}
                }
            }
            return completeList;
        }
        public static async Task<List<ListItemViewModel>> VehicleListing(int warehouseId)
        {
            List<ListItemViewModel> vanList = new List<ListItemViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("ListItems/{0}/VehiclesList", warehouseId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<ListItemViewModel>>();
                    foreach (var a in projectactivities)
                    {
                        vanList.Add(new ListItemViewModel() { Id = a.Id, Value = a.Value });
                    }
                }
            }
            return vanList;
        }

        public static async Task<List<ProfitLossModel>> ProfitLoss()
        {
            List<ProfitLossModel> completeList = new List<ProfitLossModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("CostModels/ServicesProfileLossForcast")).Result;
                if (response.IsSuccessStatusCode)
                {
                    completeList = await response.Content.ReadAsAsync<List<ProfitLossModel>>();
                }
            }
            return completeList;
        }

        public static List<ListItemViewModel> SalesForceEntities(out List<ListItemViewModel> opportunityList)
        {
            opportunityList = new List<ListItemViewModel>();
            List<ListItemViewModel> projectList = new List<ListItemViewModel>();
            var SfdcBinding = new SforceService();
            LoginResult CurrentLoginResult = null;
            var dat = System.DateTime.Now;
            dat = dat.AddDays(-180);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            CurrentLoginResult = SfdcBinding.login(salesForceUser, salesForcePWD);
            SfdcBinding.Url = CurrentLoginResult.serverUrl;
            SfdcBinding.SessionHeaderValue = new SessionHeader();
            SfdcBinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
            QueryResult queryResult = null;
            String SOQL = "";
            SOQL = salesForceQuery;
            // "select OwnerId,Name,Opportunity_Number__c from Opportunity where closedate > 2019-01-01 and stageName in('Approved - Pending to be sent to customer', 'Pending Customer Decision', 'Pending PM Allocation', 'Closed', 'Closed Won')";

            queryResult = SfdcBinding.query(SOQL);
            // StreamWriter info = new StreamWriter("C:\\temp\\salesforce.txt");
            if (queryResult.size > 0)
            {
                for (int i = 0; i < queryResult.records.Length; i++)
                {
                    Opportunity lead = (Opportunity)queryResult.records[i];
                    string firstName = lead.OwnerId;
                    string lastName = lead.Name;
                    string businessPhone = lead.Opportunity_Number__c;
                    string statusofApproval = lead.Status_of_Approval__c;
                    opportunityList.Add(new ListItemViewModel() { Value = lead.Opportunity_Number__c, OpportunityNumber = lead.Opportunity_Number__c });
                    projectList.Add(new ListItemViewModel() { Id = i, Value = lead.Name, OpportunityNumber = lead.Name });
                }
            }
            return projectList;
        }
        public static async Task<List<HRTimeSheetList>> HRTimeSheetList()
        {
            var CostModelLists = new List<HRTimeSheetList>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format("RecruiterTimesheet/HRTimeSheetList")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var projectactivities = await response.Content.ReadAsAsync<List<HRTimeSheetList>>();

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